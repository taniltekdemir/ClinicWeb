using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinik.WinUI
{
    public partial class Main: Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //label1.Text = Program.Token;
        }

        private void sapleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTest frm = new FormTest();
            frm.MdiParent = this;
            frm.Show();
        }

        private void sample3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTest frm = new FormTest();
            frm.MdiParent = this;
            frm.Show();
        }

        private void sample4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTest frm = new FormTest();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
