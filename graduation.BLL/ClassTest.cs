using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using Tinge.DBHelp;

namespace graduation.BLL
{
    public class ClassTest
    {
        public static DataTable GetTest()
        {
            string strsql = "select * from dbo.T_stu";
            return DBHelper.GetDataTable(strsql);
        }
    }
}
