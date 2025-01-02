using System.Collections.Generic;
using System.Threading.Tasks;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Business.Abstract
{
    public interface IMahalleService
    {
        Task<List<Mahalle>> GetMahalleByIlceIdAsync(int ilceId); // ID ye gore 

    }
}
