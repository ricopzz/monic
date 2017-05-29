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
    public partial class cust : Form
    {
        int counter = 0;

        public cust()
        {
            InitializeComponent();
        }
        LibraryEntities1 ent = new LibraryEntities1();
        private void cust_Load(object sender, EventArgs e)
        {
            EnabledView();
            refreshData();
        }

        void EnabledView()
        {
            groupBox1.Enabled = false;
            btnNew.Enabled = true;
            btnDelete.Enabled = true;
            customerData.Enabled = true;
            btnUpdate.Enabled = true;
        }

        void EnabledForm()
        {
            groupBox1.Enabled = true;
            btnNew.Enabled = false;
            btnDelete.Enabled = false;
            customerData.Enabled = false;
            btnUpdate.Enabled = false;
        }

        void refreshGroup()
        {
            txtAddress.ResetText();
            txtName.ResetText();
            txtPhone.ResetText();
            txtCode.ResetText();
            rbMale.Checked = false;
            rbFemale.Checked = false;
            boxType.SelectedIndex = -1;
        }

        void refreshData()
        {
            var query = from c in ent.Customers
                        select c;
            customerData.DataSource = query.ToList();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            counter = 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //validation
            if (txtCode.Text == "" || txtName.Text == "" || txtPhone.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Please fill in all data!");
            }
            else if (rbMale.Checked == false && rbFemale.Checked == false)
            {
                MessageBox.Show("Please select gender!");
            }
            else if (boxType.SelectedIndex == -1 || boxType.SelectedItem  == "")
            {
                MessageBox.Show("Please choose customer type!");
            }
            else
            {
                String g;
                if (rbFemale.Checked) g = "Female";
                else g = "Male";

                if (counter == 1)
                {
                    try
                    {
                        var data = new Customer
                        {
                            Code = txtCode.Text,
                            Name = txtName.Text,
                            Phone = txtPhone.Text,
                            Address = txtAddress.Text,
                            Type = boxType.SelectedItem.ToString(),
                            Gender = g
                        };
                        ent.Customers.Add(data);
                        ent.SaveChanges();

                        MessageBox.Show("Success Added New Customer");
                        refreshData();
                        refreshGroup();
                        EnabledView();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Adding New Customer");
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    var query = (from c in ent.Customers
                                 where c.Code.Equals(txtCode.Text)
                                 select c
                        ).First();

                    query.Name = txtName.Text;
                    query.Phone = txtPhone.Text;
                    query.Address = txtAddress.Text;
                    query.Gender = g;
                    query.Type = boxType.SelectedItem.ToString();
                    ent.SaveChanges();
                    refreshData();
                    refreshGroup();
                    EnabledView();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            EnabledView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(customerData.Rows.Count == 0)
            {
                MessageBox.Show("Data Still Empty");
            }
            else
            {
                String code = customerData.CurrentRow.Cells[0].Value.ToString();
                var query = from c in ent.Customers
                            where c.Code.Equals(code)
                            select c;
                ent.Customers.Remove(query.First());
                ent.SaveChanges();
                refreshData();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            EnabledForm();
            txtCode.Text = customerData.CurrentRow.Cells[0].Value.ToString();
            txtAddress.Text = customerData.CurrentRow.Cells[1].Value.ToString();
            txtName.Text = customerData.CurrentRow.Cells[3].Value.ToString();
            txtPhone.Text = customerData.CurrentRow.Cells[4].Value.ToString();

            if (customerData.CurrentRow.Cells[2].Value.ToString() == "Male") rbMale.Checked = true;
            else rbFemale.Checked = true;

            boxType.SelectedItem = customerData.CurrentRow.Cells[5].Value.ToString();

        }
    }
}
