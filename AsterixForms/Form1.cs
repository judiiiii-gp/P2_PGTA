using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.Logging;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using AsterixLib;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using System.Diagnostics;


namespace AsterixForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        //### EVENTS ####################################################################################################################
        private void Seleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Selecciona un archivo .ast";
            openFileDialog.Filter = "Todos los arxivos (*.ast*)|*ast*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string FilePath = openFileDialog.FileName;
                
                DataGridView formulari = new DataGridView(FilePath);
                formulari.Show(); // Al obtenir el fitxer obrirem el nou formulari

                this.Hide();
            }


        }


        private void Fichero_Click(object sender, EventArgs e)
        {
            //label1.Visible = true;
            
            //NombreFichero.Visible = true;


        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            //string fichero = NombreFichero.Text;
            //EscribirFichero(bloque, fichero);
            MessageBox.Show("S'ha escrit correctament el fitxer");
        }
    }
}
