using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualInterpreterV2
{
    public partial class Form1 : Form
    {
        private Form aboutForm = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aboutForm != null)
                return;

            aboutForm = new AboutForm();
            aboutForm.Show();
            aboutForm.Disposed += AboutFormCleanup;
        }

        public void AboutFormCleanup(object o, EventArgs e)
        {
            aboutForm = null;
        }
    }
}
