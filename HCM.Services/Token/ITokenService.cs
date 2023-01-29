using HCM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.Token
{
    public interface ITokenService
    {
        string CreateToken(UserEntity user);
    }
}
