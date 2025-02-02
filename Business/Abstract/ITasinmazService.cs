﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Business.Abstract
{
    public interface ITasinmazService
    {
        Task<List<Tasinmaz>> GetTasinmazByMahalleIdAsync(int mahalleId); // ID ye gore

        Task<Tasinmaz>AddTasinmazAsync(Tasinmaz tasinmaz);
        Task<Tasinmaz> UpdateTasinmazAsync(Tasinmaz tasinmaz,int userId);

        Task<bool> DeleteTasinmazAsync(List<int>ids,int userId);

        Task<List<Tasinmaz>> GetAllTasinmazAsync(); //tum tasinmaz

       // Task<bool> DeleteMultipleTasinmazAsync(List<int> ids);

        Task<Tasinmaz> GetTasinmazByIdAsync(int id); // ID ye gore tasinmaz for update

        Task<List<Tasinmaz>> GetTasinmazByUserIdAsync(int userId);

    }
}
