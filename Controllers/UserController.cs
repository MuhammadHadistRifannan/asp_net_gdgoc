using System.Data.Common;
using System.Security.Claims;
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
        public async Task<IActionResult> HandleUserRegist([FromBody] User user , RegisterUserValidator _userValidations)
        {
            var result = _userValidations.Validate(user);
            
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            
            User newUser = new User
            {
                id = Guid.NewGuid() ,
                email = user.email,
                password = BCrypt.Net.BCrypt.HashPassword(user.password),
                first_name = user.first_name,
                last_name = user.last_name,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _dbcontext.users.Add(newUser);
            _dbcontext.SaveChanges();
            return Ok(new
            {
                message = "success"
            });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> HandleUserLogin([FromBody] User user ,IJwtHandler _jwtHandler , LoginUserValidator _userValidations)
        {
            var result = _userValidations.Validate(user);

            if (!result.IsValid) return BadRequest(result.Errors);

            var findUser = _dbcontext.users.FirstOrDefault(e => e.email == user.email)!;
            if (findUser == null) return NotFound("User not found");
            
            var verify = BCrypt.Net.BCrypt.Verify(user.password , findUser.password);

            if (!verify) return Unauthorized("Invalid Credential");

            string jwtToken = _jwtHandler.Generate(findUser);

            return Ok(
                new
                {
                    message = "success",
                    data = findUser,
                    token = jwtToken
                }
            );
        }

        [HttpGet("profile")]
        public async Task<IActionResult> ShowUserProfile()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);

            return Ok(new
                {
                    message = "success",
                    user = new
                    {
                        username = username , 
                        email = email
                    }
                });

        }
    }
}
