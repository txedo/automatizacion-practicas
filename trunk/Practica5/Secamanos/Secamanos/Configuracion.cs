using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Secamanos
{
    public partial class Configuracion : Form
    {
        private Boolean configured = false;
        private int nuevo_tiempo = 0;

        public Configuracion(int tiempo)
        {
            this.nuevo_tiempo = tiempo;
            InitializeComponent();
            tiempoTextBox.Text = tiempo.ToString();
        }

        public Boolean estaConfiguado()
        {
            return this.configured;
        }

        public int getTiempo()
        {
            return this.nuevo_tiempo;
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            this.nuevo_tiempo = Convert.ToInt32(tiempoTextBox.Text);
            this.configured = true;
            this.Hide();
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            this.configured = false;
        }

    }
}
