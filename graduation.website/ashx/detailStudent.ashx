﻿<%@ WebHandler Language="C#" Class="detailStudent" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using graduation.BLL;

using Newtonsoft.Json;
using Tinge.DBHelp;

public class detailStudent : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string result = "";
        string stu_id = context.Request.QueryString["stu_id"].ToString().Trim();
        string strsql = "select * from dbo.T_stu where stu_id = @stu_id";
        DataTable dt = DBHelper.GetDataTable(strsql,new SqlParameter("@stu_id",stu_id));
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