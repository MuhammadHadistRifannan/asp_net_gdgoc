using System.Data.Common;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using APIResponseWrapper;
using AutoMapper;
using BCrypt.Net;
using gdgoc_aspnet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Authorize]
    [Route("users/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly AppDbContext _dbcontext;
        readonly LoginUserValidator _loginValidator;
        readonly RegisterUserValidator _registerValidator;
        readonly UpdateUserValidator _updateValidator;
        readonly IMapper _mapper;
        readonly ILoginService _loginService;
        readonly IRegisterService _registerService;
        readonly IUpdateService _updateService;
        readonly IDeleteService _deleteService;

        public UserController(
            AppDbContext context ,
            LoginUserValidator _loginVal,
            RegisterUserValidator _regVal,
            UpdateUserValidator _updVal,
            IMapper _map,
            ILoginService _loginservice,
            IRegisterService _registerservice,
            IUpdateService _updateservice,
            IDeleteService _deleteservice
        )
        {
            _dbcontext = context;
            _registerValidator = _regVal;
            _loginValidator = _loginVal;
            _updateValidator = _updVal;
            _mapper = _map;
            _loginService = _loginservice;
            _registerService = _registerservice;
            _updateService = _updateservice;
            _deleteService = _deleteservice;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> HandleUserRegist([FromBody] UserRegistRequest _request)
        {
            var result = await _registerValidator.ValidateAsync(_request);

            if (!result.IsValid) return BadRequest(new {errors = result.Errors});

            await _registerService.RegisterUserAsync(_request);

            return Created(Uri.UriSchemeHttp, new ApiResponse<string>(true , "Register Success" , _request , statusCode: System.Net.HttpStatusCode.OK));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> HandleUserLogin([FromBody] UserLoginRequest _request)
        {
            var result = await _loginValidator.ValidateAsync(_request);

            if (!result.IsValid) return BadRequest(new { errors = result.Errors});

            var response = await _loginService.Authenticate(_request);
            
            return Ok(new ApiResponse<string>(true , "Login Success" , response , statusCode:System.Net.HttpStatusCode.OK));
        }


        [HttpPatch("update")]
        public async Task<IActionResult> HandleUserUpdate([FromBody] UserUpdateRequest _request , string idKey = "id")
        {
            var validateInput = await _updateValidator.ValidateAsync(_request);
            var idUser = User.FindFirstValue(idKey)!.ToGuid();
            
            if (!validateInput.IsValid) return BadRequest(new { errors = validateInput.Errors });

            var newUser = await _updateService.UpdateUserAsync(_request , idUser);
            
            return Ok(new ApiResponse<string>(true , "Update Successfully" , newUser! , statusCode: System.Net.HttpStatusCode.OK));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> HandleUserDelete(string idKey = "id")
        {
            var id = User.FindFirstValue(idKey)!.ToGuid();
            await _deleteService.DeleteUserAsync(id);
            return Ok(new ApiResponse<string>(true , "Account Deleted" , statusCode:System.Net.HttpStatusCode.OK));
        }

        [HttpGet("profile")]
        public async Task<IActionResult> ShowUserProfile()
        {
            var id = User.FindFirstValue("id")!.ToGuid();
            var exist_user = await _dbcontext.users.FindAsync(id);
            _dbcontext.users.AsNoTracking();
            return Ok(new
            {
                message = "success",
                user = new
                {
                    id = id,
                    first_name = exist_user!.first_name,
                    last_name = exist_user.last_name,
                    email = exist_user.email,
                    address = exist_user.address
                }
            });

        }

    }

   

}
