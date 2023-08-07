using System.ComponentModel.DataAnnotations;

namespace HayvanBarinagi.Models
{
    public class Sahiplenme
    {
        [Key]
        public int id { get; set; }
        public string adSoyad { get; set; }
        public string ePosta { get; set; }
        public string telNo { get; set; }
        public string adres { get; set; }
        public int hayvanId { get; set; }
        public string gelir { get; set; }
        public string evTipi { get; set; }
        public bool deneyim { get; set; }
    }
}
