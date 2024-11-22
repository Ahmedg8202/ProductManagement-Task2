using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.Application.DTOs;
using ProductManagement.Infrastructure.Data;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public UserController(
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(CreateUserDto userDto)
        {
            var UserExists = await _userManager.FindByEmailAsync(userDto.Email);
            if (UserExists != null)
                return BadRequest("this email registered!");

            var user = new IdentityUser
            {
                UserName = userDto.Name.Replace(" ", "").ToLower(),
                Email = userDto.Email
            };

            var isCreated = await _userManager.CreateAsync(user, userDto.Password);
            if(!isCreated.Succeeded)
            {
                return BadRequest("Server error");
            }

            if (!await _roleManager.RoleExistsAsync(userDto.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(userDto.Role));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, userDto.Role);
            if (!roleResult.Succeeded)
                return BadRequest("Failed to assign role to user.");


            var userToken = GenerateToken(user);

            return Ok(new
                {
                    user,
                    userToken
                }
               );

        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LogInDto logInDto)
        {
            var userExists = await _userManager.FindByEmailAsync(logInDto.Email);
            if (userExists == null)
                return BadRequest("this email registered!");

            var token = GenerateToken(userExists);

            return Ok(new
            {
                userExists,
                Token = token
            });

        }

        private async Task<string> GenerateToken(IdentityUser user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWTConfig:Key"]);

            var roles = await _userManager.GetRolesAsync(user);

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    //new Claim("Role", user.role)
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                }.Concat(roles.Select(role => 
                    new Claim(ClaimTypes.Role, role)
                    ))),

                Expires = DateTime.Now.AddDays(30),

                SigningCredentials = new SigningCredentials
                    (
                        new SymmetricSecurityKey(key)
                        , SecurityAlgorithms.HmacSha256
                    )
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }

    }
}
