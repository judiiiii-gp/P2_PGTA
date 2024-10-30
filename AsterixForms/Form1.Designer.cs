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
            Buscar = new Button();
            SeleccionarBox = new TextBox();
            BuscarBox = new TextBox();
            Seleccionar = new Button();
            SeleccionarLabel = new Label();
            BuscarLabel = new Label();
            listBox1 = new ListBox();
            Fichero = new Button();
            NombreFichero = new TextBox();
            label1 = new Label();
            Aceptar = new Button();
            SuspendLayout();
            // 
            // Buscar
            // 
            Buscar.Location = new Point(644, 409);
            Buscar.Name = "Buscar";
            Buscar.Size = new Size(94, 29);
            Buscar.TabIndex = 0;
            Buscar.Text = "Buscar";
            Buscar.UseVisualStyleBackColor = true;
            Buscar.Click += Buscar_Click;
            // 
            // SeleccionarBox
            // 
            SeleccionarBox.Location = new Point(12, 36);
            SeleccionarBox.Name = "SeleccionarBox";
            SeleccionarBox.Size = new Size(626, 27);
            SeleccionarBox.TabIndex = 1;
            // 
            // BuscarBox
            // 
            BuscarBox.Location = new Point(12, 411);
            BuscarBox.Name = "BuscarBox";
            BuscarBox.Size = new Size(626, 27);
            BuscarBox.TabIndex = 2;
            // 
            // Seleccionar
            // 
            Seleccionar.Location = new Point(644, 35);
            Seleccionar.Name = "Seleccionar";
            Seleccionar.Size = new Size(94, 29);
            Seleccionar.TabIndex = 3;
            Seleccionar.Text = "Seleccionar";
            Seleccionar.UseVisualStyleBackColor = true;
            Seleccionar.Click += Seleccionar_Click;
            // 
            // SeleccionarLabel
            // 
            SeleccionarLabel.AutoSize = true;
            SeleccionarLabel.Location = new Point(12, 13);
            SeleccionarLabel.Name = "SeleccionarLabel";
            SeleccionarLabel.Size = new Size(146, 20);
            SeleccionarLabel.TabIndex = 4;
            SeleccionarLabel.Text = "Seleccioni la carpeta";
            // 
            // BuscarLabel
            // 
            BuscarLabel.AutoSize = true;
            BuscarLabel.Location = new Point(12, 388);
            BuscarLabel.Name = "BuscarLabel";
            BuscarLabel.Size = new Size(105, 20);
            BuscarLabel.TabIndex = 5;
            BuscarLabel.Text = "Nom del fitxer";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(12, 69);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(626, 304);
            listBox1.TabIndex = 6;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // Fichero
            // 
            Fichero.Location = new Point(644, 290);
            Fichero.Name = "Fichero";
            Fichero.Size = new Size(144, 29);
            Fichero.TabIndex = 7;
            Fichero.Text = "Escribir un fichero";
            Fichero.UseVisualStyleBackColor = true;
            Fichero.Click += Fichero_Click;
            // 
            // NombreFichero
            // 
            NombreFichero.Location = new Point(644, 257);
            NombreFichero.Name = "NombreFichero";
            NombreFichero.Size = new Size(163, 27);
            NombreFichero.TabIndex = 8;
            NombreFichero.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(517, 224);
            label1.Name = "label1";
            label1.Size = new Size(271, 20);
            label1.TabIndex = 9;
            label1.Text = "Escribe el nombre del fichero a guardar";
            label1.Visible = false;
            // 
            // Aceptar
            // 
            Aceptar.Location = new Point(672, 346);
            Aceptar.Name = "Aceptar";
            Aceptar.Size = new Size(94, 29);
            Aceptar.TabIndex = 10;
            Aceptar.Text = "Aceptar";
            Aceptar.UseVisualStyleBackColor = true;
            Aceptar.Click += Aceptar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Aceptar);
            Controls.Add(label1);
            Controls.Add(NombreFichero);
            Controls.Add(Fichero);
            Controls.Add(listBox1);
            Controls.Add(BuscarLabel);
            Controls.Add(SeleccionarLabel);
            Controls.Add(Seleccionar);
            Controls.Add(BuscarBox);
            Controls.Add(SeleccionarBox);
            Controls.Add(Buscar);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Buscar;
        private TextBox SeleccionarBox;
        private TextBox BuscarBox;
        private Button Seleccionar;
        private Label SeleccionarLabel;
        private Label BuscarLabel;
        private ListBox listBox1;
        private Button Fichero;
        private TextBox NombreFichero;
        private Label label1;
        private Button Aceptar;
    }
}
