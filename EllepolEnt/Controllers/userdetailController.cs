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
    public class userdetailController : Controller
    {
        private readonly EllepolEntContext _context;

        public userdetailController(EllepolEntContext context)
        {
            _context = context;
        }

        // GET: userdetail
        public async Task<IActionResult> Index()
        {
              return View(await _context.userdetail.ToListAsync());
        }

        // GET: userdetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.userdetail == null)
            {
                return NotFound();
            }

            var userdetail = await _context.userdetail
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userdetail == null)
            {
                return NotFound();
            }

            return View(userdetail);
        }

        // GET: userdetail/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: userdetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,NIC,RoleID")] userdetail userdetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userdetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userdetail);
        }

        // GET: userdetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.userdetail == null)
            {
                return NotFound();
            }

            var userdetail = await _context.userdetail.FindAsync(id);
            if (userdetail == null)
            {
                return NotFound();
            }
            return View(userdetail);
        }

        // POST: userdetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Name,NIC,RoleID")] userdetail userdetail)
        {
            if (id != userdetail.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userdetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!userdetailExists(userdetail.UserId))
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
            return View(userdetail);
        }

        // GET: userdetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.userdetail == null)
            {
                return NotFound();
            }

            var userdetail = await _context.userdetail
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userdetail == null)
            {
                return NotFound();
            }

            return View(userdetail);
        }

        // POST: userdetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.userdetail == null)
            {
                return Problem("Entity set 'EllepolEntContext.userdetail'  is null.");
            }
            var userdetail = await _context.userdetail.FindAsync(id);
            if (userdetail != null)
            {
                _context.userdetail.Remove(userdetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool userdetailExists(int id)
        {
          return _context.userdetail.Any(e => e.UserId == id);
        }
    }
}
