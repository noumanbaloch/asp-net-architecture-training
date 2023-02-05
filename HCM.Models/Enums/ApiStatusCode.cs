using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Models.Enums
{
    public enum ApiStatusCode
    {
        // 100 series for failure
        RecordAlreadyExist = 100,
        InvalidUserName = 101,
        InvalidPassword = 102,
        RecordNotFound = 103,

        // 200 series for success
        RecordSavedSuccessfully = 200,
        SuccessfullLogin = 201
    }
}
