using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TasinmazProject.Business.Abstract;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs([FromQuery] bool? durum = null)
        {
            var logs = await _logService.GetLogsAsync(durum);
            return Ok(logs);
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddLog([FromBody] Log log)
        {
            try
            {
                var createdLog = await _logService.LogAsync(
                    log.durum,
                    log.islemTipi,
                    log.aciklama,
                    log.userIp,
                    log.userId
                );
                return Ok("Log başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, $"Log kaydedilirken bir hata oluştu: {ex.InnerException.Message}");
                }
                else
                {
                    return StatusCode(500, $"Log kaydedilirken bir hata oluştu: {ex.Message}");
                }
            }
        }
        
    }
}
