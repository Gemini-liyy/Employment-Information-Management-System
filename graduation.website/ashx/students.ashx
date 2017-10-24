﻿<%@ WebHandler Language="C#" Class="students" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using graduation.BLL;

using Newtonsoft.Json;
using Tinge.DBHelp;


public class students : IHttpHandler{

    public void ProcessRequest(HttpContext context){
        string result = "";
        //查询语句
        string strsql = "select * from dbo.T_stu";
        DataTable dt = DBHelper.GetDataTable(strsql);
        result = JsonConvert.SerializeObject(dt);
        //string uid = context.Request.QueryString["uid"].ToString();
        //string pid = context.Request.QueryString["pid"].ToString();
        context.Response.Clear();
        context.Response.ContentType = "text/plain";
        context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        context.Response.Write(result);
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}