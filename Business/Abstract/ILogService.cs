using System.Collections.Generic;
using System.Threading.Tasks;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Business.Abstract
{
    public interface ILogService
    {
        Task<List<Log>> GetLogsAsync(bool? durum);
        Task<Log> LogAsync(bool durum, string islemTipi, string aciklama, string userIp, int? userId);

    }
}
