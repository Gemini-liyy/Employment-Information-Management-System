<%@ WebHandler Language="C#" Class="updatePersonal" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using graduation.BLL;

using Newtonsoft.Json;
using Tinge.DBHelp;

public class updatePersonal : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string result = "";
        string stu_id = context.Request.QueryString["stu_id"].ToString();
        string password = context.Request.QueryString["password"].ToString();
        string admission = context.Request.QueryString["admission"].ToString();
        string zhengzhimm = context.Request.QueryString["zhengzhimm"].ToString();
        string phone = context.Request.QueryString["phone"].ToString();
        string birthday = context.Request.QueryString["birthday"].ToString();
        string IDnumber = context.Request.QueryString["IDnumber"].ToString();
        string Postcode = context.Request.QueryString["Postcode"].ToString();
        string shengyuandi = context.Request.QueryString["shengyuandi"].ToString();
        string address = context.Request.QueryString["address"].ToString();
        string strsql = "update dbo.T_stu set password = @password,admission = @admission,zhengzhimm = @zhengzhimm,phone = @phone,birthday = @birthday,IDnumber = @IDnumber,Postcode = @Postcode,shengyuandi = @shengyuandi,address = @address where stu_id = @stu_id";
        SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@stu_id",stu_id),
                new SqlParameter("@password",password),
                new SqlParameter("@admission",admission),
                new SqlParameter("@zhengzhimm",zhengzhimm),
                new SqlParameter("@phone",phone),
                new SqlParameter("@birthday",birthday),
                new SqlParameter("@IDnumber",IDnumber),
                new SqlParameter("@Postcode",Postcode),
                new SqlParameter("@shengyuandi",shengyuandi),
                new SqlParameter("@address",address),
            };
        int ExecuteCommand = DBHelper.ExecuteCommand(strsql, paras);
        context.Response.Redirect("../html/personal.html");
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}