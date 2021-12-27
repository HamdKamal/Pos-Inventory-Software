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
    public partial class frmSystemSettings : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();

        public frmSystemSettings()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "image files(*.bmp;*.jpg;*.png;*.gif)|*.bmp*;*.jpg;*.png;*.gif;";

            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picLogo.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            picLogo.Image = picLogo.InitialImage;
        }

        public void Clear()
        {
            txtWebsite.Clear();
            txtRegNo.Clear();
            txtPhone.Clear();
            txtName.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            picLogo.Image = picLogo.InitialImage;
            LoadRecord();
            txtName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtAddress.Text == "" || txtEmail.Text == "" || txtPhone.Text == "" || txtRegNo.Text == "")
                {
                    MessageBox.Show("! رجاءً قم بإدخال جميع الحقول", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (MessageBox.Show("هل تريد حفظ بيانات المتجر ؟", "رسالة تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    byte[] bytImage = new byte[0];
                    MemoryStream ms = new System.IO.MemoryStream();
                    Bitmap bmpImage = new Bitmap(picLogo.Image);

                    bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ms.Seek(0, 0);
                    bytImage = ms.ToArray();
                    ms.Close();

                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tblsettings", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblsettings(name,phone,email,website,picture,regno,address) VALUES(@name,@phone,@email,@website,@picture,@regno,@address)", cn);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cm.Parameters.AddWithValue("@email", txtEmail.Text);
                    cm.Parameters.AddWithValue("@website", txtWebsite.Text);
                    cm.Parameters.AddWithValue("@picture", bytImage);
                    cm.Parameters.AddWithValue("@regno", txtRegNo.Text);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadRecord();
                    MessageBox.Show("تم حفظ بيانات المتجر بنجاح", "رسالة تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                return;
            }
            }

        public void LoadRecord()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblsettings", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    txtName.Text = dr["name"].ToString();
                    txtPhone.Text = dr["phone"].ToString();
                    txtEmail.Text = dr["email"].ToString();
                    txtRegNo.Text = dr["regno"].ToString();
                    txtWebsite.Text = dr["website"].ToString();
                    txtAddress.Text = dr["address"].ToString();

                    byte[] data = (byte[])dr["picture"];
                    MemoryStream ms = new MemoryStream(data);
                    picLogo.Image = Image.FromStream(ms);
                }
                else
                {
                    txtName.Text = "";
                    txtPhone.Text = "";
                    txtEmail.Text = "";
                    txtRegNo.Text = "";
                    txtAddress.Text = "";
                    txtWebsite.Text = "";

                    picLogo.Image = picLogo.InitialImage;
                }
                dr.Close();
                cn.Close();

            }
            catch
            {
                return;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("هل تريد حذف بيانات المتجر ؟", "رسالة تحقق", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    byte[] bytImage = new byte[0];
                    MemoryStream ms = new System.IO.MemoryStream();
                    Bitmap bmpImage = new Bitmap(picLogo.Image);

                    bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ms.Seek(0, 0);
                    bytImage = ms.ToArray();
                    ms.Close();

                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tblsettings", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadRecord();
                    MessageBox.Show("! تم حذف بيانات المتجر بنجاح", "رسالة تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                return;
            }
            }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
