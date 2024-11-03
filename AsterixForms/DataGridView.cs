using AsterixLib;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic.Logging;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Xml.Linq;

namespace AsterixForms
{
    public partial class DataGridView : Form
    {
        string CarpetaBusqueda;
        Computer usr = new Computer();
        List<List<DataItem>> bloque = new List<List<DataItem>>(); //tindrem una llista separada pels diferents blocs
        private string FilePath;

        /*string[] lista = {
        "NUM", "SAC", "SIC", "Time", "Latitud", "Longitud", "h", "TYP020", "SIM020", "RDP020","SPI020", "RAB020", "TST020", "ERR020", "XPP020", "ME020", "MI020", "FOEFRI_020", "RHO","THETA", "V070", "G070", "Mode_3A", "V090", "G090",
        "Flight_Level", "ModeC_corrected","SRL130", "SRR130", "SAM130", "PRL130", "PAM130", "RPD130", "APD130", "Target_Address","Target_ID", "Mode_S", "MCP_Status", "MCP_ALT", "FMS_Status", "FMS_ALT", "BP_Status","BP", "MODE_Status",
        "VNAV", "ALT_HOLD", "APP", "TARGETALT_Status", "TARGETALT_Source","RS_Status", "RA", "TTA_Status", "TTA", "GSS_Status", "GS", "TAR_Status", "TAR", "TAS_Status", "TAS", "HDG_Status", "HDG", "IAS_Status", "IAS", "MACH_Status",
        "MACH","BAR_Status", "BAR", "IVV_Status", "IVV", "Track_number", "X_component", "Y_component", "Ground_SpeedKT", "Heading", "CNF170", "RAD170", "DOU170", "MAH170", "CDM170","TRE170", "GHO170", "SUP170", "TCC170",
        "Measured_Height", "COM230", "STAT230", "SI230", "MSCC230", "ARC230", "AIC230", "B1A230", "B1B230"
        };
        string[] frutas = {
        "Manzana","Plátano","Naranja","Fresa","Uva","Pera","Piña","Kiwi","Mango","Sandía","Melón","Durazno","Cereza","Frambuesa","Limón","Papaya","Arándano","Granada","Maracuyá","Lichi","Tamarindo","Mandarina","Cajú","Higo","Coco",
        "Pomelo","Guayaba","Carambola","Aceituna","Pitahaya","Aguacate","Dátil","Fruta de la pasión","Níspero","Almendra","Clementina","Mora","Pera de agua","Fruta estrella","Castaña","Ciruela","Baya de goji","Mamey","Kumquat",
        "Chirimoya","Bergamota","Pomelo rosa","Jugo de fruta","Cereza ácida","Nectarina","Berenjena","Tamarillo","Pepino dulce","Arándano rojo","Tuna","Ciruelo","Coco joven","Naranja sanguina","Fruta del dragón","Hawái","Cactus",
        "Albaricoque","Fruta de la madera","Cereza dulce","Nuez moscada","Cranberry","Cilantro","Acerola","Mandarino","Cítricos","Tamarillo","Chabacano","Fruto del monje","Cilantro","Pepino","Limón amarillo","Chili","Guinda",
        "Fruta de hortaliza","Papaya verde","Pera espinosa","Kiwi dorado","Nuez de Brasil","Cereza negra","Guayabo","Aceituna negra","Pomelo blanco","Fruta silvestre","Higo chumbo","Limón persa","Cruz de Malta","Pera roja",
        "Almendra amarga","Ciruela japonesa","Moras negras","Arándano azul","Fruto del dragón","Aceituna verde"
        };
        string[] coches = {
        "Toyota Corolla", "Ford Fiesta", "Volkswagen Golf", "Honda Civic", "BMW Serie 3","Audi A4", "Mercedes-Benz Clase C", "Nissan Qashqai", "Peugeot 208", "Kia Sportage","Renault Clio", "Opel Astra", "Mazda 3", "Subaru Impreza",
        "Hyundai i30","Fiat 500", "Chevrolet Spark", "Citroën C3", "Dacia Sandero", "Mitsubishi ASX","Tesla Model 3", "Volvo V40", "Land Rover Discovery", "Jaguar XE", "Porsche Macan","Mini Cooper", "Lexus IS", "Infiniti Q50",
        "Chrysler 300", "Dodge Charger","Alfa Romeo Giulia", "Seat León", "Skoda Octavia", "Smart Fortwo", "Buick Encore","Volkswagen Passat", "Ford Mustang", "Subaru Outback", "Kia Seltos", "Nissan Juke","Toyota RAV4", "Honda CR-V",
        "Jeep Wrangler", "Hyundai Tucson", "Mazda CX-5","Peugeot 3008", "Citroën C5 Aircross", "Renault Captur", "Opel Mokka", "BMW X1","Audi Q3", "Mercedes-Benz GLA", "Volvo XC40", "Land Rover Range Rover Evoque","Porsche Cayenne",
        "Lexus NX", "Infiniti QX50", "Dacia Duster", "Toyota Land Cruiser","Nissan Navara", "Ford Ranger", "Chevrolet Colorado", "Ram 1500", "Mitsubishi L200","Honda HR-V", "Hyundai Kona", "Kia Niro", "Volkswagen Tiguan", "Seat Ateca",
        "Skoda Karoq", "Jeep Compass", "Subaru XV", "Mazda CX-30", "Mini Countryman","Tesla Model Y", "Buick Enclave", "Volvo XC60", "Jaguar F-Pace", "Alfa Romeo Stelvio"
        };
        string[] lugaresCatalunya = {
        "El Masnou", "Girona", "Tarragona", "Barcelona", "Sitges", "Montserrat", "Costa Brava","Salou", "Figueres", "Tarragona", "Reus", "Badalona", "Sabadell", "Terrassa","Mataró", "Manresa", "Castelldefels", "Cerdanyola del Vallès",
        "Rubí", "Granollers","Vic", "Sant Cugat del Vallès", "Tortosa", "Vilanova i la Geltrú", "Amposta","Palafrugell", "Blanes", "Calella", "Lloret de Mar", "Argentona", "Sant Feliu de Guíxols","Olot", "Berga", "Ripoll", "Girona",
        "Castelló d'Empúries", "L'Escala", "Cadaqués","Figueres", "Tossa de Mar", "S'Agaró", "Platja d'Aro", "La Bisbal d'Empordà","Cerdanyola del Vallès", "Torrelles de Llobregat", "L'Hospitalet de Llobregat","Badia del Vallès",
        "Sant Adrià de Besòs", "Martorell", "Vilafranca del Penedès","Gelida", "Sant Esteve Sesrovires", "Cabrera de Mar", "La Garriga", "Mollet del Vallès","El Masnou", "Castellar del Vallès", "Cabrils", "Sant Quirze del Vallès",
        "Sant Joan Despí", "Esplugues de Llobregat", "Santa Coloma de Gramenet","Sant Boi de Llobregat", "Llinars del Vallès", "Masquefa", "El Prat de Llobregat","Canet de Mar", "Torre de Claramunt", "Torrelles de Llobregat",
        "Martorelles","Sant Andreu de la Barca", "Corbera de Llobregat", "Palau-solità i Plegamans","Castellbisbal", "Les Franqueses del Vallès", "Lliçà d'Amunt", "Lliçà de Vall","La Pobla de Mafumet", "Roses", "Amposta", "Cambrils",
        "El Vendrell", "Cunit"
         };
        string[] lista1 = {
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20","21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40",
        "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60","61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78",
        "79", "80","81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92"
        };

        string[] lista2 = {
        "93", "94", "95", "96", "97", "98", "99", "100", "101", "102", "103", "104", "105", "106", "107", "108", "109", "110","111", "112", "113", "114", "115", "116", "117", "118", "119", "120", "121", "122", "123", "124", "125", "126", "127",
        "128", "129", "130", "131", "132", "133", "134", "135", "136", "137", "138", "139", "140", "141", "142", "143", "144","145", "146", "147", "148", "149", "150", "151", "152", "153", "154", "155", "156", "157", "158", "159", "160", "161",
        "162", "163", "164", "165", "166", "167", "168", "169", "170", "171", "172", "173", "174", "175", "176", "177", "178","179", "180", "181", "182", "183", "184"
        };

        string[] lista3 = {
        "185", "186", "187", "188", "189", "190", "191", "192", "193", "194", "195", "196", "197", "198", "199", "200", "201","202", "203", "204", "205", "206", "207", "208", "209", "210", "211", "212", "213", "214", "215", "216", "217", "218",
        "219", "220", "221", "222", "223", "224", "225", "226", "227", "228", "229", "230", "231", "232", "233", "234", "235","236", "237", "238", "239", "240", "241", "242", "243", "244", "245", "246", "247", "248", "249", "250", "251", "252",
        "253", "254", "255", "256", "257", "258", "259", "260", "261", "262", "263", "264", "265", "266", "267", "268", "269",
        "270", "271", "272", "273", "274", "275", "276"
        };*/

