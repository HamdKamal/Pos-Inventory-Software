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
    public partial class frmSalesRecord : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();

        public frmSalesRecord()
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
            if (frmLogin.usertype == "Administrator")
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblsalespayment", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["invoiceid"].ToString(), dr["cusname"].ToString(), dr["cusphone"].ToString(), dr["saletotal"].ToString(), dr["grandtotal"].ToString(), dr["amountpaid"].ToString(), dr["schange"].ToString(), dr["discount"].ToString(), dr["paymode"].ToString(), dr["status"].ToString(), dr["cashier"].ToString(), dr["date"].ToString(), dr["time"].ToString(), dr["E_Commation"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            else
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblsalespayment WHERE cashier = @cashier", cn);
                cm.Parameters.AddWithValue("@cashier", frmLogin.username);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["invoiceid"].ToString(), dr["cusname"].ToString(), dr["cusphone"].ToString(), dr["saletotal"].ToString(), dr["grandtotal"].ToString(), dr["amountpaid"].ToString(), dr["schange"].ToString(), dr["discount"].ToString(), dr["paymode"].ToString(), dr["status"].ToString(), dr["cashier"].ToString(), dr["date"].ToString(), dr["time"].ToString(), dr["E_Commation"].ToString());
                }
                dr.Close();
                cn.Close();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (frmLogin.usertype == "Administrator")
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblsalespayment WHERE invoiceid LIKE '" + txtSearch.Text + "%'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["invoiceid"].ToString(), dr["cusname"].ToString(), dr["cusphone"].ToString(), dr["saletotal"].ToString(), dr["grandtotal"].ToString(), dr["amountpaid"].ToString(), dr["schange"].ToString(), dr["discount"].ToString(), dr["paymode"].ToString(), dr["status"].ToString(), dr["cashier"].ToString(), dr["date"].ToString(), dr["time"].ToString(), dr["E_Commation"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            else
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblsalespayment WHERE invoiceid LIKE '" + txtSearch.Text + "%'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["invoiceid"].ToString(), dr["cusname"].ToString(), dr["cusphone"].ToString(), dr["saletotal"].ToString(), dr["grandtotal"].ToString(), dr["amountpaid"].ToString(), dr["schange"].ToString(), dr["discount"].ToString(), dr["paymode"].ToString(), dr["status"].ToString(), dr["cashier"].ToString(), dr["date"].ToString(), dr["time"].ToString(), dr["E_Commation"].ToString());
                }
                dr.Close();
                cn.Close();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            var dtFrom = dtpFrom.Text;
            var dtTo = dtpTo.Text;

            if (frmLogin.usertype == "Administrator")
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblsalespayment WHERE date BETWEEN @d1 AND @d2", cn);
                cm.Parameters.AddWithValue("@d1", dtpFrom.Text);
                cm.Parameters.AddWithValue("@d2", dtpTo.Text);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["invoiceid"].ToString(), dr["cusname"].ToString(), dr["cusphone"].ToString(), dr["saletotal"].ToString(), dr["grandtotal"].ToString(), dr["amountpaid"].ToString(), dr["schange"].ToString(), dr["discount"].ToString(), dr["paymode"].ToString(), dr["status"].ToString(), dr["cashier"].ToString(), dr["date"].ToString(), dr["time"].ToString(), dr["E_Commation"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            else
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblsalespayment WHERE date BETWEEN '" + dtpFrom.Text + "' AND '" + dtpTo.Text + "' AND cashier = '" + frmLogin.username + "'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["invoiceid"].ToString(), dr["cusname"].ToString(), dr["cusphone"].ToString(), dr["saletotal"].ToString(), dr["grandtotal"].ToString(), dr["amountpaid"].ToString(), dr["schange"].ToString(), dr["discount"].ToString(), dr["paymode"].ToString(), dr["status"].ToString(), dr["cashier"].ToString(), dr["date"].ToString(), dr["time"].ToString(), dr["E_Commation"].ToString());
                }
                dr.Close();
                cn.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dataGridView1.Columns[e.ColumnIndex].Name;
            string getid = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            string status = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            
            if ((ColName == "ColEdit") && (status == "Hold"))
            {
                this.Dispose();
                var f1 = new frmPOS();
                //f1.GetCategory();
                f1.GetCustomer();
                f1.getholdid = getid;
                f1.lblInvoiceNo.Text = getid;
                f1.GetItemsOnHold2();
                f1.GetTotal();
                //f1.GetCustomerOnHold2();
                f1.ShowDialog();
            }
            else if ((ColName == "ColEdit") && (status == "Settled"))
            {
                MessageBox.Show("Only Transactions on Hold can be Selected!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void cboStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStatus.Text == "Hold")
            {
                if (frmLogin.usertype == "Administrator")
                {
                    dataGridView1.Rows.Clear();
                    int i = 0;
                    cn.Open();
                    cm = new SqlCommand("SELECT * FROM tblsalespayment WHERE status = @status", cn);
                    cm.Parameters.AddWithValue("@status", "Hold");
                    dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        i++;
                        dataGridView1.Rows.Add(i, dr["invoiceid"].ToString(), dr["cusname"].ToString(), dr["cusphone"].ToString(), dr["saletotal"].ToString(), dr["grandtotal"].ToString(), dr["amountpaid"].ToString(), dr["schange"].ToString(), dr["discount"].ToString(), dr["paymode"].ToString(), dr["status"].ToString(), dr["cashier"].ToString(), dr["date"].ToString(), dr["time"].ToString(), dr["E_Commation"].ToString());
                    }
                    dr.Close();
                    cn.Close();
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    int i = 0;
                    cn.Open();
                    cm = new SqlCommand("SELECT * FROM tblsalespayment WHERE status = @status", cn);
                    cm.Parameters.AddWithValue("@status", "Hold");
                    dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        i++;
                        dataGridView1.Rows.Add(i, dr["invoiceid"].ToString(), dr["cusname"].ToString(), dr["cusphone"].ToString(), dr["saletotal"].ToString(), dr["grandtotal"].ToString(), dr["amountpaid"].ToString(), dr["schange"].ToString(), dr["discount"].ToString(), dr["paymode"].ToString(), dr["status"].ToString(), dr["cashier"].ToString(), dr["date"].ToString(), dr["time"].ToString(), dr["E_Commation"].ToString());
                    }
                    dr.Close();
                    cn.Close();
                }
            }
            else if (cboStatus.Text == "Settled")
            {
                if (frmLogin.usertype == "Administrator")
                {
                    dataGridView1.Rows.Clear();
                    int i = 0;
                    cn.Open();
                    cm = new SqlCommand("SELECT * FROM tblsalespayment WHERE status = @status", cn);
                    cm.Parameters.AddWithValue("@status", "Settled");
                    dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        i++;
                        dataGridView1.Rows.Add(i, dr["invoiceid"].ToString(), dr["cusname"].ToString(), dr["cusphone"].ToString(), dr["saletotal"].ToString(), dr["grandtotal"].ToString(), dr["amountpaid"].ToString(), dr["schange"].ToString(), dr["discount"].ToString(), dr["paymode"].ToString(), dr["status"].ToString(), dr["cashier"].ToString(), dr["date"].ToString(), dr["time"].ToString(), dr["E_Commation"].ToString());
                    }
                    dr.Close();
                    cn.Close();
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    int i = 0;
                    cn.Open();
                    cm = new SqlCommand("SELECT * FROM tblsalespayment WHERE status = @status", cn);
                    cm.Parameters.AddWithValue("@status", "Settled");
                    dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        i++;
                        dataGridView1.Rows.Add(i, dr["invoiceid"].ToString(), dr["cusname"].ToString(), dr["cusphone"].ToString(), dr["saletotal"].ToString(), dr["grandtotal"].ToString(), dr["amountpaid"].ToString(), dr["schange"].ToString(), dr["discount"].ToString(), dr["paymode"].ToString(), dr["status"].ToString(), dr["cashier"].ToString(), dr["date"].ToString(), dr["time"].ToString(), dr["E_Commation"].ToString());
                    }
                    dr.Close();
                    cn.Close();
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
