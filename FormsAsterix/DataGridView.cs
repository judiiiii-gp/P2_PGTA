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
            "SAC", "SIC", "Time of Day", "Latitud", "Longitud", "Height","TYP", "SIM", "RDP", "SPI", "RAB", "TST", "ERR", "XPP", "ME",
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
                    HeaderText = columnName
                };

                // Verificar si el nombre de la columna existe en el diccionario
                if (columnMapping.ContainsKey(columnName))
                {
                    column.DataPropertyName = columnMapping[columnName];
                }
                else
                {
                    column.DataPropertyName = string.Empty; // O manejar columnas no encontradas
                    column.HeaderText = columnName + " (No Data Property)";
                }

                dataGridView2.Columns.Add(column);
            }
        }


        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(DataGridView));
            dataGridView2 = new System.Windows.Forms.DataGridView();
            toolStrip1 = new ToolStrip();
            BtnFilter = new ToolStripButton();
            BtnSearch = new ToolStripButton();
            toolStripButton1 = new ToolStripButton();
            CSVFile = new ToolStripButton();
            ((ISupportInitialize)dataGridView2).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(0, 0);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(1484, 763);
            dataGridView2.TabIndex = 0;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { BtnFilter, BtnSearch, toolStripButton1, CSVFile });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1484, 27);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // BtnFilter
            // 
            BtnFilter.DisplayStyle = ToolStripItemDisplayStyle.Text;
            BtnFilter.Image = (Image)resources.GetObject("BtnFilter.Image");
            BtnFilter.ImageTransparentColor = Color.Magenta;
            BtnFilter.Name = "BtnFilter";
            BtnFilter.Size = new Size(46, 24);
            BtnFilter.Text = "Filter";
            BtnFilter.Click += BtnFilter_Click;
            // 
            // BtnSearch
            // 
            BtnSearch.DisplayStyle = ToolStripItemDisplayStyle.Text;
            BtnSearch.Image = (Image)resources.GetObject("BtnSearch.Image");
            BtnSearch.ImageTransparentColor = Color.Magenta;
            BtnSearch.Name = "BtnSearch";
            BtnSearch.Size = new Size(57, 24);
            BtnSearch.Text = "Search";
            BtnSearch.Click += BtnSearch_Click;
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(100, 24);
            toolStripButton1.Text = "Google Earth";
            // 
            // CSVFile
            // 
            CSVFile.DisplayStyle = ToolStripItemDisplayStyle.Text;
            CSVFile.Image = (Image)resources.GetObject("CSVFile.Image");
            CSVFile.ImageTransparentColor = Color.Magenta;
            CSVFile.Name = "CSVFile";
            CSVFile.Size = new Size(66, 24);
            CSVFile.Text = "CSV File";
            // 
            // DataGridView
            // 
            AutoSize = true;
            ClientSize = new Size(1484, 763);
            Controls.Add(toolStrip1);
            Controls.Add(dataGridView2);
            Name = "DataGridView";
            Load += DataGridView_Load;
            ((ISupportInitialize)dataGridView2).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }


        /*### FUNCIONES DATAGRIDVIEW ##############################*/
        /*private void CrearDataGridView()
        {
            if (dataGridView2 != null)
            {
                foreach (var nombreColumna in lista)
                {
                    // Verificar si la columna ya existe
                    if (!dataGridView2.Columns.Contains(nombreColumna)) { dataGridView2.Columns.Add(nombreColumna, nombreColumna); }
                }
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.RebeccaPurple;
            }
        }
        private void ColorDataGridView(int index)
        {
            if (index % 2 != 0) { dataGridView2.Rows[index].DefaultCellStyle.BackColor = Color.LightCyan; }
            else { dataGridView2.Rows[index].DefaultCellStyle.BackColor = Color.LightBlue; }
        }
        private void EscribirEnDataGridView(string[] datos)
        {
            dataGridView2.Rows.Add();

            // Asegurarse de que no excedamos el número de columnas
            for (int i = 0; i < 91; i++)
            {
                if (i < datos.Length) { dataGridView2.Rows[index].Cells[i].Value = datos[i]; }
            }
            ColorDataGridView(index);
            index++;
        }*/
        /*### FILTER ##############################################*/
        void FilterDataGridView(string condition)
        {
            string[] aux = condition.Split(';').ToArray();
            if (int.Parse(aux[1]) == 3) { TimeFilterGrid(aux); } else { NumFilterGrid(aux); }
        }
        private void OpenFilter()
        {
            using (Filter FilterForm = new Filter()) { if (FilterForm.ShowDialog() == DialogResult.OK) { FilterDataGridView(FilterForm.cmd); } }
        }
        private void NumFilterGrid(string[] aux)
        {
            DataGridView NumFilterGridView = new DataGridView();
            //NumFilterGridView.CrearDataGridView();
            NumFilterGridView.dgv_index = 1;
            index = int.Parse(aux[1]);

            if (aux[0].ToString().Equals("1")) { NumClone(NumFilterGridView, float.Parse(aux[2]), dataGridView2.Rows.Count, index); }
            else if (aux[0].ToString().Equals("2")) { NumClone(NumFilterGridView, 0, float.Parse(aux[2]), index); }
            else if (aux[0].ToString().Equals("3")) { NumClone(NumFilterGridView, float.Parse(aux[2]), float.Parse(aux[3]), index); }
            else { MessageBox.Show("[DataGridView] [NumFilterGrid] [ERROR]"); }
        }
        private void TimeFilterGrid(string[] aux)
        {
            DataGridView TimeFilterGridView = new DataGridView();
            //TimeFilterGridView.CrearDataGridView();

            if (aux[0].ToString().Equals("1")) { TimeClone(TimeFilterGridView, TimeConverter(aux[2]), TimeConverter("23:59:59:999")); }
            else if (aux[0].ToString().Equals("2")) { TimeClone(TimeFilterGridView, TimeConverter("00:00:00:000"), TimeConverter(aux[2])); }
            else if (aux[0].ToString().Equals("3")) { TimeClone(TimeFilterGridView, TimeConverter(aux[2]), TimeConverter(aux[3])); }
            else { MessageBox.Show("[DataGridView] [TimeFilterGrid] [ERROR]"); }
        }
        private int TimeConverter(string timeString)
        {
            // Passes time data to seconds
            string[] aux = timeString.Split(':').ToArray();
            int time = int.Parse(aux[0]) * 3600 + int.Parse(aux[1]) * 60 + int.Parse(aux[2]) + int.Parse(aux[3]) / 1000;
            return time;
        }
        /*### CLONE FUNCTIONS ######################################*/
        private void NumClone(DataGridView dvg_out, float start, float end, int index)
        {
            MessageBox.Show(start.ToString() + "   " + end.ToString());
            // Copiar las columnas del dataGridView2 al SearchGridView
            foreach (DataGridViewColumn col in dataGridView2.Columns) { dvg_out.dataGridView2.Columns.Add((DataGridViewColumn)col.Clone()); }

            foreach (DataGridViewRow fila in dataGridView2.Rows)
            {
                float numFila = -999;

                // Verificar si la celda no es nula antes de intentar convertirla a un entero
                if (fila.Cells[index].Value != null && !string.IsNullOrEmpty(fila.Cells[index].Value.ToString()))
                {
                    // Intentar convertir el valor a un entero
                    float.TryParse(fila.Cells[index].Value.ToString(), out numFila);
                }

                if (numFila == -999) { break; }
                if (numFila >= start && numFila <= end)
                {
                    // Crear una nueva fila en el destino
                    DataGridViewRow auxFila = (DataGridViewRow)fila.Clone();
                    // Copiar los valores de la fila seleccionada
                    for (int i = 0; i < fila.Cells.Count; i++) { auxFila.Cells[i].Value = fila.Cells[i].Value; }
                    // Agregar la nueva fila al DataGridView de destino
                    dvg_out.dataGridView2.Rows.Add(auxFila);
                }
            }
            dvg_out.ShowDialog();
        }
        private void TimeClone(DataGridView dvg_out, int start, int end)
        {
            // Copiar las columnas del dataGridView2 al SearchGridView
            foreach (DataGridViewColumn col in dataGridView2.Columns) { dvg_out.dataGridView2.Columns.Add((DataGridViewColumn)col.Clone()); }

            foreach (DataGridViewRow fila in dataGridView2.Rows)
            {
                int timeFila = TimeConverter(fila.Cells[3].Value.ToString());
                if (timeFila >= start && timeFila <= end)
                {
                    // Crear una nueva fila en el destino
                    DataGridViewRow auxFila = (DataGridViewRow)fila.Clone();
                    // Copiar los valores de la fila seleccionada
                    for (int i = 0; i < fila.Cells.Count; i++) { auxFila.Cells[i].Value = fila.Cells[i].Value; }
                    // Agregar la nueva fila al DataGridView de destino
                    dvg_out.dataGridView2.Rows.Add(auxFila);
                }
            }
            dvg_out.ShowDialog();
        }
        /*### SEARCH FUNCTIONS ####################################*/
        void SearchDataGridView(string condition)
        {
            string[] aux = condition.Split(';').ToArray();
            int index = int.Parse(aux[1]);

            DataGridView SearchGridView = new DataGridView();
            SearchGridView.dgv_index = 1;
            // Copiar las columnas del dataGridView2 al SearchGridView
            foreach (DataGridViewColumn col in dataGridView2.Columns) { SearchGridView.dataGridView2.Columns.Add((DataGridViewColumn)col.Clone()); }

            foreach (DataGridViewRow fila in dataGridView2.Rows)
            {
                string SearchFila = fila.Cells[index].Value != null ? fila.Cells[index].Value.ToString() : string.Empty;
                if (SearchFila == aux[2])
                {
                    ;                    // Crear una nueva fila en el destino
                    DataGridViewRow auxFila = (DataGridViewRow)fila.Clone();
                    // Copiar los valores de la fila seleccionada
                    for (int i = 0; i < fila.Cells.Count; i++) { auxFila.Cells[i].Value = fila.Cells[i].Value; }
                    // Agregar la nueva fila al DataGridView de destino
                    SearchGridView.dataGridView2.Rows.Add(auxFila);
                }
            }
            SearchGridView.ShowDialog();
        }
        private void OpenSearch()
        {
            using (Search SearchForm = new Search()) { if (SearchForm.ShowDialog() == DialogResult.OK) { SearchDataGridView(SearchForm.cmd); } }
        }
        /*### LOAD FUNCTIONS ######################################*/
        //private void CargarMain(List<List<DataItem>> bloque)
        //{
        //    // Configura las cabeceras del DataGridView
        //    dataGridView2.Columns.Clear();  // Limpiar cualquier columna existente
        //    dataGridView2.ColumnHeadersVisible = true;

        //    dataGridView2.EnableHeadersVisualStyles = false;  
        //    dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkTurquoise; 
        //    dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold); 
        //    dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        //    dataGridView2.RowHeadersDefaultCellStyle.BackColor = Color.LightCyan;
        //    dataGridView2.RowHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
        //    dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            
        //    List<string> lista = new List<string> {
        //    "SAC", "SIC", "Time of Day", "Latitud", "Longitud", "Height","TYP", "SIM", "RDP", "SPI", "RAB", "TST", "ERR", "XPP", "ME",
        //    "MI", "FOE", "ADSBEP", "ADSBVAL", "SCNEP", "SCNVAL", "PAIEP", "PAIVAL", "RHO", "THETA", "Mode-3/A V", "Mode-3/A G",
        //    "Mode-3/A L", "Mode-3/A reply", "FL V", "FL G", "Flight level", "Modo C_corrected", "SRL", "SRR", "SAM", "PRL", "PAM", "RPD", "APD",
        //    "Aircraft address", "Aircraft Identification", "MCPU/FCU Selected altitude", "FMS Selected Altitude", "Barometric pressure setting",
        //    "Mode status", "VNAV", "ALTHOLD", "Approach", "Target status", "Target altitude source", "Roll angle", "True track angle",
        //    "Ground Speed", "Track angle rate", "True Airspeed", "Magnetic heading", "Indicated airspeed", "Mach", "Barometric altitude rate",
        //    "Inertial Vertical Velocity", "Track Number", "X-Cartesian", "Y-Cartesian", "Calculated groundspeed", "Calculated heading",
        //    "CNF", "RAD", "DOU", "MAH", "CDM", "TRE", "GHO", "SUP", "TCC", "Height Measured by a 3D Radar", "COM", "STATUS",
        //    "SI", "MSSC", "ARC", "AIC", "B1A_message", "B1B_message"};

        //    foreach (var nombreColumna in lista)
        //    {
        //        dataGridView2.Columns.Add(nombreColumna, nombreColumna);
        //    }

        //    int NumLinea = 1;


        //    foreach (var data in bloque)
        //    {
        //        List<string> atributosDI = new List<string>();
        //        string correct_Alt = string.Empty;

        //        foreach (DataItem item in data)
        //        {
        //            string atributos = item.ObtenerAtributos();
        //            atributosDI.AddRange(atributos.Split(';'));

        //        }

        //        int rowIndex = dataGridView2.Rows.Add();

        //        dataGridView2.Rows[rowIndex].HeaderCell.Value = NumLinea.ToString();

        //        int columna = 0; // Empieza desde la primera columna

        //        foreach (string atribut in atributosDI)
        //        {
        //            if (!string.IsNullOrEmpty(atribut))
        //            {
        //                if (columna < dataGridView2.Columns.Count)
        //                {
        //                    dataGridView2.Rows[rowIndex].Cells[columna].Value = atribut;
        //                    columna++;
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }
        //        }
                        
              

        //        NumLinea++;
        //    }
        //}


        /*### EVENTS FUNCTION #####################################*/
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
            filterEnabled = !filterEnabled;
            dataGridView2.Refresh();

            if (filterEnabled)
            {
                AddArrowToColumnHeaders(true);
                dataGridView2.ColumnHeaderMouseClick += DataGridView2_ColumnHeaderMouseClick;
                dataGridView2.Refresh();
            }
            else
            {
                AddArrowToColumnHeaders(false);
                dataGridView2.ColumnHeaderMouseClick -= DataGridView2_ColumnHeaderMouseClick;
                bindingSource.RemoveFilter();
                RemoveFilterBoxes();
            }
        }

        private void DataGridView2_ColumnHeaderMouseClick (object sender, DataGridViewCellMouseEventArgs e)
        {
            if (filterEnabled)
            {
                string columna = dataGridView2.Columns[e.ColumnIndex].DataPropertyName;
                ShowFilterBox(columna, e.ColumnIndex);
            }
        }

        private void ShowFilterBox(string columna, int index)
        {
            string propertyName = dataGridView2.Columns[index].DataPropertyName;
            var uniques =asterixGrids.Select(x => x.GetType().GetProperty(propertyName)?.GetValue(x)?.ToString())
                                       .Distinct()
                                       .Where(val => val != null)
                                       .ToList();
            ComboBox filterBox = new ComboBox
            {
                DataSource = uniques,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = dataGridView2.Columns[index].Width
            };

            filterBox.SelectedIndexChanged += (s, e) =>
            {
                string SelectedValue = filterBox.SelectedItem.ToString();
                ApplyFilter(index, SelectedValue, columna);
                filterBox.Dispose();
            };

            Rectangle headerRect = dataGridView2.GetCellDisplayRectangle(index, -1, true);
            filterBox.Location = new Point(headerRect.X, headerRect.Y);
            dataGridView2.Controls.Add(filterBox);
            filterBox.BringToFront();
            filterBox.DroppedDown = true;

        }

        public void ApplyFilter (int index, string value, string columna)
        {
            // Aplica el filtro
            string propertyName = dataGridView2.Columns[index].DataPropertyName;
            string filterString = $"{propertyName} = '{value.Replace("'", "''")}'";


            // Aplica el filtro en el BindingSource
            bindingSource.Filter = filterString;

            // Crear un nuevo BindingSource con los datos filtrados
            var filteredData = asterixGrids.Where(x =>
            {
                var propertyValue = x.GetType().GetProperty(columna)?.GetValue(x)?.ToString().Trim().ToLower();
                return propertyValue == value.Trim().ToLower();
            }).ToList();
            if (filteredData.Count == 0)
            {
                MessageBox.Show("No se encontraron datos que coincidan con el filtro.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;  // Si no hay datos filtrados, no crees el nuevo DataGridView
            }
            BindingSource newBindingSource = new BindingSource();
            newBindingSource.DataSource = filteredData;

            // Crear un nuevo DataGridView y asignarle los datos filtrados
            DataGridFiltrado newDataGridView = new DataGridFiltrado(newBindingSource);
            newDataGridView.Show();

          

            this.Hide();

            //// Reemplazar el DataGridView actual con el nuevo DataGridView (si es necesario)
            //Controls.Remove(dataGridView2);  // Elimina el DataGridView actual
            //Controls.Add(newDataGridView);   // Añade el nuevo DataGridView
            //newDataGridView.Dock = DockStyle.Fill;
        }
        private void RemoveFilterBoxes()
        {
            foreach (Control control in dataGridView2.Controls)
            {
                if (control is ComboBox)
                {
                    control.Dispose();
                }
            }
        }

        private void AddArrowToColumnHeaders(bool showArrows)
        {
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {

                // Añadir bordes alrededor de la flecha (cuadrado)
                column.HeaderCell.Style.Padding = new Padding(10); // Espaciado interno

                if (showArrows)
                {
                    // Añadir flechas al encabezado de la columna
                    column.HeaderCell.Value = $"{column.HeaderText} ⬇"; // Flecha hacia abajo
                }
                else
                {
                    // Eliminar las flechas si no se debe mostrar
                    column.HeaderCell.Value = column.HeaderText;
                }

                //// Añadir un borde alrededor del cuadro (cuadrado)
                //column.HeaderCell.Style.BorderColor = Color.DarkGray;
                //column.HeaderCell.Style.BorderWidth = 1;
                //column.HeaderCell.Style.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            }
        }



        private void BtnSearch_Click(object sender, EventArgs e)
        {
            OpenSearch();
        }










    }
}
