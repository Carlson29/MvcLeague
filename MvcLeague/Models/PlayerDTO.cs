using System.ComponentModel.DataAnnotations;

namespace MvcLeague.Models
{
    public class PlayerDTO
    {
        [Key]
        public int id { get; set; }
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
            return obj is PlayerDTO dTO &&
                   id == dTO.id;
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
