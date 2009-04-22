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
    public partial class Ayuda : Form
    {
        public Ayuda()
        {
            InitializeComponent();
            textBox1.Text = "Se deben conectar los siguientes dispositivos:\r\n\t* Una lámpara en la salida digital M1.\r\n\t* Una célula óptica en la entrada digital E1.\r\n\t* Un motor en la salida digital M2.";
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
