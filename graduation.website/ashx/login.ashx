<%@ WebHandler Language="C#" Class="login" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using graduation.BLL;

using Newtonsoft.Json;
using Tinge.DBHelp;

public class login : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string result = "";
        string stu_id = context.Request.QueryString["stu_id"].ToString().Trim();
        string password = context.Request.QueryString["password"].ToString().Trim();
        string strsql = "select * from dbo.T_stu where  stu_id=@stu_id and password=@password";
        SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@stu_id",stu_id),
                new SqlParameter("@password",password),
            };
        DataTable dt = DBHelper.GetDataTable(strsql,paras);
        result = JsonConvert.SerializeObject(dt);
        context.Response.Clear();
        context.Response.ContentType = "text/plain";
        context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        context.Response.Write(result);
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}