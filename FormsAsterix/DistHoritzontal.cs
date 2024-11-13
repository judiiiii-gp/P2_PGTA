using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpKml.Dom;
using SharpKml.Base;
using SharpKml.Engine;
using Vector = SharpKml.Base.Vector;
using Document = SharpKml.Dom.Document;
using GMap.NET.MapProviders;
using GMap.NET;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using LibAsterix;
using System.Security.Cryptography;

namespace FormsAsterix
{
    public partial class DistHoritzontal : Form
    {

        // veure a on declarar per rebre info de l'altre formulari :)
        string Aircraft1;
        string Aircraft2;

        List<double> longitudList = new List<double>();
        List<double> latitudList = new List<double>();
        List<double> AltitudeList = new List<double>();

        List<String> AircraftIDList = new List<String>();
        List<string> AircraftAddrList = new List<string>();
        List<string> TrackNumList = new List<string>();
        List<string> Mode3AList = new List<string>();
        List<string> SACList = new List<string>();
        List<string> SICList = new List<string>();

        List<double> DistHor = new List<double>();



        List<long> time = new List<long>();
        long timeInicial;

        List<List<DataItem>> bloque = new List<List<DataItem>>();

        public DistHoritzontal(string A1, string A2, List<double> longitudList_sub, List<double> latitudList_sub, List<String> AircraftIDList_sub, List<string> AircraftAddrList_sub, List<string> TrackNumList_sub, List<string> Mode3AList_sub, List<string> SACList_sub, List<string> SICList_sub, List<double> AltitudeList_sub, List<long> time_sub, List<List<DataItem>> bloque_sub, List<double> DistHor_sub)
        {
            InitializeComponent();
            // set some initialization parameters
            Start_sim.FlatAppearance.BorderSize = 0;
            Start_sim.FlatAppearance.MouseDownBackColor = Color.Transparent;
            Start_sim.FlatAppearance.MouseOverBackColor = Color.Transparent;
            Start_sim.MouseEnter += (s, e) => Start_sim.Cursor = Cursors.Hand;
            Start_sim.MouseLeave += (s, e) => Start_sim.Cursor = Cursors.Default;

            this.Aircraft1 = A1;
            this.Aircraft2 = A2;
            this.AircraftIDList = AircraftIDList_sub;
            this.AircraftAddrList = AircraftAddrList_sub;
            this.TrackNumList = TrackNumList_sub;
            this.Mode3AList = Mode3AList_sub;
            this.SACList = SACList_sub;
            this.SICList = SICList_sub;
            this.AltitudeList = AltitudeList_sub;
            this.longitudList = longitudList_sub;
            this.latitudList = latitudList_sub;
            this.DistHor = DistHor_sub;

            this.time = time_sub;
            this.timeInicial = time[0];

            this.bloque = bloque_sub;


            // Definir DataGridView
            SetHeaders(Aircraft1, Aircraft2);

            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(40, 40);
            imageList.Images.Add(Properties.Resources.play_button);
            Start_sim.Image = imageList.Images[0];
            Start_sim.ImageAlign = ContentAlignment.MiddleCenter;
            Start_sim.TextImageRelation = TextImageRelation.ImageAboveText;
            Start_sim.ImageAlign = ContentAlignment.TopCenter;
            Start_sim.TextAlign = ContentAlignment.BottomCenter;
            timeInicial = time[0];
            timeTXT.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)(timeInicial / 3600), (int)((timeInicial % 3600) / 60), (int)(timeInicial % 60));

