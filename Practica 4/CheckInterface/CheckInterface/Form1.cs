using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FishFa30;

namespace CheckInterface
{
    public partial class Form1 : Form
    {
        FishFace fish = new FishFace(true, false, 0);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /* Configuramos ciertos parámetros de la interfaz de forma
             * explícita para evitar errores */
            // Barra de progreso de la entrada analógica EX
            EXProgressBar.Minimum = 0;
            EXProgressBar.Maximum = 1024;
            EXProgressBar.Value = 0;
            // Barra de progreso de la entrada analógica EY
            EYProgressBar.Minimum = 0;
            EYProgressBar.Maximum = 1024;
            EYProgressBar.Value = 0;
            // Auxiliares
            EDigAuxiliar.Visible = false;

            try
            {
                fish.OpenInterface(Port.COM1);
                cambiarEstado(0);
            }
            catch (FishFaceException)
            {
                cambiarEstado(1);
            }
        }

        private void cambiarEstado(int estado)
        {
            // Valores de estado: 0(ok) - 1(error)
            if (estado == 0) {
                EDigAuxiliar.BackColor = Color.Green;
                EDigAuxiliar.Text = "1";
                tsBarraEstado.Text = "Interfaz conectada...";
            }
            if (estado == 1)
            {
                EDigAuxiliar.BackColor = Color.Red;
                EDigAuxiliar.Text = "0";
                tsBarraEstado.Text = "La interfaz no está conectada...";
            }
            E1Label.BackColor = EDigAuxiliar.BackColor;
            E1Label.Text = EDigAuxiliar.Text;
            E2Label.BackColor = EDigAuxiliar.BackColor;
            E2Label.Text = EDigAuxiliar.Text;
            E3Label.BackColor = EDigAuxiliar.BackColor;
            E3Label.Text = EDigAuxiliar.Text;
            E4Label.BackColor = EDigAuxiliar.BackColor;
            E4Label.Text = EDigAuxiliar.Text;
            E5Label.BackColor = EDigAuxiliar.BackColor;
            E5Label.Text = EDigAuxiliar.Text;
            E6Label.BackColor = EDigAuxiliar.BackColor;
            E6Label.Text = EDigAuxiliar.Text;
            E7Label.BackColor = EDigAuxiliar.BackColor;
            E7Label.Text = EDigAuxiliar.Text;
            E8Label.BackColor = EDigAuxiliar.BackColor;
            E8Label.Text = EDigAuxiliar.Text;
        }

        private void activarMotor(Nr motor, int pos)
        {
            // Valores de pos: 0(ccw) - 1(off) - 2(cw)
            if (pos == 0) fish.SetMotor(motor, Dir.Left);
            else if (pos == 2) fish.SetMotor(motor, Dir.Right);
            else fish.SetMotor(motor, Dir.Off);
        }

        private void M1TrackBar_Scroll(object sender, EventArgs e)
        {
            activarMotor(Nr.M1, M1TrackBar.Value);
        }

        private void M2TrackBar_Scroll(object sender, EventArgs e)
        {
            activarMotor(Nr.M2, M2TrackBar.Value);
        }

        private void M3TrackBar_Scroll(object sender, EventArgs e)
        {
            activarMotor(Nr.M3, M3TrackBar.Value);
        }

        private void M4TrackBar_Scroll(object sender, EventArgs e)
        {
            activarMotor(Nr.M4, M4TrackBar.Value);
        }
    }
}
