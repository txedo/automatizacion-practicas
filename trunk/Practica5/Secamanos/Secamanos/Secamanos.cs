using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FishFa30;

namespace Secamanos
{
    public partial class Secamanos : Form
    {
        /* Se deben conectar:
        * lámpara en la salida digital M1
        * célula óptica en la entrada digital E1
        * motor en la salida digital M2
        */
        Boolean iniciado = false;
        Boolean luzDetectada = true;
        Boolean echo = false;
        int tiempo = 5; //en segundos
        // Instanciamos el interfaz FishFace
        FishFace fish = new FishFace();

        public Secamanos()
        {
            InitializeComponent();
        }

        public void CambiarEstado(int estado)
        {
            if (estado == -1) // Interfaz desconectado
            {
                fish.CloseInterface();
                iniciado = false;
                IniciarButton.Text = "Iniciar";
                IniciarButton.Enabled = true;
                DetenerButton.Enabled = false;
                manoDetectadaLabel.Text = "0";
                barraEstadoLabel.Text = "El interfaz no está conectada...";
                tsIcono.Image = global::Secamanos.Properties.Resources.gtk_cancel;
                tsIcono.Size = new System.Drawing.Size(16, 16);
            }
            if (estado == 0) // Secamanos funcionando
            {
                IniciarButton.Enabled = false;
                DetenerButton.Enabled = true;
                tsIcono.Image = global::Secamanos.Properties.Resources.gtk_apply;
                tsIcono.Size = new System.Drawing.Size(16, 16);
                barraEstadoLabel.Text = "Iniciando el sistema...";
                // Encendemos la lámpara
                fish.SetMotor(Nr.M1, Dir.On);
                // Esperamos un segundo para que se encienda la lámpara
                fish.Pause(1000);
                barraEstadoLabel.Text = "Sistema iniciado...";
                iniciado = true;
            }
        }

        private void OnOff_Click(object sender, EventArgs e)
        {
            try
            {
                if (!iniciado)
                {
                    fish.OpenInterface(Port.COM1);
                    fish.Pause(1000);
                    CambiarEstado(0);
                }
                while (iniciado)
                {
                    luzDetectada = fish.GetInput(Nr.E1);
                    if (!luzDetectada)
                    { // Si no se detecta luz es porque hemos pasado la mano
                        manoDetectadaImageLabel.Image = global::Secamanos.Properties.Resources.gtk_apply;
                        manoDetectadaLabel.Text = tiempo.ToString();
                        fish.SetMotor(Nr.M2, Dir.On);
                        echo = !luzDetectada;
                    }
                    else if (echo)
                    {
                        manoDetectadaImageLabel.Image = global::Secamanos.Properties.Resources.gtk_cancel;
                        for (int i = tiempo; i > 0; i--)
                        {
                            manoDetectadaLabel.Text = i.ToString();
                            fish.Pause(1000);
                        }
                        manoDetectadaLabel.Text = "0";
                        fish.SetMotor(Nr.M2, Dir.Off);
                        echo = false;
                    }
                }
            }
            catch (FishFaceException)
            {
                CambiarEstado(-1);
            }
        }

        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuracion conf = new Configuracion(this.tiempo);
            conf.ShowDialog();
            if (conf.estaConfiguado()) this.tiempo = conf.getTiempo();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void comoConectarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ayuda help = new Ayuda();
            help.ShowDialog();
        }

        private void DetenerButton_Click(object sender, EventArgs e)
        {
            CambiarEstado(-1);
            barraEstadoLabel.Text = "El sistema no está iniciado...";
        }

    }
}
