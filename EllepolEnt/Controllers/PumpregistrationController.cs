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
    public class PumpregistrationController : Controller
    {
        private readonly EllepolEntContext _context;

        public PumpregistrationController(EllepolEntContext context)
        {
            _context = context;
        }

        // GET: Pumpregistration
        public async Task<IActionResult> Index()
        {
              return View(await _context.Pumpregistration.ToListAsync());
        }

        // GET: Pumpregistration/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Pumpregistration == null)
            {
                return NotFound();
            }

            var pumpregistration = await _context.Pumpregistration
                .FirstOrDefaultAsync(m => m.PumpID == id);
            if (pumpregistration == null)
            {
                return NotFound();
            }

            return View(pumpregistration);
        }

        // GET: Pumpregistration/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pumpregistration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PumpID,pumpname")] Pumpregistration pumpregistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pumpregistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pumpregistration);
        }

        // GET: Pumpregistration/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Pumpregistration == null)
            {
                return NotFound();
            }

            var pumpregistration = await _context.Pumpregistration.FindAsync(id);
            if (pumpregistration == null)
            {
                return NotFound();
            }
            return View(pumpregistration);
        }

        // POST: Pumpregistration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PumpID,pumpname")] Pumpregistration pumpregistration)
        {
            if (id != pumpregistration.PumpID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pumpregistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PumpregistrationExists(pumpregistration.PumpID))
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
            return View(pumpregistration);
        }

        // GET: Pumpregistration/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Pumpregistration == null)
            {
                return NotFound();
            }

            var pumpregistration = await _context.Pumpregistration
                .FirstOrDefaultAsync(m => m.PumpID == id);
            if (pumpregistration == null)
            {
                return NotFound();
            }

            return View(pumpregistration);
        }

        // POST: Pumpregistration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Pumpregistration == null)
            {
                return Problem("Entity set 'EllepolEntContext.Pumpregistration'  is null.");
            }
            var pumpregistration = await _context.Pumpregistration.FindAsync(id);
            if (pumpregistration != null)
            {
                _context.Pumpregistration.Remove(pumpregistration);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PumpregistrationExists(string id)
        {
          return _context.Pumpregistration.Any(e => e.PumpID == id);
        }
    }
}
