using System.Collections.Generic;
using System.Threading.Tasks;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Business.Abstract
{
    public interface IIlceService
    {
        Task<List<Ilce>> GetIlcelerByIlIdAsync(int ilId); // ID ye gore ilce
    }
}
