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
    public partial class frmAdjustQuantity : Form
    {

        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();

        public frmAdjustQuantity()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadRecord()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblmedicine ORDER BY name ASC", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["category"].ToString(), dr["medid"].ToString(), dr["name"].ToString(), dr["batchid"].ToString(), dr["quantity"].ToString(), dr["date1"].ToString(), dr["date2"].ToString(), dr["date3"].ToString(), dr["price"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch
            {
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //if (txtPrice.Text == "" || txtPrice.Text == "0.00" || txtPrice.Text == "0")
            //{
            //    txtPrice.Focus();
            //    MessageBox.Show("الرجاء إدخال مبلغ صحيح!", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            if (txtNewQuantity.Text == "" || txtNewQuantity.Text == "0")
            {
                txtNewQuantity.Focus();
                MessageBox.Show("الرجاء إدخال كمية صحيحة!", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("هل تريد تعديل الكمية ", "رسالة تحقق", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try 
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblmedicine SET date1 = @date1,date2 = @date2,date3 = @date3,quantity = quantity + @quantity,price = @price WHERE batchid = @batchid", cn);
                    cm.Parameters.AddWithValue("@batchid", txtBatchNo.Text);
                    cm.Parameters.AddWithValue("@date1", dateAdded.Text);
                    cm.Parameters.AddWithValue("@date2", dateManufactured.Text);
                    cm.Parameters.AddWithValue("@date3", dateExpired.Text);
                    cm.Parameters.AddWithValue("@quantity", txtNewQuantity.Text);
                    cm.Parameters.AddWithValue("@price", txtPrice.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadRecord();
                    Clear();
                    txtNewQuantity.Text = "0";
                    txtPrice.Text = "0.00";
                    txtQuantity.Text = "0";
                    MessageBox.Show("تم تعديل كمية المنتج", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    return;
                }
            }
                dr.Close();
                cn.Close();
        }

        private void Clear()
        {
            txtBatchNo.Clear();
            txtID.Clear();
            txtName.Clear();
            txtNewQuantity.Text = "0";
            txtPrice.Text = "0.00";
            txtQuantity.Text = "0";
            dateAdded.Text = "";
            dateExpired.Text = "";
            dateManufactured.Text = "";
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

        private void txtNewQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            txtNewQuantity.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblmedicine WHERE name LIKE '%" + txtSearch.Text + "%' ORDER BY name ASC", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["category"].ToString(), dr["medid"].ToString(), dr["name"].ToString(), dr["batchid"].ToString(), dr["quantity"].ToString(), dr["date1"].ToString(), dr["date2"].ToString(), dr["date3"].ToString(), dr["price"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch
            {
                return;
            }
        }

        public void GetCategory()
        {
            try
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
            catch
            {
                return;
            }
        }

        private void cboCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblmedicine WHERE category = @category ORDER BY name ASC", cn);
                cm.Parameters.AddWithValue("@category", cboCat.Text);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["category"].ToString(), dr["medid"].ToString(), dr["name"].ToString(), dr["batchid"].ToString(), dr["quantity"].ToString(), dr["date1"].ToString(), dr["date2"].ToString(), dr["date3"].ToString(), dr["price"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch
            {
                return;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    string ColName = dataGridView1.Columns[e.ColumnIndex].Name;

            //    if (ColName == "ColEdit")
            //    {
            //        cboCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            //        txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            //        txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            //        txtBatchNo.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            //        txtQuantity.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            //        dateAdded.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            //        dateManufactured.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            //        dateExpired.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            //        txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            //    }
            //    else if (ColName == "ColDelete")
            //    {
                   
            //    }
            //}
            //catch
            //{
            //    return;
            //}
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txtBatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cboCategory.Enabled = false;
                    txtName.ReadOnly = true;
                    txtQuantity.ReadOnly = true;
                    txtPrice.ReadOnly = false;
                    dateAdded.Enabled = false;
                    dateManufactured.Enabled = false;
                    dateExpired.Enabled = false;
                    txtID.ReadOnly = true;

                    cm = new SqlCommand("SELECT * FROM tblmedicine WHERE batchid = @BarCode", cn);
                    cm.Parameters.Add(new SqlParameter("@BarCode", txtBatchNo.Text));
                    cn.Open();
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        bool Found = true;
                        if (Found is true)
                        {
                            cboCategory.Text = dr["category"].ToString();
                            txtName.Text = dr["name"].ToString();
                            txtID.Text = dr["medid"].ToString();
                            txtPrice.Text = dr["price"].ToString();
                            txtQuantity.Text = dr["quantity"].ToString();
                            txtBatchNo.Text = dr["batchid"].ToString();
                            dateAdded.Text = dr["date1"].ToString();
                            dateManufactured.Text = dr["date2"].ToString();
                            dateExpired.Text = dr["date3"].ToString();

                        }
                    }
                    else
                    {
                        MessageBox.Show("هذا المنتج غير موجود", "إشعار", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    dr.Close();
                    cn.Close();
                }

            }
            catch
            {
                return;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBatchNo.Text == "" || txtName.Text == "")
                {
                    txtNewQuantity.Focus();
                    MessageBox.Show("باركود المنتج كمية صحيحة!", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (MessageBox.Show("مسح المنتج المحدد ", "رسالة تحقق", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tblmedicine WHERE batchid = @batchid", cn);
                    cm.Parameters.AddWithValue("@batchid", txtBatchNo.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadRecord();
                    MessageBox.Show("تم حذف المنتج المحدد", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                return;
            }
        }
    }
}
