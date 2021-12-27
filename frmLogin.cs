using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pos_Inventory_Software
{
    public partial class frmLogin : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();

        public static string username, password, usertype, status, fullname;
        public bool found;

        public frmLogin()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
        }

        //this will load all the shop/store information stored in the database
        public void LoadRecord()
        {
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblsettings", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                lblName.Text = dr["name"].ToString();
                lblPhone.Text = dr["phone"].ToString();
                lblEmail.Text = dr["email"].ToString();
                lblWebsite.Text = dr["website"].ToString();
                lblAddress.Text = dr["address"].ToString();
            }
            else
            {
                lblName.Text = "";
                lblPhone.Text = "";
                lblEmail.Text = "";
                lblWebsite.Text = "";
                lblAddress.Text = "";
            }
            dr.Close();
            cn.Close();
        }

        private void InsertLogSuccess()
        {
            cn.Open();
            cm = new SqlCommand("INSERT INTO tbllogs(username,date,time,operation) VALUES(@username,@date,@time,@operation)", cn);
            cm.Parameters.AddWithValue("@username", txtUsername.Text);
            cm.Parameters.AddWithValue("@date", DateTime.Now.ToShortDateString());
            cm.Parameters.AddWithValue("@time", DateTime.Now.ToShortTimeString());
            cm.Parameters.AddWithValue("@operation", "Successfully logged in!");
            cm.ExecuteNonQuery();
            cn.Close();
        }

        //this insert the date, time, user and operation a user performed when logged in
        private void InsertLogFailed()
        {
            cn.Open();
            cm = new SqlCommand("INSERT INTO tbllogs(username,date,time,operation) VALUES(@username,@date,@time,@operation)", cn);
            cm.Parameters.AddWithValue("@username", txtUsername.Text);
            cm.Parameters.AddWithValue("@date", DateTime.Now.ToShortDateString());
            cm.Parameters.AddWithValue("@time", DateTime.Now.ToShortTimeString());
            cm.Parameters.AddWithValue("@operation", "Failed to log in!");
            cm.ExecuteNonQuery();
            cn.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            LoadRecord();
        }

        [Obsolete]
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                txtUsername.Focus();
                MessageBox.Show("Please enter a valid username", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtPassword.Text == "")
            {
                txtPassword.Focus();
                MessageBox.Show("Please enter a valid password", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                //it checks if the username and password exist in the database
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tbluser WHERE username = @username AND password = @password", cn);
                cm.Parameters.AddWithValue("@username", txtUsername.Text);
                cm.Parameters.AddWithValue("@password", txtPassword.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    //this variables then holds the data's found base on the username and password provided
                    username = dr["username"].ToString();
                    fullname = dr["fullname"].ToString();
                    password = dr["password"].ToString();
                    usertype = dr["usertype"].ToString();
                    status = dr["status"].ToString();

                   found = true;
                }
                else
                {    
                    //if username and password provided is invalid, it will prompt an error message
                    cn.Close();
                    InsertLogFailed();
                    MessageBox.Show("Invalid details provided, please check your username and password", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                   // found = false;
                }
                dr.Close();
                cn.Close();

                if (found == true)
                {
                    if (usertype == "Administrator")
                    {
                        if (status == "Active")
                        {
                            InsertLogSuccess();                           
                         //   MessageBox.Show("Welcome, " + fullname, "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPassword.Clear();
                            txtUsername.Clear();
                            var f1 = new frmMainmenu();
                            f1.GetExpiredMedicine();
                            f1.LoadUser();
                            f1.ShowDialog();                           
                            this.Hide();
                        }
                    }
                    else if (usertype == "Cashier")
                    {
                        if (status == "Active")
                        {
                            InsertLogSuccess();
                            //MessageBox.Show("Welcome, " + fullname, "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPassword.Clear();
                            txtUsername.Clear();
                            var f1 = new frmUser();
                            f1.LoadUser();
                            f1.ShowDialog();
                            this.Hide();
                        }
                    }
                    else if (usertype == "Administrator")
                    {
                        if (status == "In-Active")
                        {
                            InsertLogFailed();
                            MessageBox.Show("Your Account is in-active, Contact the admin", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    else if (usertype == "Cashier")
                    {
                        if (status == "In-Active")
                        {
                            InsertLogFailed();
                            MessageBox.Show("Your Account is in-active, Contact the admin", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void lblWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //when the store/shop website address is clicked it will redirect you to the website
            System.Diagnostics.Process.Start("https://" + lblWebsite.Text);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var f1 = new frmForgetPassword();
            f1.ShowDialog();
        }
    }
}
