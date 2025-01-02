using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasinmazProject.Business.Abstract;
using TasinmazProject.DataAccess;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Business.Concrete
{
    public class IlceService:IIlceService
    {
        private readonly ApplicationDbContext _context;

        public IlceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ilce>> GetIlcelerByIlIdAsync(int ilId) //int ilId
        {
            return await _context.Ilceler
                 .Include(ilce => ilce.Il)
               .Where(ilce => ilce.IlId == ilId)
               .ToListAsync();
        }

    
    }
}
