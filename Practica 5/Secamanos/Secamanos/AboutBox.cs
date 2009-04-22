using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Secamanos
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            this.Text = "Acerca de Secamanos";
            this.labelProductName.Text = "Secamanos";
            this.labelVersion.Text = "Versión 1.0";
            this.labelCopyright.Text = "Copyleft © 2009 Jose Domingo López y Raul Arias";
            this.labelCompanyName.Text = "UCLM";
            this.textBoxDescription.Text = "Secamanos es un prototipo de sistema de secado de manos cuyo motor permanece en reposo mientras la célula óptima no detecta presencia (fototransistor en ON), cuando se detecta presencia (fototransistor en OFF) el motor del secador se pone en marcha, y el motor se detiene cuando la célula óptica deja de detectar presencia durante al menos 5 segundos consecutivos (este parámetro es configurable).";
        }
    }
}
