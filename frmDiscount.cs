using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos_Inventory_Software
{
    public partial class frmDiscount : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();
        public void GetID()
        {
            cn.Open();
            cm = new SqlCommand("SELECT COUNT(*) FROM Discount", cn);
            string num = cm.ExecuteScalar().ToString();
            cn.Close();

            Random myrand = new Random();
            int mynum = myrand.Next(20, 1000);
            txtID.Text = "001" + num + mynum;
        }
        public void Clear()
        {
            txtNote.Clear();
            txtMoney.Clear();
            txtID.Text = "";
        }
        public frmDiscount()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
        }

        private void txtMoney_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                btnSave.Enabled = true;
                txtMoney.Focus();
                GetID();
            }
            catch
            {
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNote.Text == string.Empty || txtMoney.Text == string.Empty)
            {
                MessageBox.Show("الرجاء إدخال جميع الحقول", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("هل تريد حفظ هذه البيانات", "رسالة تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO Discount(ID,D_Money,Note) VALUES(@ID,@D_Money,@Note)", cn);
                    cm.Parameters.AddWithValue("@ID", txtID.Text);
                    cm.Parameters.AddWithValue("@D_Money", Convert.ToDouble(txtMoney.Text));
                    cm.Parameters.AddWithValue("@Note", txtNote.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    Clear();
                    GetID();
                    MessageBox.Show("تم حفظ قيمة التخفيض بنجاح", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    return;
                }
            }
        }

        private void frmDiscount_Load(object sender, EventArgs e)
        {
            GetID();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }

        private void txtMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                return;
            }
        }
    }
}
