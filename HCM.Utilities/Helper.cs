using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Utilities
{
    public static class Helper
    {
        public static DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }

        public static bool IsNullOrEmpty(object obj)
        {
            return  obj is null;
        }

        public struct StoreProcedureNames
        {
            public const string GetUserStoreProcedure = "GJS_PROC_Fetch_User";
        }

        public struct DBConstants
        {
            public const string CREATED_BY = "System User";
            public const string MODIFIED_BY = "System User";
        }
    }
}
