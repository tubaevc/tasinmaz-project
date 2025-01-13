using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TasinmazProject.Business.Abstract;
using TasinmazProject.Business.Concrete;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TasinmazController : ControllerBase
    {

        private readonly ITasinmazService _tasinmazService;

        public TasinmazController(ITasinmazService tasinmazService)
        {
            _tasinmazService = tasinmazService;
        }

        // GET: api/Ilce/by-il/1
        [HttpGet("by-mahalle/{mahalleId}")]
        public async Task<IActionResult> GetTasinmazByMahalleId(int mahalleId)
        {
            var tasinmaz = await _tasinmazService.GetTasinmazByMahalleIdAsync(mahalleId);
            if (tasinmaz == null || tasinmaz.Count == 0)
            {
                return NotFound("Bu ile ait ilçe bulunamadı."); // 404
            }
            return Ok(tasinmaz); // 200
        }

        // Tum tasinmaz
        [HttpGet()]
        public async Task<IActionResult> GetAllTasinmaz()
        {
            var tasinmaz = await _tasinmazService.GetAllTasinmazAsync();
            return Ok(tasinmaz);
        }
  
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> CreateTasinmaz([FromBody] Tasinmaz tasinmaz)
        {
            try
            {
                Console.WriteLine($"Auth Header: {Request.Headers["Authorization"]}");

                foreach (var claim in User.Claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
                }

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Console.WriteLine($"User ID Claim: {userIdClaim ?? "null"}");

                if (string.IsNullOrEmpty(userIdClaim))
                {
                    Console.WriteLine("Unauthorized: User ID claim bulunamadı");
                    return Unauthorized("Kullanıcı oturum açmamış.");
                }

                if (int.TryParse(userIdClaim, out int userId))
                {
                    tasinmaz.userId = userId;
                }
                else
                {
                    Console.WriteLine($"Geçersiz user ID formatı: {userIdClaim}");
                    return BadRequest("Geçersiz kullanıcı ID'si.");
                }

                var result = await _tasinmazService.AddTasinmazAsync(tasinmaz);
                if (result == null)
                {
                    Console.WriteLine("Taşınmaz ekleme başarısız");
                    return BadRequest("Taşınmaz eklenirken bir hata oluştu.");
                }

                return CreatedAtAction(nameof(GetTasinmazById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }


        //PUT: api/Tasinmaz/id
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTasinmaz(int id, [FromBody] Tasinmaz tasinmaz)
        {
            try
            {   
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

                if (userId == 0)
                {
                    return Unauthorized("Kullanıcı kimliği alınamadı.");
                }

                var updatedTasinmaz = await _tasinmazService.UpdateTasinmazAsync(tasinmaz, userId);

                if (updatedTasinmaz == null)
                {
                    return NotFound("Taşınmaz bulunamadı.");
                }

                return Ok(updatedTasinmaz);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update error: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return StatusCode(500, $"Güncelleme sırasında hata oluştu: {ex.Message}");
            }
        }
        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTasinmaz([FromBody] List<int> ids)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            if (userId == 0)
            {
                return Unauthorized("Kullanıcı kimliği alınamadı.");
            }

            if (ids == null || ids.Count == 0)
            {
                return BadRequest("Silinecek ID listesi boş.");
            }

            var result = await _tasinmazService.DeleteTasinmazAsync(ids, userId);

            if (!result)
            {
                return BadRequest("Bazı kayıtlar silinirken bir hata oluştu.");
            }

            return Ok(new { message = "Seçili taşınmazlar başarıyla silindi." });
        }




        //update icin id ile get
        [Authorize]
        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetTasinmazById(int id)
        {
            var tasinmaz = await _tasinmazService.GetTasinmazByIdAsync(id);
            if (tasinmaz == null)
            {
                return NotFound("Taşınmaz bulunamadı.");
            }
            return Ok(tasinmaz);
        }

        // userId ye gore tasinmaz for users tasinmaz

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetTasinmazByUserId(int userId)
        {
            var tasinmazlar = await _tasinmazService.GetTasinmazByUserIdAsync(userId);

            if (tasinmazlar == null || tasinmazlar.Count == 0)
            {
                return NotFound("Bu id ye ait tasinmaz bulunamadı.");
            }

            return Ok(tasinmazlar);
        }



    }
}

