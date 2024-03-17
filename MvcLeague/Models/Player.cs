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

        public override bool Equals(object? obj)
        {
            return obj is Player player &&
                   id == player.id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id);
        }

        public override string? ToString()
        {
            return playerName;
        }
    }
}
