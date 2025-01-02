using System.Collections.Generic;
using System.Threading.Tasks;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Business.Abstract
{
    public interface ITasinmazService
    {
        Task<List<Tasinmaz>> GetTasinmazByMahalleIdAsync(int mahalleId); // ID ye gore

        Task<Tasinmaz>AddTasinmazAsync(Tasinmaz tasinmaz);
        Task<bool> UpdateTasinmazAsync(Tasinmaz tasinmaz);

        Task<bool> DeleteTasinmazAsync(int id);

        Task<List<Tasinmaz>> GetAllTasinmazAsync(); //tum tasinmaz

        Task<bool> DeleteMultipleTasinmazAsync(List<int> ids);


    }
}
