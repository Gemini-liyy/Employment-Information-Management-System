<%@ WebHandler Language="C#" Class="addEmployment" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using graduation.BLL;

using Newtonsoft.Json;
using Tinge.DBHelp;

public class addEmployment : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string result = "";
        string stu_id = context.Request.QueryString["stu_id"].ToString();
        string name = context.Request.QueryString["name"].ToString();
        string jiuzhishijian = context.Request.QueryString["jiuzhishijian"].ToString();
        string qiye_name = context.Request.QueryString["qiye_name"].ToString();
        string jiuzhigangwei = context.Request.QueryString["jiuzhigangwei"].ToString();
        string salary = context.Request.QueryString["salary"].ToString();
        string remark = context.Request.QueryString["remark"].ToString();
        string strsql = "insert into dbo.[T_ employment](stu_id,name,jiuzhishijian,qiye_name,jiuzhigangwei,salary,remark) values(@stu_id,@name,@jiuzhishijian,@qiye_name,@jiuzhigangwei,@salary,@remark)";
        SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@stu_id",stu_id),
                new SqlParameter("@name",name),
                new SqlParameter("@jiuzhishijian",jiuzhishijian),
                new SqlParameter("@qiye_name",qiye_name),
                new SqlParameter("@jiuzhigangwei",jiuzhigangwei),
                new SqlParameter("@salary",salary),
                new SqlParameter("@remark",remark),
            };
        int ExecuteCommand = DBHelper.ExecuteCommand(strsql,paras);
        context.Response.Redirect("../html/studentEmploymentList.html");
        //result = "添加成功";
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