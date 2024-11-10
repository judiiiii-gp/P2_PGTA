using LibAsterix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Net;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Xml.Linq;
using MultiCAT6.Utils;
using System.Runtime.CompilerServices;
using ComboBox = System.Windows.Forms.ComboBox;
using Amazon.DirectoryService.Model;
using Button = System.Windows.Forms.Button;
using Amazon.DynamoDBv2.DocumentModel;
using System.Web.UI.WebControls;
using TrackBar = System.Windows.Forms.TrackBar;
using Label = System.Windows.Forms.Label;
using TextBox = System.Windows.Forms.TextBox;

namespace FormsAsterix
{
    public partial class DataGridView : Form
    {
        string CarpetaBusqueda;
        Computer usr = new Computer();
        List<List<DataItem>> bloque = new List<List<DataItem>>(); //tindrem una llista separada pels diferents blocs
        List<AsterixGrid> asterixGrids = new List<AsterixGrid>();
        BindingSource bindingSource = new BindingSource();
        bool filterEnabled = false;
        bool filterDisabled = false;
        private Dictionary<string, Dictionary_Info> originalColumnNames = new Dictionary<string, Dictionary_Info>();


        int index = 0;
        public int dgv_index { get; set; }
        public DataGridView(List<AsterixGrid> blok)
        {
            InitializeComponent();
            this.asterixGrids = blok;
            bindingSource.DataSource = asterixGrids;

            // FEM DATA GRID VIEW

            dataGridView2.ColumnHeadersVisible = true;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkTurquoise;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView2.RowHeadersDefaultCellStyle.BackColor = Color.LightCyan;
            dataGridView2.RowHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = bindingSource;
            



            List<string> lista = new List<string> {
            "Num","SAC", "SIC", "Time of Day", "Latitud", "Longitud", "Height","TYP", "SIM", "RDP", "SPI", "RAB", "TST", "ERR", "XPP", "ME",
            "MI", "FOE", "ADSBEP", "ADSBVAL", "SCNEP", "SCNVAL", "PAIEP", "PAIVAL", "RHO", "THETA", "Mode-3/A V", "Mode-3/A G",
            "Mode-3/A L", "Mode-3/A reply", "FL V", "FL G", "Flight level", "Modo C_corrected", "SRL", "SRR", "SAM", "PRL", "PAM", "RPD", "APD",
            "Aircraft address", "Aircraft Identification", "BDS4", "MCPU/FCU Selected altitude", "FMS Selected Altitude", "Barometric pressure setting",
             "VNAV", "ALTHOLD", "Approach", "Mode status","Target status", "Target altitude source","BDS_5_0", "Roll angle", "True track angle",
            "Track angle rate","True Airspeed","Ground Speed", "BDS_6_0","Magnetic heading", "Indicated airspeed", "Mach", "Barometric altitude rate",
            "Inertial Vertical Velocity", "Track Number", "X-Cartesian", "Y-Cartesian", "Calculated groundspeed", "Calculated heading",
            "CNF", "RAD", "DOU", "MAH", "CDM", "TRE", "GHO", "SUP", "TCC", "Height Measured by a 3D Radar", "COM", "STATUS",
            "SI", "MSSC", "ARC", "AIC", "B1A_message", "B1B_message"};



            SetColumnHeaders(lista);

            ////dataGridView2.DataSource = bindingSource;
            ////dataGridView2.AutoGenerateColumns = true;


            //foreach (var nombreColumna in lista)
            //{
            //    dataGridView2.Columns.Add(nombreColumna, nombreColumna);
            //}

            this.WindowState = FormWindowState.Maximized;
            //MessageBox.Show(msg);
            //dgv_index = 0;
        }
        private void SetColumnHeaders(List<string> lista)
        {
            Dictionary<string, string> columnMapping = new Dictionary<string, string>
            {
                { "Num", "Num" },
                     { "SAC", "SAC" },
                    { "SIC", "SIC" },
                    { "Time of Day", "Time" },
                    { "Latitud", "Latitude" },
                    { "Longitud", "Longitude" },
                    { "Height", "Height" },
                    { "TYP", "TYP" },
                    { "SIM", "SIM" },
                    { "RDP", "RDP" },
                    { "SPI", "SPI" },
                    { "RAB", "RAB" },
                    { "TST", "TST" },
                    { "ERR", "ERR" },
                    { "XPP", "XPP" },
                    { "ME", "ME" },
                    { "MI", "MI" },
                    { "FOE", "FOE" },
                    { "ADSBEP", "ADS_EP" },
                    { "ADSBVAL", "ADS_VAL" },
                    { "SCNEP", "SCN_EP" },
                    { "SCNVAL", "SCN_VAL" },
                    { "PAIEP", "PAI_EP" },
                    { "PAIVAL", "PAI_VAL" },
                    { "RHO", "Rho" },
                    { "THETA", "Theta" },
                    { "Mode-3/A V", "V_70" },
                    { "Mode-3/A G", "G_70" },
                    { "Mode-3/A L", "L_70" },
                    { "Mode-3/A reply", "Mode3_A_Reply" },
                    { "FL V", "V_90" },
                    { "FL G", "G_90" },
                    { "Flight level", "Flight_Level" },
                    { "Modo C_corrected", "Mode_C_Correction" },
                    { "SRL", "SRL" },
                    { "SRR", "SRR" },
                    { "SAM", "SAM" },
                    { "PRL", "PRL" },
                    { "PAM", "PAM" },
                    { "RPD", "RPD" },
                    { "APD", "APD" },
                    { "Aircraft address", "Aircraft_Address" },
                    { "Aircraft Identification", "Aircraft_Indentification" },
                    { "BDS4", "BDS_4_0" },
                    { "MCPU/FCU Selected altitude", "MCP_FCUtxt" },
                    { "FMS Selected Altitude", "FMStxt" },
                    { "Barometric pressure setting", "BARtxt" },
                    { "VNAV", "VNAVMODEtxt" },
                    { "ALTHOLD", "ALTHOLDtxt" },
                    { "Approach", "Approachtxt" },
                    { "Mode status", "Mode_stat_txt" },
                    { "Target status", "StatusTargAlt" },
                    { "Target altitude source", "TargetAltSourcetxt" },
                    { "BDS_5_0", "BDS_5_0" },
                    { "Roll angle", "Rolltxt" },
                    { "True track angle", "TrueTracktxt" },
                    { "Track angle rate", "TrackAngletxt" },
                    { "True Airspeed", "TrueAirspeedtxt" },
                    { "Ground Speed", "GroundSpeedtxt" },
                    { "BDS_6_0", "BDS_6_0" },
                    { "Magnetic heading", "MagHeadtxt" },
                    { "Indicated airspeed", "IndAirtxt" },
                    { "Mach", "MACHtxt" },
                    { "Barometric altitude rate", "BarAlttxt" },
                    { "Inertial Vertical Velocity", "InerVerttxt" },
                    { "Track Number", "Track_Number" },
                    { "X-Cartesian", "X_Component" },
                    { "Y-Cartesian", "Y_Component" },
                    { "Calculated groundspeed", "Ground_Speed" },
                    { "Calculated heading", "Heading" },
                    { "CNF", "CNF" },
                    { "RAD", "RAD" },
                    { "DOU", "DOU" },
                    { "MAH", "MAH" },
                    { "CDM", "CDM" },
                    { "TRE", "TRE" },
                    { "GHO", "GHO" },
                    { "SUP", "SUP" },
                    { "TCC", "TCC" },
                    { "Height Measured by a 3D Radar", "Height_3D" },
                    { "COM", "COM" },
                    { "STATUS", "STAT" },
                    { "SI", "SI" },
                    { "MSSC", "MSSC" },
                    { "ARC", "ARC" },
                    { "AIC", "AIC" },
                    { "B1A_message", "B1A" },
                    { "B1B_message", "B1B" }
            };
            dataGridView2.Columns.Clear();

            foreach (var columnName in lista)
            {
                var column = new DataGridViewTextBoxColumn
                {
                    HeaderText = columnName,
                    DataPropertyName = columnMapping.ContainsKey(columnName) ? columnMapping[columnName] : columnName
                };
                if (!originalColumnNames.ContainsKey(columnName))
                {
                    originalColumnNames[columnName] = new Dictionary_Info(columnName);
                   
                }

                dataGridView2.Columns.Add(column);
            }
        }


