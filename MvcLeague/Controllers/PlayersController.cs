using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcLeague.Data;
using MvcLeague.Models;

namespace MvcLeague.Controllers
{
    public class PlayersController : Controller
    {
        private readonly MvcLeagueContext _context;

        public PlayersController(MvcLeagueContext context)
        {
            _context = context;
        }

        // GET: Players
        /*  public async Task<IActionResult> Index()
          {
              return _context.Player != null ?
                          View(await _context.Player.ToListAsync()) :
                          Problem("Entity set 'MvcLeagueContext.Player'  is null.");
          }*/


       /* public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Player == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            var movies = from m in _context.Player
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.playerName!.Contains(searchString));
            }

            return View(await movies.ToListAsync());
        }*/

        public async Task<IActionResult> Index(int? teamPlayer, string searchString)
        {
            if (_context.Player == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<Team> genreQuery = from m in _context.Team
                                            select m;
            var players = from m in _context.Player
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                players = players.Where(s => s.playerName!.Contains(searchString));
            }

            if (teamPlayer > 0)
            {
                players = players.Where(x => x.teamId == teamPlayer);
            }

            var teamplayer = new TeamPlayer
            {
                teamPlayer = teamPlayer,
                teams = await genreQuery.Distinct().ToListAsync(),
                players = await players.ToListAsync()
            };

            return View(teamplayer);
        }


        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .FirstOrDefaultAsync(m => m.id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,teamId,playerName,nationality,dateOfBirth")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }

            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,teamId,playerName,nationality,dateOfBirth")] Player player)
        {
            if (id != player.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .FirstOrDefaultAsync(m => m.id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Player == null)
            {
                return Problem("Entity set 'MvcLeagueContext.Player'  is null.");
            }
            var player = await _context.Player.FindAsync(id);
            if (player != null)
            {
                _context.Player.Remove(player);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return (_context.Player?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
