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
    public class TeamsController : Controller
    {
        private readonly MvcLeagueContext _context;

        public TeamsController(MvcLeagueContext context)
        {
            _context = context;
        }

        // GET: Teams
       /* public async Task<IActionResult> Index()
        {
              return _context.Team != null ? 
                          View(await _context.Team.ToListAsync()) :
                          Problem("Entity set 'MvcLeagueContext.Team'  is null.");
        }*/
         public async Task<IActionResult> Index(string searchString)
          {
            if (UsersController.loggedInUser != null)
            {
                if (_context.Team == null)
              {
                  return Problem("Entity set ''MvcLeagueContext.Team'  is null.");
              }

              var movies = from m in _context.Team
                           select m;

              if (!String.IsNullOrEmpty(searchString))
              {
                  movies = movies.Where(s => s.teamName!.Contains(searchString));
              }

              return View(await movies.ToListAsync());
            }
           
            return RedirectToAction("Login", "Users");
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (UsersController.loggedInUser != null)
            {
                if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.teamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }
            return RedirectToAction("Login", "Users");
    }

        // GET: Teams/Create
        public IActionResult Create()
        {
            if (UsersController.loggedInUser != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Users");
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("teamId,league,throphies,marketValue,teamName")] Team team)
        {
            if (UsersController.loggedInUser != null)
            {
                if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }
            return RedirectToAction("Login", "Users");
    }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (UsersController.loggedInUser != null)
            {
                if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
            }
            return RedirectToAction("Login", "Users");
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("teamId,league,throphies,marketValue,teamName")] Team team)
        {
            if (UsersController.loggedInUser != null)
            {
                if (id != team.teamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.teamId))
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
            return View(team);
        }
            return RedirectToAction("Login", "Users");
    }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (UsersController.loggedInUser != null)
            {
                if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.teamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }
            return RedirectToAction("Login", "Users");
    }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (UsersController.loggedInUser != null)
            {
                if (_context.Team == null)
            {
                return Problem("Entity set 'MvcLeagueContext.Team'  is null.");
            }
            var team = await _context.Team.FindAsync(id);
            if (team != null)
            {
                _context.Team.Remove(team);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction("Login", "Users");
    }

        private bool TeamExists(int id)
        {
            if (UsersController.loggedInUser != null)
            {
                return (_context.Team?.Any(e => e.teamId == id)).GetValueOrDefault();
            }
            return false;
        }
    }
}
