using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasinmazProject.Entities.Concrete
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public bool durum { get; set; }
        public string islemTipi { get; set; }
        public string aciklama { get; set; }
        public DateTime zaman { get; set; }
        public string userIp { get; set; }
        [ForeignKey("User")]
        public int? userId { get; set; }
        public User User { get; set; }

    }
}
