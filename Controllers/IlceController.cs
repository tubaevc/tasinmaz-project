using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TasinmazProject.Business.Abstract;

namespace TasinmazProject.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
 
        public class IlceController : ControllerBase
        {
            private readonly IIlceService _ilceService;

            public IlceController(IIlceService ilceService)
            {
                _ilceService = ilceService;
            }

            // GET: api/Ilce/by-il/1
            [HttpGet("by-il/{ilId}")]
            public async Task<IActionResult> GetIlcelerByIlId(int ilId)
            {
                var ilceler = await _ilceService.GetIlcelerByIlIdAsync(ilId);
                if (ilceler == null || ilceler.Count == 0)
                {
                    return NotFound("Bu ile ait ilçe bulunamadı."); // 404
                }
                return Ok(ilceler); // 200
            }
        }

    }
    

