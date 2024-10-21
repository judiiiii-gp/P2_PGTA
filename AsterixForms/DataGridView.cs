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

namespace AsterixForms
{
    public partial class DataGridView : Form
    {
        string[] lista = {
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
        };
        int index = 0;
        public int dgv_index { get; set; }
        public DataGridView(string msg)
        {
            InitializeComponent();
            MessageBox.Show(msg);
            dgv_index = 0;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(DataGridView));
            dataGridView2 = new System.Windows.Forms.DataGridView();
            toolStrip1 = new ToolStrip();
            BtnFilter = new ToolStripButton();
            BtnSearch = new ToolStripButton();
            BtnGoogleEarth = new ToolStripButton();
            ((ISupportInitialize)dataGridView2).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(12, 53);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(869, 188);
            dataGridView2.TabIndex = 0;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { BtnFilter, BtnSearch, BtnGoogleEarth });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(893, 27);
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
            // BtnGoogleEarth
            // 
            BtnGoogleEarth.DisplayStyle = ToolStripItemDisplayStyle.Text;
            BtnGoogleEarth.Image = (Image)resources.GetObject("BtnGoogleEarth.Image");
            BtnGoogleEarth.ImageTransparentColor = Color.Magenta;
            BtnGoogleEarth.Name = "BtnGoogleEarth";
            BtnGoogleEarth.Size = new Size(100, 24);
            BtnGoogleEarth.Text = "Google Earth";
            // 
            // DataGridView
            // 
            ClientSize = new Size(893, 253);
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
        private void CrearDataGridView()
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
        private void EscribirEnDataGridView(string [] datos)
        {
            dataGridView2.Rows.Add();

            // Asegurarse de que no excedamos el número de columnas
            for (int i = 0; i < 91; i++)
            {
                if (i < datos.Length) { dataGridView2.Rows[index].Cells[i].Value = datos[i]; }
            }
            ColorDataGridView(index);
            index++;
        }
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
            NumFilterGridView.CrearDataGridView();
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
            TimeFilterGridView.CrearDataGridView();

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
        private void CargarMain()
        {
            for (int i = 0; i < 5; i++) { EscribirEnDataGridView(lista1); EscribirEnDataGridView(lista2); EscribirEnDataGridView(lista3); }
        }
        /*### EVENTS FUNCTION #####################################*/
        private void DataGridView_Load(object sender, EventArgs e)
        {
            CrearDataGridView();
            if (dgv_index == 0) { CargarMain(); }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            OpenFilter();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            OpenSearch();
        }
    }
}
