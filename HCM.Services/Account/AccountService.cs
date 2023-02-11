using AutoMapper;
using Dapper;
using HCM.DBCore.UnitOfWork;
using HCM.Models.Dtos.User.Request;
using HCM.Models.Dtos.User.Response;
using HCM.Models.Dtos.User.SP;
using HCM.Models.Entities;
using HCM.Models.GenericResponse;
using HCM.Services.Token;
using HCM.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static HCM.Utilities.Helper;

namespace HCM.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<GenericResponse<UserResponseDto>> RegisterUser(RegisterUserRequestDto requestDto)
        {
            if (UserExist(requestDto.UserName))
            {
                return GenericResponse<UserResponseDto>.Failure(ApiResponseMessage.InvalidUserName, Models.Enums.ApiStatusCode.InvalidUserName);
            }

            var user = _mapper.Map<UserEntity>(requestDto);

            using var hmac = new HMACSHA512();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestDto.Password));
            user.PasswordSalt = hmac.Key;
            user.CreatedBy = DBConstants.CREATED_BY;
            user.CreatedDate = GetCurrentDate();
            user.ModifiedBy = DBConstants.MODIFIED_BY;


            var userRepo = _unitOfWork.GetRepsitory<UserEntity>();

            userRepo.Add(user);

            await _unitOfWork.CommitAsync();

            var response = new UserResponseDto()
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

            return GenericResponse<UserResponseDto>.Success(response, ApiResponseMessage.RecordSavedSuccessfully, Models.Enums.ApiStatusCode.RecordSavedSuccessfully);
        }

        public async Task<GenericResponse<UserResponseDto>> LoginUser(LoginUserRequestDto requestDto)
        {
            var user = await _unitOfWork.GetRepsitory<UserEntity>().FindByFirstOrDefaultAsync(x => x.UserName == requestDto.UserName && x.Deleted == false);
        
            if(IsNullOrEmpty(user))
            {
                return GenericResponse<UserResponseDto>.Failure(ApiResponseMessage.InvalidUserName, Models.Enums.ApiStatusCode.InvalidUserName);
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestDto.Password));

            for(int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return GenericResponse<UserResponseDto>.Failure(ApiResponseMessage.InvalidPassword, Models.Enums.ApiStatusCode.InvalidPassword);
                }
            }

            var response = new UserResponseDto()
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

            return GenericResponse<UserResponseDto>.Success(response, ApiResponseMessage.RecordSavedSuccessfully, Models.Enums.ApiStatusCode.RecordSavedSuccessfully);

        }

        #region Private Methods
        private bool UserExist(string userName)
        {
            DynamicParameters parameters = new();
            parameters.Add("@Username", userName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            var result = _unitOfWork.DapperSPSingleWithParams<UserSPDto>("exec " + StoreProcedureNames.GetUserStoreProcedure + " @Username", parameters);
            return result != null;
        
        }

        #endregion


    }
}
