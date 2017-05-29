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
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LibraryEntities1 ent = new LibraryEntities1();
            var query = from l in ent.Users
                        where l.Username.Equals(txtUsername.Text) &&
                        l.Password.Equals(txtPassword.Text)
                        select l;

            if(query.ToList().Count() == 0)
            {
                MessageBox.Show("Username or Password is wrong");
            }
            else
            {
                MessageBox.Show("Login Success");
                Form2 nextForm = new Form2();
                this.Hide();
                nextForm.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