        int index = 0;
        public int dgv_index { get; set; }
        public DataGridView(string FilePath)
        {
            InitializeComponent();
            this.FilePath = FilePath;
            ReadBinaryFile(FilePath);
            Corrected_Altitude(bloque);
            //MessageBox.Show(msg);
            dgv_index = 0;
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
            CSVFile.Click += CSVFile_Click;
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
        private void CargarMain(List<List<DataItem>> bloque)
        {
            // Configura las cabeceras del DataGridView
            dataGridView2.Columns.Clear();  // Limpiar cualquier columna existente
            dataGridView2.ColumnHeadersVisible = true;

            dataGridView2.EnableHeadersVisualStyles = false;  
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkTurquoise; 
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold); 
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView2.RowHeadersDefaultCellStyle.BackColor = Color.LightCyan;
            dataGridView2.RowHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            
            List<string> lista = new List<string> {
            "SAC", "SIC", "Time of Day", "TYP", "SIM", "RDP", "SPI", "RAB", "TST", "ERR", "XPP", "ME",
            "MI", "FOE", "ADSBEP", "ADSBVAL", "SCNEP", "SCNVAL", "PAIEP", "PAIVAL", "RHO", "THETA", "Mode-3/A V", "Mode-3/A G",
            "Mode-3/A L", "Mode-3/A reply", "FL V", "FL G", "Flight level", "Modo C_corrected", "SRL", "SRR", "SAM", "PRL", "PAM", "RPD", "APD",
            "Aircraft address", "Aircraft Identification", "MCPU/FCU Selected altitude", "FMS Selected Altitude", "Barometric pressure setting",
            "Mode status", "VNAV", "ALTHOLD", "Approach", "Target status", "Target altitude source", "Roll angle", "True track angle",
            "Ground Speed", "Track angle rate", "True Airspeed", "Magnetic heading", "Indicated airspeed", "Mach", "Barometric altitude rate",
            "Inertial Vertical Velocity", "Track Number", "X-Cartesian", "Y-Cartesian", "Calculated groundspeed", "Calculated heading",
            "CNF", "RAD", "DOU", "MAH", "CDM", "TRE", "GHO", "SUP", "TCC", "Height Measured by a 3D Radar", "COM", "STATUS",
            "SI", "MSSC", "ARC", "AIC", "B1A_message", "B1B_message"};

            foreach (var nombreColumna in lista)
            {
                dataGridView2.Columns.Add(nombreColumna, nombreColumna);
            }

            int NumLinea = 1;


            foreach (var data in bloque)
            {
                List<string> atributosDI = new List<string>();
                string correct_Alt = string.Empty;

                foreach (DataItem item in data)
                {
                    string atributos = item.ObtenerAtributos();
                    atributosDI.AddRange(atributos.Split(';'));

                }

                int rowIndex = dataGridView2.Rows.Add();

                dataGridView2.Rows[rowIndex].HeaderCell.Value = NumLinea.ToString();

                int columna = 0; // Empieza desde la primera columna

                foreach (string atribut in atributosDI)
                {
                    if (!string.IsNullOrEmpty(atribut))
                    {
                        if (columna < dataGridView2.Columns.Count)
                        {
                            dataGridView2.Rows[rowIndex].Cells[columna].Value = atribut;
                            columna++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                        
              

                NumLinea++;
            }
        }


        /*### EVENTS FUNCTION #####################################*/
        private void DataGridView_Load(object sender, EventArgs e)
        {
            //CrearDataGridView();
            if (dgv_index == 0) 
            {
                CargarMain(bloque);
            }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            OpenFilter();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            OpenSearch();
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

        //Funció per a escriure en el fitxer
        private void EscribirFichero(List<List<DataItem>> bloque, string nombreFichero)
        {
            int NumLinea = 1;
            DataItem.SetNombreFichero(nombreFichero); //En el moment en que es decideixi com es diu el ficher s'ha de posar allà
            string cabecera = "Num Linea;SAC;SIC;Time of Day;TYP;SIM;RDP;SPI;RAB;TST;ERR;XPP;ME;MI;FOE;ADSBEP;ADSBVAL;SCNEP;SCNVAL;PAIEP;PAIVAL;RHO;THETA;Mode-3/A V;Mode-3/A G;Mode-3/A L;Mode-3/A reply;FL V;FL G;Flight level;Mode C Corrected;SRL;SRR;SAM;PRL;PAM;RPD;APD;Aircraft address;Aircraft Identification;MCPU/FCU Selected altitude;FMS Selected Altitude;Barometric pressure setting;Mode status;VNAV;ALTHOLD;Approach;Target status;Target altitude source;Roll angle;True track angle;Ground Speed;Track angle rate;True Airspeed;Magnetic heading;Indicated airspeed;Mach;Barometric altitude rate;Inertial Vertical Velocity;Track Number;X-Cartesian;Y-Cartesian;Calculated groundspeed;Calculated heading;CNF;RAD;DOU;MAH;CDM;TRE;GHO;SUP;TCC;Height Measured by a 3D Radar;COM;STATUS;SI;MSSC;ARC;AIC;B1A_message;B1B_message";
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

        private void CSVFile_Click(object sender, EventArgs e)
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
    }
}
