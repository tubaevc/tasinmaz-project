using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TasinmazProject.Business.Abstract;

namespace TasinmazProject.Controllers
{
        [ApiController]
        [Route("api/[controller]")]

    public class MahalleController:ControllerBase
    {
       
            private readonly IMahalleService _mahalleService;

            public MahalleController(IMahalleService mahalleService)
            {
                _mahalleService = mahalleService;
            }

            // GET: api/Ilce/by-il/1
            [HttpGet("by-ilce/{ilceId}")]
            public async Task<IActionResult> GetMahalleByIlceId(int ilceId)
            {
                var mahalleler = await _mahalleService.GetMahalleByIlceIdAsync(ilceId);
                if (mahalleler == null || mahalleler.Count == 0)
                {
                    return NotFound("Bu ile ait ilçe bulunamadı."); // 404
                }
                return Ok(mahalleler); // 200
            }
        }
    }

