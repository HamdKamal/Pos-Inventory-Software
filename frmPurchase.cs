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
    public partial class frmPurchase : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();

        public string medid;

        public frmPurchase()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
        }

        public void GetID()
        {
            cn.Open();
            cm = new SqlCommand("SELECT COUNT(DISTINCT(purchaseid)) FROM tblcart", cn);
            string num = cm.ExecuteScalar().ToString();
            cn.Close();

            Random myrand = new Random();
            int mynum = myrand.Next(20, 1000);

            txtID.Text = "P-" + DateTime.Now.Year + "-011" + num + mynum;
        }

        public void GetSupplier()
        {
            cboSupplier.Items.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblsupplier", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboSupplier.Items.Add(dr["name"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        public void GetCategory()
        {
            cboCategory.Items.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblcategory", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboCategory.Items.Add(dr["name"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        public void GetMedicine()
        {
            cboMedicine.Items.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblmedicine WHERE category = @category", cn);
            cm.Parameters.AddWithValue("@category", cboCategory.Text);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboMedicine.Items.Add(dr["name"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        public void GetMedicineID()
        {
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblmedicine WHERE name = @name AND category = @category", cn);
            cm.Parameters.AddWithValue("@name", cboMedicine.Text);
            cm.Parameters.AddWithValue("@category", cboCategory.Text);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                txtMedID.Text = dr["medid"].ToString();
            }
            else
            {
                txtMedID.Text = "";
                if (cboMedicine.Text == "")
                {
                    txtMedID.Text = "";
                }
            }
            dr.Close();
            cn.Close();
        }

        public void GetTotal()
        {
            if (dataGridView1.Rows.Count < 1)
            {
                lblTotal.Text = "0.00";
            }
            else
            {
                cn.Open();
                cm = new SqlCommand("SELECT SUM(ptotal) FROM tblcart WHERE purchaseid = @purchaseid", cn);
                cm.Parameters.AddWithValue("@purchaseid", txtID.Text);
                string num = cm.ExecuteScalar().ToString();
                cn.Close();

                lblTotal.Text = num;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtBuyingPrice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cboSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboMedicine_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;
        }

        private void cboPaymentMode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboPaymentStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtChange_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtAmountPaid_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMedicine();
        }

        private void cboMedicine_SelectedIndexChanged(object sender, EventArgs e)
        {
           GetMedicineID();
        }

        public void LoadCart()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblcart WHERE purchaseid = @purchaseid ORDER BY medname ASC", cn);
            cm.Parameters.AddWithValue("@purchaseid", txtID.Text);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["medid"].ToString(), dr["medname"].ToString(), dr["quantity"].ToString(), dr["price"].ToString(), dr["ptotal"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboSupplier.Text == "")
            {
                MessageBox.Show("Please select a valid supplier name!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (cboMedicine.Text == "")
            {
                MessageBox.Show("Please select a valid medicine name!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtQuantity.Text == "" || Convert.ToInt16(txtQuantity.Text) < 1)
            {
                txtQuantity.Focus();
                MessageBox.Show("Please enter a valid quantity!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtBuyingPrice.Text == "" || Convert.ToDecimal(txtBuyingPrice.Text) < 1)
            {
                txtBuyingPrice.Focus();
                MessageBox.Show("Please enter a valid buying price!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtPrice.Text == "" || Convert.ToDecimal(txtPrice.Text) < 1)
            {
                txtPrice.Focus();
                MessageBox.Show("Please enter a valid selling price!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Add to Cart?, أضغط yes للتأكيد  !", "ALERT", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cn.Close();
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblcart WHERE purchaseid = @purchaseid AND medid = @medid", cn);
                cm.Parameters.AddWithValue("@purchaseid", txtID.Text);
                cm.Parameters.AddWithValue("@medid", txtMedID.Text);
                dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    cn.Close();
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblcart SET quantity = quantity + @quantity ,ptotal = quantity * price WHERE purchaseid = @purchaseid AND medid = @medid", cn);
                    cm.Parameters.AddWithValue("@purchaseid", txtID.Text);
                    cm.Parameters.AddWithValue("@medid", txtMedID.Text);
                    cm.Parameters.AddWithValue("@quantity", Convert.ToInt32(txtQuantity.Text));
                    cm.ExecuteNonQuery();
                    cn.Close();

                    cn.Open();
                    cm = new SqlCommand("UPDATE tblmedicine SET quantity = quantity + @quantity WHERE medid = @medid", cn);
                    cm.Parameters.AddWithValue("@medid", txtMedID.Text);
                    cm.Parameters.AddWithValue("@quantity", txtQuantity.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadCart();
                    GetTotal();
                }
                else
                {
                    cn.Close();
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblcart(purchaseid,medid,medname,quantity,price,ptotal) VALUES(@purchaseid,@medid,@medname,@quantity,@price,@ptotal)", cn);
                    cm.Parameters.AddWithValue("@purchaseid", txtID.Text);
                    cm.Parameters.AddWithValue("@medid", txtMedID.Text);
                    cm.Parameters.AddWithValue("@medname", cboMedicine.Text);
                    cm.Parameters.AddWithValue("@quantity", Convert.ToInt32(txtQuantity.Text));
                    cm.Parameters.AddWithValue("@price", Convert.ToDecimal(txtBuyingPrice.Text));
                    cm.Parameters.AddWithValue("@ptotal", Convert.ToDecimal(txtBuyingPrice.Text) * Convert.ToInt32(txtQuantity.Text));
                    cm.ExecuteNonQuery();
                    cn.Close();

                    cn.Open();
                    cm = new SqlCommand("UPDATE tblmedicine SET name = @name,quantity = quantity + @quantity,date1 = @date1,date2 = @date2,date3 = @date3,price = @price WHERE medid = @medid", cn);
                    cm.Parameters.AddWithValue("@medid", txtMedID.Text);
                    cm.Parameters.AddWithValue("@name", cboMedicine.Text);
                    cm.Parameters.AddWithValue("@quantity", txtQuantity.Text);
                    cm.Parameters.AddWithValue("@date1", dtpDateAdded.Text);
                    cm.Parameters.AddWithValue("@date2", dtpManufactured.Text);
                    cm.Parameters.AddWithValue("@date3", dtpExpiryDate.Text);
                    cm.Parameters.AddWithValue("@price", txtPrice.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadCart();
                    GetTotal();
                }
                dr.Close();
                cn.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dataGridView1.Columns[e.ColumnIndex].Name;
            medid = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            string qty = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            if (ColName == "ColDelete")
            {
                if (MessageBox.Show("Remove Item?, أضغط yes للتأكيد  !", "ALERT", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tblcart WHERE medid = @medid", cn);
                    cm.Parameters.AddWithValue("@medid", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    cm.ExecuteNonQuery();
                    cn.Close();

                    cn.Open();
                    cm = new SqlCommand("UPDATE tblmedicine SET quantity = quantity - @quantity WHERE medid = @medid", cn);
                    cm.Parameters.AddWithValue("@medid", medid);
                    cm.Parameters.AddWithValue("@quantity", qty);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadCart();
                    GetTotal();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAmountPaid.Text == "" || txtAmountPaid.Text == "0.00")
            {
                txtAmountPaid.Focus();
                MessageBox.Show("Please enter a valid amount to pay!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (lblTotal.Text == "0.00" || lblTotal.Text == "")
            {
                MessageBox.Show("Cart can't be empty!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Convert.ToDecimal(txtAmountPaid.Text) < Convert.ToDecimal(lblTotal.Text))
            {
                txtAmountPaid.Focus();
                MessageBox.Show("Cannot proceed with transaction! \n Insufficient Amount Paid!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtDiscount.Text == "")
            {
                txtDiscount.Focus();
                MessageBox.Show("Discount field is empty!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Convert.ToDouble(txtDiscount.Text) < 1 && Convert.ToDouble(txtDiscount.Text) > 100)
            {
                txtDiscount.Focus();
                MessageBox.Show("Discount can't be less than 0 and greater than 100!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (cboPaymentMode.Text == "")
            {
                cboPaymentMode.Focus();
                MessageBox.Show("Please select a valid payment mode!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (cboPaymentStatus.Text == "")
            {
                cboPaymentStatus.Focus();
                MessageBox.Show("Please select a valid payment status!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Save Transaction?, أضغط yes للتأكيد  !", "ALERT", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                decimal num = Convert.ToDecimal(txtAmountPaid.Text) - Convert.ToDecimal(lblTotal.Text);
                txtChange.Text = num.ToString();

                cn.Open();
                cm = new SqlCommand("INSERT INTO tblpurchaselist(purchaseid,suppliername,amountpaid,total,schange,paymenttype,paymentstatus,discount,note,date,time) VALUES(@purchaseid,@suppliername,@amountpaid,@total,@schange,@paymenttype,@paymentstatus,@discount,@note,@date,@time)", cn);
                cm.Parameters.AddWithValue("@purchaseid", txtID.Text);
                cm.Parameters.AddWithValue("@suppliername", cboSupplier.Text);
                cm.Parameters.AddWithValue("@amountpaid", txtAmountPaid.Text);
                cm.Parameters.AddWithValue("@total", lblTotal.Text);
                cm.Parameters.AddWithValue("@schange", txtChange.Text);
                cm.Parameters.AddWithValue("@paymenttype", cboPaymentMode.Text);
                cm.Parameters.AddWithValue("@paymentstatus", cboPaymentStatus.Text);
                cm.Parameters.AddWithValue("@discount", txtDiscount.Text);
                cm.Parameters.AddWithValue("@note", txtNote.Text);
                cm.Parameters.AddWithValue("@date", DateTime.Now.ToShortDateString());
                cm.Parameters.AddWithValue("@time", DateTime.Now.ToShortTimeString());
                cm.ExecuteNonQuery();
                cn.Close();
                GetID();
                LoadCart();
                lblTotal.Text = "0.00";
                txtAmountPaid.Text = "0.00";
                txtChange.Text = "0.00";
                txtDiscount.Text = "0.00";
                txtQuantity.Text = "0.00";
                txtPrice.Text = "0.00";
                txtBuyingPrice.Text = "0.00";
                MessageBox.Show("Purchased has been saved succesfully!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            var f1 = new frmPurchaseList();
            f1.GetCategory();
            f1.LoadRecord();
            f1.ShowDialog();
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

        private void btnAdjustment_Click(object sender, EventArgs e)
        {
            var f1 = new frmAdjustQuantity();
            f1.LoadRecord();
            f1.GetCategory();
            f1.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cboPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
