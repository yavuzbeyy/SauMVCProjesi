using System.ComponentModel.DataAnnotations;

namespace HayvanBarinagi.Models
{
    public class Kullanici
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
