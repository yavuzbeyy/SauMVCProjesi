using System.ComponentModel.DataAnnotations;

namespace HayvanBarinagi.Models
{
    public class Hayvan
    {
        [Key]
        public int Id { get; set; }
        public string Adi { get; set; }
        public int Yas { get; set; }
        public string Cins { get; set; }


    }
}
