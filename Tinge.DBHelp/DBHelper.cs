using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace Tinge.DBHelp
{
    public class DBHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        static string connectionString = PubConstant.ConnectionString;

        #region - 属性
        private SqlConnection _connection = null;
        private SqlTransaction _trans = null;
        private SqlCommand _cmd = null;
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public SqlConnection Connection
        {
            set { _connection = value; }
            get { return _connection; }
        }
        /// <summary>
        /// 事务对象
        /// </summary>
        public SqlTransaction Transaction
        {
            set { _trans = value; }
            get { return _trans; }
        }
        /// <summary>
        /// SQL执行语句
        /// </summary>
        private SqlCommand Command
        {
            set { _cmd = value; }
            get { return _cmd; }
        }
        #endregion

        #region - 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DBHelper()
        {
            _connection = new SqlConnection(connectionString);
        }
        /// <summary>
        /// 得到web.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName">web.config配置</param>
        public DBHelper(string configName)
        {
            _connection = new SqlConnection(PubConstant.GetConnectionString(configName));
        }
        #endregion

        #region - 实例方法
        //public void BeginTransaction()
        //{
        //    if (Connection.State != ConnectionState.Open)
        //        Connection.Open();
        //    _trans = Connection.BeginTransaction();
        //}
        //public void BeginTransaction(string transName)
        //{
        //    if (Connection.State != ConnectionState.Open)
        //        Connection.Open();
        //    _trans = Connection.BeginTransaction(transName);
        //}
        //public void Commit()
        //{
        //    Transaction.Commit();
        //    Transaction.Dispose();
        //    Command.Dispose();
        //    Connection.Dispose();
        //}
        //public void Rollback()
        //{
        //    Transaction.Rollback();
        //    Transaction.Dispose();
        //    Command.Dispose();
        //    Connection.Dispose();
        //}
        //public void Rollback(string transName)
        //{
        //    Transaction.Rollback(transName);
        //    Transaction.Dispose();
        //    Command.Dispose();
        //    Connection.Dispose();
        //}

        //public int ExecuteCommandTrans(string cmdText, params SqlParameter[] cmdParms)
        //{
        //    using (Command = new SqlCommand())
        //    {
        //        try
        //        {
        //            PrepareCommand(Command, Connection, Transaction, cmdText, cmdParms);
        //            int rows = Command.ExecuteNonQuery();
        //            Command.Parameters.Clear();
        //            return rows;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message + "__" + cmdText);
        //        }
        //        finally
        //        {
        //            if (Connection.State != ConnectionState.Closed)
        //                Connection.Close();
        //        }
        //    }
        //}
        #endregion

        #region - 基本方法
        /// <summary>
        /// 执行SQL查询语句
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DataTable Query(string text)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter odda = new SqlDataAdapter(text, connection);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            DataSet ds = new DataSet();
            odda.Fill(ds);
            odda.SelectCommand.Parameters.Clear();
            if (connection.State != ConnectionState.Closed)
                connection.Close();
            return ds.Tables[0];
        }
        /// <summary>
        /// 执行SQL增删改语句
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int QueryNon(string text)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(text, connection);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            int i = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            if (connection.State != ConnectionState.Closed)
                connection.Close();
            return i;
        }
        #endregion

        /// <summary>
        /// 执行SQL查询语句
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sqlText)
        {
            return GetDataTable(sqlText, "ds", null);
        }
        /// <summary>
        /// 执行SQL增删改语句
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int ExecuteCommand(string sqlText)
        {
            return ExecuteCommand(sqlText, null);
        }

        #region - 带参数的SQL语句
        /// <summary>
        /// 读取DataTable数据
        /// </summary>
        /// <param name="text">sql语句，（带参数的完整的select语句，参数自定义）</param>
        /// <param name="param">参数数组，必须与sql参数相对应</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string cmdText, params SqlParameter[] cmdParms)
        {
            return GetDataTable(cmdText, "ds", cmdParms);
        }

        /// <summary>
        /// 读取DataTable数据
        /// </summary>
        /// <param name="text">sql语句，（带参数的完整的select语句，参数自定义）</param>
        /// <param name="tableName">用于映射的源表的名称</param>
        /// <param name="param">参数数组，必须与sql参数相对应</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string cmdText, string tableName, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, cmdText, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, tableName);
                        cmd.Parameters.Clear();
                        return ds.Tables[0];
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message + ":" + cmdText);
                    }
                    finally
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                        connection.Dispose();
                        cmd.Dispose();
                    }
                }
            }
        }


        /// <summary>
        /// 返回会受影响行数
        /// </summary>
        /// <param name="cmdText">T-SQL命令（update,insert,delete语句）</param>
        /// <param name="param">参数列表（可为空null）</param>
        /// <returns></returns>
        public static int ExecuteCommand(string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, cmdText, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message + ":" + cmdText);
                    }
                    finally
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                        connection.Dispose();
                        cmd.Dispose();
                    }
                }
            }
        }
        #region 调用示例
        /*********************
         * 调用示例：
         *  DBHelper.ExecuteCommand("insert into Log ([UserId] ,[TableName],[LogState],[PKID],[CreateTime]) values (@userId,@TableName,@LogState,@PKID,@CreateTime)",
         *  new SqlParameter[]{
         *      new SqlParameter("@userId",userId),
         *      new SqlParameter("@TableName",TableName),
         *      new SqlParameter("@LogState",LogState),
         *      new SqlParameter("@PKID",PKID),
         *      new SqlParameter("@CreateTime",CreateTime),
         *  });
         * 调用示例：
         * DBHelper.ExecuteCommand("insert into Log ([UserId] ,[TableName],[LogState],[PKID],[CreateTime]) values (@userId,@TableName,@LogState,@PKID,@CreateTime)",
         * new SqlParameter("@userId", "6"),
         * new SqlParameter("@TableName", "Administrator"),
         * new SqlParameter("@LogState", "登录"),
         * new SqlParameter("@PKID", "7"),
         * new SqlParameter("@CreateTime", "2013-03-11"));
         *********************/
        #endregion

        public static int ExecuteCommand(string cmdText, bool isTrans, params SqlParameter[] cmdParms)
        {
            if (isTrans)
            {
                CommandInfo cmdInfo = new CommandInfo(cmdText, cmdParms);
                return ExecuteSqlTran(cmdInfo);
            }
            else
                return ExecuteCommand(cmdText, cmdParms);
        }
        /// <summary>
        /// 读取DataTable数据
        /// </summary>
        /// <param name="text">sql语句，（带参数的完整的select语句，参数必须从@0,开始）</param>
        /// <param name="param">参数数组</param>
        /// <returns></returns>
        public static DataTable GetDataTableObject(string cmdText, params object[] param)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter odda = new SqlDataAdapter(cmdText, connection))
                {
                    try
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        if (param != null)
                        {
                            for (int i = 0; i < param.Length; i++)
                            {
                                SqlParameter sp = new SqlParameter("@" + i.ToString(), param[i]);
                                odda.SelectCommand.Parameters.Add(sp);
                            }
                        }
                        DataSet ds = new DataSet();
                        odda.Fill(ds);
                        return ds.Tables[0];
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        odda.SelectCommand.Parameters.Clear();
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 返回会受影响行数
        /// </summary>
        /// <param name="cmdText">T-SQL命令（update,insert,delete语句）</param>
        /// <param name="param">参数列表（可为空null）</param>
        /// <returns></returns>
        public static int ExecuteCommandObject(string cmdText, params object[] param)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    try
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        if (param != null)
                        {
                            for (int i = 0; i < param.Length; i++)
                            {
                                SqlParameter sp = new SqlParameter("@" + i.ToString(), param[i]);
                                cmd.Parameters.Add(sp);
                            }
                        }
                        int result = cmd.ExecuteNonQuery();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cmd.Parameters.Clear();
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                }
            }
        }
        #region 调用示例
        /*******************
         * 调用示例：
         *  DBHelper.GetDataTable(
         *  "insert into Log ([UserId] ,[TableName],[LogState],[PKID],[CreateTime]) values (@0,@1,@2,@3,@4)",
         *  "6","Administrator","登录","7","2013-03-11");
         *******************/
        #endregion

        #endregion

        #region - 执行一条计算查询结果语句，返回结果集中第一行第一列的值
        /// <summary>
        /// 执行一条计算查询结果语句，返回结果集中第一行第一列的值（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, cmdText, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region - 执行多条SQL语句，实现数据库事务
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="cmdList">CommandInfo数组，</param>
        public static int ExecuteSqlTran(params CommandInfo[] cmdList)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction trans = connection.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int count = 0;
                        //循环
                        foreach (CommandInfo myDE in cmdList)
                        {
                            string cmdText = myDE.CommandText;
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;
                            PrepareCommand(cmd, connection, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            count += val;
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return count;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static int ExecuteSqlTran(params String[] sqlStringList)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction trans = connection.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int count = 0;
                        foreach (String sqlString in sqlStringList)
                        {
                            if (!string.IsNullOrEmpty(sqlString.Trim()))
                            {
                                PrepareCommand(cmd, connection, trans, sqlString, null);
                                count += cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                        }
                        trans.Commit();
                        return count;
                    }
                    catch
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
        }
        #region - 调用示例
        /* 调用示例：
         * List<String> sqlList = new List<String>();
         * sqlList.Add("insert into Log ([UserId] ,[TableName],[LogState],[PKID],[CreateTime]) values ('0','TableName1','0','0','2014-04-08')");
         * sqlList.Add("insert into Log ([UserId] ,[TableName],[LogState],[PKID],[CreateTime]) values ('0','TableName2','0','0','2014-04-08')");
         * DBHelper.ExecuteSqlTran(sqlList);
         * */
        #endregion
        #endregion

        #region - 公共方法
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="tableName">表</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns>返回表的总记录数</returns>
        public static DataTable GetListByPage(string tableName, string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            { strSql.Append("order by T." + orderby); }
            else
            { strSql.Append("order by T.ID desc"); }
            strSql.Append(")AS Row, T.*  from [" + tableName + "] T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            { strSql.Append(" WHERE " + strWhere); }
            strSql.Append(" ) TT");
            if (startIndex <= endIndex)
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            else
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", endIndex, startIndex);
            return GetDataTable(strSql.ToString());
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="tableName">表</param>
        /// <param name="strWhere">条件</param>
        /// <returns>返回表的总记录数</returns>
        public static int GetRecordCount(string tableName, string strWhere = "")
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM [" + tableName + "] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion

        #region - 私有方法
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if (parameter != null)
                    {
                        if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(parameter);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 批量向数据库添加数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string BulkToDB(DataTable dt, string tableName)
        {
            string result = "OK";
            SqlConnection sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            SqlTransaction transaction = sqlConn.BeginTransaction();
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn, SqlBulkCopyOptions.Default, transaction);
            bulkCopy.DestinationTableName = tableName;
            bulkCopy.BatchSize = dt.Rows.Count;
            bulkCopy.BulkCopyTimeout = 360;

            try
            {
                if (dt != null && dt.Rows.Count != 0)
                    bulkCopy.WriteToServer(dt);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                result = ex.ToString();
                throw ex;
            }
            finally
            {
                sqlConn.Close();
                if (bulkCopy != null)
                    bulkCopy.Close();
            }
            return result;
        }
    }
}
