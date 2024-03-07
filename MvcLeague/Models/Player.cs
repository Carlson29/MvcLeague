using System.ComponentModel.DataAnnotations;

namespace MvcLeague.Models
{
    public class Player
    {
        [Key]
        public int id { get; set; }
        public int teamId { get; set; }
        public string playerName { get; set; }
        public string nationality { get; set; }
        [DataType(DataType.Date)]
        public DateTime dateOfBirth { get; set; }
    }
}
