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
using Pos_Inventory_Software;

namespace Pos_Inventory_Software
{
    public partial class frmLogin : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();

        public static string username, password, usertype, status, fullname;
        public static string Atitle, Etitle, Address, Phone;

        public bool found;

        [Obsolete]
        public frmLogin()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
        }
        public static string Username;
        public static string Password;
        public static string Fullname;
        public static string Usertype;
        //this will load all the shop/store information stored in the database
        public void LoadRecord()
        {
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblsettings", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    Atitle = dr["name"].ToString();
                    Phone = dr["phone"].ToString();
                    Address = dr["address"].ToString();
                    Etitle = dr["website"].ToString();

                }
                else
                {
                    Atitle = "";
                    Phone = "";
                    Address = "";
                    Etitle = "";
                }
            }
            catch
            {
                return;
            }
            dr.Close();
            cn.Close();
        }
        private void InsertLogSuccess()
        {
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
            try
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
            catch
            {
                return;
            }
           
        }

        //this insert the date, time, user and operation a user performed when logged in
        private void InsertLogFailed()
        {
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
            try
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
            catch
            {
                return;
            }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
            txtUsername.Text = "";
            txtPassword.Text = "";
            try
            {
                if (Pos_Inventory_Software.Properties.Settings.Default.Counter >= 1)
                    return;
                else if (Pos_Inventory_Software.Properties.Settings.Default.Counter == 0)
                {
                    Server_Confiq SYS = new Server_Confiq();
                    SYS.ShowDialog();
                    this.Hide();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("فشل الإتصال بالسيرفر","رسالة تنبيه",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            txtUsername.Focus();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            var time = DateTime.Now;
            lblTime.Text = time.ToString("h:mm:ss tt");

            lblDate.Text = time.ToLongDateString();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var time = DateTime.Now;
            lblTime.Text = time.ToString("h:mm:ss tt");

            lblDate.Text = time.ToLongDateString();
        }
        [Obsolete]
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text == "")
                {
                    txtUsername.Focus();
                    MessageBox.Show("! رجاءً قم بإدخال إسم مستخدم صحيح", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtPassword.Text == "")
                {
                    txtPassword.Focus();
                    MessageBox.Show("! رجاءً قم بإدخال كلمة مرور صحيحة", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    if (cn.State == System.Data.ConnectionState.Open)
                    {
                        cn.Close();
                    }
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
                        MessageBox.Show("الرجاء التأكد من كلمة السر وأسم المستخدم", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                                this.Hide();
                                f1.ShowDialog();
                            }
                        }
                        else if (usertype == "Cashier")
                        {
                            if (status == "Active")
                            {
                                if (cn.State == System.Data.ConnectionState.Open)
                                {
                                    cn.Close();
                                }
                                InsertLogSuccess();
                                //MessageBox.Show("Welcome, " + fullname, "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtPassword.Clear();
                                txtUsername.Clear();
                                var f1 = new frmUser();
                                f1.LoadUser();
                                this.Hide();
                                f1.ShowDialog();
                            }
                        }
                        else if (usertype == "Administrator")
                        {
                            if (status == "In-Active")
                            {
                                InsertLogFailed();
                                MessageBox.Show("لا يمكن تسجيل الدخول بهذا الحساب", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        else if (usertype == "Cashier")
                        {
                            if (status == "In-Active")
                            {
                                InsertLogFailed();
                                MessageBox.Show("لا يمكن تسجيل الدخول بهذا الحساب,الرجاء مراجعة مدير النظام", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void lblForgetPassword_Click(object sender, EventArgs e)
        {
            var f1 = new frmForgetPassword();
            f1.ShowDialog();
        }
    }
}
