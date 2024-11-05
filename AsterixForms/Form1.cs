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
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using GMap.NET.WindowsForms.Markers;
using System.Drawing;
using System.Reflection.Metadata;
using MultiCAT6.Utils;


namespace AsterixForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<List<DataItem>> bloque = new List<List<DataItem>>();
        List<AsterixGrid> asterixGrids = new List<AsterixGrid>();
        int time;



        //### EVENTS ####################################################################################################################
        /*private void Seleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Selecciona un archivo .ast";
            openFileDialog.Filter = "Todos los arxivos (*.ast*)|*ast*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string FilePath = openFileDialog.FileName;
                
                DataGridView formulari = new DataGridView(FilePath);
                formulari.Show(); // Al obtenir el fitxer obrirem el nou formulari

                this.Hide();
            }


        }*/


        private void Fichero_Click(object sender, EventArgs e)
        {
            //label1.Visible = true;

            //NombreFichero.Visible = true;


        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            //string fichero = NombreFichero.Text;
            //EscribirFichero(bloque, fichero);
            MessageBox.Show("S'ha escrit correctament el fitxer");
        }

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
            }

            groupBox1.Hide();
            groupBox2.Show();
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
                            di.Add(new AsterixLib.DataSourceIdentifier(mensaje));
                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        else
                        {
                            di.Add(new AsterixLib.DataSourceIdentifier("N/A"));
                        }
                        break;
                    case 1:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 3 * octet);
                            //Debug.WriteLine("Missatge TimeOfDay: " + mensaje);
                            di.Add(new AsterixLib.TimeOfDay(mensaje));
                            bitsleidos = bitsleidos + 3 * octet;
                        }
                        else
                        {
                            di.Add(new AsterixLib.TimeOfDay("N/A"));
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
                            di.Add(new AsterixLib.TargetReportDescriptor(mensaje));
                            cadena.Clear(); //Buidem la llista per a no gastar memòria
                        }
                        else
                        {
                            di.Add(new AsterixLib.TargetReportDescriptor("N/A"));
                        }
                        break;
                    case 3:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 4 * octet);
                            //Debug.WriteLine("Missatge PositionPolar: " + mensaje);
                            di.Add(new AsterixLib.Position_Polar(mensaje));
                            bitsleidos = bitsleidos + 4 * octet;
                        }
                        else
                        {
                            di.Add(new AsterixLib.Position_Polar("N/A"));
                        }
                        break;
                    case 4:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 2 * octet);
                            //Debug.WriteLine("Missatge Mode3A: " + mensaje);
                            di.Add(new AsterixLib.Mode3A(mensaje));
                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        else
                        {
                            di.Add(new AsterixLib.Mode3A("N/A"));
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
                        else
                        {
                            di.Add(new AsterixLib.FlightLevel("N/A"));
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
                            di.Add(new AsterixLib.RadarPlotChar(mensaje));
                            bitsleidos = bitsleidos + octet + length;
                        }
                        else
                        {
                            di.Add(new AsterixLib.RadarPlotChar("N/A"));
                        }
                        break;
                    case 7:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 3 * octet);
                            //Debug.WriteLine("Missatge AircraftAdd: " + mensaje);
                            di.Add(new AsterixLib.AircraftAdd(mensaje));
                            bitsleidos = bitsleidos + 3 * octet;
                        }
                        else
                        {
                            di.Add(new AsterixLib.AircraftAdd("N/A"));
                        }
                        break;
                    case 8:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 6 * octet);
                            //Debug.WriteLine("Missatge AircraftId: " + mensaje);
                            di.Add(new AsterixLib.AircraftID(mensaje));
                            bitsleidos = bitsleidos + 6 * octet;
                        }
                        else
                        {
                            di.Add(new AsterixLib.AircraftID("N/A"));
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
                                    di.Add(new AsterixLib.ModeS4(mensaje));
                                    flag4 = 1;
                                }
                                else if (BDS1 == 5 & BDS2 == 0)
                                {
                                    di.Add(new AsterixLib.ModeS5(mensaje));
                                    flag5 = 1;
                                }
                                else if (BDS1 == 6 & BDS2 == 0)
                                {
                                    di.Add(new AsterixLib.ModeS6(mensaje));
                                    flag6 = 1;
                                }
                                bitsleidos = bitsleidos + 8 * octet;
                            }
                            if (flag4 == 0 & flag5 == 0 & flag6 == 0)
                            {
                                di.Add(new AsterixLib.ModeS4("N/A"));
                                di.Add(new AsterixLib.ModeS5("N/A"));
                                di.Add(new AsterixLib.ModeS6("N/A"));
                            }
                            else if (flag4 == 0 & flag5 == 0)
                            {
                                di.Add(new AsterixLib.ModeS4("N/A"));
                                di.Add(new AsterixLib.ModeS5("N/A"));
                            }
                            else if (flag4 == 0 & flag6 == 0)
                            {
                                di.Add(new AsterixLib.ModeS4("N/A"));
                                di.Add(new AsterixLib.ModeS6("N/A"));
                            }
                            else if (flag5 == 0 & flag6 == 0)
                            {
                                di.Add(new AsterixLib.ModeS5("N/A"));
                                di.Add(new AsterixLib.ModeS6("N/A"));
                            }
                            else if (flag4 == 0)
                            {
                                di.Add(new AsterixLib.ModeS4("N/A"));
                            }
                            else if (flag5 == 0)
                            {
                                di.Add(new AsterixLib.ModeS5("N/A"));
                            }
                            else if (flag6 == 0)
                            {
                                di.Add(new AsterixLib.ModeS6("N/A"));
                            }


                        }
                        else
                        {
                            di.Add(new AsterixLib.ModeS4("N/A"));
                            di.Add(new AsterixLib.ModeS5("N/A"));
                            di.Add(new AsterixLib.ModeS6("N/A"));
                        }
                        break;
                    case 10:

                        if (read[i] == 1)
                        {

                            mensaje = DataBlock.Substring(bitsleidos, 2 * octet);
                            //Debug.WriteLine("Missatge TrackNum: " + mensaje);
                            di.Add(new AsterixLib.TrackNum(mensaje));
                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        else
                        {
                            di.Add(new AsterixLib.TrackNum("N/A"));
                        }
                        break;
                    case 11:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 4 * octet);
                            //Debug.WriteLine("Missatge Pos_Cart: " + mensaje);
                            di.Add(new AsterixLib.Position_Cartesian(mensaje));
                            bitsleidos = bitsleidos + 4 * octet;
                        }
                        else
                        {
                            di.Add(new AsterixLib.Position_Cartesian("N/A"));
                        }
                        break;
                    case 12:
                        if (read[i] == 1)
                        {
                            mensaje = DataBlock.Substring(bitsleidos, 4 * octet);

                            //Debug.WriteLine("Missatge Track_vel: " + mensaje);
                            di.Add(new AsterixLib.TrackVelocityPolar(mensaje));
                            bitsleidos = bitsleidos + 4 * octet;
                        }
                        else
                        {
                            di.Add(new AsterixLib.TrackVelocityPolar("N/A"));
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
                            di.Add(new AsterixLib.TrackStatus(mensaje));
                            cadena.Clear(); //Buidem la llista per a no gastar memòria

                        }
                        else
                        {
                            di.Add(new AsterixLib.TrackStatus("N/A"));
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
                        else
                        {
                            di.Add(new AsterixLib.H_3D_RADAR("N/A"));
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
                            di.Add(new AsterixLib.CommACAS(mensaje));
                            bitsleidos = bitsleidos + 2 * octet;
                        }
                        else
                        {
                            di.Add(new AsterixLib.CommACAS("N/A"));
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
            // falta posar reiniciar simulacio i timer
        }

        private void ShowDataBut_Click(object sender, EventArgs e)
        {
            
            DataGridView formulari = new DataGridView(asterixGrids);
            formulari.Show(); // Al obtenir el fitxer obrirem el nou formulari
            // Cal passar la info per poder fer el datagridview
            // passen vector amb info i no fitxer asterix crec
        }




        // **************** CAL DESCOBRIR COM AFEGIR GMapControl
        //int zoom = 7;
        /*private void gMapControl1_Load(object sender, EventArgs e)
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
        }*/


        /*int num_loop = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            Tick(ref time, ref num_loop);
            timeTXT.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)(time / 3600), (int)((time % 3600) / 60), (int)(time % 60));
        }*/

        /*private void Tick(ref int time, ref int num_loop)
        {
            if (num_loop != sim_data.Count)
            {
                for (int i = 0; num_loop < sim_data.Count && time >= sim_data[num_loop].time; num_loop++)
                {
                    AddMarkerToMap(sim_data[num_loop].lat, sim_data[num_loop].lon, sim_data[num_loop].Aircraft_Indentification);
                }
                gMapControl1.Update();
            }
            else
            {
                timer1.Stop();
                time = time - 2;
                Start_sim.Visible = false;
            }
        }*/

        /*private List<string> Sim_diccionary = new List<string>();
        private List<(string name, int position)> position = new List<(string name, int position)>();

        private void AddMarkerToMap(double lat, double lon, string name)
        {
            GMapOverlay markers = gMapControl1.Overlays.FirstOrDefault(o => o.Id == name);
            GMapMarker existingMarker = markers?.Markers.FirstOrDefault(m => m.Tag?.ToString() == name);

            if (existingMarker != null)
            {
                int planeData = sim_data.FindIndex(p => p.Aircraft_Indentification == name && p.lon == lon && p.lat == lat);
                if (position.Contains((name, planeData)) && sim_data[planeData].rho > 17)
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
                GMapMarker marker = new GMarkerGoogle(new PointLatLng(lat, lon), GMarkerGoogleType.red_small)
                {
                    Tag = name
                };

                markers.Markers.Add(marker);
                gMapControl1.Overlays.Add(markers);
                gMapControl1.UpdateMarkerLocalPosition(marker);
            }
        }*/

        /*private void gMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            string name = item.Tag?.ToString();

            GMapOverlay markers = gMapControl1.Overlays.FirstOrDefault(o => o.Id == name);
            GMapMarker existingMarker = markers?.Markers.FirstOrDefault(m => m.Tag?.ToString() == name);

            if (Sim_diccionary.Contains(name))
            {
                var position = existingMarker.Position;

                var planeData = sim_data.FirstOrDefault(p => p.Aircraft_Indentification == name && p.lon == position.Lng && p.lat == position.Lat);
                string info = $"Aircraft address: {planeData.Aircraft_address}\n" +
                                  $"Track number: {planeData.Track_numbre}\n" +
                                  $"Mode 3A Reply: {planeData.Mode_3A_Reply}\n" +
                                  $"\n" +
                                  $"Lat: {planeData.lat}º\n" +
                                  $"Lon: {planeData.lon}º\n" +
                                  $"Altitude: {planeData.altitude} ft\n" +
                                  $"\n" +
                                  $"SAC: {planeData.SAC}\n" +
                                  $"SIC: {planeData.SIC}\n";

                MessageBox.Show(info, $"Aircraft indentification: {planeData.Aircraft_Indentification}");
            }
        }*/

        /*int Click_times = 0;
        private void Start_sim_Click(object sender, EventArgs e)
        {
            if (Start_sim.Text == "Start Simulation" || Start_sim.Text == "Continue")
            {
                if (Click_times == 0)
                {
                    Start_sim.Text = "Stop";
                    sim_data = sim_data.OrderBy(data => data.time).ToList();
                    Time_lbl.Show();
                    timer1.Start();
                    Click_times++;
                    Restart.Visible = true;
                    Sim_diccionary.Clear();

                    if (position.Count == 0)
                    {
                        var lastPositions = sim_data
                        .Select((data, index) => new { Data = data, Index = index }) // Proyectar el dato y su índice
                        .GroupBy(item => item.Data.Aircraft_Indentification) // Agrupar por nombre de aeronave
                        .Select(group => new { Name = group.Key, LastPosition = group.Last().Index }); // Obtener la última posición de cada grupo

                        // Crear una lista que contienen el nombre de la aeronave y su última posición
                        position = lastPositions
                            .Select(item => (item.Name, item.LastPosition))
                            .ToList();
                    }
                }
                else
                {
                    Start_sim.Text = "Stop";
                    timer1.Start();
                }
            }
            else
            {
                Start_sim.Text = "Continue";
                timer1.Stop();
            }
        }*/

        private void RestartSimBut_Click(object sender, EventArgs e)
        {
            /*timer1.Stop();
            time = (int)sim_data[0].time;
            num_loop = 0;
            Click_times = 0;
            gMapControl1.Overlays.Clear();
            gMapControl1.ReloadMap();
            Time_lbl.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)(time / 3600), (int)((time % 3600) / 60), (int)(time % 60));
            Start_sim.Text = "Start Simulation";
            Start_sim.Visible = true;
            Restart.Visible = false;*/
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            /*int velocity = Velocity_bar.Value;
            switch (velocity)
            {
                case 0:
                    timer1.Interval = 1000;
                    Velocity_label_bar.Text = "Velocity x1";
                    break;

                case 1:
                    timer1.Interval = 500;
                    Velocity_label_bar.Text = "Velocity x2";
                    break;

                case 2:
                    timer1.Interval = 250;
                    Velocity_label_bar.Text = "Velocity x4";
                    break;

                case 3:
                    timer1.Interval = 200;
                    Velocity_label_bar.Text = "Velocity x5";
                    break;

                case 4:
                    timer1.Interval = 100;
                    Velocity_label_bar.Text = "Velocity x10";
                    break;

                case 5:
                    timer1.Interval = 10;
                    Velocity_label_bar.Text = "Velocity x100";
                    break;
            }*/
        }

        private void GetKMLBut_Click(object sender, EventArgs e)
        {
            /*SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivo KML|*.kml";
            saveFileDialog.Title = "Guardar archivo KML";
            saveFileDialog.InitialDirectory = @"C:\";  // Carpeta inicial

            // Muestra el cuadro de diálogo de guardado
            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName; // Saves the file name 

                Dictionary<string, KML_DATA> posicionesDeRepeticiones = new Dictionary<string, KML_DATA>();

                for (int i = 0; i < sim_data.Count; i++)
                {
                    string nombre = sim_data[i].Aircraft_address;
                    if (!posicionesDeRepeticiones.ContainsKey(nombre))
                    {
                        posicionesDeRepeticiones[nombre] = new KML_DATA();
                        posicionesDeRepeticiones[nombre].Positions = new List<Vector>();
                        posicionesDeRepeticiones[nombre].Description = "Aircraft address: " + nombre + " ; Aircraft indentification: " + sim_data[i].Aircraft_Indentification + " ; Track number: " + sim_data[i].Track_numbre + " ; Mode 3A Reply: " + sim_data[i].Mode_3A_Reply + " ; SAC: " + sim_data[i].SAC + " ; SIC: " + sim_data[i].SIC;
                    }
                    posicionesDeRepeticiones[nombre].Positions.Add(new Vector(sim_data[i].lat, sim_data[i].lon, sim_data[i].altitude));
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

            }*/
        }
        private class KML_DATA
        {
            //public List<Vector> Positions { get; set; }
            //public string Description { get; set; }
        }
        //Funció per a escriure en el fitxer
        private void EscribirFichero(List<List<DataItem>> bloque, string nombreFichero)
        {
            int NumLinea = 1;
            DataItem.SetNombreFichero(nombreFichero); //En el moment en que es decideixi com es diu el ficher s'ha de posar allà
            string cabecera = "Num Linea;SAC;SIC;Time of Day;Latitud;Longitud;Altura;YP;SIM;RDP;SPI;RAB;TST;ERR;XPP;ME;MI;FOE;ADSBEP;ADSBVAL;SCNEP;SCNVAL;PAIEP;PAIVAL;RHO;THETA;Mode-3/A V;Mode-3/A G;Mode-3/A L;Mode-3/A reply;FL V;FL G;Flight level;Mode C Corrected;SRL;SRR;SAM;PRL;PAM;RPD;APD;Aircraft address;Aircraft Identification;MCPU/FCU Selected altitude;FMS Selected Altitude;Barometric pressure setting;Mode status;VNAV;ALTHOLD;Approach;Target status;Target altitude source;Roll angle;True track angle;Ground Speed;Track angle rate;True Airspeed;Magnetic heading;Indicated airspeed;Mach;Barometric altitude rate;Inertial Vertical Velocity;Track Number;X-Cartesian;Y-Cartesian;Calculated groundspeed;Calculated heading;CNF;RAD;DOU;MAH;CDM;TRE;GHO;SUP;TCC;Height Measured by a 3D Radar;COM;STATUS;SI;MSSC;ARC;AIC;B1A_message;B1B_message";
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
                            Flight = null;
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
                            Long = Long_rad * GeoUtils.RADS2DEGS;
                            h = geodesic.Height;
                            mensaje = Convert.ToString(Lat) + ";" + Convert.ToString(Long) + ";" + Convert.ToString(h);
                            Geodesic_Coord geocoord = new Geodesic_Coord(mensaje);
                            geocoord.Descodificar();
                            elemento.Insert(2, geocoord);
                            break;



                        }
                    }

                }
                else
                {
                    string mensaje = "N/A";
                    Geodesic_Coord geocoord = new Geodesic_Coord(mensaje);
                    geocoord.Descodificar();
                    elemento.Insert(2, geocoord);


                }
            }

        }

        public void GenerarAsterix(List<List<DataItem>> bloque)
        {
            
            foreach (var elemento in bloque)
            {
                AsterixGrid grid = new AsterixGrid();
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
                    if (!string.IsNullOrEmpty(parcial.BDS_5_0)) grid.BDS_5_0 = parcial.BDS_5_0;
                    if (!string.IsNullOrEmpty(parcial.BDS_6_0)) grid.BDS_6_0 = parcial.BDS_6_0;

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


            }
        }
    }
}
