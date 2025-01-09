using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TasinmazProject.Data;
using TasinmazProject.DTO;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IAuthRepository _authRepository;

        private IConfiguration _configuration;
        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegister userForRegister)
        {
            if (await _authRepository.UserExists(userForRegister.UserEmail))
            {
                ModelState.AddModelError("UserEmail", "UserEmail already exists");

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userToCreate = new User
            {
                userEmail = userForRegister.UserEmail,
                role = "admin"
            };
            var createdUser = await _authRepository.Register(userToCreate, userForRegister.Password);
            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLogin userForLogin)
        {
            var user = await _authRepository.Login(userForLogin.UserEmail, userForLogin.Password);

            if (user == null)
            {
                return Unauthorized("Kullanıcı bulunamadı!");
            }

            user.role = user.role ?? "Admin"; 

            var tokenHandler = new JwtSecurityTokenHandler(); //token olusturur
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor() //bilgiler belirtilir
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
            new Claim(ClaimTypes.Email, user.userEmail),
            new Claim(ClaimTypes.Role, user.role) // role her zaman dolu olacaktır
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString });
        }
    }
}