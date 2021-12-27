using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos_Inventory_Software
{
    public partial class frmProject : Form
    {
        public frmProject()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("NOTE: This application was created by: Aim Tech Information Technology\n/Project Developer/System Analysit/Technician Support.\n\n[ APPLICATION TOOLS USED: ]\nProgramming Language: C#.Net 2019\nBack End Database: MS Sql Server 2019\nGraphics Designed: WinForm Flat User Control \n\n[ AIM TECH - makers of high quality softwares. ]\n", "Short Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
