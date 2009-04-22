namespace Secamanos
{
    partial class Secamanos
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsIcono = new System.Windows.Forms.ToolStripStatusLabel();
            this.barraEstadoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comoConectarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manoDetectadaLabel = new System.Windows.Forms.Label();
            this.IniciarButton = new System.Windows.Forms.Button();
            this.manoDetectadaImageLabel = new System.Windows.Forms.Label();
            this.DetenerButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsIcono,
            this.barraEstadoLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 107);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(254, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsIcono
            // 
            this.tsIcono.Image = global::Secamanos.Properties.Resources.gtk_cancel;
            this.tsIcono.Name = "tsIcono";
            this.tsIcono.Size = new System.Drawing.Size(16, 17);
            // 
            // barraEstadoLabel
            // 
            this.barraEstadoLabel.Name = "barraEstadoLabel";
            this.barraEstadoLabel.Size = new System.Drawing.Size(143, 17);
            this.barraEstadoLabel.Text = "El sistema no está iniciado...";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(254, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.archivoToolStripMenuItem.Text = "&Archivo";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Image = global::Secamanos.Properties.Resources.gtk_quit;
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.salirToolStripMenuItem.Text = "&Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuraciónToolStripMenuItem});
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.editarToolStripMenuItem.Text = "&Editar";
            // 
            // configuraciónToolStripMenuItem
            // 
            this.configuraciónToolStripMenuItem.Image = global::Secamanos.Properties.Resources.gtk_configure;
            this.configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            this.configuraciónToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.configuraciónToolStripMenuItem.Text = "&Configuración...";
            this.configuraciónToolStripMenuItem.Click += new System.EventHandler(this.configuraciónToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comoConectarToolStripMenuItem,
            this.acercaDeToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.ayudaToolStripMenuItem.Text = "Ay&uda";
            // 
            // comoConectarToolStripMenuItem
            // 
            this.comoConectarToolStripMenuItem.Image = global::Secamanos.Properties.Resources.gtk_help;
            this.comoConectarToolStripMenuItem.Name = "comoConectarToolStripMenuItem";
            this.comoConectarToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.comoConectarToolStripMenuItem.Text = "Cómo conectar...";
            this.comoConectarToolStripMenuItem.Click += new System.EventHandler(this.comoConectarToolStripMenuItem_Click);
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Image = global::Secamanos.Properties.Resources.gtk_about;
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.acercaDeToolStripMenuItem.Text = "&Acerca de...";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // manoDetectadaLabel
            // 
            this.manoDetectadaLabel.AutoSize = true;
            this.manoDetectadaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manoDetectadaLabel.Location = new System.Drawing.Point(102, 39);
            this.manoDetectadaLabel.Name = "manoDetectadaLabel";
            this.manoDetectadaLabel.Size = new System.Drawing.Size(51, 55);
            this.manoDetectadaLabel.TabIndex = 3;
            this.manoDetectadaLabel.Text = "0";
            // 
            // IniciarButton
            // 
            this.IniciarButton.Location = new System.Drawing.Point(168, 39);
            this.IniciarButton.Name = "IniciarButton";
            this.IniciarButton.Size = new System.Drawing.Size(74, 24);
            this.IniciarButton.TabIndex = 4;
            this.IniciarButton.Text = "Iniciar";
            this.IniciarButton.UseVisualStyleBackColor = true;
            this.IniciarButton.Click += new System.EventHandler(this.OnOff_Click);
            // 
            // manoDetectadaImageLabel
            // 
            this.manoDetectadaImageLabel.AutoSize = true;
            this.manoDetectadaImageLabel.Image = global::Secamanos.Properties.Resources.gtk_cancel;
            this.manoDetectadaImageLabel.Location = new System.Drawing.Point(12, 34);
            this.manoDetectadaImageLabel.MaximumSize = new System.Drawing.Size(60, 60);
            this.manoDetectadaImageLabel.MinimumSize = new System.Drawing.Size(60, 60);
            this.manoDetectadaImageLabel.Name = "manoDetectadaImageLabel";
            this.manoDetectadaImageLabel.Size = new System.Drawing.Size(60, 60);
            this.manoDetectadaImageLabel.TabIndex = 2;
            // 
            // DetenerButton
            // 
            this.DetenerButton.Enabled = false;
            this.DetenerButton.Location = new System.Drawing.Point(170, 69);
            this.DetenerButton.Name = "DetenerButton";
            this.DetenerButton.Size = new System.Drawing.Size(71, 24);
            this.DetenerButton.TabIndex = 5;
            this.DetenerButton.Text = "Detener";
            this.DetenerButton.UseVisualStyleBackColor = true;
            this.DetenerButton.Click += new System.EventHandler(this.DetenerButton_Click);
            // 
            // Secamanos
            // 
            this.AcceptButton = this.IniciarButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 129);
            this.Controls.Add(this.DetenerButton);
            this.Controls.Add(this.IniciarButton);
            this.Controls.Add(this.manoDetectadaLabel);
            this.Controls.Add(this.manoDetectadaImageLabel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Secamanos";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Secamanos";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel barraEstadoLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel tsIcono;
        private System.Windows.Forms.Label manoDetectadaImageLabel;
        private System.Windows.Forms.Label manoDetectadaLabel;
        private System.Windows.Forms.Button IniciarButton;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comoConectarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.Button DetenerButton;
    }
}

