<%@ WebHandler Language="C#" Class="delStudent" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using graduation.BLL;

using Newtonsoft.Json;
using Tinge.DBHelp;

public class delStudent : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string result = "";
        //获取地址栏里的参数
        string stu_id = context.Request.QueryString["stu_id"].ToString();
        string strsql = "delete from dbo.T_stu where stu_id = @stu_id";
        int ExecuteCommand = DBHelper.ExecuteCommand(strsql,new SqlParameter("@stu_id",stu_id));
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