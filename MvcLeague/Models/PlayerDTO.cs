using System.ComponentModel.DataAnnotations;

namespace MvcLeague.Models
{
    public class PlayerDTO
    {
        [Key]
        public int id { get; set; }
        public string playerName { get; set; } 
        public DateTime dateOfBirth { get; set; }
        public string nationality { get; set; }
    }
}
