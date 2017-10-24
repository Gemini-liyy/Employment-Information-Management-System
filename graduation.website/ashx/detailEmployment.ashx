<%@ WebHandler Language="C#" Class="detailEmployment" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using graduation.BLL;

using Newtonsoft.Json;
using Tinge.DBHelp;

public class detailEmployment : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string result = "";
        string id = context.Request.QueryString["id"].ToString().Trim();
        string strsql = "select * from dbo.[T_ employment] where id = @id";
        DataTable dt = DBHelper.GetDataTable(strsql,new SqlParameter("@id",id));
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