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
     

        // GET: Users/Details/5
       
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

       


        
    }
}
