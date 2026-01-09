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

        public UserController(AppDbContext context)
        {
            _dbcontext = context;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> HandleUserRegist([FromBody] UserRegistRequest _request , RegisterUserValidator _userValidations)
        {
            var result = await _userValidations.ValidateAsync(_request);

            if (!result.IsValid)
            {
                return BadRequest(new { errors = result.Errors });
            }

            User newUser = new User
            {
                id = Guid.NewGuid(),
                email = _request.email,
                password = BCrypt.Net.BCrypt.HashPassword(_request.password),
                first_name = _request.first_name,
                last_name = _request.last_name,
                address = _request.address,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _dbcontext.users.Add(newUser);
            _dbcontext.SaveChanges();
            return Created(Uri.UriSchemeHttp, new { message = "success" });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> HandleUserLogin([FromBody] UserLoginRequest _request, IJwtHandler _jwtHandler, LoginUserValidator _userValidations)
        {
            var result = await _userValidations.ValidateAsync(_request);

            if (!result.IsValid) return BadRequest(new { result.Errors });

            var findUser = _dbcontext.users.FirstOrDefault(e => e.email == _request.email)!;
            if (findUser == null) return NotFound("User not found");

            var verify = BCrypt.Net.BCrypt.Verify(_request.password, findUser.password);

            if (!verify) return Unauthorized("Invalid Credential");

            string jwtToken = _jwtHandler.Generate(findUser);

            _dbcontext.users.AsNoTracking();
            return Ok(
                new
                {
                    message = "success",
                    data = findUser,
                    token = jwtToken
                }
            );
        }


        [HttpPost("update")]
        public async Task<IActionResult> HandleUserUpdate([FromBody] UserUpdateRequest _request, UpdateUserValidator _userValidations, IMapper _mapper)
        {
            var validateInput = await _userValidations.ValidateAsync(_request);
            
            if (!validateInput.IsValid) return BadRequest(new { errors = validateInput.Errors });

            var id = User.FindFirstValue("id")!.ToGuid();
            var existuser = await _dbcontext.users.FindAsync(id);
            if (existuser == null) return NotFound("Users Not found");
            var newUser = _mapper.Map(_request, existuser);
            _dbcontext.SaveChanges();


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

    public record UserRegistRequest(string? email , string? password , string? first_name , string? last_name , string? address);
    public record UserLoginRequest(string? email , string? password);
    public record UserUpdateRequest(string? password , string? confirm_password , string? first_name , string? last_name , string? address);

}
