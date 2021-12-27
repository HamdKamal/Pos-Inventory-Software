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
    public partial class frmCustomer : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();

        public frmCustomer()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Clear()
        {
            txtEmail.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtID.Text = "";
            txtAddress.Text = "";

        }

        public void GetID()
        {
            cn.Open();
            cm = new SqlCommand("SELECT COUNT(*) FROM tblcustomer", cn);
            string num = cm.ExecuteScalar().ToString();
            cn.Close();

            Random myrand = new Random();
            int mynum = myrand.Next(20, 1000);
            txtID.Text = "C-001" + num + mynum;
        }

        public void LoadRecord()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblcustomer ORDER BY name ASC", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["cusid"].ToString(), dr["name"].ToString(), dr["phoneno"].ToString(), dr["email"].ToString(), dr["address"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                txtName.Focus();
                MessageBox.Show("الرجاء إدخال إسم العميل", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtPhone.Text == "")
            {
                txtPhone.Focus();
                MessageBox.Show("الرجاء إدخال رقم الهاتف", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtEmail.Text == "")
            {
                txtEmail.Focus();
                MessageBox.Show("الرجاء إدخال الإيميل", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("حفظ بيانات العميل", "رسالة تحقق", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cn.Open();
                cm = new SqlCommand("INSERT INTO tblcustomer(cusid,name,email,phoneno,address) VALUES(@sid,@name,@email,@phoneno,@address)", cn);
                cm.Parameters.AddWithValue("@sid", txtID.Text);
                cm.Parameters.AddWithValue("@name", txtName.Text);
                cm.Parameters.AddWithValue("@phoneno", txtPhone.Text);
                cm.Parameters.AddWithValue("@email", txtEmail.Text);
                cm.Parameters.AddWithValue("@address", txtAddress.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                LoadRecord();
                Clear();
                GetID();
                MessageBox.Show("تم حفظ بيانات العميل", "رسالة تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                txtName.Focus();
                MessageBox.Show("الرجاء إدخال إسم العميل", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtPhone.Text == "")
            {
                txtPhone.Focus();
                MessageBox.Show("الرجاء إدخال رقم الهاتف", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtEmail.Text == "")
            {
                txtEmail.Focus();
                MessageBox.Show("الرجاء إدخال الإيميل", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Update Customer Information?, أضغط yes للتأكيد  !", "ALERT", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cn.Open();
                cm = new SqlCommand("UPDATE tblcustomer SET name = @name,phoneno = @phoneno,email = @email,address = @address WHERE cusid = @cusid", cn);
                cm.Parameters.AddWithValue("@cusid", txtID.Text);
                cm.Parameters.AddWithValue("@name", txtName.Text);
                cm.Parameters.AddWithValue("@phoneno", txtPhone.Text);
                cm.Parameters.AddWithValue("@email", txtEmail.Text);
                cm.Parameters.AddWithValue("@address", txtAddress.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                LoadRecord();
                Clear();
                GetID();
                MessageBox.Show("تم تعديل بيانات العميل", "رسالة تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (ColName == "ColEdit")
            {
                btnUpdate.Enabled = true;
                btnSave.Enabled = false;
                txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtPhone.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            else if (ColName == "ColDelete")
            {
                if (frmLogin.usertype == "Administrator")
                {
                    if (MessageBox.Show("حذف بيانات العميل", "رسالة تحقق", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("DELETE FROM tblcustomer WHERE cusid = @cusid", cn);
                        cm.Parameters.AddWithValue("@cusid", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                        cm.ExecuteNonQuery();
                        cn.Close();
                        LoadRecord();
                        GetID();
                        MessageBox.Show("تم حذف بيانات العميل", "رسالة تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("عملية الحذف غير ممكنة", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblcustomer WHERE name LIKE '%" + txtSearch.Text + "%' ORDER BY name ASC", cn);
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["cusid"].ToString(), dr["name"].ToString(), dr["phoneno"].ToString(), dr["email"].ToString(), dr["address"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtName.Focus();
            GetID();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
