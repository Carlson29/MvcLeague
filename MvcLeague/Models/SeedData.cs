using Microsoft.EntityFrameworkCore;
using MvcLeague.Data;

namespace MvcLeague.Models
{
    public class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcLeagueContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcLeagueContext>>()))
            {
                // Look for any players or teams.
                if (context.Player.Any() && context.Team.Any())
                 {
                     return;   // DB has been seeded
                 }

                context.Team.AddRange(
new Team
{

league = "Laliga",
throphies = 50,
marketValue = 8.99M,
teamName = "Barcelona",

},

new Team
{

    league = "Premier League",
    throphies = 25,
    marketValue = 50.99M,
    teamName = "Manchester United",

}


);
                context.Player.AddRange(
                    new Player
                    {

                        teamId =1 ,
                        playerName="Ibra",
                        nationality="Swedish",
                        dateOfBirth= DateTime.Parse("1984-3-13"),

                    },
                     new Player
                     {

                         teamId = 2,
                         playerName = "Pogba",
                         nationality = "French",
                         dateOfBirth = DateTime.Parse("1960-3-08"),

                     }

                );

    
            
                context.SaveChanges();
            }
        }
    }
}
