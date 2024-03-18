using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        

        public async Task<IActionResult> Index(int? teamPlayer, string searchString)
        {
            if (UsersController.loggedInUser!=null)
            {
                if (_context.Player == null)
                {
                    return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
                }

                // Use LINQ to get list of genres.
                IQueryable<Team> teamQuery = from m in _context.Team
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
               

                List <PlayerDTO> results = new List<PlayerDTO>();
                foreach (var p in players)
                {
                    PlayerDTO playerDto = new PlayerDTO()
                    {
                        id = p.id,
                        nationality=p.nationality,
                        playerName = p.playerName,
                        dateOfBirth = p.dateOfBirth,
                    };
                    results.Add(playerDto);
                }

                var teamplayer = new TeamPlayer
                {
                    teamPlayer = teamPlayer,
                    teams = await teamQuery.Distinct().ToListAsync(),
                    players = results
            };

                return View(teamplayer);
            }
            return RedirectToAction("Login", "Users");
        }


        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (UsersController.loggedInUser != null)
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
                PlayerDTO playerDto = new PlayerDTO()
                {
                    id = player.id,
                    nationality = player.nationality,
                    playerName = player.playerName,
                    dateOfBirth = player.dateOfBirth,
                };

                return View(playerDto);
            }
            return RedirectToAction("Login", "Users");
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            if (UsersController.loggedInUser != null)
            {
                return View();
        }
            return RedirectToAction("Login", "Users");
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,teamId,playerName,nationality,dateOfBirth")] Player player)
        {
            if (UsersController.loggedInUser != null)
            {
                if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
            }
            return RedirectToAction("Login", "Users");
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (UsersController.loggedInUser != null)
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
            return RedirectToAction("Login", "Users");
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,teamId,playerName,nationality,dateOfBirth")] Player player)
        {
            if (UsersController.loggedInUser != null)
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
            return RedirectToAction("Login", "Users");
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (UsersController.loggedInUser != null)
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
            return RedirectToAction("Login", "Users");
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (UsersController.loggedInUser != null)
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
            return RedirectToAction("Login", "Users");
        }

        private bool PlayerExists(int id)
        {
            if (UsersController.loggedInUser != null)
            {
                return (_context.Player?.Any(e => e.id == id)).GetValueOrDefault();
        }
            return false;
    }
    }
}
