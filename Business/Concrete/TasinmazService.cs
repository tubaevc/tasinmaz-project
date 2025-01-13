using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TasinmazProject.Business.Abstract;
using TasinmazProject.DataAccess;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Business.Concrete
{
    public class TasinmazService:ITasinmazService
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogService _logService;

        public TasinmazService(ApplicationDbContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
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

                // Log kaydı
                await _logService.LogAsync(
                    true,
                    "Ekleme",
                    $"Taşınmaz (ID: {tasinmaz.Id}) başarıyla eklendi.",
                    "127.0.0.1", 
                    tasinmaz.userId
                );

                var addedTasinmaz = await _context.Tasinmazlar
                    .Include(t => t.Mahalle)
                        .ThenInclude(m => m.Ilce)
                            .ThenInclude(i => i.Il)
                    .FirstOrDefaultAsync(t => t.Id == tasinmaz.Id);

                return addedTasinmaz;
            }
            catch (Exception ex)
            {
                await _logService.LogAsync(
                    false,
                    "Ekleme",
                    $"Hata oluştu: {ex.Message}",
                    "127.0.0.1",
                    tasinmaz.userId
                );
                throw;
            }
        }

        public async Task<Tasinmaz> UpdateTasinmazAsync(Tasinmaz tasinmaz,int userId)
        {
            try
            {

                var existingTasinmaz = await _context.Tasinmazlar.FindAsync(tasinmaz.Id);

                if (existingTasinmaz == null)
                {
             

                    await _logService.LogAsync(
                        false,
                        "Güncelleme",
                        $"Taşınmaz (ID: {tasinmaz.Id}) bulunamadı.",
                        "127.0.0.1",
                        userId
                    );
                    return null;
                }

                existingTasinmaz.Ada = tasinmaz.Ada;
                existingTasinmaz.Parsel = tasinmaz.Parsel;
                existingTasinmaz.Nitelik = tasinmaz.Nitelik;
                existingTasinmaz.Adres = tasinmaz.Adres;
                existingTasinmaz.MahalleId = tasinmaz.MahalleId;

                await _context.SaveChangesAsync();

                await _logService.LogAsync(
                    true,
                    "Güncelleme",
                    $"Taşınmaz (ID: {tasinmaz.Id}) başarıyla güncellendi.",
                    "127.0.0.1",
                    userId
                );

                var updatedTasinmaz = await _context.Tasinmazlar
                    .Include(t => t.Mahalle)
                        .ThenInclude(m => m.Ilce)
                            .ThenInclude(i => i.Il)
                    .FirstOrDefaultAsync(t => t.Id == tasinmaz.Id);

                return updatedTasinmaz;
            }
            catch (Exception ex)
            {
                await _logService.LogAsync(
                    false,
                    "Güncelleme",
                    $"Hata oluştu: {ex.Message}",
                    "127.0.0.1",
                    tasinmaz.userId
                );
                throw;
            }
        }

     
        public async Task<List<Tasinmaz>> GetAllTasinmazAsync()
        {
            return await _context.Tasinmazlar.Include(tasinmaz => tasinmaz.Mahalle).ThenInclude(mahalle => mahalle.Ilce)
              .ThenInclude(ilce => ilce.Il)
                .ToListAsync();
        }



        public async Task<bool> DeleteTasinmazAsync(List<int> ids, int userId)
        {
            var tasinmazlar = await _context.Tasinmazlar
                .Where(t => ids.Contains(t.Id))
                .ToListAsync();

            if (tasinmazlar.Count == 0)
            {
                await _logService.LogAsync(
                    false,
                    "Silme",
                    $"Hiçbir taşınmaz silinemedi. IDs: {string.Join(", ", ids)}",
                    "127.0.0.1",
                    userId
                );
                return false;
            }

            _context.Tasinmazlar.RemoveRange(tasinmazlar);
            await _context.SaveChangesAsync();

            await _logService.LogAsync(
                true,
                "Silme",
                $"Seçili taşınmazlar ({string.Join(", ", ids)}) başarıyla silindi.",
                "127.0.0.1",
                userId
            );

            return true;
        }



        public async Task<List<Tasinmaz>> GetTasinmazByUserIdAsync(int userId)
        {
            return await _context.Tasinmazlar
                .Where(t => t.userId == userId)
                .Include(t => t.Mahalle)
                .ThenInclude(m => m.Ilce)
                .ThenInclude(i => i.Il)
                .ToListAsync();
        }

    }
}

