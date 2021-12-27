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
    public partial class frmPOS : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();

        public string medid;

        public string getholdid;

        public decimal discount, total, result;
        private void InsertLogPos()
        {
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
            try
            {
                cn.Open();
                cm = new SqlCommand("INSERT INTO tbllogs(username,date,time,operation) VALUES(@username,@date,@time,@operation)", cn);
                cm.Parameters.AddWithValue("@username", frmLogin.fullname);
                cm.Parameters.AddWithValue("@date", DateTime.Now.ToShortDateString());
                cm.Parameters.AddWithValue("@time", DateTime.Now.ToShortTimeString());
                cm.Parameters.AddWithValue("@operation", "تمت إضافة عملية بيع بقيمة '"+ lblGrandTotal.Text +"'");
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch
            {
                return;
            }

        }

        public frmPOS()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                this.Dispose();
        }

        public void GetID()
        {
            cn.Open();
            cm = new SqlCommand("SELECT COUNT(DISTINCT(invoiceid)) FROM tblcart2", cn);
            string num = cm.ExecuteScalar().ToString();
            cn.Close();

            Random myrand = new Random();
            int mynum = myrand.Next(20, 1000);

            lblInvoiceNo.Text = "IVC" + DateTime.Now.Year + "10-" + num + mynum;
        }

        //public void GetCategory()
        //{
        //    cboCategory.Items.Clear();
        //    cn.Open();
        //    cm = new SqlCommand("SELECT * FROM tblcategory", cn);
        //    dr = cm.ExecuteReader();          
        //    while (dr.Read())
        //    {               
        //        cboCategory.Items.Add(dr["name"].ToString());
        //    }
        //    dr.Close();
        //    cn.Close();
        //}
        public void GetDiscount()
        {
            try
            {
                txtDiscount.Items.Clear();
                cn.Open();
                cm = new SqlCommand("SELECT * FROM Discount", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    txtDiscount.Items.Add(dr["D_Money"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch
            {
                return;
            }
        }
        public void GetCustomer()
        {       
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblcustomer", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                
            }
            dr.Close();
            cn.Close();
        }

        private void txtInStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
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
            try
            {
                var Paid = Convert.ToDecimal(txtAmountPaid.Text);
                var Total = Convert.ToDecimal(lblTotal.Text);
                var Change = Convert.ToDecimal(txtChange.Text);

                if (Paid > Total)
                {
                    MessageBox.Show("! المبلغ المدفوع أكبر من إجمالي الفاتورة", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = Total - Paid;
                Change = result;
            }
            catch
            {
                return;
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

        private void cboPaymentMode_KeyPress(object sender, KeyPressEventArgs e)
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


        private void GetRemainingStock()
        {
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblmedicine WHERE medid = @medid", cn);
            cm.Parameters.AddWithValue("@medid", txtMedID.Text);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                txtInStock.Text = dr["quantity"].ToString();
            }
            dr.Close();
            cn.Close();
        }

        //private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    cboMedicine.Items.Clear();
        //    cn.Close();
        //    cn.Open();
        //    cm = new SqlCommand("SELECT * FROM tblmedicine WHERE category = @category", cn);
        //    cm.Parameters.AddWithValue("@category", cboCategory.Text);
        //    dr = cm.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        cboMedicine.Items.Add(dr["name"].ToString());
        //    }
        //    dr.Close();
        //    cn.Close();
        //}

        private void cboMedicine_SelectedIndexChanged(object sender, EventArgs e)
        {
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblmedicine WHERE name = @name", cn);
            cm.Parameters.AddWithValue("@name", cboMedicine.Text);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                txtMedID.Text = dr["medid"].ToString();
                txtBatchID.Text = dr["batchid"].ToString();
                txtInStock.Text = dr["quantity"].ToString();
                txtPrice.Text = dr["price"].ToString();
            }
            dr.Close();
            cn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboCategory.Text == "")
            {
                MessageBox.Show("الرجاء إختيار الفئة", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (cboMedicine.Text == "")
            {
                MessageBox.Show("الرجاء إختيار المنتج", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtQuantity.Text == "" || Convert.ToInt16(txtQuantity.Text) < 1)
            {
                txtQuantity.Focus();
                MessageBox.Show("الرجاء إدخال كمية صحيحة", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtPrice.Text == "" || Convert.ToDecimal(txtPrice.Text) < 1)
            {
                txtPrice.Focus();
                MessageBox.Show("سعر البيع لا يمكن أن يكون صفر أو سالب", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtInStock.Text == "" || Convert.ToDecimal(txtInStock.Text) < 1)
            {
                txtInStock.Focus();
                MessageBox.Show("كمية المخزن منتهية ", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtDiscount.Text == "")
            {
                MessageBox.Show("التخفيض لا يمكن أن يكون قيمة فارغة", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("إضافة إلى سلة المشتريات", "رسالة تحقق", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    cn.Close();
                    cn.Open();
                    cm = new SqlCommand("SELECT * FROM tblcart2 WHERE invoiceid = @invoiceid AND medid = @medid", cn);
                    cm.Parameters.AddWithValue("@invoiceid", lblInvoiceNo.Text);
                    cm.Parameters.AddWithValue("@medid", txtMedID.Text);
                    dr = cm.ExecuteReader();
                    if (dr.HasRows)
                    {
                        cn.Close();
                        cn.Open();
                        cm = new SqlCommand("UPDATE tblcart2 SET quantity = quantity + @quantity ,invoicetotal = quantity * price WHERE invoiceid = @invoiceid AND medid = @medid", cn);
                        cm.Parameters.AddWithValue("@invoiceid", lblInvoiceNo.Text);
                        cm.Parameters.AddWithValue("@medid", txtMedID.Text);
                        cm.Parameters.AddWithValue("@quantity", Convert.ToInt32(txtQuantity.Text));
                        cm.ExecuteNonQuery();
                        cn.Close();

                        cn.Open();
                        cm = new SqlCommand("UPDATE tblmedicine SET quantity = quantity - @quantity WHERE medid = @medid", cn);
                        cm.Parameters.AddWithValue("@medid", txtMedID.Text);
                        cm.Parameters.AddWithValue("@quantity", txtQuantity.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        LoadCart();
                        GetTotal();
                        GetRemainingStock();
                        txtQuantity.Text = "0";
                    }
                    else
                    {
                        var Sales_Price = Convert.ToDecimal(txtPrice.Text);
                        var discount = Convert.ToDecimal(txtDiscount.Text);
                        var Quan = Convert.ToDecimal(txtQuantity.Text);
                        var Price = Sales_Price - discount;
                        var totinvoice = Price * Quan;
                        cn.Close();
                            cn.Open();
                            cm = new SqlCommand("INSERT INTO tblcart2(invoiceid,medid,medname,quantity,price,invoicetotal,status,discount) VALUES(@invoiceid,@medid,@medname,@quantity,@price,@invoicetotal,@status,@discount)", cn);
                            cm.Parameters.AddWithValue("@invoiceid", lblInvoiceNo.Text);
                            cm.Parameters.AddWithValue("@medid", txtMedID.Text);
                            cm.Parameters.AddWithValue("@medname", cboMedicine.Text);
                            cm.Parameters.AddWithValue("@quantity", Convert.ToInt32(txtQuantity.Text));
                            cm.Parameters.AddWithValue("@price", Convert.ToDecimal(Price));
                            cm.Parameters.AddWithValue("@invoicetotal",Convert.ToDecimal(totinvoice));
                            cm.Parameters.AddWithValue("@status", "Pending");
                            cm.Parameters.AddWithValue("@discount", Convert.ToDecimal(discount * Quan));
                            cm.ExecuteNonQuery();
                            cn.Close();

                            cn.Open();
                            cm = new SqlCommand("UPDATE tblmedicine SET quantity = quantity - @quantity WHERE medid = @medid", cn);
                            cm.Parameters.AddWithValue("@medid", txtMedID.Text);
                            cm.Parameters.AddWithValue("@quantity", txtQuantity.Text);
                            cm.ExecuteNonQuery();
                            cn.Close();
                            LoadCart();
                            GetTotal();
                            GetRemainingStock();
                            txtQuantity.Text = "0";
                        dr.Close();
                        cn.Close();
                    }
                }
                catch
                {
                    return;
                }
                foreach(Control item in this.Controls)
                {
                    if(item is TextBox)
                    {
                        item.Text = String.Empty;
                    }
                }
            }
            txtQuantity.Text = "0";
        }
        public void GetTotal()
        {
            if (dataGridView1.Rows.Count < 1)
            {
                lblTotal.Text = "0.00";
                lblGrandTotal.Text = "0.00";
            }
            else
            {
                cn.Open();
                cm = new SqlCommand("SELECT SUM(invoicetotal) FROM tblcart2 WHERE invoiceid = @invoiceid", cn);
                cm.Parameters.AddWithValue("@invoiceid", lblInvoiceNo.Text);
                string num = cm.ExecuteScalar().ToString();
                cn.Close();

                lblTotal.Text = num;
                lblGrandTotal.Text = num;
            }
        }

        public void LoadCart()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblcart2 INNER JOIN  [tblmedicine] AS M ON tblcart2.medid = M.medid WHERE invoiceid = @invoiceid order BY medname", cn);
            cm.Parameters.AddWithValue("@invoiceid", lblInvoiceNo.Text);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                var Q = Convert.ToDecimal(dr["quantity"].ToString());
                var P = Convert.ToDecimal(dr["price"].ToString());
                var T = Q * P;

                dataGridView1.Rows.Add(i, dr["batchid"].ToString(), dr["medname"].ToString(), dr["quantity"].ToString(), dr["price"].ToString(), T ,dr["medid"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void cboCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboMedicine_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dataGridView1.Columns[e.ColumnIndex].Name;
            medid = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            string qty = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            if (ColName == "ColDelete")
            {
                if (MessageBox.Show("هل تريد حذف هذا العنصر من السلة", "رسالة تحقق", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tblcart2 WHERE medid = @medid", cn);
                    cm.Parameters.AddWithValue("@medid", medid);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    cn.Open();
                    cm = new SqlCommand("UPDATE tblmedicine SET quantity = quantity + @quantity WHERE medid = @medid", cn);
                    cm.Parameters.AddWithValue("@medid", medid);
                    cm.Parameters.AddWithValue("@quantity", qty);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadCart();
                    GetTotal();
                }
            }
        }
        public decimal Commation;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAmountPaid.Text == "")
            {
                txtAmountPaid.Focus();
                MessageBox.Show("المبلغ المدفوع لا يمكن أن يكون صفر", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Convert.ToDecimal(txtAmountPaid.Text) < Convert.ToDecimal(lblGrandTotal.Text))
            {
                txtAmountPaid.Focus();
                MessageBox.Show("! المبلغ المدفوع أقل من إجمالي الفاتورة ", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cboPaymentMode.Text == "")
            {
                cboPaymentMode.Focus();
                MessageBox.Show("الرجاء التأكد من طريقة الدفع", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtAmountPaid.Text == "0.00" || Convert.ToDecimal(txtAmountPaid.Text) < 1)
            {
                txtAmountPaid.Focus();
                MessageBox.Show("الرجاء إدخال مبلغ صحيح", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dataGridView1.Rows.Count < 1)
            {
                lblTotal.Text = "0.00";
                lblGrandTotal.Text = "0.00";
                txtAmountPaid.Text = "0.00";
                txtChange.Text = "0.00";
                txtDiscount.Text = "0.00";
                MessageBox.Show("القائمة فارغة", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("هل تريد حفظ هذه الفاتورة", "رسالة تحقق", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var Tot = Convert.ToDecimal(txtAmountPaid.Text);
                    var comm = Convert.ToDecimal(3);
                    Commation = (comm / 100) * Tot;
                    cn.Open();
                    cm = new SqlCommand("SELECT * FROM tblsalespayment WHERE invoiceid = @invoiceid", cn);
                    cm.Parameters.AddWithValue("@invoiceid", lblInvoiceNo.Text);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //decimal num = Convert.ToDecimal(txtAmountPaid.Text) - Convert.ToDecimal(lblGrandTotal.Text);
                        //txtChange.Text = num.ToString();

                        cn.Close();
                        cn.Open();
                        cm = new SqlCommand("UPDATE tblsalespayment SET cusname = @cusname,cusphone = @cusphone,saletotal = @saletotal,grandtotal = @grandtotal,amountpaid = @amountpaid,schange = @schange,discount = @discount,paymode = @paymode,status = @status,date = @date,time = @time,E_Commation = @E_Commation WHERE invoiceid = @invoiceid", cn);
                        cm.Parameters.AddWithValue("@invoiceid", lblInvoiceNo.Text);
                        cm.Parameters.AddWithValue("@cusname", "WALK IN CUSTOMER");
                        cm.Parameters.AddWithValue("@cusphone", "0000-000-0000");
                        cm.Parameters.AddWithValue("@saletotal", lblTotal.Text);
                        cm.Parameters.AddWithValue("@grandtotal", lblGrandTotal.Text);
                        cm.Parameters.AddWithValue("@amountpaid", txtAmountPaid.Text);
                        cm.Parameters.AddWithValue("@schange", txtChange.Text);
                        cm.Parameters.AddWithValue("@discount", txtDiscount.Text);
                        cm.Parameters.AddWithValue("@paymode", cboPaymentMode.Text);
                        cm.Parameters.AddWithValue("@status", "Settled"); 
                        cm.Parameters.AddWithValue("@date", DateTime.Now.ToShortDateString());
                        cm.Parameters.AddWithValue("@time", DateTime.Now.ToShortTimeString());
                        cm.Parameters.AddWithValue("@E_Commation", Commation.ToString());
                        cm.ExecuteNonQuery();
                        cn.Close();

                        cn.Open();
                        cm = new SqlCommand("UPDATE tblcart2 SET status = @status WHERE invoiceid = @invoiceid", cn);
                        cm.Parameters.AddWithValue("@invoiceid", lblInvoiceNo.Text);
                        cm.Parameters.AddWithValue("@status", "Settled");
                        cm.ExecuteNonQuery();
                        cn.Close();
                        InsertLogPos();

                        //var f1 = new frmSalesReceipt();
                        //f1.invoiceid = this.lblInvoiceNo.Text;
                        //f1.ShowDialog();
                        GetID();
                        LoadCart();
                        lblTotal.Text = "0.00";
                        lblGrandTotal.Text = "0.00";
                        txtAmountPaid.Text = "0.00";
                        txtChange.Text = "0.00";
                        txtDiscount.Text = "0.00";
                        txtQuantity.Text = "0";
                        txtPrice.Text = "0.00";
                        txtBatchID.Clear();
                        cboMedicine.Text = "";
                        txtMedID.Clear();
                        txtInStock.Text = "0.00";
                        MessageBox.Show("تم حفظ فاتورة البيع بنجاح", "رسالة تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        //decimal num = Convert.ToDecimal(txtAmountPaid.Text) - Convert.ToDecimal(lblGrandTotal.Text);
                        //txtChange.Text = num.ToString();

                        cn.Close();
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tblsalespayment(invoiceid,cusname,cusphone,saletotal,grandtotal,amountpaid,schange,discount,paymode,status,cashier,date,time,E_Commation) VALUES(@invoiceid,@cusname,@cusphone,@saletotal,@grandtotal,@amountpaid,@schange,@discount,@paymode,@status,@cashier,@date,@time,@E_Commation)", cn);
                        cm.Parameters.AddWithValue("@invoiceid", lblInvoiceNo.Text);
                        cm.Parameters.AddWithValue("@cusname", "WALK IN CUSTOMER");
                        cm.Parameters.AddWithValue("@cusphone", "0000-000-0000");
                        cm.Parameters.AddWithValue("@saletotal", lblTotal.Text);
                        cm.Parameters.AddWithValue("@grandtotal", lblGrandTotal.Text);
                        cm.Parameters.AddWithValue("@amountpaid", txtAmountPaid.Text);
                        cm.Parameters.AddWithValue("@schange", txtChange.Text);
                        cm.Parameters.AddWithValue("@discount", txtDiscount.Text);
                        cm.Parameters.AddWithValue("@paymode", cboPaymentMode.Text);
                        cm.Parameters.AddWithValue("@status", "Settled");
                        cm.Parameters.AddWithValue("@cashier", frmLogin.fullname.ToUpper());
                        cm.Parameters.AddWithValue("@date", DateTime.Now.ToShortDateString());
                        cm.Parameters.AddWithValue("@time", DateTime.Now.ToShortTimeString());
                        cm.Parameters.AddWithValue("@E_Commation", Commation.ToString());
                        cm.ExecuteNonQuery();
                        cn.Close();

                        cn.Open();
                        cm = new SqlCommand("UPDATE tblcart2 SET status = @status WHERE invoiceid = @invoiceid", cn);
                        cm.Parameters.AddWithValue("@invoiceid", lblInvoiceNo.Text);
                        cm.Parameters.AddWithValue("@status", "Settled");
                        cm.ExecuteNonQuery();
                        cn.Close();
                        InsertLogPos();

                        //var f1 = new frmSalesReceipt();
                        //f1.invoiceid = this.lblInvoiceNo.Text;
                        //f1.ShowDialog();
                        GetID();
                        LoadCart();
                        lblTotal.Text = "0.00";
                        lblGrandTotal.Text = "0.00";
                        txtAmountPaid.Text = "0.00";
                        txtChange.Text = "0.00";
                        txtDiscount.Text = "0.00";
                        txtQuantity.Text = "0";
                        txtPrice.Text = "0.00";
                        txtBatchID.Clear();
                        cboMedicine.Text = "";
                        txtMedID.Clear();
                        txtInStock.Text = "0.00";
                        MessageBox.Show("تم حفظ فاتورةالبيع بنجاح", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    dr.Close();
                    cn.Close();
                }
                catch
                {
                    return;
                }
                
            }
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    var Sales_Price = Convert.ToDecimal(txtPrice.Text);
            //    var discount = Convert.ToDecimal(txtDiscount.SelectedText);
            //    var Quan = Convert.ToDecimal(txtQuantity.Text);

            //    if (Convert.ToDecimal(txtDiscount.SelectedText) < 0 && Convert.ToDecimal(txtDiscount.SelectedText) > 100)
            //    {
            //        MessageBox.Show("التخفيض لا يمكن أن يكون أقل من 0 ولا أكبر من 100", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //    //var result = discount * Quan;
            //    var gettotal = (Sales_Price * Quan);
            //    var i = gettotal - discount;
            //    lblGrandTotal.Text = gettotal.ToString();
            //    lblTotal.Text = i.ToString();
            //}
            //catch
            //{
            //    return;
            //}            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToLongDateString();
            lblTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("! لا توجد مشتريات في السلة", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            if (dataGridView1.Rows.Count >= 1)
            {
                MessageBox.Show("! الرجاء مسح المنتجات الموجودة في سلة المشتريات", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                GetID();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtInvoiceSearch.Text == String.Empty)
            {
                txtInvoiceSearch.Focus();
                txtInvoiceSearch.BackColor = Color.BlanchedAlmond;
                MessageBox.Show("الرجاء إدخال رقم الفاتورة", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                GetCartData();
                GetCustomerData();                              
            }
        }
        String C_Name, C_Phone;
        private void GetCustomerData()
        {
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblsalespayment WHERE invoiceid = @invoiceid AND status = @status", cn);
            cm.Parameters.AddWithValue("@invoiceid", txtInvoiceSearch.Text);
            cm.Parameters.AddWithValue("@status", "Settled");
            dr = cm.ExecuteReader();
            dr.Read();
            if(dr.HasRows)
            {
                txtAmountPaid.Text = dr["amountpaid"].ToString();
                lblGrandTotal.Text = dr["grandtotal"].ToString();
                lblTotal.Text = dr["saletotal"].ToString();
                C_Name = dr["cusname"].ToString();
                C_Phone = dr["cusphone"].ToString();
                txtChange.Text = dr["schange"].ToString();
                txtDiscount.Text = dr["discount"].ToString();
                cboPaymentMode.Text = dr["paymode"].ToString();
                lblInvoiceNo.Text = dr["invoiceid"].ToString();
            }
            else
            {
                cn.Close();
                MessageBox.Show("رقم الفاتورة : " + txtInvoiceSearch.Text + " غير موجود", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dr.Close();
            cn.Close();
        }

        private void GetCartData()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblcart2 WHERE invoiceid = @invoiceid AND status = @status", cn);
            cm.Parameters.AddWithValue("@invoiceid", txtInvoiceSearch.Text);
            cm.Parameters.AddWithValue("@status", "Settled");
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["medid"].ToString(), dr["medname"].ToString(), dr["quantity"].ToString(), dr["price"].ToString(), dr["invoicetotal"].ToString());
            }
            dr.Close();
            cn.Close();
        }


        private void btnGet_Click(object sender, EventArgs e)
        {
            if (txtHold.Text == String.Empty)
            {
                txtHold.Focus();
                txtHold.BackColor = Color.BlanchedAlmond;
                MessageBox.Show("الرجاء إدخال رقم الفاتورة", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {               
                GetItemsOnHold();
                //GetCustomerOnHold();
            }
        }

        private void GetItemsOnHold()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblcart2 WHERE invoiceid = @invoiceid AND status = @status", cn);
            cm.Parameters.AddWithValue("@invoiceid", txtHold.Text);
            cm.Parameters.AddWithValue("@status", "Hold");
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["medid"].ToString(), dr["medname"].ToString(), dr["quantity"].ToString(), dr["price"].ToString(), dr["invoicetotal"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        public void GetItemsOnHold2()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblcart2 WHERE invoiceid = @invoiceid AND status = @status", cn);
            cm.Parameters.AddWithValue("@invoiceid", getholdid);
            cm.Parameters.AddWithValue("@status", "Hold");
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["medid"].ToString(), dr["medname"].ToString(), dr["quantity"].ToString(), dr["price"].ToString(), dr["invoicetotal"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void GetCustomerOnHold()
        {
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblsalespayment WHERE invoiceid = @invoiceid AND status = @status", cn);
            cm.Parameters.AddWithValue("@invoiceid", txtHold.Text);
            cm.Parameters.AddWithValue("@status", "Hold");
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                bool found = true;

                if (found == true)
                {
                    C_Name = dr["cusname"].ToString();
                    C_Phone = dr["cusphone"].ToString();
                    lblTotal.Text = dr["saletotal"].ToString();
                    lblGrandTotal.Text = dr["grandtotal"].ToString();
                    txtAmountPaid.Text = dr["amountpaid"].ToString();
                    txtChange.Text = dr["schange"].ToString();
                    txtDiscount.Text = dr["discount"].ToString();
                    cboPaymentMode.Text = dr["paymode"].ToString();
                    lblInvoiceNo.Text = dr["invoiceid"].ToString();
                }
            }
            else
            {
                cn.Close();
                MessageBox.Show("غير متوفرة الفاتورة", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dr.Close();
            cn.Close();
        }

        private void txtBatchID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cboCategory.Enabled = false;
                    cboMedicine.Enabled = false;
                    txtInStock.ReadOnly = true;
                    txtPrice.ReadOnly = true;
                    cm = new SqlCommand("SELECT * FROM tblmedicine WHERE batchid = @BarCode", cn);
                    cm.Parameters.Add(new SqlParameter("@BarCode", txtBatchID.Text));
                    cn.Open();
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        bool Found = true;
                        if (Found is true)
                        {
                            cboCategory.Text = dr["category"].ToString();
                            cboMedicine.Text = dr["name"].ToString();
                            txtMedID.Text = dr["medid"].ToString();
                            txtPrice.Text = dr["price"].ToString();
                            txtInStock.Text = dr["quantity"].ToString();
                            txtBatchID.Text = dr["batchid"].ToString();
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

        private void txtBatchID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAmountPaid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtAmountPaid_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtAmountPaid_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAmountPaid_Leave(object sender, EventArgs e)
        {
            try
            {
                var Paid = Convert.ToDecimal(txtAmountPaid.Text);
                var Total = Convert.ToDecimal(lblTotal.Text);
                var Change = Convert.ToDecimal(txtChange.Text);

                if (Paid > Total)
                {
                    MessageBox.Show("! المبلغ المدفوع أكبر من إجمالي الفاتورة", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = Total - Paid;
                Change = result;
            }
            catch
            {
                return;
            }

        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            GetDiscount();
        }

        private void txtDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtDiscount.Text) < 0 && Convert.ToDecimal(txtDiscount.Text) > 100)
            {
                MessageBox.Show("التخفيض لا يمكن أن يكون أقل من 0 ولا أكبر من 100", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void txtMedID_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count >= 1)
            {
                MessageBox.Show("! الرجاء مسح المنتجات الموجودة في سلة المشتريات","رسالة تنبيه",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Dispose();
                Close();
            }
        }

        //public void GetCustomerOnHold2()
        //{
        //    cn.Open();
        //    cm = new SqlCommand("SELECT * FROM tblsalespayment WHERE invoiceid = @invoiceid AND status = @status", cn);
        //    cm.Parameters.AddWithValue("@invoiceid", getholdid);
        //    cm.Parameters.AddWithValue("@status", "Hold");
        //    dr = cm.ExecuteReader();
        //    dr.Read();
        //    if (dr.HasRows)
        //    {
        //        string phone;
        //        txtAmountPaid.Text = dr["amountpaid"].ToString();
        //        lblGrandTotal.Text = dr["grandtotal"].ToString();
        //        lblTotal.Text = dr["saletotal"].ToString();
        //        C_Name = dr["cusname"].ToString();
        //        phone = dr["cusphone"].ToString();
        //        C_Phone = phone;
        //        txtChange.Text = dr["schange"].ToString();
        //        txtDiscount.Text = dr["discount"].ToString();
        //        cboPaymentMode.Text = dr["paymode"].ToString();
        //        lblInvoiceNo.Text = dr["invoiceid"].ToString();
        //    }
        //    else
        //    {
        //        cn.Close();
        //        MessageBox.Show(" الفاتورة غير متوفرة ", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
        //    dr.Close();
        //    cn.Close();
        //}

        private void btnSalesRecord_Click(object sender, EventArgs e)
        {
            this.Dispose();
            var f1 = new frmSalesRecord();
            f1.LoadRecord();
            f1.ShowDialog();
        }
    }
}
