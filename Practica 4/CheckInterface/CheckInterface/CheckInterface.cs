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
    public partial class CheckInterface : Form
    {
        private Port Puerto;
        private int rateIface;
        private int rateInputs;
        private Boolean encendido;
        FishFace fish = new FishFace(true, false, 0);

        public CheckInterface()
        {
            // Inicializamos los atributos con los valores por defecto
            this.encendido = false;
            this.Puerto = Port.COM1;
            this.rateIface = 500;
            this.rateInputs = 10;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /* Configuramos ciertos parámetros de la interfaz de forma
             * explícita para evitar errores */
            int min = 0;
            int max = 1024;
            // Barra de progreso de la entrada analógica EX
            EXProgressBar.Minimum = min;
            EXProgressBar.Maximum = max;
            EXProgressBar.Value = min;
            // Barra de progreso de la entrada analógica EY
            EYProgressBar.Minimum = min;
            EYProgressBar.Maximum = max;
            EYProgressBar.Value = min;
            // Auxiliares
            EDigAuxiliar.Visible = false;
            // Inicializamos los timers con los valores por defecto
            timerCheckInterface.Interval = this.rateIface;
            timerCheckInterface.Enabled = true;
            timerEntradas.Interval = this.rateInputs;
        }

        private void cambiarEstado(int estado)
        {
            EDigAuxiliar.BackColor = Color.Red;
            EDigAuxiliar.Text = "0";
            // Valores de estado: 0(ok) - 1(error)
            if (estado == 0)
            { // La interfaz está conectada
                // Indicamos en la variable de control que la interfaz está encendida
                this.encendido = true;
                // Activamos el timer que dispara la lectura de las entradas
                timerEntradas.Enabled = true;
                // Configuramos el interfaz a su estado de START
                this.tsIcono.Image = global::CheckInterface.Properties.Resources.gtk_yes;
                this.tsIcono.Size = new System.Drawing.Size(16, 16);
                tsBarraEstado.Text = "Interfaz conectada...";
                M1TrackBar.Enabled = true;
                M2TrackBar.Enabled = true;
                M3TrackBar.Enabled = true;
                M4TrackBar.Enabled = true;
            }
            if (estado == 1)
            { // La interfaz NO está conectada
                // Indicamos en la variable de control que la interfaz no esta encendida
                this.encendido = false;
                // Paramos el timer que dispara la lectura de las entradas
                timerEntradas.Enabled = false;
                // Cerramos la conexión con la interfaz
                this.fish.CloseInterface();
                // Configuramos el interfaz a su estado de STOP
                this.tsIcono.Image = global::CheckInterface.Properties.Resources.gtk_no;
                this.tsIcono.Size = new System.Drawing.Size(16, 16);
                tsBarraEstado.Text = "La interfaz no está conectada...";
                M1TrackBar.Value = 1;
                M1TrackBar.Enabled = false;
                M2TrackBar.Value = 1;
                M2TrackBar.Enabled = false;
                M3TrackBar.Value = 1;
                M3TrackBar.Enabled = false;
                M4TrackBar.Value = 1;
                M4TrackBar.Enabled = false;
                EXProgressBar.Value = EXProgressBar.Minimum;
                EYProgressBar.Value = EYProgressBar.Minimum;
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

        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuracion conf = new Configuracion(this.Puerto, timerCheckInterface.Interval, timerEntradas.Interval);
            conf.ShowDialog();
            if (conf.estaConfigurado())
            {
                if (this.Puerto != conf.getPuerto())
                {
                    this.encendido = false;
                    fish.CloseInterface();
                    this.Puerto = conf.getPuerto();
                    try
                    {
                        fish.OpenInterface(this.Puerto);
                        this.encendido = true;
                    }
                    catch (FishFaceException)
                    {
                        cambiarEstado(1);
                    }
                }
                this.rateIface = conf.getRefrescoInterfaz();
                this.rateInputs = conf.getRefrescoEntradas();
                timerCheckInterface.Interval = this.rateIface;
                timerEntradas.Interval = this.rateInputs;
            }
        }

        private void cambiarEstadoEntradaDigital(Nr input, Label label)
        {
            if (fish.GetInput(input))
            {
                label.Text = "1";
                label.BackColor = Color.Green;
            }
            else
            {
                label.Text = "0";
                label.BackColor = Color.Red;
            }
        }

        private void cambiarEstadoEntradaAnalogica(Nr input, ProgressBar bar)
        {
            int valor = 0;
            valor = fish.GetAnalog(input);
            bar.Value = valor;
        }

        private void timerEntradas_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.encendido)
                {
                    cambiarEstadoEntradaDigital(Nr.E1, E1Label);
                    cambiarEstadoEntradaDigital(Nr.E2, E2Label);
                    cambiarEstadoEntradaDigital(Nr.E3, E3Label);
                    cambiarEstadoEntradaDigital(Nr.E4, E4Label);
                    cambiarEstadoEntradaDigital(Nr.E5, E5Label);
                    cambiarEstadoEntradaDigital(Nr.E6, E6Label);
                    cambiarEstadoEntradaDigital(Nr.E7, E7Label);
                    cambiarEstadoEntradaDigital(Nr.E8, E8Label);
                    cambiarEstadoEntradaAnalogica(Nr.EX, EXProgressBar);
                    cambiarEstadoEntradaAnalogica(Nr.EY, EYProgressBar);
                }
            }
            catch (FishFaceException)
            {
                cambiarEstado(1);
            }
        }

        private void timerCheckInterface_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!encendido)
                {
                    fish.OpenInterface(this.Puerto);
                    cambiarEstado(0);
                }
            }
            catch (FishFaceException)
            {
                cambiarEstado(1);
            }
        }

        private void salirMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }
    }
}
