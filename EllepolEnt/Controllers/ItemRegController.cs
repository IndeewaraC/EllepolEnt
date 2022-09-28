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
    public class ItemRegController : Controller
    {
        private readonly EllepolEntContext _context;

        public ItemRegController(EllepolEntContext context)
        {
            _context = context;
        }

        // GET: ItemReg
        public async Task<IActionResult> Index()
        {
              return View(await _context.ItemReg.ToListAsync());
        }

        // GET: ItemReg/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ItemReg == null)
            {
                return NotFound();
            }

            var itemReg = await _context.ItemReg
                .FirstOrDefaultAsync(m => m.itemid == id);
            if (itemReg == null)
            {
                return NotFound();
            }

            return View(itemReg);
        }

        // GET: ItemReg/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemReg/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("itemid,itemname,Saleprice")] ItemReg itemReg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemReg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemReg);
        }

        // GET: ItemReg/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ItemReg == null)
            {
                return NotFound();
            }

            var itemReg = await _context.ItemReg.FindAsync(id);
            if (itemReg == null)
            {
                return NotFound();
            }
            return View(itemReg);
        }

        // POST: ItemReg/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("itemid,itemname,Saleprice")] ItemReg itemReg)
        {
            if (id != itemReg.itemid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemReg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemRegExists(itemReg.itemid))
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
            return View(itemReg);
        }

        // GET: ItemReg/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ItemReg == null)
            {
                return NotFound();
            }

            var itemReg = await _context.ItemReg
                .FirstOrDefaultAsync(m => m.itemid == id);
            if (itemReg == null)
            {
                return NotFound();
            }

            return View(itemReg);
        }

        // POST: ItemReg/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ItemReg == null)
            {
                return Problem("Entity set 'EllepolEntContext.ItemReg'  is null.");
            }
            var itemReg = await _context.ItemReg.FindAsync(id);
            if (itemReg != null)
            {
                _context.ItemReg.Remove(itemReg);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemRegExists(string id)
        {
          return _context.ItemReg.Any(e => e.itemid == id);
        }
    }
}
