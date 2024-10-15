using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsterixForms
{
    public partial class DataGridView : Form
    {
        string[] lista = { "Data Source Id", "Time of the day", "Target Report", "Position Polar Coordinates", "Mode 3A", "Flight Level", "Radar Plot Char", "Aircraft Add", "Aircraft ID", "Mode S MB Data", "Track Num", "Position Cartesian Coordinates", "Track Velocity Polar", "Track Sattus", "Height 3D Radar", "Communications ACAS" };
        int index = 0;
        public DataGridView(string msg)
        {
            InitializeComponent();
            MessageBox.Show(msg);
        }

        private void InitializeComponent()
        {
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            ((ISupportInitialize)this.dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new Point(12, 12);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new Size(869, 188);
            this.dataGridView2.TabIndex = 0;
            // 
            // DataGridView
            // 
            ClientSize = new Size(893, 253);
            Controls.Add(this.dataGridView2);
           
            Name = "DataGridView";
            ((ISupportInitialize)this.dataGridView2).EndInit();
            ResumeLayout(false);
        }
        private void CrearDataGridView()
        {
            if (dataGridView2 != null)
            {
                foreach (var nombreColumna in lista)
                {
                    // Verificar si la columna ya existe
                    if (!dataGridView2.Columns.Contains(nombreColumna))
                    {
                        dataGridView2.Columns.Add(nombreColumna, nombreColumna);
                    }
                }
            }
        }
        private void EscribirEnDataGridView(string mensaje)
        {
            // Dividir el mensaje en partes, suponiendo que están separados por comas
            string[] datos = mensaje.Split(';');

            // Verificar si hay suficientes columnas
            if (datos.Length > dataGridView2.Columns.Count)
            {
                throw new ArgumentException("El mensaje contiene más datos de los que hay columnas en el DataGridView.");
            }

            // Comprobar si la fila actual está llena
            bool filaLlena = true;
            for (int j = 0; j < dataGridView2.Columns.Count; j++)
            {
                if (dataGridView2.Rows[index].Cells[j].Value == null)
                {
                    filaLlena = false;
                    break;
                }
            }

            // Si la fila está llena, incrementar el índice y agregar una nueva fila
            if (filaLlena)
            {
                index++;
                dataGridView2.Rows.Add();
            }

            // Asegurarse de que no excedamos el número de columnas
            for (int i = 0; i < datos.Length; i++)
            {
                dataGridView2.Rows[index].Cells[i].Value = datos[i].Trim(); // Trim para quitar espacios
            }
        }
    }
}
