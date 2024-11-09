using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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


    }
}
