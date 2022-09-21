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
    public class tbl_LoginController : Controller
    {
        private readonly EllepolEntContext _context;

        public tbl_LoginController(EllepolEntContext context)
        {
            _context = context;
        }

        // GET: tbl_Login
        public async Task<IActionResult> Index()
        {
              return View(await _context.tbl_Login.ToListAsync());
        }

        // GET: tbl_Login/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.tbl_Login == null)
            {
                return NotFound();
            }

            var tbl_Login = await _context.tbl_Login
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (tbl_Login == null)
            {
                return NotFound();
            }

            return View(tbl_Login);
        }

        // GET: tbl_Login/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: tbl_Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,UserName,Password")] tbl_Login tbl_Login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_Login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_Login);
        }

        // GET: tbl_Login/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.tbl_Login == null)
            {
                return NotFound();
            }

            var tbl_Login = await _context.tbl_Login.FindAsync(id);
            if (tbl_Login == null)
            {
                return NotFound();
            }
            return View(tbl_Login);
        }

        // POST: tbl_Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserID,UserName,Password")] tbl_Login tbl_Login)
        {
            if (id != tbl_Login.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_Login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_LoginExists(tbl_Login.UserID))
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
            return View(tbl_Login);
        }

        // GET: tbl_Login/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.tbl_Login == null)
            {
                return NotFound();
            }

            var tbl_Login = await _context.tbl_Login
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (tbl_Login == null)
            {
                return NotFound();
            }

            return View(tbl_Login);
        }

        // POST: tbl_Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.tbl_Login == null)
            {
                return Problem("Entity set 'EllepolEntContext.Logincs'  is null.");
            }
            var tbl_Login = await _context.tbl_Login.FindAsync(id);
            if (tbl_Login != null)
            {
                _context.tbl_Login.Remove(tbl_Login);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_LoginExists(string id)
        {
          return _context.tbl_Login.Any(e => e.UserID == id);
        }
    }
}
