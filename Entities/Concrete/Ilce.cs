using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasinmazProject.Entities.Concrete
{
    public class Ilce
    {
        // ilId
        [Key]
        public int Id { get; set; }
        public string IlceAdi { get; set; }

        public int IlId { get; set; }
        [ForeignKey("IlId")]
        public Il Il { get; set; } // Il turunde 

      //  public ICollection<Mahalle> Mahalleler { get; set; }
    }
}
