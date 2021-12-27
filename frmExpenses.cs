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
    public partial class frmExpenses : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ClassDB db = new ClassDB();
        public void GetID()
        {
            cn.Open();
            cm = new SqlCommand("SELECT COUNT(*) FROM Expenses", cn);
            string num = cm.ExecuteScalar().ToString();
            cn.Close();

            Random myrand = new Random();
            int mynum = myrand.Next(20, 1000);
            txtID.Text = "S-001" + num + mynum;
        }
        public void Clear()
        {
            txtType.Clear();
            txtNote.Clear();
            txtMoney.Clear();
            txtID.Text = "";
        }
        public void LoadRecord()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("SELECT * FROM Expenses ORDER BY Type ASC", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["ID"].ToString(), dr["Type"].ToString(), dr["Money"].ToString(), dr["E_date"].ToString(), dr["Note"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch
            {
                return;
            }
        }
        private void InsertLogExpenses()
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
                cm.Parameters.AddWithValue("@operation", "تمت إضافة مصروفات بقيمة '" + txtMoney.Text + "'");
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch
            {
                return;
            }

        }

        public frmExpenses()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
                txtType.Focus();
                GetID();
            }
            catch
            {
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtType.Text == string.Empty || txtMoney.Text == string.Empty)
            {
                MessageBox.Show("الرجاء إدخال جميع الحقول", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("هل تريد حفظ هذه البيانات", "رسالة تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO Expenses(ID,Type,Money,Note,E_date) VALUES(@ID,@Type,@Money,@Note,@E_date)", cn);
                    cm.Parameters.AddWithValue("@ID", txtID.Text);
                    cm.Parameters.AddWithValue("@Type", txtType.Text);
                    cm.Parameters.AddWithValue("@Money", Convert.ToDouble(txtMoney.Text));
                    cm.Parameters.AddWithValue("@Note", txtNote.Text);
                    cm.Parameters.AddWithValue("@E_date", dateExpenses.Text);
                    cm.ExecuteNonQuery();
                    InsertLogExpenses();
                    cn.Close();
                    LoadRecord();
                    Clear();
                    GetID();
                    MessageBox.Show("تم حفظ بيانات الصرف بنجاح", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    return;
                }
                }
        }

        private void frmExpenses_Load(object sender, EventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string ColName = dataGridView1.Columns[e.ColumnIndex].Name;
                if (ColName == "ColEdit")
                {
                    btnUpdate.Enabled = true;
                    btnSave.Enabled = false;
                    txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtType.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtMoney.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    dateExpenses.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtNote.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                }
                else if (ColName == "ColDelete")
                {
                    if (frmLogin.usertype == "Administrator")
                    {
                        if (MessageBox.Show(" هل تريد حذف هذه البيانات", "رسالة تحقق", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            cn.Open();
                            cm = new SqlCommand("DELETE FROM Expenses WHERE ID = @ID", cn);
                            cm.Parameters.AddWithValue("@ID", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                            cm.ExecuteNonQuery();
                            cn.Close();
                            LoadRecord();
                            GetID();
                            MessageBox.Show("تم حذف بيانات الصرف بنجاح", "رسالة تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Access Denied", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch
            {
                return;
            }
            }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtType.Text == string.Empty || txtMoney.Text == string.Empty)
            {
                MessageBox.Show("الرجاء إدخال جميع الحقول", "رسالة تحقق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                if (MessageBox.Show("هل تريد تعديل هذه البيانات", "رسالة تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE Expenses SET ID = @ID,Type = @Type,Money = @Money,Note = @Note,E_date = @E_date WHERE ID = @ID", cn);
                    cm.Parameters.AddWithValue("@ID", txtID.Text);
                    cm.Parameters.AddWithValue("@Type", txtType.Text);
                    cm.Parameters.AddWithValue("@Money", Convert.ToDouble(txtMoney.Text));
                    cm.Parameters.AddWithValue("@Note", txtNote.Text);
                    cm.Parameters.AddWithValue("@E_date", dateExpenses.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadRecord();
                    Clear();
                    GetID();
                    MessageBox.Show("تم تعديل بيانات الصرف بنجاح", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch
            {
                return;
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            try
            {
                int i = 0;
                cn.Open();
                cm = new SqlCommand("SELECT * FROM Expenses WHERE E_date = @d1", cn);
                cm.Parameters.AddWithValue("@d1", dateSearch.Text);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["ID"].ToString(), dr["Type"].ToString(), dr["Money"].ToString(), dr["E_date"].ToString(), dr["Note"].ToString());
                }
                if (dr.HasRows)
                {
                }
                else
                {
                    MessageBox.Show("! لا يوجد منصرفات في هذا اليوم", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