        private void InitializeComponent()
        {
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BtnFilter = new System.Windows.Forms.ToolStripButton();
            this.BtnSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.bot2 = new System.Windows.Forms.ToolStripButton();
            this.CSV_File = new System.Windows.Forms.ToolStripButton();
            this.bot5 = new System.Windows.Forms.ToolStripButton();
            this.Filtered_Values = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.No_ground_flights = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.blancos_puros = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(1484, 763);
            this.dataGridView2.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnFilter,
            this.BtnSearch,
            this.toolStripButton1,
            this.bot2,
            this.CSV_File,
            this.bot5,
            this.Filtered_Values,
            this.toolStripButton2,
            this.No_ground_flights,
            this.toolStripButton3,
            this.blancos_puros});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1484, 31);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BtnFilter
            // 
            this.BtnFilter.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BtnFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnFilter.Name = "BtnFilter";
            this.BtnFilter.Size = new System.Drawing.Size(46, 28);
            this.BtnFilter.Text = "Filter";
            this.BtnFilter.Click += new System.EventHandler(this.BtnFilter_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.BackColor = System.Drawing.Color.Transparent;
            this.BtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(29, 28);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(100, 28);
            this.toolStripButton1.Text = "Google Earth";
            // 
            // bot2
            // 
            this.bot2.BackColor = System.Drawing.Color.Transparent;
            this.bot2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bot2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bot2.Name = "bot2";
            this.bot2.Size = new System.Drawing.Size(29, 28);
            // 
            // CSV_File
            // 
            this.CSV_File.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.CSV_File.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CSV_File.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CSV_File.Name = "CSV_File";
            this.CSV_File.Size = new System.Drawing.Size(66, 28);
            this.CSV_File.Text = "CSV File";
            this.CSV_File.Click += new System.EventHandler(this.CSV_File_Click);
            // 
            // bot5
            // 
            this.bot5.BackColor = System.Drawing.Color.Transparent;
            this.bot5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bot5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bot5.Name = "bot5";
            this.bot5.Size = new System.Drawing.Size(29, 28);
            // 
            // Filtered_Values
            // 
            this.Filtered_Values.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Filtered_Values.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Filtered_Values.Name = "Filtered_Values";
            this.Filtered_Values.Size = new System.Drawing.Size(109, 28);
            this.Filtered_Values.Text = "Filtered Values";
            this.Filtered_Values.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Filtered_Values.ToolTipText = "Filtered Values";
            this.Filtered_Values.Visible = false;
            this.Filtered_Values.Click += new System.EventHandler(this.Filtered_Values_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.BackColor = System.Drawing.Color.Transparent;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(29, 28);
            // 
            // No_ground_flights
            // 
            this.No_ground_flights.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.No_ground_flights.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.No_ground_flights.Name = "No_ground_flights";
            this.No_ground_flights.Size = new System.Drawing.Size(193, 28);
            this.No_ground_flights.Text = "Eliminate on ground flights";
            this.No_ground_flights.Visible = false;
            this.No_ground_flights.Click += new System.EventHandler(this.No_ground_flights_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.BackColor = System.Drawing.Color.Transparent;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(29, 28);
            // 
            // blancos_puros
            // 
            this.blancos_puros.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.blancos_puros.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.blancos_puros.Name = "blancos_puros";
            this.blancos_puros.Size = new System.Drawing.Size(163, 28);
            this.blancos_puros.Text = "Eliminar blancos puros";
            this.blancos_puros.Visible = false;
            this.blancos_puros.Click += new System.EventHandler(this.blancos_puros_Click);
            // 
            // DataGridView
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1484, 763);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView2);
            this.Name = "DataGridView";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        
        private void DataGridView_Load(object sender, EventArgs e)
        {
            //CrearDataGridView();
            if (dgv_index == 0) 
            {
                //CargarMain(bloque);
            }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            Filtered_Values.Visible = true;
            No_ground_flights.Visible = true;
            blancos_puros.Visible = true;
            toolStrip1.BringToFront();
            filterEnabled = !filterEnabled;
            


            if (filterEnabled)
            {
                AddArrowToColumnHeaders(true);
                dataGridView2.ColumnHeaderMouseClick += DataGridView2_ColumnHeaderMouseClick;
                
            }
            else
            {
                AddArrowToColumnHeaders(false);
                ResetForm();
            }
   

            dataGridView2.Refresh();
        }
        private Dictionary<string, List<Control>> filterControls = new Dictionary<string, List<Control>>();
        private void DataGridView2_ColumnHeaderMouseClick (object sender, DataGridViewCellMouseEventArgs e)
        {
            if (filterEnabled)
            {
                filterDisabled = !filterDisabled;
                string columna = dataGridView2.Columns[e.ColumnIndex].DataPropertyName;
                if (filterDisabled)
                {
                    ShowFilterBox(columna, e.ColumnIndex);
                }
                else
                {
                    // Si el filtro no está visible, lo mostramos
                    RemoveFilterControls(columna);
                }

            }

        }
        private void RemoveFilterControls(string columna)
        {
            if (filterControls.ContainsKey(columna))
            {
                // Obtener la lista de controles asociados a la columna
                var controlsToRemove = filterControls[columna];

                // Eliminar y disponer todos los controles de la lista
                foreach (var control in controlsToRemove)
                {
                    dataGridView2.Controls.Remove(control);
                    control.Dispose();
                }

                // Eliminar la entrada del diccionario
                filterControls.Remove(columna);
            }
        }
        private IEnumerable<object> GetDistinctValues(DataGridViewColumn column)
        {
            return dataGridView1.Rows.Cast<DataGridViewRow>()
                .Select(row => row.Cells[column.Name].Value)
                .Distinct();
        }

        private Dictionary<string, object> selectedFilters = new Dictionary<string, object>();
        private Dictionary<string, Tuple<int, int>> rangoSeleccionado = new Dictionary<string, Tuple<int, int>>();
        private void ShowFilterBox(string columna, int index)
        {
            RemoveFilterControls(columna);
            string propertyName = dataGridView2.Columns[index].DataPropertyName;
            var uniques = asterixGrids.Select(x => x.GetType().GetProperty(propertyName)?.GetValue(x)?.ToString())
                                       .Distinct()
                                       .Where(val => val != null)
                                       .ToList();
            Rectangle headerRect = dataGridView2.GetCellDisplayRectangle(index, -1, true);
            var specificColumns = new List<string> { "Latitude", "Longitude", "Height", "Flight_Level" };
            List<Control> controlsToAdd = new List<Control>();
            // Si hay más de 10 valores únicos, mostrar un control para seleccionar un rango
            if (specificColumns.Contains(columna))
            {
                // Si la columna es numérica, mostramos un rango numérico, sino, pedimos un rango de texto
                if (uniques.All(val => double.TryParse(val, out _))) // Comprobamos si todos son números
                {
                    // Determinar el valor mínimo y máximo para configurar los rangos
                    var minValue = uniques.Min(val => Convert.ToDouble(val));
                    var maxValue = uniques.Max(val => Convert.ToDouble(val));

                    // Factor de escala para convertir double a entero
                    double range = maxValue - minValue;
                    double scaleFactor = range < 10 ? 1000 : 100;

                    // Normalizar los valores min y max para que sean enteros
                    int minTrackBarValue = (int)(minValue * scaleFactor);
                    int maxTrackBarValue = (int)(maxValue * scaleFactor);

                    Label minLabel = new Label
                    {
                        Text = "Mínimo:",
                        Location = new Point(headerRect.X, headerRect.Bottom + 10),
                        Width = 60
                    };

                    Label maxLabel = new Label
                    {
                        Text = "Máximo:",
                        Location = new Point(headerRect.X, headerRect.Bottom + 50),
                        Width = 60
                    };

                    Label minValueLabel = new Label
                    {
                        Text = minValue.ToString(),
                        Location = new Point(headerRect.X + 70, headerRect.Bottom + 10),
                        Width = 80
                    };

                    Label maxValueLabel = new Label
                    {
                        Text = maxValue.ToString(),
                        Location = new Point(headerRect.X + 70, headerRect.Bottom + 50),
                        Width = 80
                    };

                    TrackBar minTrackBar = new TrackBar
                    {
                        Minimum = minTrackBarValue,
                        Maximum = maxTrackBarValue,
                        Value = minTrackBarValue,
                        Location = new Point(headerRect.X + 150, headerRect.Bottom + 10),
                        Width = 200
                    };

                    TrackBar maxTrackBar = new TrackBar
                    {
                        Minimum = minTrackBarValue,
                        Maximum = maxTrackBarValue,
                        Value = maxTrackBarValue,
                        Location = new Point(headerRect.X + 150, headerRect.Bottom + 50),
                        Width = 200
                    };

                    // Actualizar los valores cuando los TrackBar cambian
                    minTrackBar.Scroll += (s, e) =>
                    {
                        // Convertir el valor del TrackBar de vuelta a double
                        double currentMinValue = minTrackBar.Value / scaleFactor;
                        minValueLabel.Text = currentMinValue.ToString("F2"); // Muestra el valor con 2 decimales
                    };

                    maxTrackBar.Scroll += (s, e) =>
                    {
                        // Convertir el valor del TrackBar de vuelta a double
                        double currentMaxValue = maxTrackBar.Value / scaleFactor;
                        maxValueLabel.Text = currentMaxValue.ToString("F2"); // Muestra el valor con 2 decimales
                    };

                    Button acceptButton = new Button()
                    {
                        Text = "Aceptar",
                        Height = 30,
                        Location = new Point(headerRect.X, maxTrackBar.Bottom + 10),
                        FlatStyle = FlatStyle.Flat
                    };

                    acceptButton.FlatAppearance.BorderSize = 3; // Ajusta el grosor del borde
                    acceptButton.FlatAppearance.BorderColor = Color.Black;

                    acceptButton.Click += (s, e) =>
                    {
                        double selectedMin = minTrackBar.Value / scaleFactor;
                        double selectedMax = maxTrackBar.Value / scaleFactor;
                        // Aplicar el filtro con los valores seleccionados
                        selectedFilters[columna] = new List<double> { selectedMin, selectedMax };

                        acceptButton.Dispose();
                        RemoveFilterControls(columna);
                        dataGridView2.Controls.Remove(minLabel);
                        dataGridView2.Controls.Remove(maxLabel);
                        dataGridView2.Controls.Remove(minValueLabel);
                        dataGridView2.Controls.Remove(maxValueLabel);
                        dataGridView2.Controls.Remove(minTrackBar);
                        dataGridView2.Controls.Remove(maxTrackBar);
                        dataGridView2.Controls.Remove(acceptButton);

                    };

                    // Añadir los controles al DataGridView para que se muestren en pantalla
                    dataGridView2.Controls.Add(minLabel);
                    dataGridView2.Controls.Add(maxLabel);
                    dataGridView2.Controls.Add(minValueLabel);
                    dataGridView2.Controls.Add(maxValueLabel);
                    dataGridView2.Controls.Add(minTrackBar);
                    dataGridView2.Controls.Add(maxTrackBar);
                    dataGridView2.Controls.Add(acceptButton);

                    minLabel.BringToFront();
                    maxLabel.BringToFront();
                    minValueLabel.BringToFront();
                    maxValueLabel.BringToFront();
                    minTrackBar.BringToFront();
                    maxTrackBar.BringToFront();
                    acceptButton.BringToFront();

                    dataGridView2.Controls.AddRange(new Control[] { minLabel, maxLabel, minValueLabel, maxValueLabel, minTrackBar, maxTrackBar, acceptButton });
                    controlsToAdd.AddRange(new Control[] { minLabel, maxLabel, minValueLabel, maxValueLabel, minTrackBar, maxTrackBar, acceptButton });
                }
                else
                {
                    // Si son valores de texto, mostrar un rango de valores mediante TextBox
                    TextBox minValueTextBox = new TextBox
                    {
                        Width = 100,
                        Location = new Point(headerRect.X, headerRect.Bottom + 10)
                    };

                    TextBox maxValueTextBox = new TextBox
                    {
                        Width = 100,
                        Location = new Point(minValueTextBox.Right + 10, headerRect.Bottom + 10)
                    };

                    Button acceptButton = new Button
                    {
                        Text = "Aceptar",
                        Height = 30,
                        Location = new Point(maxValueTextBox.Right + 10, headerRect.Bottom + 10),
                        FlatStyle = FlatStyle.Flat
                    };

                    acceptButton.Click += (s, e) =>
                    {
                        // Aplicar filtro basado en el rango de texto
                        var minVal = minValueTextBox.Text;
                        var maxVal = maxValueTextBox.Text;

                        // Obtener valores dentro del rango de texto
                        var rangeValues = uniques.Where(val => string.Compare(val, minVal) >= 0 && string.Compare(val, maxVal) <= 0).ToList();

                        if (selectedFilters.ContainsKey(columna))
                        {
                            selectedFilters[columna] = rangeValues;
                        }
                        else
                        {
                            selectedFilters.Add(columna, rangeValues);
                        }

                        // Limpiar controles
                        minValueTextBox.Dispose();
                        maxValueTextBox.Dispose();
                        acceptButton.Dispose();
                    };

                    dataGridView2.Controls.Add(minValueTextBox);
                    dataGridView2.Controls.Add(maxValueTextBox);
                    dataGridView2.Controls.Add(acceptButton);
                    minValueTextBox.BringToFront();
                    maxValueTextBox.BringToFront();
                    acceptButton.BringToFront();


                }
            }
            else
            {
                // Mostrar el filtro con CheckedListBox para valores con menos de 10 elementos
                CheckedListBox filterBox = new CheckedListBox
                {
                    DataSource = uniques,
                    Width = dataGridView2.Columns[index].Width,
                    Height = 150,
                    IntegralHeight = true,
                    Location = new Point(headerRect.X, headerRect.Bottom)
                };

                filterBox.ItemCheck += (s, e) =>
                {
                    var timer = new Timer();
                    timer.Interval = 100;
                    timer.Tick += (sender, args) =>
                    {
                        var values = filterBox.CheckedItems.Cast<string>().ToList();

                        if (selectedFilters.ContainsKey(columna))
                        {
                            selectedFilters[columna] = values;
                        }
                        else
                        {
                            selectedFilters.Add(columna, values);
                        }

                        timer.Stop();
                    };
                    timer.Start();
                };

                Button acceptButton = new Button
                {
                    Text = "Aceptar",
                    Height = 30,
                    Location = new Point(headerRect.X, filterBox.Bottom + 5),
                    FlatStyle = FlatStyle.Flat
                };

                acceptButton.Click += (s, e) =>
                {
                    //ApplyFilter(index, filterBox.CheckedItems.Cast<string>().ToList(), columna);
                    filterBox.Dispose();
                    acceptButton.Dispose();
                };


                filterBox.Location = new Point(headerRect.X, headerRect.Bottom);
                dataGridView2.Controls.Add(filterBox);
                dataGridView2.Controls.Add(acceptButton);
                controlsToAdd.AddRange(new Control[] { filterBox, acceptButton });
                filterBox.BringToFront();
                acceptButton.BringToFront();
            }
            filterControls[columna] = controlsToAdd;

        }

        public void ApplyFilter ()
        {
            var filteredData = asterixGrids.Where(x =>
            {
                foreach (var filter in selectedFilters)
                {
                    var propertyValue = x.GetType().GetProperty(filter.Key)?.GetValue(x)?.ToString();
                    if (propertyValue == null) return false;

                    // Diferenciar el tipo de filtro
                    if (filter.Value is List<string> stringValues)
                    {
                        // Filtro para valores seleccionados (CheckedListBox)
                        if (!stringValues.Contains(propertyValue.Trim(), StringComparer.OrdinalIgnoreCase))
                        {
                            return false;
                        }
                    }
                    else if (filter.Value is List<double> numericRange)
                    {
                        if (double.TryParse(propertyValue, out double numericValue))
                        {
                            double minRange = numericRange[0];
                            double maxRange = numericRange[1];
                            if (numericValue < minRange || numericValue > maxRange)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return true;
            }).ToList();

            if (filteredData.Count == 0)
            {
                MessageBox.Show("No hay datos con los filtros");
                return;
            }

            // Aplica el filtro
            BindingSource newBindingSource = new BindingSource();
            newBindingSource.DataSource = filteredData;

            // Crear un nuevo DataGridView y asignarle los datos filtrados
            DataGridFiltrado newDataGridView = new DataGridFiltrado(newBindingSource);
            this.Hide();
            newDataGridView.ShowDialog();

            // Resetear el formulario
            ResetForm();
        }
        private void RemoveFilterBoxes()
        {
            foreach (Control control in dataGridView2.Controls)
            {
                if (control is CheckedListBox || control is Button)
                {
                    control.Dispose();
                }
            }
        }
     

        private void AddArrowToColumnHeaders(bool showArrows)
        {
            dataGridView2.ColumnHeadersVisible = true;

            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                string headerText = column.HeaderText;

                // Verificar si el headerText contiene la flecha y obtener el nombre sin flecha para la comparación
                string columnNameToCompare = headerText.EndsWith(" ⬇")
                    ? headerText.Substring(0, headerText.Length-2)  // Eliminar " ⬇"
                    : headerText;  // Mantener el nombre sin cambiar

                // Verificar si el nombre sin la flecha existe en el diccionario
                if (originalColumnNames.ContainsKey(columnNameToCompare))
                {
                    var columnInfo = originalColumnNames[columnNameToCompare];

                    // Si se deben mostrar las flechas, mostramos el nombre con flecha
                    if (showArrows)
                    {
                        column.HeaderCell.Value = columnInfo.NameWithArrow;  // Mostrar el nombre con flecha
                        //Debug.WriteLine($"Columna: {headerText}, Flecha mostrada: {columnInfo.NameWithArrow}");
                    }
                    else
                    {
                        column.HeaderCell.Value = columnInfo.OriginalName;  // Mostrar solo el nombre original
                        //Debug.WriteLine($"Columna: {headerText}, Flecha eliminada: {columnInfo.OriginalName}");
                    }
                }
                else
                {
                    // Si no existe en el diccionario, mantenemos el valor actual (sin cambios)
                    column.HeaderCell.Value = headerText;
                    //Debug.WriteLine($"Columna sin flecha: {headerText}");
                }
            }

            // Forzar la actualización de la vista
            dataGridView2.Refresh();
        }

        private void ResetForm()
        {
            dataGridView2.SuspendLayout();
            // Eliminar todos los eventos suscritos (para desactivar cualquier filtrado en acción)
            dataGridView2.ColumnHeaderMouseClick -= DataGridView2_ColumnHeaderMouseClick;

            // Eliminar cualquier filtro aplicado
            bindingSource.RemoveFilter();
            selectedFilters.Clear();

            // Restaurar los datos originales (esto asegura que no haya filtro aplicado)
            dataGridView2.DataSource = bindingSource;

            // Ocultar el botón de "Filtered_Values" y otros controles relacionados con el filtrado
            Filtered_Values.Visible = false;
            No_ground_flights.Visible = false;
            blancos_puros.Visible = false;

            // Eliminar cualquier CheckedListBox que pueda estar aún visible
            RemoveFilterBoxes();

            // Asegúrate de quitar las flechas de filtrado en las cabeceras de columna
            AddArrowToColumnHeaders(false);
            dataGridView2.ColumnHeadersVisible = true;
            dataGridView2.ResumeLayout();
            // Si necesitas mostrar el formulario de nuevo desde un estado "limpio":
            this.Show();
            
        }



        private void Filtered_Values_Click(object sender, EventArgs e)
        {
            ApplyFilter();
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

                    EscribirFichero(filePath);
                    MessageBox.Show("S'ha escrit el fitxer correctament");
                }
            }
        }
        private void EscribirFichero(string filePath)
        {
            StringBuilder csvfile = new StringBuilder();

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                csvfile.Append(dataGridView1.Columns[i].HeaderText);

                if (i < dataGridView1.Columns.Count - 1)
                {
                    csvfile.Append(";");
                }

            }
            csvfile.AppendLine();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    csvfile.Append(row.Cells[i].Value?.ToString());

                    if (i < dataGridView1.Columns.Count - 1)
                    {
                        csvfile.Append(";");
                    }

                }
                csvfile.AppendLine();
            }

            File.WriteAllText(filePath, csvfile.ToString(), Encoding.UTF8);
        }

        private void No_ground_flights_Click(object sender, EventArgs e)
        {
            RemoveRowsWithNAInFlightLevel();
        }
        private void RemoveRowsWithNAInFlightLevel()
        {
            // Crear una lista para almacenar las filas filtradas
            var filteredData = new List<object>(); // Reemplaza 'object' con el tipo de datos de tus filas

            // Recorremos las filas del DataGridView
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                var flightLevel = row.Cells[32].Value?.ToString();  // Asegúrate de que el nombre de la columna sea correcto

                // Si el valor no es "N/A", lo agregamos a la lista filtrada
                if (flightLevel != "N/A")
                {
                    filteredData.Add(row.DataBoundItem);  // Asumiendo que las filas están vinculadas a un origen de datos
                }
            }

            // Si no hay filas después de filtrar
            if (filteredData.Count == 0)
            {
                MessageBox.Show("No hay datos después de aplicar el filtro.");
                return;
            }
            BindingSource newBindingSource = new BindingSource();
            newBindingSource.DataSource = filteredData;

            // Crear un nuevo DataGridView y asignarle los datos filtrados
            DataGridFiltrado newDataGridView = new DataGridFiltrado(newBindingSource);
            this.Hide();
            newDataGridView.ShowDialog();

            // Resetear el formulario
            ResetForm();
            

        }
        private void RemoveRowsWithInvalidTYP()
        {
            // Filtrar las filas del DataGridView original según el valor de la columna TYP
            var filteredData = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                var typValue = row.Cells[7].Value?.ToString();  // Verifica que el nombre de la columna sea correcto

                // Filtra solo las filas que tengan TYP igual a "Mode S roll Call" o "Mde S + PSR"
                if (typValue == "Single ModeS All-Call" || typValue == "Single ModeS Roll-Call" || typValue == "ModeS All-Call + PSR" || typValue == "ModeS Roll-Call +PSR")
                {
                    filteredData.Add(row);  // Agregar a la lista de filas filtradas
                }
            }

            // Si hay datos filtrados, abre un nuevo formulario con el nuevo DataGridView
            if (filteredData.Count > 0)
            {
                OpenFilteredDataGridForm(filteredData);
            }
            else
            {
                MessageBox.Show("No hay datos que coincidan con el filtro.");
            }
        }

        private void OpenFilteredDataGridForm(List<DataGridViewRow> filteredData)
        {
            // Filtrar los datos en la lista original (asterixGrids) para obtener solo las filas que cumplen con el filtro
            DataTable filteredDataTable = new DataTable();

            // Asumir que las columnas del DataGridView son las mismas, añádelas al DataTable
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                filteredDataTable.Columns.Add(column.HeaderText);  // O usa column.Name si prefieres el nombre
            }

            // Agregar las filas filtradas al DataTable
            foreach (var row in filteredData)
            {
                DataRow dataRow = filteredDataTable.NewRow();
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    dataRow[i] = row.Cells[i].Value;  // Copiar los valores de cada celda
                }
                filteredDataTable.Rows.Add(dataRow);
            }


            BindingSource newBindingSource = new BindingSource();
            newBindingSource.DataSource = filteredDataTable;

            // Crear un nuevo DataGridView y asignarle los datos filtrados
            DataGridFiltrado newDataGridView = new DataGridFiltrado(newBindingSource);
            this.Hide();
            newDataGridView.ShowDialog();

            // Resetear el formulario
            ResetForm();
        }

        private void blancos_puros_Click(object sender, EventArgs e)
        {
            RemoveRowsWithInvalidTYP();
        }
    }
}
