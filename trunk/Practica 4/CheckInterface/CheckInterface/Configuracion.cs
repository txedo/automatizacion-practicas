using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FishFa30;

namespace CheckInterface
{
    public partial class Configuracion : Form
    {
        private Boolean configurado = false;
        private Port puerto;
        private int refrescoInterfaz;
        private int refrescoEntradas;

        public Configuracion(Port port, int rateIface, int rateInputs)
        {
            InitializeComponent();
            puertoComboBox.Items.Add("COM1");
            puertoComboBox.Items.Add("COM2");
            puertoComboBox.Items.Add("COM3");
            puertoComboBox.Items.Add("COM4");
            switch (port)
            {
                case Port.COM1: puertoComboBox.SelectedIndex = 0; break;
                case Port.COM2: puertoComboBox.SelectedIndex = 1; break;
                case Port.COM3: puertoComboBox.SelectedIndex = 2; break;
                case Port.COM4: puertoComboBox.SelectedIndex = 3; break;
            }
            this.refrescoInterfaz = rateIface;
            refrescoInterfazTextBox.Text = this.refrescoInterfaz.ToString();
            this.refrescoEntradas = rateInputs;
            refrescoEntradasTextBox.Text = this.refrescoEntradas.ToString();
        }

        public Boolean estaConfigurado()
        {
            return this.configurado;
        }

        public Port getPuerto()
        {
            return this.puerto;
        }

        public int getRefrescoInterfaz()
        {
            return this.refrescoInterfaz;
        }

        public int getRefrescoEntradas()
        {
            return this.refrescoEntradas;
        }

        private void botonAceptar_Click(object sender, EventArgs e)
        {
            this.configurado = true;
            switch (puertoComboBox.SelectedIndex)
            {
                case 0: this.puerto = Port.COM1; break;
                case 1: this.puerto = Port.COM2; break;
                case 2: this.puerto = Port.COM3; break;
                case 3: this.puerto = Port.COM4; break;
            }
            if (refrescoInterfazTextBox.Text != "")
                this.refrescoInterfaz = Convert.ToInt32(refrescoInterfazTextBox.Text);
            if (refrescoEntradasTextBox.Text != "")
                this.refrescoEntradas = Convert.ToInt32(refrescoEntradasTextBox.Text);
            this.Hide();
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            this.configurado = false;
            this.Hide();
        }
    }
}