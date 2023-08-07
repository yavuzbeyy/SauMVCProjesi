using System.ComponentModel.DataAnnotations;

namespace HayvanBarinagi.Models
{
    public class Iletisim
    {
        [Key]
        public int id { get; set; }
        public string adSoyad { get; set; }
        public string ePosta { get; set; }
        public string konu { get; set; }
        public string mesaj { get; set; }
    }
}
