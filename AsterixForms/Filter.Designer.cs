namespace AsterixForms
{
    partial class Filter
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
            BtnFilter = new Button();
            comboBox1 = new ComboBox();
            txtBox_Start = new TextBox();
            txtBox_End = new TextBox();
            SuspendLayout();
            // 
            // BtnFilter
            // 
            BtnFilter.Location = new Point(395, 143);
            BtnFilter.Name = "BtnFilter";
            BtnFilter.Size = new Size(94, 29);
            BtnFilter.TabIndex = 0;
            BtnFilter.Text = "Filter";
            BtnFilter.UseVisualStyleBackColor = true;
            BtnFilter.Click += BtnFilter_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 1;
            // 
            // txtBox_Start
            // 
            txtBox_Start.Location = new Point(364, 13);
            txtBox_Start.Name = "txtBox_Start";
            txtBox_Start.Size = new Size(125, 27);
            txtBox_Start.TabIndex = 2;
            // 
            // txtBox_End
            // 
            txtBox_End.Location = new Point(364, 79);
            txtBox_End.Name = "txtBox_End";
            txtBox_End.Size = new Size(125, 27);
            txtBox_End.TabIndex = 3;
            // 
            // Filter
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(503, 185);
            Controls.Add(txtBox_End);
            Controls.Add(txtBox_Start);
            Controls.Add(comboBox1);
            Controls.Add(BtnFilter);
            Name = "Filter";
            Text = "Filter";
            Load += Filter_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnFilter;
        private ComboBox comboBox1;
        private TextBox txtBox_Start;
        private TextBox txtBox_End;
    }
}