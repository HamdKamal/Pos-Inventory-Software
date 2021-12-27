using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos_Inventory_Software.Report
{
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void frmReport_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DayReport RT = new DayReport();
            RT.Parameters["Atitle"].Value = frmLogin.Atitle;
            RT.Parameters["Etitle"].Value = frmLogin.Etitle;
            RT.Parameters["Address"].Value = frmLogin.Address;
            RT.Parameters["Phone"].Value = frmLogin.Phone;
            ReportPrintTool printTool = new ReportPrintTool(RT);
            printTool.PreviewForm.StartPosition = FormStartPosition.CenterScreen;
            printTool.ShowPreviewDialog();
            this.Cursor = Cursors.Default;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GeneralReport RT = new GeneralReport();
            RT.Parameters["Atitle"].Value = frmLogin.Atitle;
            RT.Parameters["Etitle"].Value = frmLogin.Etitle;
            RT.Parameters["Address"].Value = frmLogin.Address;
            RT.Parameters["Phone"].Value = frmLogin.Phone;
            ReportPrintTool printTool = new ReportPrintTool(RT);
            printTool.PreviewForm.StartPosition = FormStartPosition.CenterScreen;
            printTool.ShowPreviewDialog();
            this.Cursor = Cursors.Default;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            EmployeeCommation RT = new EmployeeCommation();
            RT.Parameters["Atitle"].Value = frmLogin.Atitle;
            RT.Parameters["Etitle"].Value = frmLogin.Etitle;
            RT.Parameters["Address"].Value = frmLogin.Address;
            RT.Parameters["Phone"].Value = frmLogin.Phone;
            ReportPrintTool printTool = new ReportPrintTool(RT);
            printTool.PreviewForm.StartPosition = FormStartPosition.CenterScreen;
            printTool.ShowPreviewDialog();
            this.Cursor = Cursors.Default;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            QuanMaxReport RT = new QuanMaxReport();
            RT.Parameters["Atitle"].Value = frmLogin.Atitle;
            RT.Parameters["Etitle"].Value = frmLogin.Etitle;
            RT.Parameters["Address"].Value = frmLogin.Address;
            RT.Parameters["Phone"].Value = frmLogin.Phone;
            ReportPrintTool printTool = new ReportPrintTool(RT);
            printTool.PreviewForm.StartPosition = FormStartPosition.CenterScreen;
            printTool.ShowPreviewDialog();
            this.Cursor = Cursors.Default;
        }
    }
}
