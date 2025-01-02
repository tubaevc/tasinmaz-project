using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TasinmazProject.Entities.Concrete
{
    public class Il
    {
        [Key]
        public int Id { get; set; }
        public string IlAdi { get; set;}

     //   public ICollection<Ilce> Ilceler { get; set; }
    }
}
