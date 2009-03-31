namespace CheckInterface
{
    partial class Configuracion
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.puertoComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.refrescoInterfazTextBox = new System.Windows.Forms.TextBox();
            this.botonAceptar = new System.Windows.Forms.Button();
            this.botonCancelar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.refrescoEntradasTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // puertoComboBox
            // 
            this.puertoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.puertoComboBox.FormattingEnabled = true;
            this.puertoComboBox.Location = new System.Drawing.Point(170, 15);
            this.puertoComboBox.Name = "puertoComboBox";
            this.puertoComboBox.Size = new System.Drawing.Size(59, 21);
            this.puertoComboBox.Sorted = true;
            this.puertoComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Puerto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Refresco del interfaz (ms):";
            // 
            // refrescoInterfazTextBox
            // 
            this.refrescoInterfazTextBox.Location = new System.Drawing.Point(170, 45);
            this.refrescoInterfazTextBox.Name = "refrescoInterfazTextBox";
            this.refrescoInterfazTextBox.Size = new System.Drawing.Size(59, 20);
            this.refrescoInterfazTextBox.TabIndex = 1;
            // 
            // botonAceptar
            // 
            this.botonAceptar.Location = new System.Drawing.Point(18, 110);
            this.botonAceptar.Name = "botonAceptar";
            this.botonAceptar.Size = new System.Drawing.Size(70, 25);
            this.botonAceptar.TabIndex = 3;
            this.botonAceptar.Text = "Aceptar";
            this.botonAceptar.UseVisualStyleBackColor = true;
            this.botonAceptar.Click += new System.EventHandler(this.botonAceptar_Click);
            // 
            // botonCancelar
            // 
            this.botonCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botonCancelar.Location = new System.Drawing.Point(159, 110);
            this.botonCancelar.Name = "botonCancelar";
            this.botonCancelar.Size = new System.Drawing.Size(70, 25);
            this.botonCancelar.TabIndex = 4;
            this.botonCancelar.Text = "Cancelar";
            this.botonCancelar.UseVisualStyleBackColor = true;
            this.botonCancelar.Click += new System.EventHandler(this.botonCancelar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Refresco de las entradas (ms):";
            // 
            // refrescoEntradasTextBox
            // 
            this.refrescoEntradasTextBox.Location = new System.Drawing.Point(170, 75);
            this.refrescoEntradasTextBox.Name = "refrescoEntradasTextBox";
            this.refrescoEntradasTextBox.Size = new System.Drawing.Size(59, 20);
            this.refrescoEntradasTextBox.TabIndex = 2;
            // 
            // Configuracion
            // 
            this.AcceptButton = this.botonAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.botonCancelar;
            this.ClientSize = new System.Drawing.Size(241, 151);
            this.Controls.Add(this.refrescoEntradasTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.botonCancelar);
            this.Controls.Add(this.botonAceptar);
            this.Controls.Add(this.refrescoInterfazTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.puertoComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Configuracion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Panel de configuración";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox puertoComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox refrescoInterfazTextBox;
        private System.Windows.Forms.Button botonAceptar;
        private System.Windows.Forms.Button botonCancelar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox refrescoEntradasTextBox;
    }
}