            valueTXT.Text = "";
        }

        private void SetHeaders(string A1, string A2)
        {

            List<string> lista = new List<string> {
            "Aircraft address","Track Number", "Mode-3/A reply","Latitud", "Longitud", "Height", "SAC", "SIC"};
            
            dataGridView1.Columns.Add(A1, A1);
            dataGridView1.Columns.Add(A2, A2);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.RowHeadersWidth = 150;

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView1.RowHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold); // Para encabezados de fila también

            dataGridView1.AllowUserToAddRows = false;

            foreach (string headerText in lista)
            {
                int rowIndex = dataGridView1.Rows.Add(); // Agregar una nueva fila
                dataGridView1.Rows[rowIndex].HeaderCell.Value = headerText; // Asignar el valor como encabezado de fila
            }


            int flag1 = 0;
            int flag2 = 0;
            int iter = 0;

            for (int i = 0; i < AircraftAddrList.Count; i++)
            {
                if (AircraftIDList[i].Trim() == A1.Trim() && flag1 == 0)
                {
                    dataGridView1.Rows[0].Cells[0].Value = AircraftAddrList[iter];
                    dataGridView1.Rows[1].Cells[0].Value = TrackNumList[iter];
                    dataGridView1.Rows[2].Cells[0].Value = Mode3AList[iter];
                    dataGridView1.Rows[3].Cells[0].Value = latitudList[iter];
                    dataGridView1.Rows[4].Cells[0].Value = longitudList[iter];
                    dataGridView1.Rows[5].Cells[0].Value = AltitudeList[iter];
                    dataGridView1.Rows[6].Cells[0].Value = SACList[iter];
                    dataGridView1.Rows[7].Cells[0].Value = SICList[iter];
                    flag1 = 1;
                }
                else if (AircraftIDList[i].Trim() == A2.Trim() && flag2 == 0)
                {
                    dataGridView1.Rows[0].Cells[1].Value = AircraftAddrList[iter];
                    dataGridView1.Rows[1].Cells[1].Value = TrackNumList[iter];
                    dataGridView1.Rows[2].Cells[1].Value = Mode3AList[iter];
                    dataGridView1.Rows[3].Cells[1].Value = latitudList[iter];
                    dataGridView1.Rows[4].Cells[1].Value = longitudList[iter];
                    dataGridView1.Rows[5].Cells[1].Value = AltitudeList[iter];
                    dataGridView1.Rows[6].Cells[1].Value = SACList[iter];
                    dataGridView1.Rows[7].Cells[1].Value = SICList[iter];
                    flag2 = 1;
                }
                iter++;
            }
        }
      


        // GET KML 
        private class KML_DATA
        {
            public List<Vector> Positions { get; set; }
            public string Description { get; set; }
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
        int tick = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Increment the initial time(timeInicial) each time the timer ticks
            timeInicial++;

            Tick(ref timeInicial, ref num_loop, Aircraft1, Aircraft2);

            // Update the time display (timeTXT) with the formatted time
            timeTXT.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)(timeInicial / 3600), (int)((timeInicial % 3600) / 60), (int)(timeInicial % 60));
            
            tick++;
        }

        private void Tick(ref long timeTick, ref int num_loop, string a1, string a2)
        {
            // Check if num_loop is less than the number of aircraft in the list
            if (num_loop != AircraftIDList.Count)
            {
                for (int i = 0; num_loop < AircraftIDList.Count && timeTick >= time[num_loop]; num_loop++)
                {
                    if(AircraftIDList[num_loop] != "N/A")
                    {
                        SetValuesCells(AircraftIDList[num_loop], num_loop);
                        AddMarkerToMap(latitudList[num_loop], longitudList[num_loop], AircraftIDList[num_loop], num_loop);
                        valueTXT.Text = Convert.ToString(DistHor[num_loop]) + " km";
                        dataGridView1.Refresh();
                    }
                      
                    
                }
                gMapControl1.Update();
            }
            else
            {
                // If num_loop reaches the total number of aircraft, stop the timer and adjust the timeTick
                timer1.Stop();
                timeTick = timeTick - 2;
                Start_sim.Visible = false;
            }
        }

        private void SetValuesCells(string Aid, int iter)
        {
            if (Aid.Trim() == Aircraft1.Trim())
            {
                dataGridView1.Rows[3].Cells[0].Value = latitudList[iter];
                dataGridView1.Rows[4].Cells[0].Value = longitudList[iter];
                dataGridView1.Rows[5].Cells[0].Value = AltitudeList[iter];
            }
            else if (Aid.Trim() == Aircraft2.Trim())
            {
                dataGridView1.Rows[3].Cells[1].Value = latitudList[iter];
                dataGridView1.Rows[4].Cells[1].Value = longitudList[iter];
                dataGridView1.Rows[5].Cells[1].Value = AltitudeList[iter];
            }
        }


        // Funtions to show the Markers on map
        private List<string> Sim_diccionary = new List<string>();
        private List<(string name, int position)> position = new List<(string name, int position)>();

        private void AddMarkerToMap(double lat, double lon, string name, int currentIndex)
        {
            // DETECTAR QUE SI JA NO APAREIXEN MÉS cap dels dos ES PARI LA SIMULACIO

            // Check if the aircraft appears again after this index
            bool nameAppearsInFuture1 = AircraftIDList.Skip(currentIndex + 1).Contains(Aircraft1.Trim());
            bool nameAppearsInFuture2 = AircraftIDList.Skip(currentIndex + 1).Contains(Aircraft2.Trim());

            GMapOverlay markers = gMapControl1.Overlays.FirstOrDefault(o => o.Id == name);
            GMapMarker existingMarker = markers?.Markers.FirstOrDefault(m => m.Tag?.ToString() == name);

            if (!nameAppearsInFuture1 && !nameAppearsInFuture2)
            {
                Start_sim.Hide();
                timer1.Stop();
                MessageBox.Show("No more data is available for both aircrafts.");
                return;  
            }

            
            if (existingMarker != null)
            {
                if (position.Contains((name, currentIndex)))
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

        int Click_times = 0;
        private void Start_sim_Click(object sender, EventArgs e)
        {
            // Check if the button text is either "Start" or "Continue"
            if (Start_sim.Text == " Start" || Start_sim.Text == " Continue")
            {
                // Set up the ImageList to display the play button image
                ImageList imageList = new ImageList();
                imageList.ImageSize = new Size(40, 40);
                imageList.Images.Add(Properties.Resources.play_button);
                Start_sim.Image = imageList.Images[0];
                Start_sim.ImageAlign = ContentAlignment.MiddleCenter;
                Start_sim.TextImageRelation = TextImageRelation.ImageAboveText;
                Start_sim.ImageAlign = ContentAlignment.TopCenter;
                Start_sim.TextAlign = ContentAlignment.BottomCenter;

                // If the button is clicked for the first time
                if (Click_times == 0)
                {
                    // Set up the ImageList to display the stop button image
                    ImageList imageListStop = new ImageList();
                    imageListStop.ImageSize = new Size(40, 40);
                    imageListStop.Images.Add(Properties.Resources.pause);
                    Start_sim.Image = imageListStop.Images[0];
                    Start_sim.ImageAlign = ContentAlignment.MiddleCenter;
                    Start_sim.TextImageRelation = TextImageRelation.ImageAboveText;
                    Start_sim.ImageAlign = ContentAlignment.TopCenter;
                    Start_sim.TextAlign = ContentAlignment.BottomCenter;
                    Start_sim.Text = " Stop";

                    // Sort the 'bloque' data based on the third element (index 2)
                    // bloque = bloque.OrderBy(data => Convert.ToString(data[2])).ToList();

                    // Show the time text control and start the timer
                    timeTXT.Show();
                    timer1.Start();

                    // Increment the click count and clear the simulation dictionary
                    Click_times++;
                    Sim_diccionary.Clear();

                    if (position.Count == 0)
                    {
                        // Get the maximum index (last position)
                        var maxIndex = bloque
                        .Select((data, index) => index)
                        .Max();

                        // Get the last position for each aircraft group
                        var lastPositions = bloque
                        .Select((data, index) => new { Data = data, Index = index })
                        .GroupBy(item => AircraftIDList)
                        .Select(group => new { Name = group.Key, LastPosition = group.Last().Index });

                        // Create a list that contains the aircraft name and its last position
                        position = lastPositions
                            .Select(item => (Convert.ToString(item.Name), item.LastPosition))
                            .ToList();
                    }
                }
                else
                {
                    // Set up the ImageList to display the stop button image
                    ImageList imageListStop = new ImageList();
                    imageListStop.ImageSize = new Size(40, 40);
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
                // Set up the ImageList to display the play button image
                ImageList imageList = new ImageList();
                imageList.ImageSize = new Size(40, 40);
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
            // Stop the simulation timer to pause the simulation
            timer1.Stop();

            // Reset the initial time to the first time in the time array (time[0])
            timeInicial = time[0];

            // Reset the loop counter and click count
            num_loop = 0;
            Click_times = 0;

            // Clear all overlays from the map (removes any markers or other graphical elements)
            gMapControl1.Overlays.Clear();
            gMapControl1.ReloadMap();

            // Set the time text to show the formatted initial time (HH:mm:ss)
            timeTXT.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)(timeInicial / 3600), (int)((timeInicial % 3600) / 60), (int)(timeInicial % 60));

            // Set up the ImageList to display the play button image
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(40, 40);
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
            // Get the current value of the trackbar (the velocity level selected by the user)
            int velocity = trackBar1.Value;
            switch (velocity)
            {
                case 0:
                case 1:
                    timer1.Interval = 1000;
                    Velocity_label_bar.Text = "Sim. Speed x1";
                    break;

                case 2:
                case 3:
                    timer1.Interval = 500;
                    Velocity_label_bar.Text = "Sim. Speed x2";
                    break;

                case 4:
                case 5:
                    timer1.Interval = 250;
                    Velocity_label_bar.Text = "Sim. Speed x4";
                    break;

                case 6:
                case 7:
                    timer1.Interval = 200;
                    Velocity_label_bar.Text = "Sim. Speed x5";
                    break;

                case 8:
                case 9:
                    timer1.Interval = 100;
                    Velocity_label_bar.Text = "Sim. Speed x10";
                    break;

                case 10:
                    timer1.Interval = 10;
                    Velocity_label_bar.Text = "Sim. Speed x100";
                    break;
            }
        }
    }
}
