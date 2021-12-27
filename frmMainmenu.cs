using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Pos_Inventory_Software.Report;
using DevExpress.XtraReports.UI;

namespace Pos_Inventory_Software
{
    public partial class frmMainmenu : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();

        public IList<string> dt; //assign a dt to store all list of product in stock

        [Obsolete]
        public frmMainmenu()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
        }

        private void frmMainmenu_Load(object sender, EventArgs e)
        {
            GetLowStock();
            GetLow();
            GetCat1();

            //CheckExpiredMedicine();
            //GetExpiredMedicine();
            GetExpiredMed();
        }

        //to get all category name 
        public void GetCat1()
        {
            cboCat.Items.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblcategory", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboCat.Items.Add(dr["name"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        //to get the numbers of categories 
        public void GetCategory()
        {
            cn.Close();
            cn.Open();
            cm = new SqlCommand("SELECT COUNT(*) FROM tblcategory", cn);
            string num = cm.ExecuteScalar().ToString();
            cn.Close();

            lblCategory.Text = num;
        }

        //to get the total numbers of medicines
        public void GetMedicine()
        {
            cn.Open();
            cm = new SqlCommand("SELECT COUNT(*) FROM tblmedicine", cn);
            string num = cm.ExecuteScalar().ToString();
            cn.Close();

            lblMedicine.Text = num;
        }

        //to get the total numbers of customers
        public void GetCustomer()
        {
            cn.Open();
            cm = new SqlCommand("SELECT Sum(Money) FROM Expenses", cn);
            string num = cm.ExecuteScalar().ToString();
            cn.Close();

            lblCustomer.Text = num;
        }

        //to get the total sum of quntities of medicines
        public void GetStockQuantity()
        {
            cn.Open();
            cm = new SqlCommand("SELECT SUM(quantity) FROM tblmedicine", cn);
            string num = cm.ExecuteScalar().ToString();
            cn.Close();

            if (num == String.Empty || num == "0")
            {
                lblStock.Text = "0";
            }
            else
            {               
                lblStock.Text = num;
            }
        }

        //to get the total numbers of suppliers
        public void GetSupplier()
        {
            cn.Open();
            cm = new SqlCommand("SELECT COUNT(*) FROM tblsupplier", cn);
            string num = cm.ExecuteScalar().ToString();
            cn.Close();

            lblSupplier.Text = num;
        }

        //to get the total numbers of available users
        public void GetUser()
        {
            cn.Open();
            cm = new SqlCommand("SELECT COUNT(*) FROM tbluser", cn);
            string num = cm.ExecuteScalar().ToString();
            cn.Close();

            lblUser.Text = num;
        }

        public void GetLow()
        {
            cn.Open();
            cm = new SqlCommand("SELECT COUNT(quantity) FROM tblmedicine WHERE quantity < 1", cn);
            string num = cm.ExecuteScalar().ToString();
            cn.Close();

            lblLowStockFound.Text = "عدد المنتجات التي وجدت: " + num;
        }

        //to check for expired medicines
        public void CheckExpiredMedicine()
        {
            //dt = new List<string>();
            //var curDate = DateTime.Today.Date;

            //da = new SqlDataReader("SELECT * FROM tblmedicine", cn);
            //DataTable listofData = new DataTable();
            //da.Fill(listofData);
            //if (listofData.Rows.Count > 0)
            //{
            //    //loop through all data in datatable
            //    foreach (DataRow row in listofData.Rows)
            //    {
            //        //check whether current greater than or equal to expiry date set
            //        //if (curDate.Date >= Convert.ToDateTime(row["date3"]))
            //        if (curDate.Date >= Convert.ToDateTime(row["date3"]))
            //        {
            //            //add it into the public datatable by adding the id and name of the expired product
            //            dt.Add(row["medid"].ToString());
            //        }
            //    }
            //}
        }

        public void GetExpiredMed()
        {
            dataGridView2.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblmedicine WHERE date3 <= @today", cn);
            cm.Parameters.AddWithValue("@today", DateTime.UtcNow.ToShortDateString());
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                i++;
                dataGridView2.Rows.Add(i, dr["batchid"].ToString(), dr["name"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        //to load expired medicines into datagrid
        public void GetExpiredMedicine()
        {
            //dt = new List<string>();
            //dataGridView1.Rows.Clear();
            //foreach (var row in dt)
            //{
            //    da = new SqlDataReader("SELECT * FROM tblmedicine WHERE medid = '" + row + "'", cn);
            //    DataTable dtt = new DataTable();
            //    da.Fill(dtt);
            //    if (dtt.Rows.Count > 0)
            //    {
            //        dataGridView1.Rows.Add(dtt.Rows[0][2].ToString(), dtt.Rows[0][3].ToString(), dtt.Rows[0][7].ToString());
            //    }
                //int i = 0;
                //cn.Open();
                //cm = new SqlCommand("SELECT * FROM tblmedicine WHERE medid = @medid", cn);
                //cm.Parameters.AddWithValue("@medid", row);
                //dr.Read();
                //if (dr.HasRows)
                //{
                //    i++;
                //    dataGridView1.Rows.Add(i, dr["medid"].ToString(), dr["name"].ToString());
                //}
                //dr.Close();
                //cn.Close();
            //}
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            var f1 = new frmCategory();
            f1.LoadRecord();
            f1.btnUpdate.Enabled = false;
            f1.ShowDialog();
        }

        //get the sum of medicines sold for the day
        public void GetTodaySale()
        {
            cn.Open();
            cm = new SqlCommand("SELECT SUM(grandtotal) FROM tblsalespayment WHERE date = @date", cn);
            cm.Parameters.AddWithValue("@date", DateTime.Now.ToShortDateString());
            string num = cm.ExecuteScalar().ToString();
            cn.Close();

            if (num != String.Empty)
            {
                lblTodaySale.Text = num;
            }
            else
            {
                lblTodaySale.Text = "0.00";
            }
        }

        //to get the sum of all sold medicines
        public void GetAllTimeSale()
        {
            cn.Open();
            cm = new SqlCommand("SELECT SUM(grandtotal) FROM tblsalespayment", cn);
            string num = cm.ExecuteScalar().ToString();
            cn.Close();

            if (num != String.Empty)
            {
                lblAllTimeSale.Text = num;
            }
            else
            {
                lblAllTimeSale.Text = "0.00";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToLongDateString();
            lblTime.Text = DateTime.Now.ToLongTimeString();

            GetCategory();
            GetMedicine();
            GetUser();
            GetSupplier();
            GetStockQuantity();
            GetCustomer();
            GetAllTimeSale();
            GetTodaySale();
            LoadUserPic();
        }

        private void btnMedicine_Click(object sender, EventArgs e)
        {
            var f1 = new frmMedicine();
            f1.GetCategory();
            f1.GetID();
            f1.LoadRecord();
            f1.btnUpdate.Enabled = false;
            f1.ShowDialog();
        }

        private void btnStoreSettings_Click(object sender, EventArgs e)
        {
            var f1 = new frmSystemSettings();
            f1.LoadRecord();
            f1.ShowDialog();
        }

        //this insert the date, time, user and operation a user performed when logged in
        private void InsertLogSuccess()
        {
            cn.Open();
            cm = new SqlCommand("INSERT INTO tbllogs(username,date,time,operation) VALUES(@username,@date,@time,@operation)", cn);
            cm.Parameters.AddWithValue("@username", frmLogin.username);
            cm.Parameters.AddWithValue("@date", DateTime.Now.ToShortDateString());
            cm.Parameters.AddWithValue("@time", DateTime.Now.ToShortTimeString());
            cm.Parameters.AddWithValue("@operation", "Successfully logged out!");
            cm.ExecuteNonQuery();
            cn.Close();
        }

        [Obsolete]
        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" هل تريد تسجيل الخروج من البرنامج ؟", "تسجيل الخروج", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();
                }

                InsertLogSuccess();
                this.Hide();
                var f1 = new frmLogin();
                f1.Show();
            }
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            var f1 = new frmUserManagement();
            f1.btnUpdate.Enabled = false;
            f1.LoadRecord();
            f1.ShowDialog();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            var f1 = new frmSupplier();
            f1.btnUpdate.Enabled = false;
            f1.GetID();
            f1.LoadRecord();
            f1.ShowDialog();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            var f1 = new frmPurchase();
            f1.GetID();
            f1.GetSupplier();
            f1.GetCategory();
            f1.ShowDialog();
        }

        private void btnStockList_Click(object sender, EventArgs e)
        {
            var f1 = new frmStockList();
            f1.GetCategory();
            f1.LoadRecord();
            f1.ShowDialog();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            var f1 = new frmCustomer();
            f1.btnUpdate.Enabled = false;
            f1.GetID();
            f1.LoadRecord();
            f1.ShowDialog();
        }

        public void GetLowStock()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblmedicine WHERE quantity < 1 ORDER BY name ASC", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["category"].ToString(), dr["medid"].ToString(), dr["name"].ToString(), dr["batchid"].ToString(), dr["quantity"].ToString());
            }
            dr.Close();
            cn.Close();

            GetDatagridCount();
        }

        //this is use to set the column color of medicines less than 1 to red color
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToInt32(row.Cells["Column6"].Value) < 1)
                {
                    row.Cells["Column6"].Style.BackColor = Color.Red;
                }
            }
        }

        private void cboCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblmedicine WHERE quantity < 5 AND category LIKE '" + cboCat.Text + "%' ORDER BY name ASC", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["category"].ToString(), dr["medid"].ToString(), dr["name"].ToString(), dr["batchid"].ToString(), dr["quantity"].ToString());
            }
            dr.Close();
            cn.Close();

            GetDatagridCount();
        }

        private void GetDatagridCount()
        {
            string num = dataGridView1.Rows.Count.ToString();
            lblLowStockFound.Text = "Total Found: " + num;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblmedicine WHERE quantity < 5 AND name LIKE '%" + txtSearch.Text + "%' ORDER BY name ASC", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["category"].ToString(), dr["medid"].ToString(), dr["name"].ToString(), dr["batchid"].ToString(), dr["quantity"].ToString());
            }
            dr.Close();
            cn.Close();

            GetDatagridCount();
        }

        //this will load user details from database 
        public void LoadUser()
        {
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tbluser WHERE username = @username", cn);
            cm.Parameters.AddWithValue("@username", frmLogin.username);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                lblFullName.Text = dr["fullname"].ToString();
                lblUsertype.Text = dr["usertype"].ToString();

                //byte[] data = (byte[])dr["picture"];
                //MemoryStream ms = new MemoryStream(data);
                //picUser.Image = Image.FromStream(ms);
            }
            else
            {
                lblFullName.Text = "";
                lblUsertype.Text = "";

                //picUser.Image = picUser.InitialImage;
            }
            dr.Close();
            cn.Close();
        }

        public void LoadUserPic()
        {
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tbluser WHERE username = @username", cn);
            cm.Parameters.AddWithValue("@username", frmLogin.username);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                try
                {
                    byte[] data = (byte[])dr["picture"];
                    MemoryStream ms = new MemoryStream(data);
                    //picUser.Image = Image.FromStream(ms);
                }
                catch
                {
                    return;
                }

                lblFullName.Text = dr["fullname"].ToString();
            }
            else
            {
                //picUser.Image = picUser.InitialImage;
            }
            dr.Close();
            cn.Close();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            var f1 = new frmSalesRecord();
            f1.LoadRecord();
            f1.ShowDialog();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();

            string ColName = dataGridView2.Columns[e.ColumnIndex].Name;

            if (ColName == "Column11")
            {
                var f1 = new frmPreview();
                f1._id = id;
                f1.LoadDetails();
                f1.ShowDialog();
            }
            else if (ColName == "Column12")
            {
                var f1 = new frmPreview();
                f1._id = id;
                f1.LoadDetails();
                f1.ShowDialog();
            }

            
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            var f1 = new frmLogs();
            f1.GetUser();
            f1.ShowDialog();
        }

        private void btnAdjust_Click(object sender, EventArgs e)
        {
            var f1 = new frmAdjustQuantity();
            f1.LoadRecord();
            f1.GetCategory();
            f1.ShowDialog();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCategory_MouseHover(object sender, EventArgs e)
        {
            btnCategory.ForeColor = SystemColors.GrayText;

        }

        private void btnSupplier_MouseHover(object sender, EventArgs e)
        {
            btnSupplier.ForeColor = SystemColors.GrayText;
        }

        private void btnCustomer_MouseHover(object sender, EventArgs e)
        {
            btnCustomer.ForeColor = SystemColors.GrayText;
        }

        private void btnPurchase_MouseHover(object sender, EventArgs e)
        {
            btnPurchase.ForeColor = SystemColors.GrayText;
        }

        private void btnMedicine_MouseHover(object sender, EventArgs e)
        {
            btnMedicine.ForeColor = SystemColors.GrayText;
        }

        private void btnStockList_MouseHover(object sender, EventArgs e)
        {
            btnStockList.ForeColor = SystemColors.GrayText;
        }

        private void btnSales_MouseHover(object sender, EventArgs e)
        {
            btnSales.ForeColor = SystemColors.GrayText;
        }

        private void btnStoreSettings_MouseHover(object sender, EventArgs e)
        {
            btnStoreSettings.ForeColor = SystemColors.GrayText;
        }

        private void btnUserManagement_MouseHover(object sender, EventArgs e)
        {
            btnUserManagement.ForeColor = SystemColors.GrayText;
        }

        private void btnLogs_MouseHover(object sender, EventArgs e)
        {
            btnLogs.ForeColor = SystemColors.GrayText;
        }

        private void btnAdjust_MouseHover(object sender, EventArgs e)
        {
            btnAdjust.ForeColor = SystemColors.GrayText;
        }

        private void btnLogout_MouseHover(object sender, EventArgs e)
        {
            btnLogout.ForeColor = SystemColors.GrayText;
        }

        private void btnCategory_MouseLeave(object sender, EventArgs e)
        {
            btnCategory.ForeColor = Color.White;
        }

        private void btnSupplier_MouseLeave(object sender, EventArgs e)
        {
            btnSupplier.ForeColor = Color.White;
        }

        private void btnCustomer_MouseLeave(object sender, EventArgs e)
        {
            btnCustomer.ForeColor = Color.White;
        }

        private void btnPurchase_MouseLeave(object sender, EventArgs e)
        {
            btnPurchase.ForeColor = Color.White;
        }

        private void btnMedicine_MouseLeave(object sender, EventArgs e)
        {
            btnMedicine.ForeColor = Color.White;
        }

        private void btnStockList_MouseLeave(object sender, EventArgs e)
        {
            btnStockList.ForeColor = Color.White;
        }

        private void btnSales_MouseLeave(object sender, EventArgs e)
        {
            btnSales.ForeColor = Color.White;
        }

        private void btnStoreSettings_MouseLeave(object sender, EventArgs e)
        {
            btnStoreSettings.ForeColor = Color.White;
        }

        private void btnUserManagement_MouseLeave(object sender, EventArgs e)
        {
            btnUserManagement.ForeColor = Color.White;
        }

        private void btnLogs_MouseLeave(object sender, EventArgs e)
        {
            btnLogs.ForeColor = Color.White;
        }

        private void btnAdjust_MouseLeave(object sender, EventArgs e)
        {
            btnAdjust.ForeColor = Color.White;
        }

        private void btnLogout_MouseLeave(object sender, EventArgs e)
        {
            btnLogout.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f1 = new frmExpenses();
            f1.LoadRecord();
            f1.ShowDialog();
        }

        private void btnBac_Click(object sender, EventArgs e)
        {
            var f1 = new frmDatabase();
            f1.ShowDialog();
        }

        private void PICsys_Click(object sender, EventArgs e)
        {
            var f1 = new frmProject();
            f1.ShowDialog();
        }

        private void PICdev_Click(object sender, EventArgs e)
        {
            var f1 = new frmDeveloper();
            f1.ShowDialog();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.ForeColor = SystemColors.GrayText;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.ForeColor = SystemColors.GrayText;
        }

        private void btnBac_MouseHover(object sender, EventArgs e)
        {
            btnBac.ForeColor = SystemColors.GrayText;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.White;
        }

        private void btnBac_MouseLeave(object sender, EventArgs e)
        {
            btnBac.ForeColor = Color.White;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            var f1 = new frmPOS();
            f1.GetID();
            f1.GetCustomer();
            f1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f1 = new frmReport();
            f1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var f1 = new frmDiscount();
            f1.ShowDialog();
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            btnBac.ForeColor = SystemColors.GrayText;

        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            btnBac.ForeColor = Color.White;

        }
    }
}
