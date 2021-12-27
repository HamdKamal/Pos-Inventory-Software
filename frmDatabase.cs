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
    public partial class frmDatabase : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        ClassDB db = new ClassDB();
        public frmDatabase()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                txt_path.BackColor = Color.White;
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    txt_path.Text = folderBrowserDialog1.SelectedPath;
                }
            }
            catch
            {
                return;
            }
        }

        private void btnBac_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_path.Text == "")
                {
                    txt_path.BackColor = Color.Red;
                    return;
                }
                string filename = txt_path.Text + "//POS_DB" + DateTime.Now.ToLongDateString().Replace('/', '-') + " ^ " + DateTime.Now.ToShortTimeString().Replace(':', '-');
                string strqry = "Backup Database [POS_DB] to Disk ='" + filename + ".bak'";
                cm = new SqlCommand(strqry,cn);
                cn.Open();
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("! تم عمل النسخة الإحتياطية بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_path.Text = "";
            }
            catch
            {
                return;
            }
        }

        private void btnPste_Click(object sender, EventArgs e)
        {
            try
            {
                TXT_PATH1.BackColor = Color.White;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    TXT_PATH1.Text = openFileDialog1.FileName;
                }
            }
            catch
            {
                return;
            }
        }

        private void btnRes_Click(object sender, EventArgs e)
        {
            try
            {
                if (TXT_PATH1.Text == "")
                {
                    TXT_PATH1.BackColor = Color.Red;
                    return;
                }
                string strqry = "ALTER Database [POS_DB] SET OFFLINE WITH ROLLBACK IMMEDIATE; Restore Database [POS_DB] From Disk ='" + TXT_PATH1.Text + "'";
                cm = new SqlCommand(strqry,cn);
                cn.Open();
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("! تم إستعادة النسخة الإحتياطية بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TXT_PATH1.Text = "";
            }
            catch
            {
                return;
            }
        }
    }
}
