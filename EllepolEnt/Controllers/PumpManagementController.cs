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
    public class PumpManagementController : Controller
    {
        private readonly EllepolEntContext _context;

        public PumpManagementController(EllepolEntContext context)
        {
            _context = context;
        }

        // GET: PumpManagement
        public async Task<IActionResult> Index()
        {
              return View(await _context.PumpManagement.ToListAsync());
        }

        // GET: PumpManagement/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PumpManagement == null)
            {
                return NotFound();
            }

            var pumpManagement = await _context.PumpManagement
                .FirstOrDefaultAsync(m => m.ItemID == id);
            if (pumpManagement == null)
            {
                return NotFound();
            }

            return View(pumpManagement);
        }

        // GET: PumpManagement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PumpManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemID,UserID,pumpID,GRNID,Date,cashIn,Ltrs")] PumpManagement pumpManagement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pumpManagement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pumpManagement);
        }

        // GET: PumpManagement/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PumpManagement == null)
            {
                return NotFound();
            }

            var pumpManagement = await _context.PumpManagement.FindAsync(id);
            if (pumpManagement == null)
            {
                return NotFound();
            }
            return View(pumpManagement);
        }

        // POST: PumpManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ItemID,UserID,pumpID,GRNID,Date,cashIn,Ltrs")] PumpManagement pumpManagement)
        {
            if (id != pumpManagement.ItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pumpManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PumpManagementExists(pumpManagement.ItemID))
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
            return View(pumpManagement);
        }

        // GET: PumpManagement/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PumpManagement == null)
            {
                return NotFound();
            }

            var pumpManagement = await _context.PumpManagement
                .FirstOrDefaultAsync(m => m.ItemID == id);
            if (pumpManagement == null)
            {
                return NotFound();
            }

            return View(pumpManagement);
        }

        // POST: PumpManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PumpManagement == null)
            {
                return Problem("Entity set 'EllepolEntContext.PumpManagement'  is null.");
            }
            var pumpManagement = await _context.PumpManagement.FindAsync(id);
            if (pumpManagement != null)
            {
                _context.PumpManagement.Remove(pumpManagement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PumpManagementExists(string id)
        {
          return _context.PumpManagement.Any(e => e.ItemID == id);
        }
    }
}
