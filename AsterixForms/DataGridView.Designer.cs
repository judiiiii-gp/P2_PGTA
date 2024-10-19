using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.Logging;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using AsterixLib;

namespace AsterixForms
{

    public partial class DataGridView : Form
    {
        string[] headers = new string[]
        {
            "NUM", "SAC", "SIC", "Time", "Latitud", "Longitud", "h", "TYP020", "SIM020", "RDP020","SPI020", "RAB020", "TST020", "ERR020", "XPP020", "ME020", "MI020", "FOEFRI_020", "RHO",
            "THETA", "V070", "G070", "Mode_3A", "V090", "G090", "Flight_Level", "ModeC_corrected","SRL130", "SRR130", "SAM130", "PRL130", "PAM130", "RPD130", "APD130", "Target_Address",
            "Target_ID", "Mode_S", "MCP_Status", "MCP_ALT", "FMS_Status", "FMS_ALT", "BP_Status","BP", "MODE_Status", "VNAV", "ALT_HOLD", "APP", "TARGETALT_Status", "TARGETALT_Source",
            "RS_Status", "RA", "TTA_Status", "TTA", "GSS_Status", "GS", "TAR_Status", "TAR", "TAS_Status", "TAS", "HDG_Status", "HDG", "IAS_Status", "IAS", "MACH_Status", "MACH", 
            "BAR_Status", "BAR", "IVV_Status", "IVV", "Track_number", "X_component", "Y_component", "Ground_SpeedKT", "Heading", "CNF170", "RAD170", "DOU170", "MAH170", "CDM170", 
            "TRE170", "GHO170", "SUP170", "TCC170", "Measured_Height", "COM230", "STAT230", "SI230", "MSCC230", "ARC230", "AIC230", "B1A230", "B1B230"
        };
        // falta fer que si es canvia de mida el form, s'amplii bé el formulari i es segueixi veient be (project requeriment que puja punts jeje)
        // falta posar relacio entre formularis, que aquest quan s'obri l'altre es tanqui
        //InformationList list; // passem una llista amb tota la info dels paquets?
        public DataGridView()
        {
            InitializeComponent();
        }

        //public void GetList(InformationList list)
        //{
            //this.list = list;
        //}

        private void DataGrid(object sender, EventArgs e)
        {
            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.RowCount = list.DameNum(); 
            dataGridView1.ColumnCount = headers.Length; // he agafat els mateixos que mostra el SW d'exemple del profe

            // Assignem les capaçaleres a cada columna
            for (int i = 0; i < headers.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = headers[i];
            }

            //for (int i = 0; i < list.DameNum(); i++) //Ponemos los valores de cada plan de vuelo en su casilla correspondiente de la tabla, y para ello debemos recorrer toda la lista
            //{
            //dataGridView1.Rows[i].Cells[0].Value = 1; // fer-ho per cada fila --> pensar com omplir

            //}
        }

        /* 
            jo faria funcio exportar CSV aquí --> canviar funcio escribir fichero
                - en decodificar afegir a una llista i llavors la buidem aqui a dins --> aquest es el proces que crec que ha de ser < 1 min
                - fer botó dins del form de taula que permeti exportar CSV --> exemple profe té dues funcions: sencer o columnes reduides (podriem fer que es pogues escollir quins parametres concrets es vol --> funcionalitats extres)
                - el del profe exporta també kml --> xulissim pq es pot obrir amb el google earth
                    - si mes no, cal pensar noves coses a poder afegir :) --> que donen punts extres jeje
        */

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private ToolStrip toolStrip1;
        private ToolStripButton BtnFilter;
        private ToolStripButton BtnGoogleEarth;
    }
}