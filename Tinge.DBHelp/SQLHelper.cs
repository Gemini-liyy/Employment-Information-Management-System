using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace Helper
{
    public class SQLHelper
    {
        private static readonly string conStr = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        public static int ExecuteNonQuery(string sqlText, params SqlParameter[] paras)
        {
            using (SqlConnection conn=new SqlConnection(conStr) )
            {
                using (SqlCommand cmd=new SqlCommand(sqlText,conn))
                {
                    if (paras!=null)
                    {
                        cmd.Parameters.AddRange(paras);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static object ExecuteScalar(string sqlText, params SqlParameter[] paras)
        {
            using (SqlConnection conn=new SqlConnection(conStr))
            {
                using (SqlCommand cmd=new SqlCommand(sqlText,conn))
                {
                    if (paras!=null)
                    {
                        cmd.Parameters.AddRange(paras);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
        public static SqlDataReader ExecuteReader(string sqlText, params SqlParameter[] paras)
        {
            SqlConnection conn=new SqlConnection(conStr);
            using (SqlCommand cmd=new SqlCommand(sqlText,conn))
            {
                if (paras!=null)
                {
                    cmd.Parameters.AddRange(paras);
                        
                }
               
                try
                {
                    conn.Open();
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                }
                catch (Exception)
                {
                    conn.Close();
                    conn.Dispose();
                    throw;
                }
            }             
        }
        public static DataTable ExecuteDataTable(string sqlText, params SqlParameter[] paras)
        {
            using (SqlDataAdapter da=new SqlDataAdapter(sqlText,conStr))
            {
                if (paras!=null)
                {
                    da.SelectCommand.Parameters.AddRange(paras);
                }
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

    }
}
