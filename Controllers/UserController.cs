using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TasinmazProject.Business.Abstract;
using TasinmazProject.Business.Concrete;
using TasinmazProject.DataAccess;
using TasinmazProject.DTO;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
      
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var iller = await _userService.GetAllUsersAsync();
            return Ok(iller);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Password))
                return BadRequest("Password is required.");

            var user = await _userService.AddUserAsync(userDto.Email, userDto.Password, userDto.Role);

            return Ok(user); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDto)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(id, userDto);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            await _userService.DeleteUserAsync(id);

            return Ok(new { message = "User deleted successfully." });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }


    }
}
