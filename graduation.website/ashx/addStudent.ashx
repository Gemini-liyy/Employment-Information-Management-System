<%@ WebHandler Language="C#" Class="addStudent" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using graduation.BLL;

using Newtonsoft.Json;
using Tinge.DBHelp;

public class addStudent : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string result = "";
        string stu_id = context.Request.QueryString["stu_id"].ToString();
        string password = context.Request.QueryString["password"].ToString();
        string name = context.Request.QueryString["name"].ToString();
        string gender = context.Request.QueryString["gender"].ToString();
        string nation = context.Request.QueryString["nation"].ToString();
        string admission = context.Request.QueryString["admission"].ToString();
        string zhengzhimm = context.Request.QueryString["zhengzhimm"].ToString();
        string phone = context.Request.QueryString["phone"].ToString();
        string birthday = context.Request.QueryString["birthday"].ToString();
        string IDnumber = context.Request.QueryString["IDnumber"].ToString();
        string Postcode = context.Request.QueryString["Postcode"].ToString();
        string shengyuandi = context.Request.QueryString["shengyuandi"].ToString();
        string address = context.Request.QueryString["address"].ToString();
        string strsql = "insert into dbo.T_stu(stu_id,password,name,gender,nation,admission,zhengzhimm,phone,birthday,IDnumber,Postcode,shengyuandi,address) values(@stu_id,@password,@name,@gender,@nation,@admission,@zhengzhimm,@phone,@birthday,@IDnumber,@Postcode,@shengyuandi,@address)";
        SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@stu_id",stu_id),
                new SqlParameter("@password",password),
                new SqlParameter("@name",name),
                new SqlParameter("@gender",gender),
                new SqlParameter("@nation",nation),
                new SqlParameter("@admission",admission),
                new SqlParameter("@zhengzhimm",zhengzhimm),
                new SqlParameter("@phone",phone),
                new SqlParameter("@birthday",birthday),
                new SqlParameter("@IDnumber",IDnumber),
                new SqlParameter("@Postcode",Postcode),
                new SqlParameter("@shengyuandi",shengyuandi),
                new SqlParameter("@address",address),
            };
        int ExecuteCommand = DBHelper.ExecuteCommand(strsql,paras);
        context.Response.Redirect("../html/studentList.html");
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