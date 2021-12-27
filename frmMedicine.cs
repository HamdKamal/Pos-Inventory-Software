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
    public partial class frmMedicine : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();

        string id;
        public void JustNumber(KeyPressEventArgs r)
        {
            if (!char.IsDigit(r.KeyChar))
            {
                if (!char.IsControl(r.KeyChar))
                {
                    if (!char.IsPunctuation(r.KeyChar))
                    {
                        if (!char.IsWhiteSpace(r.KeyChar))
                        {
                            if (!char.IsSeparator(r.KeyChar))
                                r.Handled = true;
                        }
                    }
                }
            }
        }
        public frmMedicine()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
        }

        public void GetCategory()
        {
            cboCat.Items.Clear();
            cboCategory.Items.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblcategory", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboCat.Items.Add(dr["name"].ToString());
                cboCategory.Items.Add(dr["name"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        public void GetID()
        {
            cn.Open();
            cm = new SqlCommand("SELECT COUNT(*) FROM tblmedicine", cn);
            string num = cm.ExecuteScalar().ToString();
            cn.Close();
            Random myrand = new Random();
            int mynum = myrand.Next(20, 1000);
            txtID.Text = "M-001" + num + mynum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public void Clear()
        {
            txtBar.Clear();
            txtID.Clear();
            txtName.Clear();
            cboCategory.Text = "";
        }

        public void LoadRecord()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblmedicine ORDER BY name ASC", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["category"].ToString(), dr["medid"].ToString(), dr["name"].ToString(), dr["batchid"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboCategory.Text == "")
            {
                MessageBox.Show("Please select category", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtID.Text == "")
            {
                txtID.Focus();
                MessageBox.Show("الرجاء إدخال باركود المنتج", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtName.Text == "")
            {
                txtName.Focus();
                MessageBox.Show("الرجاء إدخال إسم المنتج", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtBar.Text == "")
            {
                txtBar.Focus();
                MessageBox.Show("الرجاء إدخال رقم الدفعة", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("هل تريد حفظ البيانات", "رسالة تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblmedicine WHERE batchid = @batchid", cn);
                cm.Parameters.AddWithValue("@batchid", txtBar.Text);
                dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Close();
                    cn.Close();
                    MessageBox.Show("! باركود المنتج موجود مسبقاً ", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                dr.Close();
                cn.Close();

                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblmedicine WHERE name = @name", cn);
                cm.Parameters.AddWithValue("@name", txtName.Text);
                dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Close();
                    cn.Close();
                    MessageBox.Show("إسم المنتج موجود مسبقاً", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                dr.Close();
                cn.Close();

                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblmedicine WHERE medid = @medid", cn);
                cm.Parameters.AddWithValue("@medid", txtID.Text);
                dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Close();
                    cn.Close();
                    MessageBox.Show("الرقم التسلسلي للمنتج موجود مسبقاً", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    dr.Close();
                    //cn.Open();
                    cm = new SqlCommand("INSERT INTO tblmedicine(category,medid,name,batchid,quantity,date1,date2,date3,price) VALUES(@category,@medid,@name,@batchid,@quantity,@date1,@date2,@date3,@price)", cn);
                    cm.Parameters.AddWithValue("@category", cboCategory.Text);
                    cm.Parameters.AddWithValue("@medid", txtID.Text);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@batchid", txtBar.Text);
                    cm.Parameters.AddWithValue("@quantity", "0");
                    cm.Parameters.AddWithValue("@date1", "");
                    cm.Parameters.AddWithValue("@date2", "");
                    cm.Parameters.AddWithValue("@date3", "");
                    cm.Parameters.AddWithValue("@price", "0");
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadRecord();
                    Clear();
                    GetID();
                    MessageBox.Show("تم حفظ بيانات المنتج", "رسالة تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dr.Close();
                cn.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (ColName == "ColEdit")
            {
                btnUpdate.Enabled = true;
                btnSave.Enabled = false;
                cboCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                id = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtBar.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            else if (ColName == "ColDelete")
            {
                if (MessageBox.Show("هل تريد حذف المنتج المحدد", "رسالة تحقق", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tblmedicine WHERE medid = @medid", cn);
                    cm.Parameters.AddWithValue("@medid", dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadRecord();
                    GetID();
                    MessageBox.Show("تم حذف بيانات المنتج", "رسالة تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            txtName.Focus();
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            GetID();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cboCategory.Text == "")
            {
                MessageBox.Show("الرجاء إختيار الفئة", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtID.Text == "")
            {
                txtID.Focus();
                MessageBox.Show("الرجاء إدخال باركود المنتج", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtName.Text == "")
            {
                txtName.Focus();
                MessageBox.Show("الرجاء إدخال إسم المنتج", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtBar.Text == "")
            {
                txtBar.Focus();
                MessageBox.Show("الرجاء إدخال رقم الدفعة", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("تعديل بيانات المنتج", "رسالة تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblmedicine WHERE name = @name", cn);
                cm.Parameters.AddWithValue("@name", txtName.Text);
                dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Close();
                    cn.Close();
                    MessageBox.Show("رقم الدفعة موجود مسبقاً", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    dr.Close();
                    //cn.Open();
                    cm = new SqlCommand("UPDATE tblmedicine SET category = @category,medid = @medid,name = @name,batchid = @batchid WHERE medid = '" + id + "'", cn);
                    cm.Parameters.AddWithValue("@category", cboCategory.Text);
                    cm.Parameters.AddWithValue("@medid", txtID.Text);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@batchid", txtBar.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadRecord();
                    Clear();
                    GetID();
                    MessageBox.Show("تم تعديل المنتج", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dr.Close();
                cn.Close();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblmedicine WHERE name LIKE '%" + txtSearch.Text + "%' ORDER BY name ASC", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["category"].ToString(), dr["medid"].ToString(), dr["name"].ToString(), dr["batchid"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void txtSearch3_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblmedicine WHERE batchid LIKE '" + txtSearch3.Text + "%' ORDER BY name ASC", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["category"].ToString(), dr["medid"].ToString(), dr["name"].ToString(), dr["batchid"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void cboCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblmedicine WHERE category LIKE '" + cboCat.Text + "%' ORDER BY name ASC", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["category"].ToString(), dr["medid"].ToString(), dr["name"].ToString(), dr["batchid"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtBar_Leave(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("select batchid as Bar from tblmedicine WHERE batchid = '" + txtBar.Text + "'", cn);
                SqlDataReader red = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                string ttx = (txtBar.Text);

                if (red.HasRows)
                {
                    red.Read();
                    if (red["Bar"].ToString() == ttx)
                    {
                        MessageBox.Show("تنبيه ! تم إضافة هذا المنتج من قبل  ");
                        txtBar.Text = "";
                        txtBar.Focus();
                    }
                    else
                    {
                        txtName.Focus();
                    }
                }
                red.Close();
                cn.Close();
            }
            catch
            {
                return;
            }

        }

        private void txtBar_KeyPress(object sender, KeyPressEventArgs e)
        {
            JustNumber(e);
        }

        private void frmMedicine_Load(object sender, EventArgs e)
        {
            GetID();
        }
    }
}
