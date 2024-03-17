using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcLeague.Models
{
    public class TeamDTO
    {
        [Key]
        public int teamId { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string teamName { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string league { get; set; }
        public int? throphies { get; set; }
        public override bool Equals(object? obj)
        {
            return obj is Team team &&
                   teamId == team.teamId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(teamId);
        }

        public override string? ToString()
        {
            return teamName;
        }
    }
}
