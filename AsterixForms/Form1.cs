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
                    if (firstOctet == 48) //Nom�s agafem els data blocks de la cat 48
                    {
                        // Leer los siguientes dos octetos(16 bits) como un short
                        ushort variableLength = reader.ReadUInt16();
                        variableLength = (ushort)((variableLength >> 8) | (variableLength << 8)); // Corregir la endianidad

                        Console.WriteLine($"Variable length en decimal: {variableLength}");
                        
                        // Calcular cu�ntos bits a leer seg�n la longitud variable
                        int bitsToRead = variableLength - 3 * 8; // Restamos los octetos de cat y length

                        // Asegurarse de que hay suficientes bytes para leer
                        if (bitsToRead > 0)
                        {
                            byte[] buffer = new byte[(bitsToRead + 7) / 8]; // Redondear hacia arriba
                            reader.Read(buffer, 0, buffer.Length);
                            string DataBlock = ConvertirByte2String(buffer); //Dades que tenim en un DataBlock
                            MessageBox.Show(DataBlock);
                            //Ara hem de mirar el FSPEC per saber quants DataItems tenim al record 
                            int FSPEC_bits = FSPEC(DataBlock); //Obtenim quants bits t� el FSPEC
                            MessageBox.Show("FSPEC_bits.ToString()");
                            //MessageBox.Show(FSPEC_bits.ToString());
                        }
                        else
                        {
                            Console.WriteLine("No hay bits suficientes para leer despu�s de restar 3 octetos.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurri� un error: {ex.Message}");
            }
        }
        static string ConvertirByte2String(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in bytes)
            {
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0')); //El PadLeft nos asegura que cada byte se representa con 8 bits, rellenando si es necesario
                // Com que sempre hi han octets jo crec que est� b� que faci aix�
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
                    char ultimbit = aux[i + 7]; //Busquem el valor de l'�ltim bit per saber si hi ha m�s FSPEC
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
            // Creaci� del cuadre per seleccionar carptea
            FolderBrowserDialog Carpeta = new FolderBrowserDialog();

            // Codi que s'executa al seleccionar una carpeta
            if (Carpeta.ShowDialog() == DialogResult.OK)
            {
                // Carpeta seleccionada
                CarpetaBusqueda = Carpeta.SelectedPath;

                // Verificaci�
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
