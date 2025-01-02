using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasinmazProject.Business.Abstract;
using TasinmazProject.DataAccess;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Business.Concrete
{
    public class TasinmazService:ITasinmazService
    {
        
            private readonly ApplicationDbContext _context;

            public TasinmazService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Tasinmaz>> GetTasinmazByMahalleIdAsync(int mahalleId) 
            {
                return await _context.Tasinmazlar
                .Include(tasinmaz =>tasinmaz.Mahalle)
              .ThenInclude(mahalle =>mahalle.Ilce)
              .ThenInclude(ilce=>ilce.Il)
                   .Where(tasinmaz=>tasinmaz.MahalleId==mahalleId)
                   .ToListAsync();
            }
        public async Task<Tasinmaz> AddTasinmazAsync(Tasinmaz tasinmaz)
        {
            await _context.Tasinmazlar.AddAsync(tasinmaz);
            await _context.SaveChangesAsync();
            return tasinmaz;
        }

        public async Task<bool> UpdateTasinmazAsync(Tasinmaz tasinmaz)
        {
            var existingTasinmaz = await _context.Tasinmazlar.FindAsync(tasinmaz.Id);
            if (existingTasinmaz == null)
                return false;

            _context.Entry(existingTasinmaz).CurrentValues.SetValues(tasinmaz);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteTasinmazAsync(int id)
        {
            var tasinmaz = await _context.Tasinmazlar.FindAsync(id);
            if (tasinmaz == null)
                return false;

            _context.Tasinmazlar.Remove(tasinmaz);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<List<Tasinmaz>> GetAllTasinmazAsync()
        {
            return await _context.Tasinmazlar.Include(tasinmaz => tasinmaz.Mahalle).ThenInclude(mahalle => mahalle.Ilce)
              .ThenInclude(ilce => ilce.Il)
                .ToListAsync();
        }
    }
    }

