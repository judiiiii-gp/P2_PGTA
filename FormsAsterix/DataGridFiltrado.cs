﻿using CsvHelper;
using LibAsterix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsAsterix
{
    public partial class DataGridFiltrado : Form
    {
        private List<AsterixGrid> asterixGrids; // Lista de datos filtrados

        private BindingSource bindingSource; // Fuente de datos dinámica


        // Constructor for DataGridFiltrado, which initializes the form and sets up the DataGridView
        public DataGridFiltrado(BindingSource bindingSource)
        {
            InitializeComponent();

            // Configure the appearance of the DataGridView headers
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkTurquoise;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            // Configure the appearance of the DataGridView row headers
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.LightCyan;
            dataGridView1.RowHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = bindingSource;

            // Guarda la referencia al BindingSource
            this.bindingSource = bindingSource;

            // Maximize the form window
            this.WindowState = FormWindowState.Maximized;

            // Additional DataGridView configuration
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            // Ensure that the DataGridView takes up all the available space in the form
            dataGridView1.Dock = DockStyle.Fill;
        }

        private void DataGrid_No_Filter_Click(object sender, EventArgs e)
        {
            // Close the current form when the button is clicked
            this.Close();
        }

        private void Fitxer_CSV_Click(object sender, EventArgs e)
        {
            // Open a SaveFileDialog to allow the user to specify the file path and name for the CSV
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.Title = "Seleccionar la ubicación y el nombre del fichero";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // Write the DataGridView data to the specified file
                    EscribirFicheroConCsvHelper(filePath);
                    MessageBox.Show("S'ha escrit el fitxer correctament");
                }
            }
        }

        //private void EscribirFichero(string filePath)
        //{
        //    // Build a CSV file from the DataGridView content
        //    StringBuilder csvfile = new StringBuilder();

        //    // Add column headers to the CSV
        //    for (int i = 0; i < dataGridView1.Columns.Count; i++)
        //    {
        //        csvfile.Append(dataGridView1.Columns[i].HeaderText);

        //        if (i < dataGridView1.Columns.Count - 1)
        //        {
        //            csvfile.Append(";"); // Add a semicolon delimiter between column headers
        //        }

        //    }
        //    csvfile.AppendLine();

        //    // Add rows to the CSV
        //    foreach (DataGridViewRow row in dataGridView1.Rows)
        //    {
        //        if (row.IsNewRow) continue;

        //        for (int i = 0; i < dataGridView1.Columns.Count; i++)
        //        {
        //            csvfile.Append(row.Cells[i].Value?.ToString());

        //            if (i < dataGridView1.Columns.Count - 1)
        //            {
        //                csvfile.Append(";"); // Add a semicolon delimiter between cell values
        //            }

        //        }
        //        csvfile.AppendLine();
        //    }

        //    // Write the CSV content to the specified file path
        //    File.WriteAllText(filePath, csvfile.ToString(), Encoding.UTF8);
        //}
        private void EscribirFicheroConCsvHelper(string filePath)
        {
            // Convert the BindingSource into a list of generic objects
            var dataList = bindingSource.List.Cast<object>().ToList();

            if (dataList == null || dataList.Count == 0)
            {
                MessageBox.Show("No data to export.");
                return;
            }

            using (var writer = new StreamWriter(filePath))
            {
                var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";" // Change the delimiter to a semicolon
                };

                using (var csv = new CsvWriter(writer, config))
                {
                    // Use reflection to get property names and write the header
                    var properties = dataList[0].GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        csv.WriteField(property.Name);
                    }
                    csv.NextRecord();

                    // Write each row dynamically
                    foreach (var item in dataList)
                    {
                        foreach (var property in properties)
                        {
                            var value = property.GetValue(item)?.ToString() ?? string.Empty;
                            csv.WriteField(value);
                        }
                        csv.NextRecord();
                    }
                }
            }
        }
    }
}
