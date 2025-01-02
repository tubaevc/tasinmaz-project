using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace TasinmazProject.Entities.Concrete
{
    public class Tasinmaz
    {
        

        [Key]
        public int Id { get; set; }
        public string Ada { get; set; }

        public string Parsel { get; set; }

        public string Nitelik { get; set; }

        public string Adres {  get; set; }
 
        public string Koordinat { get; set; }
        // mahalleId
        public int MahalleId{ get; set; }
        [ForeignKey("MahalleId")]

        public Mahalle Mahalle { get; set; }
    }
}
