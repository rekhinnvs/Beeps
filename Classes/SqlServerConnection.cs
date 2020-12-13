using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;

namespace Beeps.Classes
{
    class DBClass
    {
        public static string GetConnectionString()
        {
           
            string strConnectionString = ConfigurationManager.ConnectionStrings["conString"].ToString();
            return strConnectionString;
        }

        public static string sql;
        public static SqlConnection con = new SqlConnection();
        public static SqlCommand cmd = new SqlCommand(sql,con);
        public static SqlDataReader dReader;
        public static SqlDataAdapter dAdapter;
        public static DataTable dTable;
        
        

        public static void OpenConnection()
        {
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.ConnectionString = GetConnectionString();
                    con.Open();
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("Failed to establish a connection" + e.Message,"SQL Server connection", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void CloseConnection()
        {
            try
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
