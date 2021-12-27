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
using Pos_Inventory_Software;
using System.Configuration;
using System.Configuration.Provider;

namespace Pos_Inventory_Software
{
    public partial class Server_Confiq : Form
    {
        public Server_Confiq()
        {
            InitializeComponent();
        }

        private void bt_conn_Click(object sender, EventArgs e)
        {

            Pos_Inventory_Software.Properties.Settings.Default.Server = txt_server.Text;
            Pos_Inventory_Software.Properties.Settings.Default.DataBase = txt_base.Text;
            Pos_Inventory_Software.Properties.Settings.Default.Mode = rbt_win.Checked == true ? "Windows" : "Windows";
            Pos_Inventory_Software.Properties.Settings.Default.Counter += 1;
            Pos_Inventory_Software.Properties.Settings.Default.Save();

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.ConnectionStrings.ConnectionStrings["localhost_POS_DB_Connection"].ConnectionString = "data source='" + txt_server.Text + "';integrated security=True;initial catalog='" + txt_base.Text + "';MultipleActiveResultSets=True";
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("connectionStrings");

            config.ConnectionStrings.ConnectionStrings["Pos_Model"].ConnectionString = "data source='" + txt_server.Text + "';integrated security=True;initial catalog='" + txt_base.Text + "';MultipleActiveResultSets=True";
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("connectionStrings");

            config.ConnectionStrings.ConnectionStrings["Pos_Inventory_Software.Properties.Settings.ConnectionString"].ConnectionString = "data source='" + txt_server.Text + "';integrated security=True;initial catalog='" + txt_base.Text + "';MultipleActiveResultSets=True";
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("connectionStrings");

            MessageBox.Show("... تم تفعيل الإتصال بالسيرفر بنجاح ","رسالة تأكيد",MessageBoxButtons.OK,MessageBoxIcon.Information);
            this.Close();
        }

        private void Server_Confiq_Load(object sender, EventArgs e)
        {
            txt_server.Text = Pos_Inventory_Software.Properties.Settings.Default.Server;
            txt_base.Text = Pos_Inventory_Software.Properties.Settings.Default.DataBase;
            if (Pos_Inventory_Software.Properties.Settings.Default.Mode == "Windows")
                rbt_win.Checked = true;
        }
    }
}
