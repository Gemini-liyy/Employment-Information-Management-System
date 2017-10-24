using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinge.DBHelp
{
    public class CommandInfo
    {
        public CommandInfo()
        {

        }
        public CommandInfo(string sqlText, params System.Data.Common.DbParameter[] parms)
        {
            this.CommandText = sqlText;
            this.Parameters = parms;
        }

        public string CommandText;
        public System.Data.Common.DbParameter[] Parameters;

    }
}
