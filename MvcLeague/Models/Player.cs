using System.ComponentModel.DataAnnotations;

namespace MvcLeague.Models
{
    public class Player
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int teamId { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string playerName { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string nationality { get; set; }
        [DataType(DataType.Date)]
        public DateTime dateOfBirth { get; set; }
    }
}
