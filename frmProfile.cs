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
using System.IO;

namespace Pos_Inventory_Software
{

    public partial class frmProfile : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();

        public frmProfile()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ( txtFullName.Text == "" || txtPassword.Text == "" || txtCPassword.Text == "")
            {
                MessageBox.Show("Please, all field are required!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtPassword.Text != txtCPassword.Text)
            {
                MessageBox.Show("Password does not match!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Make Changes?, أضغط yes للتأكيد  !", "ALERT", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    byte[] bytImage = new byte[0];
                    MemoryStream ms = new System.IO.MemoryStream();
                    Bitmap bmpImage = new Bitmap(picUser.Image);

                    bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ms.Seek(0, 0);
                    bytImage = ms.ToArray();
                    ms.Close();

                    cn.Close();
                    cn.Open();
                    cm = new SqlCommand("UPDATE tbluser SET fullname = @fullname,password = @password,picture = @picture WHERE username = @username", cn);
                    cm.Parameters.AddWithValue("@username", frmLogin.username);
                    cm.Parameters.AddWithValue("@fullname", txtFullName.Text);
                    cm.Parameters.AddWithValue("@password", txtPassword.Text);
                    cm.Parameters.AddWithValue("@picture", bytImage);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadUserDetails();
                }
                catch
                {
                    return;
                }  
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "image files(*.bmp;*.jpg;*.png;*.gif)|*.bmp*;*.jpg;*.png;*.gif;";

            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picUser.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            picUser.Image = picUser.InitialImage;
        }

        public void LoadUserDetails()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tbluser WHERE username = @username", cn);
                cm.Parameters.AddWithValue("@username", frmLogin.username);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    txtFullName.Text = dr["fullname"].ToString();
                    txtPassword.Text = dr["password"].ToString();
                    txtCPassword.Text = dr["password"].ToString();

                    byte[] data = (byte[])dr["picture"];
                    MemoryStream ms = new MemoryStream(data);
                    picUser.Image = Image.FromStream(ms);
                }
                else
                {
                    txtFullName.Text = "";
                    txtPassword.Text = "";
                    txtPassword.Text = "";

                    picUser.Image = picUser.InitialImage;
                }
                dr.Close();
                cn.Close();
            }
            catch
            {
                return;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
