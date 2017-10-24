<%@ WebHandler Language="C#" Class="delJob" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using graduation.BLL;

using Newtonsoft.Json;
using Tinge.DBHelp;

public class delJob : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string result = "";
        //获取的参数
        string id = context.Request.QueryString["id"].ToString();
        string strsql = "delete from dbo.[T_ job] where id = @id";
        int ExecuteCommand = DBHelper.ExecuteCommand(strsql,new SqlParameter("@id",id));
        context.Response.Clear();
        context.Response.ContentType = "text/plain";
        context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        result = "删除成功";
        context.Response.Write(result);
        context.Response.End();
    }


    public bool IsReusable {
        get {
            return false;
        }
    }

}