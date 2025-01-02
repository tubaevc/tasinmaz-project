using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasinmazProject.Business.Abstract;
using TasinmazProject.DataAccess;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Business.Concrete
{
    public class IlService : IIlService
    {
        private readonly ApplicationDbContext _context;

        public IlService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Il> GetIlByIdAsync(int id)
        {
            return await _context.Iller.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Il>> GetAllIllerAsync()
        {
            return await _context.Iller.ToListAsync();
        }
    }
}
