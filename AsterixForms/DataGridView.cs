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
        string[] lista = { "NUM", "SAC", "SIC", "Time", "Latitud", "Longitud", "h", "TYP020", "SIM020", "RDP020","SPI020", "RAB020", "TST020", "ERR020", "XPP020", "ME020", "MI020", "FOEFRI_020", "RHO",
            "THETA", "V070", "G070", "Mode_3A", "V090", "G090", "Flight_Level", "ModeC_corrected","SRL130", "SRR130", "SAM130", "PRL130", "PAM130", "RPD130", "APD130", "Target_Address",
            "Target_ID", "Mode_S", "MCP_Status", "MCP_ALT", "FMS_Status", "FMS_ALT", "BP_Status","BP", "MODE_Status", "VNAV", "ALT_HOLD", "APP", "TARGETALT_Status", "TARGETALT_Source",
            "RS_Status", "RA", "TTA_Status", "TTA", "GSS_Status", "GS", "TAR_Status", "TAR", "TAS_Status", "TAS", "HDG_Status", "HDG", "IAS_Status", "IAS", "MACH_Status", "MACH",
            "BAR_Status", "BAR", "IVV_Status", "IVV", "Track_number", "X_component", "Y_component", "Ground_SpeedKT", "Heading", "CNF170", "RAD170", "DOU170", "MAH170", "CDM170",
            "TRE170", "GHO170", "SUP170", "TCC170", "Measured_Height", "COM230", "STAT230", "SI230", "MSCC230", "ARC230", "AIC230", "B1A230", "B1B230"
        };
        string[] frutas = new string[]
{
    "Manzana",
    "Plátano",
    "Naranja",
    "Fresa",
    "Uva",
    "Pera",
    "Piña",
    "Kiwi",
    "Mango",
    "Sandía",
    "Melón",
    "Durazno",
    "Cereza",
    "Frambuesa",
    "Limón",
    "Papaya",
    "Arándano",
    "Granada",
    "Maracuyá",
    "Lichi",
    "Tamarindo",
    "Mandarina",
    "Cajú",
    "Higo",
    "Coco",
    "Pomelo",
    "Guayaba",
    "Carambola",
    "Aceituna",
    "Pitahaya",
    "Aguacate",
    "Dátil",
    "Fruta de la pasión",
    "Níspero",
    "Almendra",
    "Clementina",
    "Mora",
    "Pera de agua",
    "Fruta estrella",
    "Castaña",
    "Ciruela",
    "Baya de goji",
    "Mamey",
    "Kumquat",
    "Chirimoya",
    "Bergamota",
    "Pomelo rosa",
    "Jugo de fruta",
    "Cereza ácida",
    "Nectarina",
    "Berenjena",
    "Tamarillo",
    "Pepino dulce",
    "Arándano rojo",
    "Tuna",
    "Ciruelo",
    "Coco joven",
    "Naranja sanguina",
    "Fruta del dragón",
    "Hawái",
    "Cactus",
    "Albaricoque",
    "Fruta de la madera",
    "Cereza dulce",
    "Nuez moscada",
    "Cranberry",
    "Cilantro",
    "Acerola",
    "Mandarino",
    "Cítricos",
    "Tamarillo",
    "Chabacano",
    "Fruto del monje",
    "Cilantro",
    "Pepino",
    "Limón amarillo",
    "Chili",
    "Guinda",
    "Fruta de hortaliza",
    "Papaya verde",
    "Pera espinosa",
    "Kiwi dorado",
    "Nuez de Brasil",
    "Cereza negra",
    "Guayabo",
    "Aceituna negra",
    "Pomelo blanco",
    "Fruta silvestre",
    "Higo chumbo",
    "Limón persa",
    "Cruz de Malta",
    "Pera roja",
    "Almendra amarga",
    "Ciruela japonesa",
    "Moras negras",
    "Arándano azul",
    "Fruto del dragón",
    "Aceituna verde"
};
        string[] coches = {
        "Toyota Corolla", "Ford Fiesta", "Volkswagen Golf", "Honda Civic", "BMW Serie 3",
        "Audi A4", "Mercedes-Benz Clase C", "Nissan Qashqai", "Peugeot 208", "Kia Sportage",
        "Renault Clio", "Opel Astra", "Mazda 3", "Subaru Impreza", "Hyundai i30",
        "Fiat 500", "Chevrolet Spark", "Citroën C3", "Dacia Sandero", "Mitsubishi ASX",
        "Tesla Model 3", "Volvo V40", "Land Rover Discovery", "Jaguar XE", "Porsche Macan",
        "Mini Cooper", "Lexus IS", "Infiniti Q50", "Chrysler 300", "Dodge Charger",
        "Alfa Romeo Giulia", "Seat León", "Skoda Octavia", "Smart Fortwo", "Buick Encore",
        "Volkswagen Passat", "Ford Mustang", "Subaru Outback", "Kia Seltos", "Nissan Juke",
        "Toyota RAV4", "Honda CR-V", "Jeep Wrangler", "Hyundai Tucson", "Mazda CX-5",
        "Peugeot 3008", "Citroën C5 Aircross", "Renault Captur", "Opel Mokka", "BMW X1",
        "Audi Q3", "Mercedes-Benz GLA", "Volvo XC40", "Land Rover Range Rover Evoque",
        "Porsche Cayenne", "Lexus NX", "Infiniti QX50", "Dacia Duster", "Toyota Land Cruiser",
        "Nissan Navara", "Ford Ranger", "Chevrolet Colorado", "Ram 1500", "Mitsubishi L200",
        "Honda HR-V", "Hyundai Kona", "Kia Niro", "Volkswagen Tiguan", "Seat Ateca",
        "Skoda Karoq", "Jeep Compass", "Subaru XV", "Mazda CX-30", "Mini Countryman",
        "Tesla Model Y", "Buick Enclave", "Volvo XC60", "Jaguar F-Pace", "Alfa Romeo Stelvio"
};
        string[] lugaresCatalunya = {
        "Barcelona", "Girona", "Tarragona", "El Masnou", "Sitges", "Montserrat", "Costa Brava",
        "Salou", "Figueres", "Tarragona", "Reus", "Badalona", "Sabadell", "Terrassa",
        "Mataró", "Manresa", "Castelldefels", "Cerdanyola del Vallès", "Rubí", "Granollers",
        "Vic", "Sant Cugat del Vallès", "Tortosa", "Vilanova i la Geltrú", "Amposta",
        "Palafrugell", "Blanes", "Calella", "Lloret de Mar", "Argentona", "Sant Feliu de Guíxols",
        "Olot", "Berga", "Ripoll", "Girona", "Castelló d'Empúries", "L'Escala", "Cadaqués",
        "Figueres", "Tossa de Mar", "S'Agaró", "Platja d'Aro", "La Bisbal d'Empordà",
        "Cerdanyola del Vallès", "Torrelles de Llobregat", "L'Hospitalet de Llobregat",
        "Badia del Vallès", "Sant Adrià de Besòs", "Martorell", "Vilafranca del Penedès",
        "Gelida", "Sant Esteve Sesrovires", "Cabrera de Mar", "La Garriga", "Mollet del Vallès",
        "El Masnou", "Castellar del Vallès", "Cabrils", "Sant Quirze del Vallès",
        "Sant Joan Despí", "Esplugues de Llobregat", "Santa Coloma de Gramenet",
        "Sant Boi de Llobregat", "Llinars del Vallès", "Masquefa", "El Prat de Llobregat",
        "Canet de Mar", "Torre de Claramunt", "Torrelles de Llobregat", "Martorelles",
        "Sant Andreu de la Barca", "Corbera de Llobregat", "Palau-solità i Plegamans",
        "Castellbisbal", "Les Franqueses del Vallès", "Lliçà d'Amunt", "Lliçà de Vall",
        "La Pobla de Mafumet", "Roses", "Amposta", "Cambrils", "El Vendrell", "Cunit"
};
        int index = 0;
        public DataGridView(string msg)
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(DataGridView));
            dataGridView2 = new System.Windows.Forms.DataGridView();
            toolStrip1 = new ToolStrip();
            BtnFilter = new ToolStripButton();
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
            dataGridView2.Size = new Size(1658, 188);
            dataGridView2.TabIndex = 0;
            dataGridView2.CellContentClick += dataGridView2_CellContentClick;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { BtnFilter, BtnGoogleEarth });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1682, 27);
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
            ClientSize = new Size(1682, 253);
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

        /*### CREACIÖ DATAGRIDVIEW #######################################################################################################################################*/
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
        private void EscribirEnDataGridView(string[] datos)
        {
            dataGridView2.Rows.Add();

            // Asegurarse de que no excedamos el número de columnas
            for (int i = 0; i < 91; i++)
            {
                if (i < datos.Length)
                {
                    dataGridView2.Rows[index].Cells[i].Value = datos[i];//.Trim(); // Trim para quitar espacios
                }
            }
            ColorDataGridView(index);
            index++;
        }

        private void CrearToolStrip()
        {

        }
        private void ColorDataGridView(int index)
        {
            if (index % 2 != 0) { dataGridView2.Rows[index].DefaultCellStyle.BackColor = Color.LightCyan; }
            else { dataGridView2.Rows[index].DefaultCellStyle.BackColor = Color.LightBlue; }
        }
        /*### FILTER ##############################################*/
        void FilterDataGridView(string condition)
        {
            string[] aux = condition.Split(';').ToArray();

            switch (int.Parse(aux[1])-1)
            {
                case 0:
                    //NUM
                    NumFilterGrid(aux, 0);
                    break;
                case 1:
                    // SIC
                    NumFilterGrid(aux, 1);
                    break;
                case 2:
                    // SAC
                    NumFilterGrid(aux, 2);
                    break;
                case 3:
                    //Time
                    TimeFilterGrid(aux);
                    break;
                case 4:
                    // Latitud
                    NumFilterGrid(aux, 4);
                    break;
                case 5:
                    //Longitud
                    NumFilterGrid(aux, 5);
                    break;
                case 6:
                    // Altura
                    NumFilterGrid(aux, 6);
                    break;
                case 18:
                    // Rho
                    NumFilterGrid(aux, 18);
                    break;
                case 19:
                    // Theta
                    NumFilterGrid(aux, 19);
                    break;
                case 20:
                    NumFilterGrid(aux, 20);
                    break;
                case 21:
                    NumFilterGrid(aux, 21);
                    break;
                case 23:
                    NumFilterGrid(aux, 23);
                    break;
                case 24:
                    NumFilterGrid(aux, 24);
                    break;
            }


        }
        private void OpenFilter()
        {
            using (Filter FilterForm = new Filter()) { if (FilterForm.ShowDialog() == DialogResult.OK) { FilterDataGridView(FilterForm.cmd); } }
        }
        private void NumFilterGrid(string[] aux, int dataitem)
        {
            DataGridView NumFilterGridView = new DataGridView();
            NumFilterGridView.CrearDataGridView();

            if (aux[1].ToString().Equals("1")) { NumClone(NumFilterGridView, int.Parse(aux[2]), dataGridView2.Rows.Count,dataitem); }
            else if (aux[1].ToString().Equals("2")) { NumClone(NumFilterGridView, 0, int.Parse(aux[2]), dataitem); }
            else if (aux[1].ToString().Equals("3"))  { NumClone(NumFilterGridView, int.Parse(aux[2]), int.Parse(aux[3]), dataitem); }
            else { MessageBox.Show("[DataGridView] [NumFilterGrid] [ERROR]"); }
        }
        private void TimeFilterGrid(string[] aux)
        {
            DataGridView TimeFilterGridView = new DataGridView();
            TimeFilterGridView.CrearDataGridView();

            if (aux[1].ToString().Equals("1")) { TimeClone(TimeFilterGridView, TimeConverter(aux[2]), TimeConverter("23:59:59:999")); }
            else if (aux[1].ToString().Equals("2")) { TimeClone(TimeFilterGridView, TimeConverter("00:00:00:000"), TimeConverter(aux[2])); }
            else if (aux[1].ToString().Equals("3")) { TimeClone(TimeFilterGridView, TimeConverter(aux[2]), TimeConverter(aux[3])); }
            else { MessageBox.Show("[DataGridView] [TimeFilterGrid] [ERROR]"); }
        }
        private int TimeConverter(string timeString)
        {
            // Passes time data to seconds
            string[] aux = timeString.Split(':').ToArray();
            int time = int.Parse(aux[0])*3600 + int.Parse(aux[1])*60 + int.Parse(aux[2]) + int.Parse(aux[3])/1000;
            return time;
        }

        /*### CLONE FUNCTIONS #############################################*/
        private void NumClone(DataGridView dvg_out, int start, int end,  int dataitem) {
            foreach (DataGridViewRow fila in dataGridView2.Rows)
            {
                int numFila = int.Parse(fila.Cells[dataitem].Value.ToString());
                if (numFila >= start && numFila <= end)
                {
                    // Crear una nueva fila en el destino
                    DataGridViewRow auxFila = (DataGridViewRow)fila.Clone();

                    // Copiar los valores de la fila seleccionada
                    for (int i = 0; i < fila.Cells.Count; i++)
                    {
                        auxFila.Cells[i].Value = fila.Cells[i].Value;
                    }
                    // Agregar la nueva fila al DataGridView de destino
                    dvg_out.dataGridView2.Rows.Add(auxFila);
                }
            }
        }
        private void TimeClone(DataGridView dvg_out, int start, int end)
        {
            foreach (DataGridViewRow fila in dataGridView2.Rows)
            {
                int timeFila = TimeConverter(fila.Cells[3].Value.ToString());
                if (timeFila >= start && timeFila <= end)
                {
                    // Crear una nueva fila en el destino
                    DataGridViewRow auxFila = (DataGridViewRow)fila.Clone();

                    // Copiar los valores de la fila seleccionada
                    for (int i = 0; i < fila.Cells.Count; i++)
                    {
                        auxFila.Cells[i].Value = fila.Cells[i].Value;
                    }
                    // Agregar la nueva fila al DataGridView de destino
                    dvg_out.dataGridView2.Rows.Add(auxFila);
                }
            }
        }
        
        /*### EVENTOS #############################################*/
        private void DataGridView_Load(object sender, EventArgs e)
        {
            CrearToolStrip();
            CrearDataGridView();
            for (int i = 0; i < 5; i++)
            {
                EscribirEnDataGridView(frutas);
                EscribirEnDataGridView(coches);
                EscribirEnDataGridView(lugaresCatalunya);
            }
        }
        private void toolStripButton_Click(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;

            if (button != null)
            {
                switch (button.Text.ToString())
                {
                    case "Order":

                        break;
                    case "filtrar":
                        // Lógica para filtrar
                        break;
                    case "eliminar":
                        // Lógica para eliminar
                        break;
                    default:
                        break;
                }
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            OpenFilter();
        }
    }
}
