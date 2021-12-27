using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Pos_Inventory_Software
{
    class ClassDB
    {
        public string GetConnection()
        {
            string cn = "server= " + Properties.Settings.Default.Server + ";Database=" + Properties.Settings.Default.DataBase + ";Integrated Security=True";

            //string cn = @"data source = OPREKIN-PC\SQLEXPRESS01; initial catalog = POS_DB; integrated security = True; MultipleActiveResultSets = True; App = EntityFramework";
            return cn;
        }
        //public SqlConnection Sqlconnection;

        //constructor inisialize connection object 
        //public ClassDB()
        //{
        //    // connected bettwin pro & server using setting in application properties

        //    Sqlconnection = new SqlConnection(@"data source =MO-KAMAL\SQLEXPRESS;initial catalog=SamarPro.db_model;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");

        //}

        //// method to open connection
        //public void open()
        //{
        //    if (Sqlconnection.State != ConnectionState.Open)
        //    {
        //        Sqlconnection.Open();
        //    }
        //}

        //// method to close  connection
        //public void close()
        //{
        //    if (Sqlconnection.State != ConnectionState.Closed)
        //    {
        //        Sqlconnection.Close();
        //    }
        //}
    }
}
