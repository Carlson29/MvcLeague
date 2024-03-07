using System.ComponentModel.DataAnnotations;

namespace MvcLeague.Models
{
    public class Team
    {
        [Key]
        public int id;
        public string league;
        public int throphies;
        public string marketValue;
    }
}
