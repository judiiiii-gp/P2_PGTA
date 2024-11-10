﻿using System.Drawing;
using System.Windows.Forms;

namespace FormsAsterix
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.Velocity_label_bar = new System.Windows.Forms.Label();
            this.Start_sim = new System.Windows.Forms.Button();
            this.timeTXT = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CSV_File = new System.Windows.Forms.ToolStripButton();
            this.GetKMLBut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.RestartSimBut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.NewDataBut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.ShowDataBut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TitolTXT = new System.Windows.Forms.Label();
            this.DescodBUT = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.gMapControl1);
            this.groupBox2.Controls.Add(this.Velocity_label_bar);
            this.groupBox2.Controls.Add(this.Start_sim);
            this.groupBox2.Controls.Add(this.timeTXT);
            this.groupBox2.Controls.Add(this.trackBar1);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(1434, 645);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Visible = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // gMapControl1
            // 
            this.gMapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gMapControl1.AutoSize = true;
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(349, 62);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(1073, 578);
            this.gMapControl1.TabIndex = 6;
            this.gMapControl1.Zoom = 0D;
            this.gMapControl1.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gMapControl1_OnMarkerClick);
            this.gMapControl1.Load += new System.EventHandler(this.gMapControl1_Load);
            // 
            // Velocity_label_bar
            // 
            this.Velocity_label_bar.AutoSize = true;
            this.Velocity_label_bar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Velocity_label_bar.Location = new System.Drawing.Point(103, 253);
            this.Velocity_label_bar.Name = "Velocity_label_bar";
            this.Velocity_label_bar.Size = new System.Drawing.Size(0, 22);
            this.Velocity_label_bar.TabIndex = 5;
            // 
            // Start_sim
            // 
            this.Start_sim.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_sim.Location = new System.Drawing.Point(79, 96);
            this.Start_sim.Name = "Start_sim";
            this.Start_sim.Size = new System.Drawing.Size(167, 45);
            this.Start_sim.TabIndex = 4;
            this.Start_sim.Text = "RUN";
            this.Start_sim.UseVisualStyleBackColor = true;
            this.Start_sim.Click += new System.EventHandler(this.Start_sim_Click);
            // 
            // timeTXT
            // 
            this.timeTXT.AutoSize = true;
            this.timeTXT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeTXT.Location = new System.Drawing.Point(114, 161);
            this.timeTXT.Name = "timeTXT";
            this.timeTXT.Size = new System.Drawing.Size(55, 28);
            this.timeTXT.TabIndex = 2;
            this.timeTXT.Text = "time";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(32, 196);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(277, 56);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GetKMLBut,
            this.toolStripButton3,
            this.RestartSimBut,
            this.toolStripButton4,
            this.NewDataBut,
            this.toolStripButton5,
            this.ShowDataBut,
            this.toolStripButton1,
            this.CSV_File});
            this.toolStrip1.Location = new System.Drawing.Point(3, 17);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1428, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // CSV_File
            // 
            this.CSV_File.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.CSV_File.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CSV_File.Name = "CSV_File";
            this.CSV_File.Size = new System.Drawing.Size(66, 36);
            this.CSV_File.Text = "CSV File";
            this.CSV_File.Click += new System.EventHandler(this.CSV_File_Click);
            // 
            // GetKMLBut
            // 
            this.GetKMLBut.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.GetKMLBut.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GetKMLBut.Image = global::FormsAsterix.Properties.Resources.how_to_draw_earth_for_kids;
            this.GetKMLBut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GetKMLBut.Name = "GetKMLBut";
            this.GetKMLBut.Size = new System.Drawing.Size(94, 36);
            this.GetKMLBut.Text = "Get KML";
            this.GetKMLBut.ToolTipText = "Get KML";
            this.GetKMLBut.Click += new System.EventHandler(this.GetKMLBut_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton3.Text = "   ";
            // 
            // RestartSimBut
            // 
            this.RestartSimBut.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.RestartSimBut.BackgroundImage = global::FormsAsterix.Properties.Resources.images__1_1;
            this.RestartSimBut.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RestartSimBut.Image = global::FormsAsterix.Properties.Resources.images__1_;
            this.RestartSimBut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RestartSimBut.Name = "RestartSimBut";
            this.RestartSimBut.Size = new System.Drawing.Size(114, 36);
            this.RestartSimBut.Text = "Restart Sim";
            this.RestartSimBut.Click += new System.EventHandler(this.RestartSimBut_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(29, 36);
            // 
            // NewDataBut
            // 
            this.NewDataBut.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.NewDataBut.BackgroundImage = global::FormsAsterix.Properties.Resources.d2fcf1c06735d7899e5e0766edd9498e1;
            this.NewDataBut.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewDataBut.Image = global::FormsAsterix.Properties.Resources.d2fcf1c06735d7899e5e0766edd9498e;
            this.NewDataBut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewDataBut.Name = "NewDataBut";
            this.NewDataBut.Size = new System.Drawing.Size(102, 24);
            this.NewDataBut.Text = "New Data";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(29, 36);
            this.toolStripButton5.Text = "toolStripButton5";
            // 
            // ShowDataBut
            // 
            this.ShowDataBut.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ShowDataBut.BackgroundImage = global::FormsAsterix.Properties.Resources.file_statistics_linear_icon_thin_line_illustration_document_with_diagram_contour_symbol_isolated_outline_drawing_vector;
            this.ShowDataBut.Image = global::FormsAsterix.Properties.Resources.file_statistics_linear_icon_thin_line_illustration_document_with_diagram_contour_symbol_isolated_outline_drawing_vector;
            this.ShowDataBut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ShowDataBut.Name = "ShowDataBut";
            this.ShowDataBut.Size = new System.Drawing.Size(108, 24);
            this.ShowDataBut.Text = "Show Data";
            this.ShowDataBut.Click += new System.EventHandler(this.ShowDataBut_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 36);
            this.toolStripButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImage = global::FormsAsterix.Properties.Resources.fondo;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.TitolTXT);
            this.groupBox1.Controls.Add(this.DescodBUT);
            this.groupBox1.Location = new System.Drawing.Point(0, -14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(1434, 659);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // TitolTXT
            // 
            this.TitolTXT.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TitolTXT.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TitolTXT.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitolTXT.Location = new System.Drawing.Point(261, 204);
            this.TitolTXT.Name = "TitolTXT";
            this.TitolTXT.Size = new System.Drawing.Size(840, 72);
            this.TitolTXT.TabIndex = 15;
            this.TitolTXT.Text = "ASTERIX Hackathon - Projecte 2 PGTA";
            this.TitolTXT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DescodBUT
            // 
            this.DescodBUT.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DescodBUT.BackColor = System.Drawing.SystemColors.ControlLight;
            this.DescodBUT.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescodBUT.Location = new System.Drawing.Point(523, 295);
            this.DescodBUT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DescodBUT.Name = "DescodBUT";
            this.DescodBUT.Size = new System.Drawing.Size(286, 96);
            this.DescodBUT.TabIndex = 13;
            this.DescodBUT.Text = "Seleccionar fitxer .ast a descodificar";
            this.DescodBUT.UseVisualStyleBackColor = false;
            this.DescodBUT.Click += new System.EventHandler(this.DescodBUT_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1434, 645);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button DescodBUT;
        private Label TitolTXT;
        private GroupBox groupBox1;
        private System.Windows.Forms.Timer timer1;
        private GroupBox groupBox2;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private Label Velocity_label_bar;
        private Button Start_sim;
        private Label timeTXT;
        private TrackBar trackBar1;
        private ToolStrip toolStrip1;
        private ToolStripButton GetKMLBut;
        private ToolStripButton toolStripButton3;
        private ToolStripButton RestartSimBut;
        private ToolStripButton toolStripButton4;
        private ToolStripButton NewDataBut;
        private ToolStripButton toolStripButton5;
        private ToolStripButton ShowDataBut;
        private ToolStripButton toolStripButton1;
        private ToolStripButton CSV_File;
    }
}
