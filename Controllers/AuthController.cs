using Microsoft.AspNetCore.Authorization;
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
using TasinmazProject.Business.Abstract;
using TasinmazProject.Business.Concrete;
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
        private  ILogService _logService;

        private IConfiguration _configuration;
        public AuthController(IAuthRepository authRepository,
                        IConfiguration configuration,
                        ILogService logService)  
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _logService = logService;  
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegister userForRegister)
        {
            try
            {
                var userToCreate = new User
                {
                    userEmail = userForRegister.UserEmail,
                    role = "admin"
                };

                var createdUser = await _authRepository.Register(userToCreate, userForRegister.Password);
                return StatusCode(201, new { message = "Kullanıcı başarıyla oluşturuldu." });
            }
            catch (UserAlreadyExistsException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return StatusCode(500, "Bir hata oluştu.");
            }
        }
        public class UserAlreadyExistsException : Exception
        {
            public UserAlreadyExistsException(string message) : base(message) { }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLogin userForLogin)
        {
            try
            {
                var user = await _authRepository.Login(userForLogin.UserEmail, userForLogin.Password);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
                new Claim(ClaimTypes.Email, user.userEmail),
                new Claim(ClaimTypes.Role, user.role)
                    }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                await _logService.LogAsync(
                    durum: true,
                    islemTipi: "Login",
                    aciklama: $"Başarılı giriş. Kullanıcı: {user.userEmail}",
                    userIp: HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    userId: user.userId
                );

                return Ok(new { token = tokenString });
            }
            catch (UserNotFoundException ex)
            {
                await _logService.LogAsync(
                    durum: false,
                    islemTipi: "Login",
                    aciklama: $"Başarısız giriş denemesi. {ex.Message}. Email: {userForLogin.UserEmail}",
                    userIp: HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    userId: null
                );

                return NotFound(ex.Message); // 404 Not Found
            }
            catch (InvalidPasswordException ex)
            {
                await _logService.LogAsync(
                    durum: false,
                    islemTipi: "Login",
                    aciklama: $"Başarısız giriş denemesi. {ex.Message}. Email: {userForLogin.UserEmail}",
                    userIp: HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    userId: null
                );

                return Unauthorized(ex.Message); // 401 Unauthorized
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return StatusCode(500, "Bir hata oluştu.");
            }
        }
        public class UserNotFoundException : Exception
        {
            public UserNotFoundException(string message) : base(message) { }
        }

        public class InvalidPasswordException : Exception
        {
            public InvalidPasswordException(string message) : base(message) { }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }

                }
                return true;

            }
        }
    }
}