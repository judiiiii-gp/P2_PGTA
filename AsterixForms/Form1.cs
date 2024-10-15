using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.Logging;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
                            MessageBox.Show("FSPEC_bits.ToString()");
                            //MessageBox.Show(FSPEC_bits.ToString());
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
            AsterixLib.DataItem dataitem = AsterixLib.DataItem();
            for (int i = 0; i < (DataBlock.Length - read.Length); i++)
            {
                switch (i)
                {
                    case 0:
                        if (read[i] == 1)
                        {
                            AsterixLib.DataSourceIdentifier id = dataitem;
                            id.Decodificar();
                            dataitem = id;
                        }
                        break;
                    case 1:
                        if (read[i] == 1)
                        {
                            AsterixLib.TimeOfDay tod = dataitem;
                            tod.Decodificar();
                            dataitem = tod;
                        }
                        break;
                    case 2:
                        if (read[i] == 1)
                        {
                            AsterixLib.TargetReportDescriptor tr = dataitem;
                            tr.Decodificar();
                            dataitem = tr;
                        }
                        break;
                    case 3:
                        if (read[i] == 1)
                        {
                            AsterixLib.Position_Polar ppc = dataitem;
                            ppc.Decodificar();
                            dataitem = ppc;
                        }
                        break;
                    case 4:
                        if (read[i] == 1)
                        {
                            AsterixLib.Mode3A m3a = dataitem;
                            m3a.Decodificar();
                            dataitem = m3a;
                        }
                        break;
                    case 5:
                        if (read[i] == 1)
                        {
                            AsterixLib.FlightLevel fl = dataitem;
                            fl.Decodificar();
                            dataitem = fl;
                        }
                        break;
                    case 6:
                        if (read[i] == 1)
                        {
                            AsterixLib.RadarPlotChar rpc = dataitem;
                            rpc.Decodificar();
                            dataitem = rpc;
                        }
                        break;
                    case 7:
                        if (read[i] == 1)
                        {
                            AsterixLib.AircraftAdd aa = dataitem;
                            aa.Decodificar();
                            dataitem = aa;
                        }
                        break;
                    case 8:
                        if (read[i] == 1)
                        {
                            AsterixLib.AircraftID ai = dataitem;
                            ai.Decodificar();
                            dataitem = ai;
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
                            AsterixLib.TrackNum tn = dataitem;
                            tn.Decodificar();
                            dataitem = tn;
                        }
                        break;
                    case 11:
                        if (read[i] == 1)
                        {
                            AsterixLib.Position_Cartesian pc = dataitem;
                            pc.Decodificar();
                            dataitem = pc;
                        }
                        break;
                    case 12:
                        if (read[i] == 1)
                        {
                            AsterixLib.TrackVelocityPolar tvp = dataitem;
                            tvp.Decodificar();
                            dataitem = tvp;
                        }
                        break;
                    case 13:
                        if (read[i] == 1)
                        {
                            AsterixLib.H_3D_RADAR h3r = dataitem;
                            h3r.Decodificar();
                            dataitem = h3r;
                        }
                        break;
                    case 14:
                        if (read[i] == 1)
                        {
                            AsterixLib.CommACAS h3r = dataitem;
                            h3r.Decodificar();
                            dataitem = h3r;
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
