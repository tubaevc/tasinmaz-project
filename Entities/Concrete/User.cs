using System.ComponentModel.DataAnnotations;

namespace TasinmazProject.Entities.Concrete
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string userEmail { get; set; }

        public byte[] PasswordHash { get; set; }
    
        public byte[] PasswordSalt { get; set; }
    
        public string role { get; set; }
    }
}
