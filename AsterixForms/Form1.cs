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
using System.Diagnostics;


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
                    while (fs.Position < fs.Length)
                    {

                        // Leer el primer octeto (8 bits)
                        byte firstOctet = reader.ReadByte();
                        
                        
                        //MessageBox.Show("Primer octeto en decimal:" + Convert.ToString(firstOctet));
                        if (firstOctet == 48) //Només agafem els data blocks de la cat 48
                        {
                            // Leer los siguientes dos octetos(16 bits) como un short
                            byte[] longitud = new byte[2];
                            longitud[0]=reader.ReadByte();
                            longitud[1]=reader.ReadByte();
                            int variableLength = BitConverter.ToInt16(longitud, 0);
                            variableLength = (ushort)((variableLength >> 8) | (variableLength << 8)); // Corregir la endianidad

                            
                            //Debug.WriteLine("Variable length en decimal: " + Convert.ToString(variableLength));

                            // Calcular cuántos bits a leer según la longitud variable
                            int bitsToRead = variableLength*8 - 3 * 8; // Restamos los octetos de cat y length

                            // Asegurarse de que hay suficientes bytes para leer
                            if (bitsToRead > 0)
                            {
                                byte[] buffer = new byte[(bitsToRead + 7) / 8]; // Redondear hacia arriba
                                reader.Read(buffer, 0, buffer.Length);
                                string DataBlock = ConvertirByte2String(buffer); //Dades que tenim en un DataBlock
                                //MessageBox.Show("Length DataBlock amb el FSPEC: " + Convert.ToString(DataBlock.Length));
                                //Ara hem de mirar el FSPEC per saber quants DataItems tenim al record 
                                int FSPEC_bits = FSPEC(DataBlock); //Obtenim quants bits té el FSPEC
                                int[] FSPEC_vector = new int[FSPEC_bits]; //Creem un vector amb la longitud del FSPEC
                                FSPEC_vector = ConvertirBits(DataBlock, FSPEC_bits);
                                DataBlock = DataBlock.Substring(FSPEC_bits); //eliminem del missatge els bits del FSPEC
                                ReadPacket(FSPEC_vector, DataBlock); //Cridem a la funció per a llegir el paquet
                                //MessageBox.Show("Hem llegit el paquet");

                            }
                            else
                            {
                                Console.WriteLine("No hay bits suficientes para leer después de restar 3 octetos.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("El paquete no pertenece a la ct 48 y no se lee");
                        }
                    }
                    MessageBox.Show("Tot el fitxer s'ha descodificat correctament");
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
            for (int i = 0; i < length; i=i+8)
            {   
                
                if (i + 8 <= length)
                {
                    string aux = DataBlock.Substring(i, 8);
                    char ultimbit = aux[7]; //Busquem el valor de l'últim bit per saber si hi ha més FSPEC
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
            
            int numBits = (length / 8) * 7;
            int[] vectorBits = new int[numBits];
            int indexBit = 0;
            for (int i = 0;i < length; i+=8)
            {
                //Eliminem els bits FX, ja que no ens indiquen res
               if (i + 8 <= length)
                {
                    string octeto = DataBlock.Substring(i, 8);
                    for (int j=0; j<7; j++)
                    {
                        vectorBits[indexBit] = octeto[j] == '1' ? 1 : 0;
                        indexBit++;
                    }
                }
                
            }
            
            return vectorBits;
        }


        public void Motor(string motor)
        {
            // Archivos
            int i = 0;
            int encontrado = 0;
            while(i< Directory.EnumerateFiles(CarpetaBusqueda).Count() & encontrado == 0)
            {
                FileInfo k = new FileInfo(Directory.GetFiles(CarpetaBusqueda)[i]);
                if (k.Name == BuscarBox.Text)
                {
                    MessageBox.Show("Arxiu seleccionat: " + k.Name);
                    listBox1.Items.Add("Arxiu seleccionat: " + k.Name);
                    ReadBinaryFile(k.FullName);
                    break;
                }
            }
            
        }
        void CargarDataGridView(bool found)
        {
            if (found)
            {
                string msg = BuscarBox.Text;

                DataGridView dataGridView = new DataGridView(msg);
                dataGridView.ShowDialog();
            }
        }

        public void ReadPacket(int[] read, string DataBlock)
        {
            //MessageBox.Show("Length DataBlock sense el FSPEC: " + Convert.ToString(DataBlock.Length));
            string mensaje;
            int octet = 8; // Longitud d'un octet
            int bitsleidos = 0;
            int final;
            int j;
            List<string> cadena = new List<string>();
            List<DataItem> di = new List<DataItem>();
            
            
            for (int i = 0; i < read.Length; i++)
            {
                //MessageBox.Show("Valor de read[i]: " + Convert.ToString(read[i]));

                switch (i)
                {
                    case 0:
                        if (read[i] == 1)
                        {
                            
                            mensaje = DataBlock.Substring(bitsleidos, 2*octet); //La longitud és fixa en aquest cas
                            //Debug.WriteLine("Missatge DSI: " + mensaje);
                            di.Add(new AsterixLib.DataSourceIdentifier(mensaje));
                            bitsleidos=bitsleidos + 2*octet;
                        }
                        break;
                    case 1:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 3*octet);
                            //Debug.WriteLine("Missatge TimeOfDay: " + mensaje);
                            di.Add(new AsterixLib.TimeOfDay(mensaje));
                            bitsleidos= bitsleidos + 3*octet;
                        }
                        break;
                    case 2:
                        if (read[i] == 1)
                        {
                            
                            final = 0;
                            j = 0;
                            while (final == 0)
                            {
                                cadena.Add(DataBlock.Substring(bitsleidos, octet));
                                bitsleidos = bitsleidos + octet;
                                if (cadena[j][j+octet-1] == '1')
                                {
                                    j = j + 1;
                                }
                                else
                                {
                                    final = 1;
                                }
                            }
                           
                            mensaje = String.Join("", cadena); //Unim tots els bits en una sola string
                            //Debug.WriteLine("Missatge TargetReportDescriptor: " + mensaje);
                            di.Add(new AsterixLib.TargetReportDescriptor(mensaje));
                            cadena.Clear(); //Buidem la llista per a no gastar memòria
                        }
                        break;
                    case 3:
                        if (read[i] == 1)
                        {
                            mensaje=DataBlock.Substring(bitsleidos, 4*octet);
                            //Debug.WriteLine("Missatge PositionPolar: " + mensaje);
                            di.Add(new AsterixLib.Position_Polar(mensaje));
                            bitsleidos=bitsleidos + 4*octet;
                        }
                        break;
                    case 4:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 2*octet);
                            //Debug.WriteLine("Missatge Mode3A: " + mensaje);
                            di.Add(new AsterixLib.Mode3A(mensaje));
                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        break;
                    case 5:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 2 * octet);
                            //Debug.WriteLine("Missatge FlightLevel: " + mensaje);
                            di.Add(new AsterixLib.FlightLevel(mensaje));
                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        break;
                    case 6:
                        if (read[i] == 1)
                        {
                            string[] dades = new string[8]; //La longitud màxima serà de 8 octets
                            
                            int length=0;
                            for (int t=0; t<dades.Length; t++)
                            {
                                dades[t] = DataBlock.Substring(bitsleidos + t, 1);
                                if (dades[t] == "1")
                                {
                                    length = length + octet; //Així trobarem la longitud del missatge a llegir
                                }
                            }
                            //Debug.WriteLine("Longitud del missatge RadarPlot: " + Convert.ToString(length));
                            mensaje = DataBlock.Substring(bitsleidos, octet + length);
                            //Debug.WriteLine("Missatge RadarPlotChart: " + mensaje);
                            di.Add(new AsterixLib.RadarPlotChar(mensaje));
                            bitsleidos = bitsleidos + octet + length;
                        }
                        break;
                    case 7:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 3 * octet);
                            //Debug.WriteLine("Missatge AircraftAdd: " + mensaje);
                            di.Add(new AsterixLib.AircraftAdd(mensaje));
                            bitsleidos = bitsleidos + 3*octet;
                        }
                        break;
                    case 8:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 6 * octet);
                            //Debug.WriteLine("Missatge AircraftId: " + mensaje);
                            di.Add(new AsterixLib.AircraftID(mensaje));
                            bitsleidos=bitsleidos + 6*octet;
                        }
                        break;
                    case 9:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, octet);
                            bitsleidos = bitsleidos + octet;
                            int rep = Convert.ToInt32(mensaje, 2); // Passem a int per saber el nombre de repeticions
                            //Debug.WriteLine("Hem de repetir: " + rep);
                            for (int k =0; k< rep; k++)
                            {
                                mensaje = DataBlock.Substring(bitsleidos, 8 * octet);
                                //Debug.WriteLine("El missatge és: " + mensaje);
                                bitsleidos = bitsleidos + 8 * octet;
                                int BDS1 = Convert.ToInt32(mensaje.Substring(56, 4),2);
                                int BDS2 = Convert.ToInt32(mensaje.Substring(60, 4),2);
                                Debug.WriteLine("BDS1 i BDS2: " + Convert.ToString(BDS1) + Convert.ToString(BDS2));
                                if (BDS1==4 & BDS2 == 0)
                                {
                                    di.Add(new AsterixLib.ModeS4(mensaje));
                                }
                                else if (BDS1==5 & BDS2 == 0)
                                {
                                    di.Add(new AsterixLib.ModeS5(mensaje));
                                }
                                else if (BDS1==6 & BDS2 == 0)
                                {
                                    di.Add(new AsterixLib.ModeS6(mensaje));
                                }
                            }
                        }
                        break;
                    case 10:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 2 * octet);
                            //Debug.WriteLine("Missatge TrackNum: " + mensaje);
                            di.Add(new AsterixLib.TrackNum(mensaje));
                            bitsleidos=bitsleidos+2*octet;
                        }
                        break;
                    case 11:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 4 * octet);
                            //Debug.WriteLine("Missatge Pos_Cart: " + mensaje);
                            di.Add(new AsterixLib.Position_Cartesian(mensaje));
                            bitsleidos= bitsleidos+4*octet;
                        }
                        break;
                    case 12:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 4 * octet);
                            //Debug.WriteLine("Missatge Track_vel: " + mensaje);
                            di.Add(new AsterixLib.TrackVelocityPolar(mensaje));
                            bitsleidos = bitsleidos+4*octet;
                        }
                        break;
                    case 13:
                        if (read[i] == 1)
                        {
                            final = 0;
                            j = 0;
                            while (final == 0)
                            {
                                cadena.Add(DataBlock.Substring(bitsleidos, octet));
                                bitsleidos = bitsleidos + octet;
                                if (cadena[j][j + octet - 1] == '1')
                                {
                                    j = j + 1;
                                }
                                else
                                {
                                    final = 1;
                                }
                            }

                            mensaje = String.Join("", cadena); //Unim tots els bits en una sola string
                            //Debug.WriteLine("Missatge TrackStat: " + mensaje);
                            di.Add(new AsterixLib.TargetReportDescriptor(mensaje));
                            cadena.Clear(); //Buidem la llista per a no gastar memòria

                        }
                        break;
                    case 14:
                        if (read[i] == 1)
                        {
                            bitsleidos = bitsleidos + 4 * octet;
                        }
                        break;
                    case 15: 
                        if (read[i] == 1)
                        {
                            final = 0;
                            j = 0;
                            while (final == 0)
                            {
                                cadena.Add(DataBlock.Substring(bitsleidos, octet));
                                bitsleidos = bitsleidos + octet;
                                if (cadena[j][j + octet - 1] == '1')
                                {
                                    j = j + 1;
                                }
                                else
                                {
                                    final = 1;
                                }
                            }
                            cadena.Clear();
                        }
                        break;
                    case 16:
                        if (read[i] == 1)
                        {
                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        
                        break;
                    case 17:
                        if (read[i] == 1)
                        {
                            bitsleidos = bitsleidos + 4 * octet;
                        }
                        
                        break;
                                                
                    case 18:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 2 * octet);
                            //Debug.WriteLine("Missatge H_3D_Radar: " + mensaje);
                            di.Add(new AsterixLib.H_3D_RADAR(mensaje));
                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        break;
                    case 19:
                        if (read[i] == 1)
                        {
                            final = 0;
                            j = 0;
                            while (final == 0)
                            {
                                cadena.Add(DataBlock.Substring(bitsleidos, octet));
                                bitsleidos = bitsleidos + octet;
                                if (cadena[j][j + octet - 1] == '1')
                                {
                                    j = j + 1;
                                }
                                else
                                {
                                    final = 1;
                                }
                            }
                            cadena.Clear();
                        }
                        break;

                    case 20:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 2*octet);
                            //Debug.WriteLine("Missatge CommACAS: " + mensaje);
                            di.Add(new AsterixLib.CommACAS(mensaje));
                            bitsleidos = bitsleidos + 2*octet;
                        }
                        break;
                    case 21:
                        if (read[i] == 1)
                        {
                            bitsleidos = bitsleidos + 7 * octet;
                        }
                        break;
                    case 22:
                        if (read[i] == 1)
                        {
                            bitsleidos = bitsleidos + octet;
                        }
                        break;
                    case 23:
                        if (read[i] == 1)
                        {
                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        break;
                    case 24:
                        if (read[i] == 1)
                        {
                            bitsleidos=bitsleidos + octet;
                        }
                        break;
                    case 25:
                        if (read[i] == 1)
                        {
                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        break;
                    case 26:
                        if (read[i] == 1)
                        {

                        }
                        break;
                    case 27:
                        if (read[i] == 1)
                        {

                        }
                        break;


                }
            }
            //Debug.WriteLine("Hem llegit tot el bloc");
            Descodificar(di); //Cridem a la funció descodificar
            //MessageBox.Show("Hem descodificat correctament el missatge");
        }

        private void Descodificar(List<DataItem> data)
        {
            
            //AQUESTA LINEA NO VA AQUI
            DataItem.SetNombreFichero("C:\\Users\\judig\\OneDrive\\Escritorio\\PGTA_Proj2\\prueba.txt"); //En el moment en que es decideixi com es diu el ficher s'ha de posar allà
            for(int i=0; i<data.Count; i++)
            {
                //MessageBox.Show("Estem dins el for de descodificar");
                data[i].Descodificar();
            }
            //DataItem.EscribirEnFichero("\n"); QUAN S'HAGI ESCRIT TOT AL FITXER FEM UN SALT DE LÍNEA
        }
        //### EVENTS ####################################################################################################################
        private void Buscar_Click(object sender, EventArgs e)
        {
            if (CarpetaBusqueda == null) { MessageBox.Show("Falta seleccioanr una carpeta"); }
            else { //Motor(CarpetaBusqueda);
                   CargarDataGridView(true); }
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
