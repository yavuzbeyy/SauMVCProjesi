using System.ComponentModel.DataAnnotations;

namespace HayvanBarinagi.Models
{
    public class HayvanVerme
    {
        [Key]
        public int id { get; set; }
        public string hayvanAd { get; set; }
        public string hayvanTur { get; set; }
        public int yas { get; set; }
        public string cinsiyet { get; set; }
        public string saglikDurumu { get; set; }
        public string aciklama { get; set; }
    }
}
