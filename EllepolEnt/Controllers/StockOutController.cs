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
    public class StockOutController : Controller
    {
        private readonly EllepolEntContext _context;

        public StockOutController(EllepolEntContext context)
        {
            _context = context;
        }

        // GET: StockOut
        public async Task<IActionResult> Index()
        {
              return View(await _context.StockOut.ToListAsync());
        }

        // GET: StockOut/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.StockOut == null)
            {
                return NotFound();
            }

            var stockOut = await _context.StockOut
                .FirstOrDefaultAsync(m => m.Stock_ID == id);
            if (stockOut == null)
            {
                return NotFound();
            }

            return View(stockOut);
        }

        // GET: StockOut/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockOut/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Stock_ID,stockoutDate,ItemId,Stockout,StockoutPrice")] StockOut stockOut)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockOut);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockOut);
        }

        // GET: StockOut/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.StockOut == null)
            {
                return NotFound();
            }

            var stockOut = await _context.StockOut.FindAsync(id);
            if (stockOut == null)
            {
                return NotFound();
            }
            return View(stockOut);
        }

        // POST: StockOut/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Stock_ID,stockoutDate,ItemId,Stockout,StockoutPrice")] StockOut stockOut)
        {
            if (id != stockOut.Stock_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockOut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockOutExists(stockOut.Stock_ID))
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
            return View(stockOut);
        }

        // GET: StockOut/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.StockOut == null)
            {
                return NotFound();
            }

            var stockOut = await _context.StockOut
                .FirstOrDefaultAsync(m => m.Stock_ID == id);
            if (stockOut == null)
            {
                return NotFound();
            }

            return View(stockOut);
        }

        // POST: StockOut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.StockOut == null)
            {
                return Problem("Entity set 'EllepolEntContext.StockOut'  is null.");
            }
            var stockOut = await _context.StockOut.FindAsync(id);
            if (stockOut != null)
            {
                _context.StockOut.Remove(stockOut);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockOutExists(string id)
        {
          return _context.StockOut.Any(e => e.Stock_ID == id);
        }
    }
}
