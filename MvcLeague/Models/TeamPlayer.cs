using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcLeague.Models
{
    public class TeamPlayer
    {
        public List<Player> players { get; set; }
        // public SelectList?  teams { get; set; }
        public List<Team> teams { get; set; }
        public int? teamPlayer { get; set; }
        public string? SearchString { get; set; }

    }
}
