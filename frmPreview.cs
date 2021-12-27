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
    public partial class frmPreview : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();

        public string _id;

        public frmPreview()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadDetails()
        {
            cn.Open();
            cm = new SqlCommand("SELECT * FROM vwpreview WHERE batchid = @batchid", cn);
            cm.Parameters.AddWithValue("@batchid", _id);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                lblMedID.Text = dr["batchid"].ToString();
                lblCategory.Text = dr["category"].ToString();
                lblMedicineName.Text = dr["name"].ToString();
                lblbatchID.Text = dr["quantity"].ToString();
                lblDateAdded.Text = dr["date1"].ToString();
                lblDateManufactured.Text = dr["date2"].ToString();
                lblExpired.Text = dr["date3"].ToString();
                lblPrice.Text = dr["price"].ToString();
                lblSupplierName.Text = dr["suppliername"].ToString();
            }
            else
            {
                lblMedID.Text = "";
                lblCategory.Text = "";
                lblMedicineName.Text = "";
                lblbatchID.Text = "";
                lblDateAdded.Text = "";
                lblDateManufactured.Text = "";
                lblExpired.Text = "";
                lblPrice.Text = "";
                lblSupplierName.Text = "";
            }
            dr.Close();
            cn.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
