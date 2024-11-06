namespace AsterixForms
{
    partial class DataGridFiltrado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataGridFiltrado));
            toolStrip1 = new ToolStrip();
            DataGrid_No_Filter = new ToolStripButton();
            toolStripButton1 = new ToolStripButton();
            Fitxer_CSV = new ToolStripButton();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { DataGrid_No_Filter, toolStripButton1, Fitxer_CSV });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(800, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // DataGrid_No_Filter
            // 
            DataGrid_No_Filter.DisplayStyle = ToolStripItemDisplayStyle.Image;
            DataGrid_No_Filter.Image = (Image)resources.GetObject("DataGrid_No_Filter.Image");
            DataGrid_No_Filter.ImageTransparentColor = Color.Magenta;
            DataGrid_No_Filter.Name = "DataGrid_No_Filter";
            DataGrid_No_Filter.Size = new Size(29, 24);
            DataGrid_No_Filter.Text = "Tornar al DataGrid sense filtrar";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(29, 24);
            toolStripButton1.Text = "toolStripButton1";
            // 
            // Fitxer_CSV
            // 
            Fitxer_CSV.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Fitxer_CSV.Image = (Image)resources.GetObject("Fitxer_CSV.Image");
            Fitxer_CSV.ImageTransparentColor = Color.Magenta;
            Fitxer_CSV.Name = "Fitxer_CSV";
            Fitxer_CSV.Size = new Size(29, 24);
            Fitxer_CSV.Text = "Fitxer CSV";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 30);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(800, 423);
            dataGridView1.TabIndex = 1;
            // 
            // DataGridFiltrado
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(toolStrip1);
            Name = "DataGridFiltrado";
            Text = "DataGridFiltrado";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton DataGrid_No_Filter;
        private ToolStripButton toolStripButton1;
        private ToolStripButton Fitxer_CSV;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}