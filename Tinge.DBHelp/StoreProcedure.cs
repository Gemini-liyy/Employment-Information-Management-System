using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace Tinge.DBHelp
{
    public class StoreProcedure
    {
        /// <summary>
        /// 存储过程名称
        /// </summary>
        private string _name;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string _conStr = PubConstant.ConnectionString;// System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sprocName">存储过程名称</param>
        public StoreProcedure(string sprocName)
        {
            _name = sprocName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprocName"></param>
        /// <returns></returns>
        public DataTable ExecuteNoQuery(string sprocName)
        {
            SqlCommand cmd = new SqlCommand(sprocName, new SqlConnection(_conStr));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 执行存储过程，不返回值
        /// </summary>
        /// <param name="paraValues">参数值列表</param>
        /// <returns>void</returns>
        public int ExecuteNoQueryByPar(params object[] paraValues)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand comm = new SqlCommand(_name, con);
                comm.CommandType = CommandType.StoredProcedure;

                AddInParaValues(comm, paraValues);

                con.Open();
                int result = comm.ExecuteNonQuery();
                con.Close();
                return result;
            }
        }
        /// <summary>
        /// 执行存储过程返回一个表
        /// </summary>
        /// <param name="paraValues">参数值列表</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDataTable(params object[] paraValues)
        {
            SqlCommand comm = new SqlCommand(_name, new SqlConnection(_conStr));
            comm.CommandType = CommandType.StoredProcedure;
            AddInParaValues(comm, paraValues);

            SqlDataAdapter sda = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            return dt;
        }
        /// <summary>
        /// 执行存储过程，返回SqlDataReader对象，
        /// 在SqlDataReader对象关闭的同时，数据库连接自动关闭
        /// </summary>
        /// <param name="paraValues">要传递给给存储过程的参数值类表</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteDataReader(params object[] paraValues)
        {
            SqlConnection con = new SqlConnection(_conStr);
            SqlCommand comm = new SqlCommand(_name, con);
            comm.CommandType = CommandType.StoredProcedure;
            AddInParaValues(comm, paraValues);
            con.Open();
            return comm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        #region - 私有方法
        /// <summary>
        /// 获取存储过程的参数列表
        /// </summary>
        /// <returns></returns>
        private System.Collections.ArrayList GetParas()
        {
            SqlCommand comm = new SqlCommand("sp_sproc_columns_90",
                       new SqlConnection(_conStr));
            comm.CommandType = CommandType.StoredProcedure;
            //comm.Parameters.AddWithValue("@procedure_name", (object)_name);
            comm.Parameters.Add(new SqlParameter("@procedure_name", _name));
            SqlDataAdapter sda = new SqlDataAdapter(comm);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            System.Collections.ArrayList al = new System.Collections.ArrayList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                al.Add(dt.Rows[i][3].ToString());
            }
            return al;
        }
        /// <summary>
        /// 为 SqlCommand 添加参数及赋值
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="paraValues"></param>
        private void AddInParaValues(SqlCommand comm, params object[] paraValues)
        {
            comm.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int));
            comm.Parameters["@RETURN_VALUE"].Direction =
                           ParameterDirection.ReturnValue;
            if (paraValues != null)
            {
                System.Collections.ArrayList al = GetParas();
                for (int i = 0; i < paraValues.Length; i++)
                {
                    comm.Parameters.AddWithValue(al[i + 1].ToString(),
                         paraValues[i]);
                }
            }
        }
        #endregion

    }
}
