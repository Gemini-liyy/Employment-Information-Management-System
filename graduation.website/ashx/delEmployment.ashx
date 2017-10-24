<%@ WebHandler Language="C#" Class="delEmployment" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using graduation.BLL;

using Newtonsoft.Json;
using Tinge.DBHelp;

public class delEmployment : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string result = "";
        //获取地址栏里的参数
        string id = context.Request.QueryString["id"].ToString();
        string strsql = "delete from dbo.[T_ employment] where id = @id";
        int ExecuteCommand = DBHelper.ExecuteCommand(strsql,new SqlParameter("@id",id));
        context.Response.ContentType = "text/plain";
        context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        result ="删除成功";
        context.Response.Write(result);
        context.Response.End();
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}