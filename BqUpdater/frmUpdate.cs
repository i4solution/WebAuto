using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bqInet;

namespace BqUpdater
{
    public partial class frmUpdate : Form
    {
        string _ver = "";
        public frmUpdate()
        {
            InitializeComponent();
        }
        public void SetCurrentVer(string ver)
        {
            _ver = ver;
        }
    }
}
