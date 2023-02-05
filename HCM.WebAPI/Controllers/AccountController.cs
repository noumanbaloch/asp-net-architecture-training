using HCM.Models.Dtos.User.Request;
using HCM.Services.Account;
using Microsoft.AspNetCore.Mvc;

namespace HCM.WebAPI.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserRequestDto requestDto)
        {
            return Ok(await _accountService.RegisterUser(requestDto));
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserRequestDto requestDto)
        {
            return Ok(await _accountService.LoginUser(requestDto));
        }

    }
}
