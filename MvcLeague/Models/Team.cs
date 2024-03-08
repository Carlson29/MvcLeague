using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcLeague.Models
{
    public class Team
    {
        [Key]
        public int teamId { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string teamName { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string league { get; set; }
        public int? throphies { get; set; }
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal marketValue { get; set; }

    }
}
