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
using Microsoft.VisualBasic;


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

        public void ReadBinaryFile(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    // Leer el primer octeto (8 bits)
                    byte firstOctet = reader.ReadByte();
                    MessageBox.Show("Primer octeto en decimal:"+ Convert.ToString(firstOctet));
                    if (firstOctet == 48) //Només agafem els data blocks de la cat 48
                    {
                        // Leer los siguientes dos octetos(16 bits) como un short
                        ushort variableLength = reader.ReadUInt16();
                        variableLength = (ushort)((variableLength >> 8) | (variableLength << 8)); // Corregir la endianidad

                        MessageBox.Show("Variable length en decimal: " + Convert.ToString(variableLength));
                        
                        // Calcular cuántos bits a leer según la longitud variable
                        int bitsToRead = variableLength - 3 * 8; // Restamos los octetos de cat y length

                        // Asegurarse de que hay suficientes bytes para leer
                        if (bitsToRead > 0)
                        {
                            byte[] buffer = new byte[(bitsToRead + 7) / 8]; // Redondear hacia arriba
                            reader.Read(buffer, 0, buffer.Length);
                            string DataBlock = ConvertirByte2String(buffer); //Dades que tenim en un DataBlock
                            MessageBox.Show("El Data Block és: "+ DataBlock);
                            //Ara hem de mirar el FSPEC per saber quants DataItems tenim al record 
                            int FSPEC_bits = FSPEC(DataBlock); //Obtenim quants bits té el FSPEC
                            int[] FSPEC_vector = new int[FSPEC_bits]; //Creem un vector amb la longitud del FSPEC
                            FSPEC_vector = ConvertirBits(DataBlock, FSPEC_bits);
                            DataBlock = DataBlock.Substring(FSPEC_bits); //eliminem del missatge els bits del FSPEC
                            ReadPacket(FSPEC_vector, DataBlock); //Cridem a la funció per a llegir el paquet

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


        public void Motor(string motor)
        {
            // Archivos
            for (int i = 0; i < Directory.EnumerateFiles(CarpetaBusqueda).Count(); i++)
            { 
                FileInfo k = new FileInfo(Directory.GetFiles(CarpetaBusqueda)[i]);
                if (k.Name == BuscarBox.Text)
                {
                    MessageBox.Show("Arxiu seleccionat: " + k.Name);
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

        public void ReadPacket(int[] read, string DataBlock)
        {
            string mensaje;
            int octet = 8; // Longitud d'un octet
            int bitsleidos = 0;
            
            List<DataItem> di = new List<DataItem>();
            AsterixLib.DataItem dataitem;
            for (int i = 0; i < read.Length; i++)
            {
        
                switch (i)
                {
                    case 0:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 2*octet); //La longitud és fixa en aquest cas
                            di.Add(new AsterixLib.DataSourceIdentifier(mensaje));
                            bitsleidos=bitsleidos + 2*octet;
                        }
                        break;
                    case 1:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 3*octet);
                            di.Add(new AsterixLib.TimeOfDay(mensaje));
                            bitsleidos= bitsleidos + 3*octet;
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
                            di.Add(new AsterixLib.TargetReportDescriptor(mensaje));
                        }
                        break;
                    case 3:
                        if (read[i] == 1)
                        {
                            mensaje=DataBlock.Substring(bitsleidos, 4*octet);
                            di.Add(new AsterixLib.Position_Polar(mensaje));
                            bitsleidos=bitsleidos + 4*octet;
                        }
                        break;
                    case 4:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 2*octet);
                            di.Add(new AsterixLib.Mode3A(mensaje));
                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        break;
                    case 5:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 2 * octet);
                            di.Add(new AsterixLib.FlightLevel(mensaje));
                            bitsleidos = bitsleidos + 2 * octet;
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
                            di.Add(new AsterixLib.RadarPlotChar(mensaje));
                            bitsleidos = bitsleidos + octet + length;
                        }
                        break;
                    case 7:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 3 * octet);
                            di.Add(new AsterixLib.AircraftAdd(mensaje));
                            bitsleidos = bitsleidos + 3*octet;
                        }
                        break;
                    case 8:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 6 * octet);
                            di.Add(new AsterixLib.AircraftID(mensaje));
                            bitsleidos=bitsleidos + 6*octet;
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
                            di.Add(new AsterixLib.TrackNum(mensaje));
                            bitsleidos=bitsleidos+2*octet;
                        }
                        break;
                    case 11:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 4 * octet);
                            di.Add(new AsterixLib.Position_Cartesian(mensaje));
                            bitsleidos= bitsleidos+4*octet;
                        }
                        break;
                    case 12:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 4 * octet);
                            di.Add(new AsterixLib.TrackVelocityPolar(mensaje));
                            bitsleidos = bitsleidos+4*octet;
                        }
                        break;
                    case 13:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos + 2*octet);
                            di.Add(new AsterixLib.TrackStatus(mensaje));
                            bitsleidos= bitsleidos+2*octet;
                        }
                        break;
                    case 14:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos+2*octet);
                            di.Add(new AsterixLib.CommACAS(mensaje));
                            bitsleidos = bitsleidos + 2*octet;
                        }
                        break;
                }
            }
            Descodificar(di); //Cridem a la funció descodificar
        }

        private void Descodificar(List<DataItem> data)
        {
            //AQUESTA LINEA NO VA AQUI
            DataItem.SetNombreFichero("fichero.txt"); //En el moment en que es decideixi com es diu el ficher s'ha de posar allà

            for(int i=0; i<data.Count; i++)
            {
                data[i].Descodificar();
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
