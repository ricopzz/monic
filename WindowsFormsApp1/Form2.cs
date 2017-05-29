using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        cust custForm;
        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (custForm==null)
            {
                custForm = new cust();
                custForm.Show();
                custForm.MdiParent = this;
            }
            else if(custForm.IsDisposed)
            {
                custForm = new cust();
                custForm.Show();
                custForm.MdiParent = this;
            }
        }
    }
}
