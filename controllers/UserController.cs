using System.Data.Common;
using System.Security.Claims;
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

        public UserController(
            AppDbContext context ,
            LoginUserValidator _loginVal,
            RegisterUserValidator _regVal,
            UpdateUserValidator _updVal,
            IMapper _map,
            ILoginService _loginservice,
            IRegisterService _registerservice,
            IUpdateService _updateservice
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
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> HandleUserRegist([FromBody] UserRegistRequest _request)
        {
            var result = await _registerValidator.ValidateAsync(_request);

            if (!result.IsValid) return BadRequest(new {errors = result.Errors});

            await _registerService.RegisterUserAsync(_request);

            return Created(Uri.UriSchemeHttp, new { message = "success" });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> HandleUserLogin([FromBody] UserLoginRequest _request)
        {
            var result = await _loginValidator.ValidateAsync(_request);

            if (!result.IsValid) return BadRequest(new { errors = result.Errors});

            var response = await _loginService.Authenticate(_request);
            
            return Ok(response);
        }


        [HttpPost("update")]
        public async Task<IActionResult> HandleUserUpdate([FromBody] UserUpdateRequest _request , string idKey = "id")
        {
            var validateInput = await _updateValidator.ValidateAsync(_request);
            var idUser = User.FindFirstValue(idKey)!.ToGuid();
            
            if (!validateInput.IsValid) return BadRequest(new { errors = validateInput.Errors });

            var newUser = await _updateService.UpdateUserAsync(_request , idUser);
            
            return Ok(new { message = "success", data = newUser });
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
