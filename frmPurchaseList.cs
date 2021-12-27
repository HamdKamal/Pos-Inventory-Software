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
    public partial class frmPurchaseList : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();

        public static string purchaseid;

        public frmPurchaseList()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void GetCategory()
        {
            cboCat.Items.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblsupplier", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboCat.Items.Add(dr["name"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        public void LoadRecord()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblpurchaselist", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["purchaseid"].ToString(), dr["suppliername"].ToString(), dr["amountpaid"].ToString(), dr["total"].ToString(), dr["schange"].ToString(), dr["paymenttype"].ToString(), dr["paymentstatus"].ToString(), dr["discount"].ToString(), dr["note"].ToString(), dr["date"].ToString(), dr["time"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblpurchaselist WHERE purchaseid LIKE '" + txtSearch.Text + "%'", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["purchaseid"].ToString(), dr["suppliername"].ToString(), dr["amountpaid"].ToString(), dr["total"].ToString(), dr["schange"].ToString(), dr["paymenttype"].ToString(), dr["paymentstatus"].ToString(), dr["discount"].ToString(), dr["note"].ToString(), dr["date"].ToString(), dr["time"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void cboCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblpurchaselist WHERE suppliername LIKE '" + cboCat.Text + "%' ORDER BY suppliername ASC", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["purchaseid"].ToString(), dr["suppliername"].ToString(), dr["amountpaid"].ToString(), dr["total"].ToString(), dr["schange"].ToString(), dr["paymenttype"].ToString(), dr["paymentstatus"].ToString(), dr["discount"].ToString(), dr["note"].ToString(), dr["date"].ToString(), dr["time"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            string ColName = dataGridView1.Columns[e.ColumnIndex].Name;

            if (ColName == "ColPrint")
            {
                var f1 = new frmPrintPurchase();
                f1.purchaseid = id;
                f1.ShowDialog();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
