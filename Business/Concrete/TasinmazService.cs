using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task<Tasinmaz> GetTasinmazByIdAsync(int id)
        {
            return await _context.Tasinmazlar
                   .Include(t => t.Mahalle)
                   .ThenInclude(m => m.Ilce) 
                   .ThenInclude(i => i.Il) 
                   .FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<List<Tasinmaz>> GetTasinmazByMahalleIdAsync(int mahalleId)
        {
            return await _context.Tasinmazlar
            .Include(tasinmaz => tasinmaz.Mahalle)
          .ThenInclude(mahalle => mahalle.Ilce)
          .ThenInclude(ilce => ilce.Il)
               .Where(tasinmaz => tasinmaz.MahalleId == mahalleId)
               .ToListAsync();
        }
        public async Task<Tasinmaz> AddTasinmazAsync(Tasinmaz tasinmaz)
        {
            try
            {
                await _context.Tasinmazlar.AddAsync(tasinmaz);
                await _context.SaveChangesAsync();

                var addedTasinmaz = await _context.Tasinmazlar
                    .Include(t => t.Mahalle)
                        .ThenInclude(m => m.Ilce)
                            .ThenInclude(i => i.Il)
                    .FirstOrDefaultAsync(t => t.Id == tasinmaz.Id);

                return addedTasinmaz;
            }
            catch (Exception ex)
            {
                throw new Exception($"Veritabanına eklerken hata oluştu: {ex.Message}. Inner Exception: {ex.InnerException?.Message}");
            }
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

        public async Task<bool> DeleteMultipleTasinmazAsync(List<int> ids)
        {
            var tasinmazlar = await _context.Tasinmazlar
                .Where(t => ids.Contains(t.Id))
                .ToListAsync();

            if (tasinmazlar.Count == 0)
            {
                return false; 
            }

            _context.Tasinmazlar.RemoveRange(tasinmazlar);
            await _context.SaveChangesAsync();

            return true;
        }



    }
}

