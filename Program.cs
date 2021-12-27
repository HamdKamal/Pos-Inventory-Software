using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos_Inventory_Software
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [Obsolete]
        static void Main()
        {
            Application.EnableVisualStyles();
            DevExpress.DataAccess.Sql.SqlDataSource.AllowCustomSqlQueries = true;
            DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true;
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
