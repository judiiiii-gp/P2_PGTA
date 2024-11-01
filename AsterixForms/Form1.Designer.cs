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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Seleccionar = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // Seleccionar
            // 
            Seleccionar.BackColor = Color.Blue;
            Seleccionar.Image = (Image)resources.GetObject("Seleccionar.Image");
            Seleccionar.Location = new Point(-27, -9);
            Seleccionar.Name = "Seleccionar";
            Seleccionar.Size = new Size(834, 469);
            Seleccionar.TabIndex = 3;
            Seleccionar.Text = "Seleccionar";
            Seleccionar.UseVisualStyleBackColor = false;
            Seleccionar.Click += Seleccionar_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(175, 9);
            label2.Name = "label2";
            label2.Size = new Size(423, 31);
            label2.TabIndex = 11;
            label2.Text = "Escull el fitxer .ast que vols descodificar";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(Seleccionar);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button Seleccionar;
        private Label label2;
    }
}
