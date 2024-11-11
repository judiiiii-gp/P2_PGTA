//using Microsoft.VisualBasic.Devices;
//using Microsoft.VisualBasic.Logging;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using LibAsterix;
//using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using System.Diagnostics;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Drawing;
//using System.Reflection.Metadata;
using MultiCAT6.Utils;
using System.Collections.Generic;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Linq;
using Amazon.DynamoDBv2.DocumentModel;
using System.Windows;
using SharpKml.Dom;
using SharpKml.Base;
using SharpKml.Engine;
using Vector = SharpKml.Base.Vector;
using Document = SharpKml.Dom.Document;
using TimeSpan = System.TimeSpan;
using System.Globalization;
using Size = System.Drawing.Size;



namespace FormsAsterix

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Start_sim.FlatAppearance.BorderSize = 0;
            Start_sim.FlatAppearance.MouseDownBackColor = Color.Transparent; 
            Start_sim.FlatAppearance.MouseOverBackColor = Color.Transparent;
            Start_sim.MouseEnter += (s, e) => Start_sim.Cursor = Cursors.Hand;
            Start_sim.MouseLeave += (s, e) => Start_sim.Cursor = Cursors.Default;
            DescodBUT.MouseEnter += (s, e) => DescodBUT.Cursor = Cursors.Hand;
            DescodBUT.MouseLeave += (s, e) => DescodBUT.Cursor = Cursors.Default;
        }

        List<List<DataItem>> bloque = new List<List<DataItem>>();
        List<AsterixGrid> asterixGrids = new List<AsterixGrid>();
        List<long> time = new List<long>();
        List<double> longitudList = new List<double>();
        List<double> latitudList = new List<double>();
        List<String> AircraftIDList = new List<String>();
        List<double> thetaList = new List<double>();

        List<string> AircraftAddrList = new List<string>();
        List<string> TrackNumList = new List<string>();
        List<string> Mode3AList = new List<string>();
        List<string> SACList = new List<string>();
        List<string> SICList = new List<string>();
        List<string> AltitudeList = new List<string>();

        long timeInicial;
        


        //////// DESCODIFICACIO FITXER

        private void DescodBUT_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Selecciona un archivo .ast";
            openFileDialog.Filter = "Todos los arxivos (*.ast*)|*ast*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //DataGridView formulari = new DataGridView(openFileDialog.FileName);
                //formulari.ShowDialog();
                ReadBinaryFile(openFileDialog.FileName);
                Corrected_Altitude(bloque);
                Calcular_Lat_Long(bloque);
                GenerarAsterix(bloque);
                groupBox1.Hide();
                groupBox2.Show();
                ImageList imageList = new ImageList();
                imageList.ImageSize = new Size(40, 40); // Define el tamaño deseado
                imageList.Images.Add(Properties.Resources.play_button);
                Start_sim.Image = imageList.Images[0];
                Start_sim.ImageAlign = ContentAlignment.MiddleCenter;
                Start_sim.TextImageRelation = TextImageRelation.ImageAboveText;
                Start_sim.ImageAlign = ContentAlignment.TopCenter; 
                Start_sim.TextAlign = ContentAlignment.BottomCenter;
                timeInicial = time[0];
                timeTXT.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)(timeInicial / 3600), (int)((timeInicial % 3600) / 60), (int)(timeInicial % 60));
            }
        }





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
                            longitud[0] = reader.ReadByte();
                            longitud[1] = reader.ReadByte();
                            int variableLength = BitConverter.ToInt16(longitud, 0);
                            variableLength = (ushort)((variableLength >> 8) | (variableLength << 8)); // Corregir la endianidad


                            //Debug.WriteLine("Variable length en decimal: " + Convert.ToString(variableLength));

                            // Calcular cuántos bits a leer según la longitud variable
                            int bitsToRead = variableLength * 8 - 3 * 8; // Restamos los octetos de cat y length

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
            for (int i = 0; i < length; i = i + 8)
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
            for (int i = 0; i < length; i += 8)
            {
                //Eliminem els bits FX, ja que no ens indiquen res
                if (i + 8 <= length)
                {
                    string octeto = DataBlock.Substring(i, 8);
                    for (int j = 0; j < 7; j++)
                    {
                        vectorBits[indexBit] = octeto[j] == '1' ? 1 : 0;
                        indexBit++;
                    }
                }

            }

            return vectorBits;
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

                            mensaje = DataBlock.Substring(bitsleidos, 2 * octet); //La longitud és fixa en aquest cas
                            //Debug.WriteLine("Missatge DSI: " + mensaje);
                            di.Add(new LibAsterix.DataSourceIdentifier(mensaje));

                            int length = 8; //Cada octeto tiene 8 bits

                            // Convertir SAC y SIC de binario a decimal

                            SACList.Add(Convert.ToString(Convert.ToInt32(mensaje.Substring(0, length), 2)));
                            SICList.Add(Convert.ToString(Convert.ToInt32(mensaje.Substring(length), 2)));

                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        else
                        {
                            di.Add(new LibAsterix.DataSourceIdentifier("N/A"));
                            SACList.Add("N/A");
                            SICList.Add("N/A");
                        }
                        break;
                    case 1:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 3 * octet);
                            
                            long timeActual = Convert.ToInt64(mensaje, 2) / 128;
                            time.Add(timeActual);
                            di.Add(new LibAsterix.TimeOfDay(mensaje));
                            bitsleidos = bitsleidos + 3 * octet;
                        }
                        else
                        {
                            di.Add(new LibAsterix.TimeOfDay("N/A"));
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
                                if (cadena[j][j + octet - 1] == '1')
                                {
                                    j = j + 1;
                                }
                                else
                                {
                                    final = 1;
                                }
                            }

                            mensaje = System.String.Join("", cadena); //Unim tots els bits en una sola string
                            //Debug.WriteLine("Missatge TargetReportDescriptor: " + mensaje);
                            di.Add(new LibAsterix.TargetReportDescriptor(mensaje));
                            cadena.Clear(); //Buidem la llista per a no gastar memòria
                        }
                        else
                        {
                            di.Add(new LibAsterix.TargetReportDescriptor("N/A"));
                        }
                        break;
                    case 3:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 4 * octet);
                            //Debug.WriteLine("Missatge PositionPolar: " + mensaje);
                            di.Add(new LibAsterix.Position_Polar(mensaje));
                            double Theta = Convert.ToInt32(mensaje.Substring(16,2*octet), 2) * (360 / Math.Pow(2, 16));
                            thetaList.Add(Theta);
                            bitsleidos = bitsleidos + 4 * octet;
                        }
                        else
                        {
                            di.Add(new LibAsterix.Position_Polar("N/A"));
                        }
                        break;
                    case 4:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 2 * octet);
                            //Debug.WriteLine("Missatge Mode3A: " + mensaje);
                            di.Add(new LibAsterix.Mode3A(mensaje));

                            string message = Convert.ToString(Convert.ToInt32(mensaje.Substring(4), 2), 8).PadLeft(4, '0');

                            Mode3AList.Add(message);

                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        else
                        {
                            di.Add(new LibAsterix.Mode3A("N/A"));
                            Mode3AList.Add("N/A");
                        }
                        break;
                    case 5:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 2 * octet);
                            //Debug.WriteLine("Missatge FlightLevel: " + mensaje);
                            di.Add(new LibAsterix.FlightLevel(mensaje));
                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        else
                        {
                            di.Add(new LibAsterix.FlightLevel("N/A"));
                        }
                        break;
                    case 6:
                        if (read[i] == 1)
                        {
                            string[] dades = new string[8]; //La longitud màxima serà de 8 octets

                            int length = 0;
                            for (int t = 0; t < dades.Length; t++)
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
                            di.Add(new LibAsterix.RadarPlotChar(mensaje));
                            bitsleidos = bitsleidos + octet + length;
                        }
                        else
                        {
                            di.Add(new LibAsterix.RadarPlotChar("N/A"));
                        }
                        break;
                    case 7:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 3 * octet);
                            //Debug.WriteLine("Missatge AircraftAdd: " + mensaje);
                            di.Add(new LibAsterix.AircraftAdd(mensaje));
                            string add = string.Empty;
                            string address = string.Empty;
                            for (int m = 0; m < mensaje.Length; m += 4)
                            {
                                string bits = mensaje.Substring(m, 4); //Agafem grups de 4 per a passar-ho a hexadecimal
                                int decval = Convert.ToInt32(bits, 2); //Ho passem a decimal
                                string address_char = decval.ToString("X");
                                add += address_char;
                            }
                            AircraftAddrList.Add(add);
                            bitsleidos = bitsleidos + 3 * octet;
                        }
                        else
                        {
                            di.Add(new LibAsterix.AircraftAdd("N/A"));
                            AircraftAddrList.Add("N/A");
                        }
                        break;
                    case 8:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 6 * octet);
                            //Debug.WriteLine("Missatge AircraftId: " + mensaje);
                            di.Add(new LibAsterix.AircraftID(mensaje));
                            string ID = "";
                            for (int m = 0; m < mensaje.Length; m += 6)
                            {
                                string block = mensaje.Substring(m, 6);
                                ID += ConvertirBitsAChar(block);
                            }
                            AircraftIDList.Add(ID); 
                            bitsleidos = bitsleidos + 6 * octet;
                        }
                        else
                        {
                            di.Add(new LibAsterix.AircraftID("N/A"));
                            AircraftIDList.Add(Convert.ToString(di[i]));
                        }
                        break;
                    case 9:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, octet);
                            bitsleidos = bitsleidos + octet;
                            int rep = Convert.ToInt32(mensaje, 2); // Passem a int per saber el nombre de repeticions
                            int flag4 = 0;
                            int flag5 = 0;
                            int flag6 = 0;
                            for (int k = 0; k < rep; k++)
                            {
                                mensaje = DataBlock.Substring(bitsleidos, 8 * octet);

                                int BDS1 = Convert.ToInt32(mensaje.Substring(56, 4), 2);
                                int BDS2 = Convert.ToInt32(mensaje.Substring(60, 4), 2);


                                if (BDS1 == 4 & BDS2 == 0)
                                {
                                    di.Add(new LibAsterix.ModeS4(mensaje));
                                    flag4 = 1;
                                }
                                else if (BDS1 == 5 & BDS2 == 0)
                                {
                                    di.Add(new LibAsterix.ModeS5(mensaje));
                                    flag5 = 1;
                                }
                                else if (BDS1 == 6 & BDS2 == 0)
                                {
                                    di.Add(new LibAsterix.ModeS6(mensaje));
                                    flag6 = 1;
                                }
                                bitsleidos = bitsleidos + 8 * octet;
                            }
                            if (flag4 == 0 & flag5 == 0 & flag6 == 0)
                            {
                                di.Add(new LibAsterix.ModeS4("N/A"));
                                di.Add(new LibAsterix.ModeS5("N/A"));
                                di.Add(new LibAsterix.ModeS6("N/A"));
                            }
                            else if (flag4 == 0 & flag5 == 0)
                            {
                                di.Add(new LibAsterix.ModeS4("N/A"));
                                di.Add(new LibAsterix.ModeS5("N/A"));
                            }
                            else if (flag4 == 0 & flag6 == 0)
                            {
                                di.Add(new LibAsterix.ModeS4("N/A"));
                                di.Add(new LibAsterix.ModeS6("N/A"));
                            }
                            else if (flag5 == 0 & flag6 == 0)
                            {
                                di.Add(new LibAsterix.ModeS5("N/A"));
                                di.Add(new LibAsterix.ModeS6("N/A"));
                            }
                            else if (flag4 == 0)
                            {
                                di.Add(new LibAsterix.ModeS4("N/A"));
                            }
                            else if (flag5 == 0)
                            {
                                di.Add(new LibAsterix.ModeS5("N/A"));
                            }
                            else if (flag6 == 0)
                            {
                                di.Add(new LibAsterix.ModeS6("N/A"));
                            }


                        }
                        else
                        {
                            di.Add(new LibAsterix.ModeS4("N/A"));
                            di.Add(new LibAsterix.ModeS5("N/A"));
                            di.Add(new LibAsterix.ModeS6("N/A"));
                        }
                        break;
                    case 10:

                        if (read[i] == 1)
                        {

                            mensaje = DataBlock.Substring(bitsleidos, 2 * octet);
                            //Debug.WriteLine("Missatge TrackNum: " + mensaje);
                            di.Add(new LibAsterix.TrackNum(mensaje));

                            TrackNumList.Add(Convert.ToString(Convert.ToInt32(mensaje.Substring(4, 12), 2)));

                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        else
                        {
                            di.Add(new LibAsterix.TrackNum("N/A"));
                            TrackNumList.Add("N/A");
                        }
                        break;
                    case 11:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 4 * octet);
                            //Debug.WriteLine("Missatge Pos_Cart: " + mensaje);
                            di.Add(new LibAsterix.Position_Cartesian(mensaje));
                            bitsleidos = bitsleidos + 4 * octet;
                        }
                        else
                        {
                            di.Add(new LibAsterix.Position_Cartesian("N/A"));
                        }
                        break;
                    case 12:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 4 * octet);

                            //Debug.WriteLine("Missatge Track_vel: " + mensaje);
                            di.Add(new LibAsterix.TrackVelocityPolar(mensaje));
                            bitsleidos = bitsleidos + 4 * octet;
                        }
                        else
                        {
                            di.Add(new LibAsterix.TrackVelocityPolar("N/A"));
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

                            mensaje = System.String.Join("", cadena); //Unim tots els bits en una sola string
                            //Debug.WriteLine("Missatge TrackStat: " + mensaje);
                            di.Add(new LibAsterix.TrackStatus(mensaje));
                            cadena.Clear(); //Buidem la llista per a no gastar memòria

                        }
                        else
                        {
                            di.Add(new LibAsterix.TrackStatus("N/A"));
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
                            di.Add(new LibAsterix.H_3D_RADAR(mensaje));
                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        else
                        {
                            di.Add(new LibAsterix.H_3D_RADAR("N/A"));
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
                            mensaje = DataBlock.Substring(bitsleidos, 2 * octet);
                            //Debug.WriteLine("Missatge CommACAS: " + mensaje);
                            di.Add(new LibAsterix.CommACAS(mensaje));
                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        else
                        {
                            di.Add(new LibAsterix.CommACAS("N/A"));
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
                            bitsleidos = bitsleidos + octet;
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
                //MessageBox.Show("Acaba SWITCH");
            }

            //Debug.WriteLine("Hem llegit tot el bloc");
            Descodificar(di); //Cridem a la funció descodificar
            bloque.Add(di);

            //MessageBox.Show("Hem descodificat correctament el missatge");
        }
        private static readonly Dictionary<char, string> ia5Mapping = new Dictionary<char, string>
    {
        {'A', "000001"}, {'B', "000010"}, {'C', "000011"}, {'D', "000100"}, {'E', "000101"},
        {'F', "000110"}, {'G', "000111"}, {'H', "001000"}, {'I', "001001"}, {'J', "001010"},
        {'K', "001011"}, {'L', "001100"}, {'M', "001101"}, {'N', "001110"}, {'O', "001111"},
        {'P', "010000"}, {'Q', "010001"}, {'R', "010010"}, {'S', "010011"}, {'T', "010100"},
        {'U', "010101"}, {'V', "010110"}, {'W', "010111"}, {'X', "011000"}, {'Y', "011001"},
        {'Z', "011010"}, {'0', "110000"}, {'1', "110001"}, {'2', "110010"}, {'3', "110011"},
        {'4', "110100"}, {'5', "110101"}, {'6', "110110"}, {'7', "110111"}, {'8', "111000"},
        {'9', "111001"}, {' ', "100000"},
    };
        static char ConvertirBitsAChar(string cadena)
        {
            foreach (var entry in ia5Mapping)
            {
                if (entry.Value == cadena)
                {
                    return entry.Key;
                }
            }
            return '?';
        }

        private void Descodificar(List<DataItem> data)
        {

            for (int i = 0; i < data.Count; i++)
            {
                //MessageBox.Show("Estem dins el for de descodificar");
                data[i].Descodificar();               
             
            }

        }


        private void NewDataBut_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Selecciona un archivo .ast";
            openFileDialog.Filter = "Todos los arxivos (*.ast*)|*ast*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {                
                ReadBinaryFile(openFileDialog.FileName);
                Corrected_Altitude(bloque);
                Calcular_Lat_Long(bloque);
                GenerarAsterix(bloque);
            }
            timer1.Stop();
            timeInicial = time[0];
            num_loop = 0;
            Click_times = 0;
            gMapControl1.Overlays.Clear();
            gMapControl1.ReloadMap();
            timeTXT.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)(timeInicial / 3600), (int)((timeInicial % 3600) / 60), (int)(timeInicial % 60));
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(40, 40); // Define el tamaño deseado
            imageList.Images.Add(Properties.Resources.play_button);
            Start_sim.Image = imageList.Images[0];
            Start_sim.ImageAlign = ContentAlignment.MiddleCenter;
            Start_sim.TextImageRelation = TextImageRelation.ImageAboveText;
            Start_sim.ImageAlign = ContentAlignment.TopCenter;
            Start_sim.TextAlign = ContentAlignment.BottomCenter;
            Start_sim.Text = " Start";
            Start_sim.Visible = true;
        }

        private void ShowDataBut_Click(object sender, EventArgs e)
        {
            DataGridView formulari = new DataGridView(asterixGrids);
            formulari.Show(); // Al obtenir el fitxer obrirem el nou formulari
        }



        int zoom = 7;
        private void gMapControl1_Load(object sender, EventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            gMapControl1.PolygonsEnabled = true;
            gMapControl1.MarkersEnabled = true;
            gMapControl1.NegativeMode = false;
            gMapControl1.RetryLoadTile = 0;
            gMapControl1.ShowTileGridLines = false;
            gMapControl1.AllowDrop = true;
            gMapControl1.IgnoreMarkerOnMouseWheel = true;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.DisableFocusOnMouseEnter = true;
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = zoom;
            gMapControl1.Position = new PointLatLng(41.300702, 2.102058);
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.ShowCenter = false;

            
        }


        int num_loop = 0;
        int steps = 1;
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            timeInicial++;
            Tick(ref timeInicial, ref num_loop);
            timeTXT.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)(timeInicial / 3600), (int)((timeInicial % 3600) / 60), (int)(timeInicial % 60));
        }


        private void Tick(ref long timeTick, ref int num_loop)
        {
            
            if (num_loop != AircraftIDList.Count)
            { 
                for (int i = 0; num_loop < AircraftIDList.Count && timeTick >= time[num_loop]; num_loop++)
                {
                    AddMarkerToMap(latitudList[num_loop], longitudList[num_loop], AircraftIDList[num_loop]);
                }
                gMapControl1.Update();

            }
            else
            {
                timer1.Stop();
                timeTick = timeTick - 2;
                Start_sim.Visible = false;
            }

        }

        private List<string> Sim_diccionary = new List<string>();
        private List<(string name, int position)> position = new List<(string name, int position)>();

        private void AddMarkerToMap(double lat, double lon, string name)
        {
            GMapOverlay markers = gMapControl1.Overlays.FirstOrDefault(o => o.Id == name);
            GMapMarker existingMarker = markers?.Markers.FirstOrDefault(m => m.Tag?.ToString() == name);

            if (existingMarker != null)
            {
                int planeData = bloque.FindIndex(item =>
                    AircraftIDList.Contains(name) &&
                    longitudList.Contains(lon) &&
                    latitudList.Contains(lat)
                    );
                if (position.Contains((name, planeData)) && thetaList[planeData] > 17)
                {
                    markers.Markers.Remove(existingMarker);
                    Sim_diccionary.Remove(name);
                }
                else
                {
                    existingMarker.Position = new PointLatLng(lat, lon);
                }
            }

            else
            {
                Sim_diccionary.Add(name);
                markers = new GMapOverlay(name);
                GMapMarker marker = new GMarkerGoogle(new PointLatLng(lat, lon), GMarkerGoogleType.blue_dot)
                {
                    Tag = name
                };

                markers.Markers.Add(marker);
                gMapControl1.Overlays.Add(markers);
                gMapControl1.UpdateMarkerLocalPosition(marker);
            }
        }

        private void gMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            string name = item.Tag?.ToString();

            GMapOverlay markers = gMapControl1.Overlays.FirstOrDefault(o => o.Id == name);
            GMapMarker existingMarker = markers?.Markers.FirstOrDefault(m => m.Tag?.ToString() == name);

            if (Sim_diccionary.Contains(name))
            {
                var position = existingMarker.Position;

                int indexAir = AircraftIDList.FindIndex(id => id == name);

                string info = $"Aircraft address: {AircraftAddrList[indexAir]}\n" +
                                  $"Track number: {TrackNumList[indexAir]}\n" +
                                  $"Mode 3A Reply: {Mode3AList[indexAir]}\n" +
                                  $"\n" +
                                  $"Lat: {position.Lat}º\n" +
                                  $"Lon: {position.Lng}º\n" +
                                  //$"Altitude: {planeData[6]} ft\n" +
                                  $"\n" +
                                  $"SAC: {SACList[indexAir]}\n" +
                                  $"SIC: {SICList[indexAir]}\n";

                MessageBox.Show(info, $"Aircraft indentification: {name}");
            }
        }

        int Click_times = 0;
        private void Start_sim_Click(object sender, EventArgs e)
        {
            if (Start_sim.Text == " Start" || Start_sim.Text == " Continue")
            {
                ImageList imageList = new ImageList();
                imageList.ImageSize = new Size(40, 40); // Define el tamaño deseado
                imageList.Images.Add(Properties.Resources.play_button);
                Start_sim.Image = imageList.Images[0];
                Start_sim.ImageAlign = ContentAlignment.MiddleCenter;
                Start_sim.TextImageRelation = TextImageRelation.ImageAboveText;
                Start_sim.ImageAlign = ContentAlignment.TopCenter;
                Start_sim.TextAlign = ContentAlignment.BottomCenter;
                if (Click_times == 0)
                {
                    ImageList imageListStop = new ImageList();
                    imageListStop.ImageSize = new Size(40, 40); // Define el tamaño deseado
                    imageListStop.Images.Add(Properties.Resources.pause);
                    Start_sim.Image = imageListStop.Images[0];
                    Start_sim.ImageAlign = ContentAlignment.MiddleCenter;
                    Start_sim.TextImageRelation = TextImageRelation.ImageAboveText;
                    Start_sim.ImageAlign = ContentAlignment.TopCenter;
                    Start_sim.TextAlign = ContentAlignment.BottomCenter;
                    Start_sim.Text = " Stop";
                    bloque = bloque.OrderBy(data => Convert.ToString(data[2])).ToList();
                    timeTXT.Show();
                    timer1.Start();
                    Click_times++;
                    Sim_diccionary.Clear();

                    if (position.Count == 0)
                    {
                        var maxIndex = bloque
                        .Select((data, index) => index) // Selecciona solo los índices
                        .Max(); // Obtiene el valor máximo
                        //MessageBox.Show(Convert.ToString(maxIndex));
                        var lastPositions = bloque
                        .Select((data, index) => new { Data = data, Index = index }) // Proyectar el dato y su índice
                        .GroupBy(item => AircraftIDList) // Agrupar por nombre de aeronave
                        .Select(group => new { Name = group.Key, LastPosition = group.Last().Index }); // Obtener la última posición de cada grupo

                        // Crear una lista que contienen el nombre de la aeronave y su última posición
                        position = lastPositions
                            .Select(item => (Convert.ToString(item.Name), item.LastPosition))
                            .ToList();
                    }
                }
                else
                {
                    ImageList imageListStop = new ImageList();
                    imageListStop.ImageSize = new Size(40, 40); // Define el tamaño deseado
                    imageListStop.Images.Add(Properties.Resources.pause);
                    Start_sim.Image = imageListStop.Images[0];
                    Start_sim.ImageAlign = ContentAlignment.MiddleCenter;
                    Start_sim.TextImageRelation = TextImageRelation.ImageAboveText;
                    Start_sim.ImageAlign = ContentAlignment.TopCenter;
                    Start_sim.TextAlign = ContentAlignment.BottomCenter;
                    Start_sim.Text = " Stop";
                    timer1.Start();
                }
            }
            else
            {
                ImageList imageList = new ImageList();
                imageList.ImageSize = new Size(40, 40); // Define el tamaño deseado
                imageList.Images.Add(Properties.Resources.play_button);
                Start_sim.Image = imageList.Images[0];
                Start_sim.ImageAlign = ContentAlignment.MiddleCenter;
                Start_sim.TextImageRelation = TextImageRelation.ImageAboveText;
                Start_sim.ImageAlign = ContentAlignment.TopCenter;
                Start_sim.TextAlign = ContentAlignment.BottomCenter;
                Start_sim.Text = " Continue";
                timer1.Stop();
            }
        }
        private void RestartSimBut_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timeInicial = time[0];
            num_loop = 0;
            Click_times = 0;
            gMapControl1.Overlays.Clear();
            gMapControl1.ReloadMap();
            timeTXT.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)(timeInicial / 3600), (int)((timeInicial % 3600) / 60), (int)(timeInicial % 60));
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(40, 40); // Define el tamaño deseado
            imageList.Images.Add(Properties.Resources.play_button);
            Start_sim.Image = imageList.Images[0];
            Start_sim.ImageAlign = ContentAlignment.MiddleCenter;
            Start_sim.TextImageRelation = TextImageRelation.ImageAboveText;
            Start_sim.ImageAlign = ContentAlignment.TopCenter;
            Start_sim.TextAlign = ContentAlignment.BottomCenter;
            Start_sim.Text = " Start";
            Start_sim.Visible = true;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int velocity = trackBar1.Value;
            switch (velocity)
            {
                case 0:
                    timer1.Interval = 1000;
                    Velocity_label_bar.Text = "Sim. Speed x1";
                    break;

                case 1:
                    timer1.Interval = 500;
                    Velocity_label_bar.Text = "Sim. Speed x2";
                    break;

                case 2:
                    timer1.Interval = 250;
                    Velocity_label_bar.Text = "Sim. Speed x4";
                    break;

                case 3:
                    timer1.Interval = 200;
                    Velocity_label_bar.Text = "Sim. Speed x5";
                    break;

                case 4:
                    timer1.Interval = 100;
                    Velocity_label_bar.Text = "Sim. Speed x10";
                    break;

                case 5:
                    timer1.Interval = 10;
                    Velocity_label_bar.Text = "Sim. Speed x100";
                    break;
            }
        }
        private void GetKMLBut_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivo KML|*.kml";
            saveFileDialog.Title = "Guardar archivo KML";
            saveFileDialog.InitialDirectory = @"C:\";  // Carpeta inicial

            // Muestra el cuadro de diálogo de guardado
            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName; // Saves the file name 

                Dictionary<string, KML_DATA> posicionesDeRepeticiones = new Dictionary<string, KML_DATA>();

                for (int i = 0; i < bloque.Count; i++)
                {
                    string nombre = AircraftIDList[i];
                    if (!posicionesDeRepeticiones.ContainsKey(nombre))
                    {
                        posicionesDeRepeticiones[nombre] = new KML_DATA();
                        posicionesDeRepeticiones[nombre].Positions = new List<Vector>();
                        posicionesDeRepeticiones[nombre].Description = "Aircraft address: " + nombre + " ; Aircraft indentification: " + AircraftIDList[i] + " ; Track number: " + TrackNumList[i] + " ; Mode 3A Reply: " + Mode3AList[i] + " ; SAC: " + SACList[i] + " ; SIC: " + SICList[i];
                    }
                    posicionesDeRepeticiones[nombre].Positions.Add(new Vector(latitudList[i], longitudList[i], Convert.ToDouble(bloque[i][5]))); // falta posar alçada
                }

                var document = new Document();
                var kml = new Kml();

                int styleCount = 0; // To give each style a unique ID
                foreach (var kvp in posicionesDeRepeticiones)
                {
                    string nombreAeronave = kvp.Key;
                    string description = kvp.Value.Description;

                    var placemark = new SharpKml.Dom.Placemark();
                    placemark.Name = nombreAeronave;
                    placemark.Description = new Description { Text = description };

                    // Create a custom style for each placemark
                    var style = new Style();
                    style.Id = "Style" + styleCount; // Assign a unique style ID
                    style.Line = new LineStyle
                    {
                        Color = new Color32(255, 0, 0, 255), // Red color
                        Width = 0.5 // Line width
                    };

                    placemark.StyleUrl = new Uri("#" + style.Id, UriKind.Relative);

                    var lineString = new LineString();
                    lineString.Coordinates = new CoordinateCollection();

                    var point = new SharpKml.Dom.Point();

                    // Itera a través de las posiciones de la aeronave
                    foreach (Vector posicion in kvp.Value.Positions)
                    {
                        lineString.Coordinates.Add(posicion);
                        if (posicion.Altitude <= 1828.8)
                        {
                            lineString.AltitudeMode = AltitudeMode.RelativeToGround;
                        }
                        else
                        {
                            lineString.AltitudeMode = AltitudeMode.Absolute;
                        }

                    }

                    placemark.Geometry = lineString;

                    // Add the custom style to the KML
                    document.AddStyle(style);
                    document.AddFeature(placemark);

                    styleCount++;
                }

                Style lineStyle = new Style();
                lineStyle.Line = new LineStyle();
                lineStyle.Line.Width = 5; // You can set the line width here
                lineStyle.Line.Color = new Color32(0, 255, 0, 255); // You can set the line color here

                document.AddStyle(lineStyle);

                // Add document to kml
                kml.Feature = document;

                // Create xml based kml file
                KmlFile kmlFile = KmlFile.Create(kml, false);

                // Save file to a memory stream
                MemoryStream memStream = new MemoryStream();
                kmlFile.Save(memStream);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    memStream.Seek(0, SeekOrigin.Begin);
                    memStream.CopyTo(fileStream);
                }
            }
            else
            {

            }
        }

        private class KML_DATA
        {
            public List<Vector> Positions { get; set; }
            public string Description { get; set; }
        }
        //Funció per a escriure en el fitxer
        private void EscribirFichero(List<List<DataItem>> bloque, string nombreFichero)
        {
            int NumLinea = 1;
            DataItem.SetNombreFichero(nombreFichero); //En el moment en que es decideixi com es diu el ficher s'ha de posar allà
            string cabecera = "Num Linea;SAC;SIC;Time of Day;Latitud;Longitud;Altura;TYP;SIM;RDP;SPI;RAB;TST;ERR;XPP;ME;MI;FOE;ADSBEP;ADSBVAL;SCNEP;SCNVAL;PAIEP;PAIVAL;RHO;THETA;Mode-3/A V;Mode-3/A G;Mode-3/A L;Mode-3/A reply;FL V;FL G;Flight level;Mode C Corrected;SRL;SRR;SAM;PRL;PAM;RPD;APD;Aircraft address;Aircraft Identification;BDS4;MCPU/FCU Selected altitude;FMS Selected Altitude;Barometric pressure setting;Mode status;VNAV;ALTHOLD;Approach;Target status;Target altitude source;BDS5;Roll angle;True track angle;Ground Speed;Track angle rate;True Airspeed;BDS6;Magnetic heading;Indicated airspeed;Mach;Barometric altitude rate;Inertial Vertical Velocity;Track Number;X-Cartesian;Y-Cartesian;Calculated groundspeed;Calculated heading;CNF;RAD;DOU;MAH;CDM;TRE;GHO;SUP;TCC;Height Measured by a 3D Radar;COM;STATUS;SI;MSSC;ARC;AIC;B1A_message;B1B_message";
            if (bloque.Count > 0)
            {
                bloque[0][0].EscribirEnFichero(cabecera + "\n", false);
            }
            foreach (var data in bloque)
            {
                List<string> atributosDI = new List<string>();

                foreach (DataItem item in data)
                {
                    atributosDI.Add(item.ObtenerAtributos()); // Obtenim els atributs dels elements
                }
                string mensaje = string.Join("", atributosDI);
                if (data.Count > 0)
                {
                    data[0].EscribirEnFichero($"{NumLinea}" + ";", false);

                    NumLinea++;
                }
                data[0].EscribirEnFichero(mensaje, false); //Escribim al fitxer
                if (data.Count > 0)
                {
                    data[0].EscribirEnFichero("\n", true);
                }
            }



        }


        private void CSV_File_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.Title = "Seleccionar la ubicación y el nombre del fichero";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    EscribirFichero(bloque, filePath);
                    MessageBox.Show("S'ha escrit el fitxer correctament");
                }
            }
        }


        public void Corrected_Altitude(List<List<DataItem>> bloques)
        {
            foreach (var bloque in bloques)
            {
                // Primero, obtenemos la presión del bloque
                double? presion = null;
                foreach (var di in bloque)
                {
                    if (di is ModeS4 diConPresion)
                    {
                        if (diConPresion.BARtxt != "N/A")
                        {
                            string p = diConPresion.BARtxt;
                            presion = Convert.ToDouble(p);
                            break; // Obtenemos la presión una vez y salimos
                        }
                    }
                }

                // Si tenemos presión, procesamos los elementos que necesitan la corrección
                if (presion.HasValue)
                {
                    foreach (var di in bloque)
                    {
                        if (di is FlightLevel fl)
                        {
                            if (fl.FL != "N/A")
                            {
                                // Llamamos a CorrectedAltitude y almacenamos el valor en CorrectedAltitudeValue
                                fl.CorrectedAltitude(presion.Value);
                            }
                            else
                            {
                                fl.Alt_correct = " ";
                            }

                        }
                    }
                }
                else
                {
                    foreach (var di in bloque)
                    {
                        if (di is FlightLevel fl)
                        {
                            fl.Alt_correct = " ";
                        }
                    }
                }

            }

        }
        public void Calcular_Lat_Long(List<List<DataItem>> bloques)
        {

            foreach (var elemento in bloques)
            {
                double? Flight = null;
                foreach (var di in elemento)
                {
                    if (di is FlightLevel fl)
                    {
                        if (fl.FL != "N/A")
                        {
                            Flight = Convert.ToDouble(fl.FL);
                        }
                        else
                        {
                            Flight = 0;
                        }


                    }
                }
                if (Flight.HasValue)
                {
                    double Rho;
                    double Theta;
                    double Elev;
                    double Lat;
                    double Long;
                    double h;
                    string mensaje;
                    CoordinatesWGS84 coord = new CoordinatesWGS84(41.300702 * GeoUtils.DEGS2RADS, 2.102058 * GeoUtils.DEGS2RADS, 2.007 + 25.25);
                    GeoUtils geoUtils = new GeoUtils();
                    for (int i = 0; i < elemento.Count; i++)
                    {
                        if (elemento[i] is Position_Polar polar)
                        {
                            Rho = Convert.ToDouble(polar.rho) * GeoUtils.NM2METERS;
                            Theta = Convert.ToDouble(polar.theta);
                            double RadEarth = geoUtils.CalculateEarthRadius(coord);
                            Elev = GeoUtils.CalculateElevation(coord, RadEarth, Rho, Math.Abs(Convert.ToDouble(Flight)) * 100 * GeoUtils.FEET2METERS);
                            CoordinatesXYZ cartesian = GeoUtils.change_radar_spherical2radar_cartesian(new CoordinatesPolar(Rho, Theta * GeoUtils.DEGS2RADS, Elev));
                            CoordinatesXYZ geocentric = geoUtils.change_radar_cartesian2geocentric(coord, cartesian);
                            CoordinatesWGS84 geodesic = geoUtils.change_geocentric2geodesic(geocentric); //Obtenim la latitud, longitud i elevació
                            double Lat_rad = geodesic.Lat;
                            double Long_rad = geodesic.Lon;
                            Lat = Lat_rad * GeoUtils.RADS2DEGS;
                            latitudList.Add(Lat);
                            Long = Long_rad * GeoUtils.RADS2DEGS;
                            longitudList.Add(Long);
                            h = geodesic.Height;
                            mensaje = Convert.ToString(Lat) + ";" + Convert.ToString(Long) + ";" + Convert.ToString(h);
                            Geodesic_Coord geocoord = new Geodesic_Coord(mensaje);
                            geocoord.Descodificar();
                            elemento.Insert(2, geocoord);
                            break;



                        }
                    }

                }
            }

        }

        public void GenerarAsterix(List<List<DataItem>> bloque)
        {
            int Num = 1;
            foreach (var elemento in bloque)
            {
                AsterixGrid grid = new AsterixGrid();
                grid.Num = Convert.ToString(Num);
                foreach (var di in elemento)
                {
                    AsterixGrid parcial = di.ObtenerAsterix();
                    
                    if (!string.IsNullOrEmpty(parcial.SAC)) grid.SAC = parcial.SAC;
                    if (!string.IsNullOrEmpty(parcial.SIC)) grid.SIC = parcial.SIC;
                    if (!string.IsNullOrEmpty(parcial.Time)) grid.Time = parcial.Time;
                    if (!string.IsNullOrEmpty(parcial.TYP)) grid.TYP = parcial.TYP;
                    if (!string.IsNullOrEmpty(parcial.SIM)) grid.SIM = parcial.SIM;
                    if (!string.IsNullOrEmpty(parcial.RDP)) grid.RDP = parcial.RDP;
                    if (!string.IsNullOrEmpty(parcial.SPI)) grid.SPI = parcial.SPI;
                    if (!string.IsNullOrEmpty(parcial.RAB)) grid.RAB = parcial.RAB;
                    if (!string.IsNullOrEmpty(parcial.TST)) grid.TST = parcial.TST;
                    if (!string.IsNullOrEmpty(parcial.ERR)) grid.ERR = parcial.ERR;
                    if (!string.IsNullOrEmpty(parcial.XPP)) grid.XPP = parcial.XPP;
                    if (!string.IsNullOrEmpty(parcial.ME)) grid.ME = parcial.ME;
                    if (!string.IsNullOrEmpty(parcial.MI)) grid.MI = parcial.MI;
                    if (!string.IsNullOrEmpty(parcial.FOE)) grid.FOE = parcial.FOE;
                    if (!string.IsNullOrEmpty(parcial.ADS_EP)) grid.ADS_EP = parcial.ADS_EP;
                    if (!string.IsNullOrEmpty(parcial.ADS_VAL)) grid.ADS_VAL = parcial.ADS_VAL;
                    if (!string.IsNullOrEmpty(parcial.SCN_EP)) grid.SCN_EP = parcial.SCN_EP;
                    if (!string.IsNullOrEmpty(parcial.SCN_VAL)) grid.SCN_VAL = parcial.SCN_VAL;
                    if (!string.IsNullOrEmpty(parcial.PAI_EP)) grid.PAI_EP = parcial.PAI_EP;
                    if (!string.IsNullOrEmpty(parcial.PAI_VAL)) grid.PAI_VAL = parcial.PAI_VAL;
                    if (!string.IsNullOrEmpty(parcial.Rho)) grid.Rho = parcial.Rho;
                    if (!string.IsNullOrEmpty(parcial.Theta)) grid.Theta = parcial.Theta;
                    if (!string.IsNullOrEmpty(parcial.Latitude)) grid.Latitude = parcial.Latitude;
                    if (!string.IsNullOrEmpty(parcial.Longitude)) grid.Longitude = parcial.Longitude;
                    if (!string.IsNullOrEmpty(parcial.Height)) grid.Height = parcial.Height;
                    if (!string.IsNullOrEmpty(parcial.V_70)) grid.V_70 = parcial.V_70;
                    if (!string.IsNullOrEmpty(parcial.G_70)) grid.G_70 = parcial.G_70;
                    if (!string.IsNullOrEmpty(parcial.L_70)) grid.L_70 = parcial.L_70;
                    if (!string.IsNullOrEmpty(parcial.Mode3_A_Reply)) grid.Mode3_A_Reply = parcial.Mode3_A_Reply;
                    if (!string.IsNullOrEmpty(parcial.V_90)) grid.V_90 = parcial.V_90;
                    if (!string.IsNullOrEmpty(parcial.G_90)) grid.G_90 = parcial.G_90;
                    if (!string.IsNullOrEmpty(parcial.Flight_Level)) grid.Flight_Level = parcial.Flight_Level;
                    if (!string.IsNullOrEmpty(parcial.Mode_C_Correction)) grid.Mode_C_Correction = parcial.Mode_C_Correction;
                    if (!string.IsNullOrEmpty(parcial.SRL)) grid.SRL = parcial.SRL;
                    if (!string.IsNullOrEmpty(parcial.SRR)) grid.SRR = parcial.SRR;
                    if (!string.IsNullOrEmpty(parcial.SAM)) grid.SAM = parcial.SAM;
                    if (!string.IsNullOrEmpty(parcial.PRL)) grid.PRL = parcial.PRL;
                    if (!string.IsNullOrEmpty(parcial.PAM)) grid.PAM = parcial.PAM;
                    if (!string.IsNullOrEmpty(parcial.RPD)) grid.RPD = parcial.RPD;
                    if (!string.IsNullOrEmpty(parcial.APD)) grid.APD = parcial.APD;
                    if (!string.IsNullOrEmpty(parcial.Aircraft_Address)) grid.Aircraft_Address = parcial.Aircraft_Address;
                    if (!string.IsNullOrEmpty(parcial.Aircraft_Indentification)) grid.Aircraft_Indentification = parcial.Aircraft_Indentification;


                    // Asignamos las propiedades solo si parcial las tiene llenas
                    if (!string.IsNullOrEmpty(parcial.BDS_4_0)) grid.BDS_4_0 = parcial.BDS_4_0;
                    if (!string.IsNullOrEmpty(parcial.MCP_FCUtxt)) grid.MCP_FCUtxt = parcial.MCP_FCUtxt;
                    if (!string.IsNullOrEmpty(parcial.FMStxt)) grid.FMStxt = parcial.FMStxt;
                    if (!string.IsNullOrEmpty(parcial.BARtxt)) grid.BARtxt = parcial.BARtxt;
                    if (!string.IsNullOrEmpty(parcial.Mode_stat_txt)) grid.Mode_stat_txt = parcial.Mode_stat_txt;
                    if (!string.IsNullOrEmpty(parcial.VNAVMODEtxt)) grid.VNAVMODEtxt = parcial.VNAVMODEtxt;
                    if (!string.IsNullOrEmpty(parcial.ALTHOLDtxt)) grid.ALTHOLDtxt = parcial.ALTHOLDtxt;
                    if (!string.IsNullOrEmpty(parcial.Approachtxt)) grid.Approachtxt = parcial.Approachtxt;
                    if (!string.IsNullOrEmpty(parcial.StatusTargAlt)) grid.StatusTargAlt = parcial.StatusTargAlt;
                    if (!string.IsNullOrEmpty(parcial.TargetAltSourcetxt)) grid.TargetAltSourcetxt = parcial.TargetAltSourcetxt;

                    if (!string.IsNullOrEmpty(parcial.BDS_5_0)) grid.BDS_5_0 = parcial.BDS_5_0;
                    if (!string.IsNullOrEmpty(parcial.Rolltxt)) grid.Rolltxt = parcial.Rolltxt;
                    if (!string.IsNullOrEmpty(parcial.TrueTracktxt)) grid.TrueTracktxt = parcial.TrueTracktxt;
                    if (!string.IsNullOrEmpty(parcial.GroundSpeedtxt)) grid.GroundSpeedtxt = parcial.GroundSpeedtxt;
                    if (!string.IsNullOrEmpty(parcial.TrackAngletxt)) grid.TrackAngletxt = parcial.TrackAngletxt;
                    if (!string.IsNullOrEmpty(parcial.TrueAirspeedtxt)) grid.TrueAirspeedtxt = parcial.TrueAirspeedtxt;

                    if (!string.IsNullOrEmpty(parcial.BDS_6_0)) grid.BDS_6_0 = parcial.BDS_6_0;
                    if (!string.IsNullOrEmpty(parcial.MagHeadtxt)) grid.MagHeadtxt = parcial.MagHeadtxt;
                    if (!string.IsNullOrEmpty(parcial.IndAirtxt)) grid.IndAirtxt = parcial.IndAirtxt;
                    if (!string.IsNullOrEmpty(parcial.MACHtxt)) grid.MACHtxt = parcial.MACHtxt;
                    if (!string.IsNullOrEmpty(parcial.BarAlttxt)) grid.BarAlttxt = parcial.BarAlttxt;
                    if (!string.IsNullOrEmpty(parcial.InerVerttxt)) grid.InerVerttxt = parcial.InerVerttxt;

                    if (!string.IsNullOrEmpty(parcial.Track_Number)) grid.Track_Number = parcial.Track_Number;
                    if (!string.IsNullOrEmpty(parcial.X_Component)) grid.X_Component = parcial.X_Component;
                    if (!string.IsNullOrEmpty(parcial.Y_Component)) grid.Y_Component = parcial.Y_Component;

                    if (!string.IsNullOrEmpty(parcial.Ground_Speed)) grid.Ground_Speed = parcial.Ground_Speed;
                    if (!string.IsNullOrEmpty(parcial.Heading)) grid.Heading = parcial.Heading;

                    if (!string.IsNullOrEmpty(parcial.CNF)) grid.CNF = parcial.CNF;
                    if (!string.IsNullOrEmpty(parcial.RAD)) grid.RAD = parcial.RAD;
                    if (!string.IsNullOrEmpty(parcial.DOU)) grid.DOU = parcial.DOU;
                    if (!string.IsNullOrEmpty(parcial.MAH)) grid.MAH = parcial.MAH;
                    if (!string.IsNullOrEmpty(parcial.CDM)) grid.CDM = parcial.CDM;
                    if (!string.IsNullOrEmpty(parcial.TRE)) grid.TRE = parcial.TRE;
                    if (!string.IsNullOrEmpty(parcial.GHO)) grid.GHO = parcial.GHO;
                    if (!string.IsNullOrEmpty(parcial.SUP)) grid.SUP = parcial.SUP;
                    if (!string.IsNullOrEmpty(parcial.TCC)) grid.TCC = parcial.TCC;

                    if (!string.IsNullOrEmpty(parcial.Height_3D)) grid.Height_3D = parcial.Height_3D;

                    if (!string.IsNullOrEmpty(parcial.COM)) grid.COM = parcial.COM;
                    if (!string.IsNullOrEmpty(parcial.STAT)) grid.STAT = parcial.STAT;
                    if (!string.IsNullOrEmpty(parcial.SI)) grid.SI = parcial.SI;
                    if (!string.IsNullOrEmpty(parcial.MSSC)) grid.MSSC = parcial.MSSC;
                    if (!string.IsNullOrEmpty(parcial.ARC)) grid.ARC = parcial.ARC;
                    if (!string.IsNullOrEmpty(parcial.AIC)) grid.AIC = parcial.AIC;
                    if (!string.IsNullOrEmpty(parcial.B1A)) grid.B1A = parcial.B1A;
                    if (!string.IsNullOrEmpty(parcial.B1B)) grid.B1B = parcial.B1B;
                  


                }
                asterixGrids.Add(grid);
                Num++;


            }
        }

    }
}
