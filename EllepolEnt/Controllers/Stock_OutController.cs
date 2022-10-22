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
    public class Stock_OutController : Controller
    {
        private readonly EllepolEntContext _context;

        public Stock_OutController(EllepolEntContext context)
        {
            _context = context;
        }

        // GET: Stock_Out
        public async Task<IActionResult> Index()
        {
              return View(await _context.Stock_Out.ToListAsync());
        }

        // GET: Stock_Out/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stock_Out == null)
            {
                return NotFound();
            }

            var stock_Out = await _context.Stock_Out
                .FirstOrDefaultAsync(m => m.Stock_ID == id);
            if (stock_Out == null)
            {
                return NotFound();
            }

            return View(stock_Out);
        }

        // GET: Stock_Out/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stock_Out/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Stock_ID,stockoutDate,ItemId,Stockout,StockoutPrice")] Stock_Out stock_Out)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stock_Out);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stock_Out);
        }

        // GET: Stock_Out/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stock_Out == null)
            {
                return NotFound();
            }

            var stock_Out = await _context.Stock_Out.FindAsync(id);
            if (stock_Out == null)
            {
                return NotFound();
            }
            return View(stock_Out);
        }

        // POST: Stock_Out/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Stock_ID,stockoutDate,ItemId,Stockout,StockoutPrice")] Stock_Out stock_Out)
        {
            if (id != stock_Out.Stock_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stock_Out);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Stock_OutExists(stock_Out.Stock_ID))
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
            return View(stock_Out);
        }

        // GET: Stock_Out/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stock_Out == null)
            {
                return NotFound();
            }

            var stock_Out = await _context.Stock_Out
                .FirstOrDefaultAsync(m => m.Stock_ID == id);
            if (stock_Out == null)
            {
                return NotFound();
            }

            return View(stock_Out);
        }

        // POST: Stock_Out/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stock_Out == null)
            {
                return Problem("Entity set 'EllepolEntContext.Stock_Out'  is null.");
            }
            var stock_Out = await _context.Stock_Out.FindAsync(id);
            if (stock_Out != null)
            {
                _context.Stock_Out.Remove(stock_Out);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Stock_OutExists(int id)
        {
          return _context.Stock_Out.Any(e => e.Stock_ID == id);
        }
    }
}
