namespace AsterixForms
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            DescodBUT = new Button();
            TitolTXT = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            panel1 = new Panel();
            timeTXT = new Label();
            trackBar1 = new TrackBar();
            toolStrip1 = new ToolStrip();
            GetKMLBut = new ToolStripButton();
            toolStripButton3 = new ToolStripButton();
            RestartSimBut = new ToolStripButton();
            toolStripButton4 = new ToolStripButton();
            NewDataBut = new ToolStripButton();
            toolStripButton5 = new ToolStripButton();
            ShowDataBut = new ToolStripButton();
            timer1 = new System.Windows.Forms.Timer(components);
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // DescodBUT
            // 
            DescodBUT.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DescodBUT.Font = new Font("Segoe UI", 20F);
            DescodBUT.Location = new Point(459, 291);
            DescodBUT.Name = "DescodBUT";
            DescodBUT.Size = new Size(350, 188);
            DescodBUT.TabIndex = 13;
            DescodBUT.Text = "Seleccionar fitxer .ast a descodificar";
            DescodBUT.UseVisualStyleBackColor = true;
            DescodBUT.Click += DescodBUT_Click;
            // 
            // TitolTXT
            // 
            TitolTXT.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TitolTXT.AutoSize = true;
            TitolTXT.BackColor = Color.Transparent;
            TitolTXT.Font = new Font("Segoe UI", 30F);
            TitolTXT.Location = new Point(209, 196);
            TitolTXT.Name = "TitolTXT";
            TitolTXT.Size = new Size(859, 67);
            TitolTXT.TabIndex = 15;
            TitolTXT.Text = "ASTERIX Hackathon - Projecte 2 PGTA";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.BackColor = Color.Transparent;
            groupBox1.Controls.Add(TitolTXT);
            groupBox1.Controls.Add(DescodBUT);
            groupBox1.Location = new Point(65, 41);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1289, 726);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.Transparent;
            groupBox2.Controls.Add(panel1);
            groupBox2.Controls.Add(timeTXT);
            groupBox2.Controls.Add(trackBar1);
            groupBox2.Controls.Add(toolStrip1);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1434, 806);
            groupBox2.TabIndex = 17;
            groupBox2.TabStop = false;
            groupBox2.Visible = false;
            // 
            // panel1
            // 
            panel1.Location = new Point(260, 67);
            panel1.Name = "panel1";
            panel1.Size = new Size(1162, 727);
            panel1.TabIndex = 3;
            // 
            // timeTXT
            // 
            timeTXT.AutoSize = true;
            timeTXT.Font = new Font("Segoe UI", 12F);
            timeTXT.Location = new Point(103, 139);
            timeTXT.Name = "timeTXT";
            timeTXT.Size = new Size(51, 28);
            timeTXT.TabIndex = 2;
            timeTXT.Text = "time";
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(32, 183);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(195, 56);
            trackBar1.TabIndex = 1;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { GetKMLBut, toolStripButton3, RestartSimBut, toolStripButton4, NewDataBut, toolStripButton5, ShowDataBut });
            toolStrip1.Location = new Point(3, 23);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1428, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // GetKMLBut
            // 
            GetKMLBut.BackColor = SystemColors.ActiveBorder;
            GetKMLBut.Image = Properties.Resources.how_to_draw_earth_for_kids;
            GetKMLBut.ImageTransparentColor = Color.Magenta;
            GetKMLBut.Name = "GetKMLBut";
            GetKMLBut.Size = new Size(89, 24);
            GetKMLBut.Text = "Get KML";
            GetKMLBut.ToolTipText = "Get KML";
            GetKMLBut.Click += GetKMLBut_Click;
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(29, 24);
            toolStripButton3.Text = "   ";
            // 
            // RestartSimBut
            // 
            RestartSimBut.BackColor = SystemColors.ActiveBorder;
            RestartSimBut.Image = Properties.Resources.images__1_;
            RestartSimBut.ImageTransparentColor = Color.Magenta;
            RestartSimBut.Name = "RestartSimBut";
            RestartSimBut.Size = new Size(108, 24);
            RestartSimBut.Text = "Restart Sim";
            RestartSimBut.Click += RestartSimBut_Click;
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton4.Image = (Image)resources.GetObject("toolStripButton4.Image");
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(29, 24);
            // 
            // NewDataBut
            // 
            NewDataBut.BackColor = SystemColors.ActiveBorder;
            NewDataBut.Image = Properties.Resources.d2fcf1c06735d7899e5e0766edd9498e;
            NewDataBut.ImageTransparentColor = Color.Magenta;
            NewDataBut.Name = "NewDataBut";
            NewDataBut.Size = new Size(99, 24);
            NewDataBut.Text = "New Data";
            NewDataBut.Click += NewDataBut_Click;
            // 
            // toolStripButton5
            // 
            toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.None;
            toolStripButton5.Image = (Image)resources.GetObject("toolStripButton5.Image");
            toolStripButton5.ImageTransparentColor = Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new Size(29, 24);
            toolStripButton5.Text = "toolStripButton5";
            // 
            // ShowDataBut
            // 
            ShowDataBut.BackColor = SystemColors.ActiveBorder;
            ShowDataBut.Image = Properties.Resources.file_statistics_linear_icon_thin_line_illustration_document_with_diagram_contour_symbol_isolated_outline_drawing_vector;
            ShowDataBut.ImageTransparentColor = Color.Magenta;
            ShowDataBut.Name = "ShowDataBut";
            ShowDataBut.Size = new Size(105, 24);
            ShowDataBut.Text = "Show Data";
            ShowDataBut.Click += ShowDataBut_Click;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1434, 806);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button DescodBUT;
        private Label TitolTXT;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ToolStrip toolStrip1;
        private ToolStripButton GetKMLBut;
        private ToolStripButton RestartSimBut;
        private TrackBar trackBar1;
        private ToolStripButton toolStripButton3;
        private Label timeTXT;
        private ToolStripButton toolStripButton4;
        private ToolStripButton NewDataBut;
        private System.Windows.Forms.Timer timer1;
        private ToolStripButton toolStripButton5;
        private ToolStripButton ShowDataBut;
        private Panel panel1;
    }
}
