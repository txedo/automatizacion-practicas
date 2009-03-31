using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CheckInterface
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            this.Text = "Acerca de CheckInterface";
            this.labelProductName.Text = "CheckInterface";
            this.labelVersion.Text = "Versión 1.0";
            this.labelCopyright.Text = "Copyleft © 2009 Jose Domingo López y Raul Arias";
            this.labelCompanyName.Text = "UCLM";
            this.textBoxDescription.Text = "CheckInterface es un programa con una funcionalidad similar a la herramienta \"Check Interface\" incluida en LLWin.\r\nCheckInterface permite visualizar en todo instante el estado de las entradas de la interfaz y ajustar el estado deseado para cualquiera de las salidas.";
        }
    }
}
