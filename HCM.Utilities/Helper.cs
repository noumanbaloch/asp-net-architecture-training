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

        public struct StoreProcedureNames
        {
            public const string GetUserStoreProcedure = "HCM_PROC_FETCH_USER";
        }
    }

    
}
