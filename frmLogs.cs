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
    public partial class frmLogs : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();

        public frmLogs()
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
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tbllogs", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["username"].ToString(), dr["date"].ToString(), dr["time"].ToString(), dr["operation"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        public void GetUser()
        {
            cboUserID.Items.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tbluser", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboUserID.Items.Add(dr["username"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void cboUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboUserID_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tbllogs WHERE username = @username", cn);
            cm.Parameters.AddWithValue("@username", cboUserID.Text);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["username"].ToString(), dr["date"].ToString(), dr["time"].ToString(), dr["operation"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tbllogs WHERE date BETWEEN @d1 AND @d2", cn);
            cm.Parameters.AddWithValue("@d1", dtpFrom.Text);
            cm.Parameters.AddWithValue("@d2", dtpTo.Text);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["username"].ToString(), dr["date"].ToString(), dr["time"].ToString(), dr["operation"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete all records?, أضغط yes للتأكيد  !", "ALERT", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cn.Open();
                cm = new SqlCommand("DELETE FROM tbllogs", cn);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("تم حذف جميع السجلات", "رسالة تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
