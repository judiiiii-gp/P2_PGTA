using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.Logging;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using AsterixLib;
using System.ComponentModel.DataAnnotations;


namespace AsterixForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string CarpetaBusqueda;
        Computer usr = new Computer();

        static void ReadBinaryFile(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    // Leer el primer octeto (8 bits)
                    byte firstOctet = reader.ReadByte();
                    Console.WriteLine($"Primer octeto en decimal: {firstOctet}");
                    if (firstOctet == 48) //Només agafem els data blocks de la cat 48
                    {
                        // Leer los siguientes dos octetos(16 bits) como un short
                        ushort variableLength = reader.ReadUInt16();
                        variableLength = (ushort)((variableLength >> 8) | (variableLength << 8)); // Corregir la endianidad

                        Console.WriteLine($"Variable length en decimal: {variableLength}");
                        
                        // Calcular cuántos bits a leer según la longitud variable
                        int bitsToRead = variableLength - 3 * 8; // Restamos los octetos de cat y length

                        // Asegurarse de que hay suficientes bytes para leer
                        if (bitsToRead > 0)
                        {
                            byte[] buffer = new byte[(bitsToRead + 7) / 8]; // Redondear hacia arriba
                            reader.Read(buffer, 0, buffer.Length);
                            string DataBlock = ConvertirByte2String(buffer); //Dades que tenim en un DataBlock
                            MessageBox.Show(DataBlock);
                            //Ara hem de mirar el FSPEC per saber quants DataItems tenim al record 
                            int FSPEC_bits = FSPEC(DataBlock); //Obtenim quants bits té el FSPEC
                            int[] FSPEC_vector = new int[FSPEC_bits]; //Creem un vector amb la longitud del FSPEC
                            FSPEC_vector = ConvertirBits(DataBlock, FSPEC_bits);
                            DataBlock = DataBlock.Substring(FSPEC_bits); //liminem del missatge els bits del FSPEC

                        }
                        else
                        {
                            Console.WriteLine("No hay bits suficientes para leer después de restar 3 octetos.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }
        }
        static string ConvertirByte2String(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in bytes)
            {
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0')); //El PadLeft nos asegura que cada byte se representa con 8 bits, rellenando si es necesario
                // Com que sempre hi han octets jo crec que està bé que faci això
            }
            return sb.ToString();
        }

        //Llegim el FSPEC per saber on comencen els Data Items
        static int FSPEC(string DataBlock)
        {
            int length = DataBlock.Length; //No hem de superar mai la longitud
            MessageBox.Show(length.ToString());
            for (int i = 0; i < length; i=i+8)
            {   
                MessageBox.Show(i.ToString());
                int a = i;
                if (a + 8 <= length)
                {
                    string aux = DataBlock.Substring(i, 8);
                    MessageBox.Show(aux);
                    char ultimbit = aux[i + 7]; //Busquem el valor de l'últim bit per saber si hi ha més FSPEC
                    if (ultimbit == '0')
                    {
                        return i + 8; //Obtenim quants bits de FSPEC hi ha 
                    }
                }
            }
            return -1; //Si hi ha algun error 
        }

        //Convertim els bits del FSPEC en un vector
        static int[] ConvertirBits(string DataBlock, int length)
        {
            int[] vectorBits = new int[length];

            for (int i = 0;i < length; i++)
            {
                vectorBits[i] = DataBlock[i] == '1' ? 1 : 0;
            }
            return vectorBits;
        }


        void Motor(string motor)
        {
            // Archivos
            for (int i = 0; i < Directory.EnumerateFiles(CarpetaBusqueda).Count(); i++)
            { 
                FileInfo k = new FileInfo(Directory.GetFiles(CarpetaBusqueda)[i]);
                if (k.Name == BuscarBox.Text)
                {
                    listBox1.Items.Add("Arxiu seleccionat: " + k.Name);
                    ReadBinaryFile(k.FullName);
                }
            }
            // Carpetas
            if (Directory.EnumerateDirectories(CarpetaBusqueda).Count() > 0)
            {
                for (int i = 0; i < Directory.EnumerateDirectories(CarpetaBusqueda).Count(); i++)
                {
                    Motor(Directory.GetDirectories(CarpetaBusqueda)[i]);
                    if (i >= 5000)
                    {
                        MessageBox.Show("No s'ha trobat el arxiu");
                        break;
                    }
                    else
                    {
                        string msg = BuscarBox.Text;
                        DataGridView dataGridView = new DataGridView(msg);
                        dataGridView.Show();
                    }
                }
            }
        }

        private void ReadPacket(int[] read, string DataBlock)
        {
            string mensaje;
            int octet = 8; // Longitud d'un octet
            int bitsleidos = 0;
            //AsterixLib.DataItem dataitem = new AsterixLib.DataItem();
            for (int i = 0; i < (DataBlock.Length - read.Length); i++)
            {
                switch (i)
                {
                    case 0:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 2*octet); //La longitud és fixa en aquest cas
                            AsterixLib.DataSourceIdentifier id = new AsterixLib.DataSourceIdentifier(mensaje);
                            id.Descodificar();
                            bitsleidos=bitsleidos + 2*octet;
                            //dataitem = id;
                        }
                        break;
                    case 1:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 3*octet);
                            AsterixLib.TimeOfDay tod = new AsterixLib.TimeOfDay(mensaje);
                            tod.Descodificar();
                            bitsleidos= bitsleidos + 3*octet;
                            //dataitem = tod;
                        }
                        break;
                    case 2:
                        if (read[i] == 1)
                        {
                            string[] cadena = new string[3*octet]; //Espero que no pugui ser més llarg de 3 octets
                            cadena[0] = DataBlock.Substring(bitsleidos, octet);
                            bitsleidos= bitsleidos + octet;
                            if (cadena[octet-1] == "1")
                            {                             
                                cadena[octet] = DataBlock.Substring(bitsleidos, octet);
                                bitsleidos = bitsleidos + octet;
                                if (cadena[2*octet-1] == "1")
                                {
                                    cadena[2*octet] = DataBlock.Substring(bitsleidos, octet);
                                    bitsleidos = bitsleidos + octet;
                                }
                            }
                            mensaje = String.Join("", cadena); //Unim tots els bits en una sola string
                            AsterixLib.TargetReportDescriptor tr = new AsterixLib.TargetReportDescriptor(mensaje);
                            tr.Descodificar();
                            //dataitem = tr;
                        }
                        break;
                    case 3:
                        if (read[i] == 1)
                        {
                            mensaje=DataBlock.Substring(bitsleidos, 4*octet);
                            AsterixLib.Position_Polar ppc = new AsterixLib.Position_Polar(mensaje);
                            ppc.Descodificar();
                            bitsleidos=bitsleidos + 4*octet;
                            //dataitem = ppc;
                        }
                        break;
                    case 4:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 2*octet);
                            AsterixLib.Mode3A m3a = new AsterixLib.Mode3A(mensaje);
                            m3a.Descodificar();
                            bitsleidos = bitsleidos + 2 * octet;
                            //dataitem = m3a;
                        }
                        break;
                    case 5:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 2 * octet);
                            AsterixLib.FlightLevel fl = new AsterixLib.FlightLevel(mensaje);
                            fl.Descodificar();
                            bitsleidos = bitsleidos + 2 * octet;
                            //dataitem = fl;
                        }
                        break;
                    case 6:
                        if (read[i] == 1)
                        {
                            string[] dades = new string[8 * octet]; //La longitud màxima serà de 8 octets
                            dades[0] = DataBlock.Substring(bitsleidos, octet);
                            int length=0;
                            for (int j=0; j<dades.Length; j++)
                            {
                                if (dades[j] == "1")
                                {
                                    length = length + octet; //Així trobarem la longitud del missatge a llegir
                                }
                            }
                            mensaje = DataBlock.Substring(bitsleidos, octet + length);
                            AsterixLib.RadarPlotChar rpc = new AsterixLib.RadarPlotChar(mensaje);
                            rpc.Descodificar();
                            bitsleidos = bitsleidos + octet + length;
                            //dataitem = rpc;
                        }
                        break;
                    case 7:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 3 * octet);
                            AsterixLib.AircraftAdd aa = new AsterixLib.AircraftAdd(mensaje); ;
                            aa.Descodificar();
                            bitsleidos = bitsleidos + 3*octet;
                            //dataitem = aa;
                        }
                        break;
                    case 8:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 6 * octet);
                            AsterixLib.AircraftID ai = new AsterixLib.AircraftID(mensaje);
                            ai.Descodificar();
                            bitsleidos=bitsleidos + 6*octet;
                            //dataitem = ai;
                        }
                        break;
                    case 9:
                        if (read[i] == 1)
                        {
                            //decodificar 010 preguntar julia
                        }
                        break;
                    case 10:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 2 * octet);
                            AsterixLib.TrackNum tn = new AsterixLib.TrackNum(mensaje);
                            tn.Descodificar();
                            bitsleidos=bitsleidos+2*octet;
                            //dataitem = tn;
                        }
                        break;
                    case 11:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 4 * octet);
                            AsterixLib.Position_Cartesian pc = new AsterixLib.Position_Cartesian(mensaje);
                            pc.Descodificar();
                            bitsleidos= bitsleidos+4*octet;
                            //dataitem = pc;
                        }
                        break;
                    case 12:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 4 * octet);
                            AsterixLib.TrackVelocityPolar tvp = new AsterixLib.TrackVelocityPolar(mensaje);
                            tvp.Descodificar();
                            bitsleidos = bitsleidos+4*octet;
                            //dataitem = tvp;
                        }
                        break;
                    case 13:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos + 2*octet);
                            AsterixLib.H_3D_RADAR h3r = new AsterixLib.H_3D_RADAR(mensaje);
                            h3r.Descodificar();
                            bitsleidos= bitsleidos+2*octet;
                            //dataitem = h3r;
                        }
                        break;
                    case 14:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos+2*octet);
                            AsterixLib.CommACAS h3r = new AsterixLib.CommACAS(mensaje);
                            h3r.Descodificar();
                            bitsleidos = bitsleidos + 2*octet;
                            //dataitem = h3r;
                        }
                        break;
                }
            }
        }

        //### EVENTS ####################################################################################################################
        private void Buscar_Click(object sender, EventArgs e)
        {
            if (CarpetaBusqueda == null) { MessageBox.Show("Falta seleccioanr una carpeta"); }
            else { Motor(CarpetaBusqueda); }
        }
        private void Seleccionar_Click(object sender, EventArgs e)
        {
            // Creació del cuadre per seleccionar carptea
            FolderBrowserDialog Carpeta = new FolderBrowserDialog();

            // Codi que s'executa al seleccionar una carpeta
            if (Carpeta.ShowDialog() == DialogResult.OK)
            {
                // Carpeta seleccionada
                CarpetaBusqueda = Carpeta.SelectedPath;

                // Verificació
                //MessageBox.Show(Carpeta.SelectedPath);
            }
            SeleccionarBox.Text = CarpetaBusqueda;

            if (CarpetaBusqueda != null)
            {
                DirectoryInfo di = new DirectoryInfo(@CarpetaBusqueda);

                // Mostra els arxius de la carpeta selecionada
                foreach (var item in di.GetFiles()) { listBox1.Items.Add(item.Name); }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try{ BuscarBox.Text = listBox1.Items[listBox1.SelectedIndex].ToString(); } 
            catch { }
            
        }
        
    }
}
