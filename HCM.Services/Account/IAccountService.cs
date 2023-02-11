using HCM.Models.Dtos.User.Request;
using HCM.Models.Dtos.User.Response;
using HCM.Models.GenericResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.Account
{
    public interface IAccountService
    {
        Task<GenericResponse<UserResponseDto>> RegisterUser(RegisterUserRequestDto requestDto);
        Task<GenericResponse<UserResponseDto>> LoginUser(LoginUserRequestDto requestDto);
    }
}
