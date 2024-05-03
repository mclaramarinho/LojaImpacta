using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojaImpacta.Data;
using LojaImpacta.Models;
using Microsoft.AspNetCore.Identity;

namespace LojaImpacta.Controllers
{
    public class UsersController : Controller
    {
        private readonly LojaImpactaContext _context;

        public UsersController(LojaImpactaContext context)
        {
            _context = context;
        }

        public async Task<SelectList> GetOptions()
        {
            var levels = await _context.AccessLevel.ToListAsync();
            SelectList options = new SelectList(_context.AccessLevel, nameof(AccessLevel.AccessLevelID), nameof(AccessLevel.LevelName));

            return options;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            var levels = await _context.AccessLevel.ToListAsync();
            SelectList items = new(levels, "AccessLevelID", "LevelName");

            ViewData["AccessLevels"] = items;

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string godPswd, [Bind("UserID,Name,CPF,Email,Password,ConfirmPswd,AccountActive,AccessLevel")] User user)
        {
            if (godPswd != "1Av1viX")
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction();
            }
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string CPF, string Password)
        {
            if (ModelState.IsValid)
            {
                if (_context.User.Any(e => e.CPF == CPF))
                {
                    var u = await _context.User.FirstOrDefaultAsync(m => m.CPF == CPF);

                    if (u.Password == Password)
                    {
                        return RedirectToAction(actionName: "Details", routeValues: new RouteValueDictionary(
                             new {Id = u.UserID}
                            ));
                    }
                    else
                    {
                        return NotFound(ModelState);
                    }
                }
                else
                {
                    return NotFound(ModelState);
                }
            
            }
            return View();
        }



        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
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
        public async Task<IActionResult> Edit(int id, [Bind("UserID,Name,CPF,Email,Password,ConfirmPswd,AccountActive")] User user)
        {

            if (id != user.UserID)
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
                    if (!UserExists(user.UserID))
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
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserID == id);
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
            return _context.User.Any(e => e.UserID == id);
        }
    }
}
