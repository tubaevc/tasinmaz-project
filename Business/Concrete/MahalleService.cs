using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasinmazProject.Business.Abstract;
using TasinmazProject.DataAccess;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Business.Concrete
{
    public class MahalleService:IMahalleService
    {
        private readonly ApplicationDbContext _context;

        public MahalleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Mahalle>> GetMahalleByIlceIdAsync(int ilceId) 
        {
            return await _context.Mahalleler
                .Include(mahalle=>mahalle.Ilce)
                 .ThenInclude(ilce => ilce.Il)
               .Where(mahalle => mahalle.IlceId == ilceId)
               .ToListAsync();
        }

    }
}
