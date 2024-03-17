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
    public class UsersController : Controller
    {
        public static User loggedInUser=null;
        private readonly MvcLeagueContext _context;

        public UsersController(MvcLeagueContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.User != null ? 
                          View(await _context.User.ToListAsync()) :
                          Problem("Entity set 'MvcLeagueContext.User'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.userId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        public async Task<IActionResult> doLogin(string userName, string password)
        {
        

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.userName == userName &&  m.password == password);
            if (user == null)
            {
                return RedirectToAction(nameof(Login));
            }
            loggedInUser = user;
            return RedirectToAction("Index", "Players");
        }
        public IActionResult Login()
        {
            ViewData["Login"] = "false";
            return View();
        }
        public IActionResult Logout()
        {
            ViewData["Login"] = "false";
            loggedInUser = null;
            return RedirectToAction(nameof(Login));
        }
    

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("userId,userName,password,dateOfBirth")] User user)
        {
            if (ModelState.IsValid)
            {
                var VerifyUser = await _context.User
               .FirstOrDefaultAsync(m => m.userName == user.userName);
                if (VerifyUser== null) {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    loggedInUser = user;
                    return RedirectToAction("Index", "Players");
                }
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("userId,userName,password,dateOfBirth")] User user)
        {
            if (id != user.userId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.userId))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.userId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'MvcLeagueContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.User?.Any(e => e.userId == id)).GetValueOrDefault();
        }
    }
}
