using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TasinmazProject.Business.Abstract;
using TasinmazProject.DataAccess;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
    public class IllerController:ControllerBase
    {
     
            private readonly IIlService _ilService;

            public IllerController(IIlService ilService)
            {
                _ilService = ilService;
            }

            // GET: api/Il/1
            [HttpGet("{id}")]
            public async Task<IActionResult> GetIlById(int id)
            {
                var il = await _ilService.GetIlByIdAsync(id);
                if (il == null)
                {
                    return NotFound("İl bulunamadı.");
                }
                return Ok(il);
            }

            // GET: api/Il
            [HttpGet]
            public async Task<IActionResult> GetAllIller()
            {
                var iller = await _ilService.GetAllIllerAsync();
                return Ok(iller);
            }

        }
}
