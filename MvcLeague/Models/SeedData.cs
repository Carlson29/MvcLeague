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
                // Look for any movies.
                if (context.Player.Any())
                {
                    return;   // DB has been seeded
                }
                context.Player.AddRange(
                    new Player
                    {

                        teamId = 1,
                        playerName="Ibra",
                        nationality="Swedish",
                        dateOfBirth= DateTime.Parse("1984-3-13"),

                    }

                );
                context.SaveChanges();
            }
        }
    }
}
