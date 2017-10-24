<%@ WebHandler Language="C#" Class="addJob" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using graduation.BLL;

using Newtonsoft.Json;
using Tinge.DBHelp;

public class addJob : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string result = "";
        string post = context.Request.QueryString["post"].ToString().Trim();
        string xueli = context.Request.QueryString["xueli"].ToString().Trim();
        string salary = context.Request.QueryString["salary"].ToString().Trim();
        string address = context.Request.QueryString["address"].ToString().Trim();
        string experience = context.Request.QueryString["experience"].ToString().Trim();
        string remark = context.Request.QueryString["remark"].ToString().Trim();
        string strsql = "insert into dbo.[T_ job](post,xueli,salary,address,experience,remark) values(@post,@xueli,@salary,@address,@experience,@remark)";
        SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@post",post),
                new SqlParameter("@xueli",xueli),
                new SqlParameter("@salary",salary),
                new SqlParameter("@address",address),
                new SqlParameter("@experience",experience),
                new SqlParameter("@remark",remark),
            };
        int ExecuteCommand = DBHelper.ExecuteCommand(strsql,paras);
        context.Response.Redirect("../html/jobList.html");
        //context.Response.Clear();
        //context.Response.ContentType = "text/plain";
        //context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        //context.Response.Write(result);
        //context.Response.End();
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}