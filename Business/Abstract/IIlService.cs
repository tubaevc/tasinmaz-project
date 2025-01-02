using System.Collections.Generic;
using System.Threading.Tasks;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Business.Abstract
{
    public interface IIlService
    {
      
        // asenkron
        Task<Il> GetIlByIdAsync(int id); // ID ye gore il
        Task<List<Il>> GetAllIllerAsync(); //tum iller
    }
}
