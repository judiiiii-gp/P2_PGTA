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

namespace FormsAsterix
{
    public partial class DataGridFiltrado : Form
    {
        public DataGridFiltrado(BindingSource bindingSource)
        {
            InitializeComponent();

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkTurquoise;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.LightCyan;
            dataGridView1.RowHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = bindingSource;
            this.WindowState = FormWindowState.Maximized;
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            // Asegúrate de que el DataGridView ocupe todo el espacio disponible
            dataGridView1.Dock = DockStyle.Fill;
        }

        private void DataGrid_No_Filter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Fitxer_CSV_Click(object sender, EventArgs e)
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

            for (int i = 0; i< dataGridView1.Columns.Count; i++)
            {
                csvfile.Append(dataGridView1.Columns[i].HeaderText);

                if (i< dataGridView1.Columns.Count - 1)
                {
                    csvfile.Append(";");
                }

            }
            csvfile.AppendLine();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                for (int i=0; i<dataGridView1.Columns.Count; i++)
                {
                    csvfile.Append(row.Cells[i].Value?.ToString());

                    if (i< dataGridView1.Columns.Count - 1)
                    {
                        csvfile.Append(";");
                    }

                }
                csvfile.AppendLine();
            }

            File.WriteAllText(filePath, csvfile.ToString(), Encoding.UTF8);
        }
    }
}
