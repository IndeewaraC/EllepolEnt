using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EllepolEnt.Data;
using EllepolEnt.Models;

namespace EllepolEnt.Controllers
{
    public class LoginController : Controller
    {
        private readonly EllepolEntContext _context;

        public LoginController(EllepolEntContext context)
        {
            _context = context;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
              return View(await _context.Logins.ToListAsync());
        }

        // GET: Login/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Logins == null)
            {
                return NotFound();
            }

            var login = await _context.Logins
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,UserName,Password")] dbLogin login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(login);
        }

        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Logins == null)
            {
                return NotFound();
            }

            var login = await _context.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            return View(login);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserID,UserName,Password")] dbLogin login)
        {
            if (id != login.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.UserID))
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
            return View(login);
        }

        // GET: Login/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Logins == null)
            {
                return NotFound();
            }

            var login = await _context.Logins
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Logins == null)
            {
                return Problem("Entity set 'EllepolEntContext.Logins'  is null.");
            }
            var login = await _context.Logins.FindAsync(id);
            if (login != null)
            {
                _context.Logins.Remove(login);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginExists(string id)
        {
          return _context.Logins.Any(e => e.UserID == id);
        }
    }
}
