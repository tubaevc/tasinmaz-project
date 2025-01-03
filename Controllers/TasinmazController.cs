﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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


        // POST: api/Tasinmaz
        [HttpPost]
        public async Task<IActionResult> CreateTasinmaz([FromBody] Tasinmaz tasinmaz)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Model hatalarını dön

            if (tasinmaz == null)
                return BadRequest("Taşınmaz bilgileri boş olamaz.");

            var result = await _tasinmazService.AddTasinmazAsync(tasinmaz);
            if (result == null)
                return BadRequest("Taşınmaz eklenirken bir hata oluştu.");

            return CreatedAtAction(nameof(GetTasinmazByMahalleId), new { mahalleId = tasinmaz.MahalleId }, result);


        }


        // PUT: api/Tasinmaz/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTasinmaz(int id, [FromBody] Tasinmaz tasinmaz)
        //{
        //    if (id != tasinmaz.Id)
        //        return BadRequest("ID'ler eşleşmiyor.");

        //    var result = await _tasinmazService.UpdateTasinmazAsync(tasinmaz);
        //    if (!result)
        //        return NotFound($"ID: {id} olan taşınmaz bulunamadı.");

        //    return NoContent(); // 204
        //}

        //DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasinmaz(int id)
        {
            // ID check
            var tasinmaz = await _tasinmazService.GetTasinmazByMahalleIdAsync(id);
            if (tasinmaz == null)
            {
                // 404
                return NotFound($"ID: {id} olan taşınmaz bulunamadı.");
            }

            // delete
            var result = await _tasinmazService.DeleteTasinmazAsync(id);
            if (!result)
            {
                return BadRequest("Taşınmaz silinirken bir hata oluştu.");
            }

            return NoContent();
        }

        //multi delete için list alsın
        [HttpPost("multi-delete")]
        public async Task<IActionResult> DeleteMultipleTasinmaz([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("Silinecek ID listesi boş.");
            }

            var result = await _tasinmazService.DeleteMultipleTasinmazAsync(ids);

            if (!result)
            {
                return BadRequest("Bazı kayıtlar silinirken bir hata oluştu.");
            }

            return Ok("Seçili taşınmazlar başarıyla silindi.");
        }

        //update icin id ile get
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTasinmazById(int id)
        {
            var tasinmaz = await _tasinmazService.GetTasinmazByIdAsync(id);
            if (tasinmaz == null)
            {
                return NotFound("Taşınmaz bulunamadı.");
            }
            return Ok(tasinmaz);
        }
      

    }
}

