using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasinmazProject.Entities.Concrete
{
    public class Mahalle
    {

        [Key]
        public int Id { get; set; } 
        // ilceId

        public string MahalleAdi { get; set; }
        public int IlceId { get; set; }
        [ForeignKey("IlceId")]
        public Ilce Ilce { get; set; }


    }
}
