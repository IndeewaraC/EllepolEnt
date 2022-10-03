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
    public class GRNController : Controller
    {
        private readonly EllepolEntContext _context;

        public GRNController(EllepolEntContext context)
        {
            _context = context;
        }

        // GET: GRN
        public async Task<IActionResult> Index()
        {
            var loginsession = HttpContext.Session.GetString("Uname");
            var rolesession = HttpContext.Session.GetString("Role");
            if (loginsession != null && loginsession != "")
            {
                if (rolesession != null && rolesession != "" && (rolesession == "Admin"))
                {
                    return View(await _context.GRN.ToListAsync());
                }
                else 
                {
                    return RedirectToAction("AccessDinied", "Home");
                }
            }
            else 
            {
                return RedirectToAction("LoginUser", "Login");
            }
            


        }

        // GET: GRN/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.GRN == null)
            {
                return NotFound();
            }

            var gRN = await _context.GRN
                .FirstOrDefaultAsync(m => m.GRN_ID == id);
            if (gRN == null)
            {
                return NotFound();
            }

            return View(gRN);
        }

        // GET: GRN/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GRN/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GRN_ID,GRN_Date,Item_ID,In_Price,Stock_In_Amount")] GRN gRN)
        {
            var GRN_ID = gRN.GRN_ID;
            DateTime GRN_Date = gRN.GRN_Date;
            var itemid = gRN.Item_ID;
            float In_Price = gRN.In_Price;
            int Stock_In_Amount = gRN.Stock_In_Amount;

            if (!String.IsNullOrEmpty(itemid))
            {

                if (ModelState.IsValid)
                {
                    _context.GRN.Add(gRN);
                    await _context.SaveChangesAsync();

                    var stockrecord = _context.Stock.FirstOrDefault(e=> e.Itemid== itemid);
                    if (stockrecord != null)
                    {
                        stockrecord.Available_Stock += Stock_In_Amount;
                    }
                    else
                    {
                        var stockrecord2 = new Stock();
                        stockrecord2.Itemid = itemid;
                        stockrecord2.Available_Stock = Stock_In_Amount;

                        _context.Stock.Add(stockrecord2);
                        
                    }
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(gRN);
        }

        // GET: GRN/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.GRN == null)
            {
                return NotFound();
            }

            var gRN = await _context.GRN.FindAsync(id);
            if (gRN == null)
            {
                return NotFound();
            }
            return View(gRN);
        }

        // POST: GRN/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("GRN_ID,GRN_Date,Item_ID,In_Price,Stock_In_Amount")] GRN gRN)
        {
            if (id != gRN.GRN_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gRN);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GRNExists(gRN.GRN_ID))
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
            return View(gRN);
        }

        // GET: GRN/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.GRN == null)
            {
                return NotFound();
            }

            var gRN = await _context.GRN
                .FirstOrDefaultAsync(m => m.GRN_ID == id);
            if (gRN == null)
            {
                return NotFound();
            }

            return View(gRN);
        }

        // POST: GRN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.GRN == null)
            {
                return Problem("Entity set 'EllepolEntContext.GRN'  is null.");
            }
            var gRN = await _context.GRN.FindAsync(id);
            if (gRN != null)
            {
                _context.GRN.Remove(gRN);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GRNExists(string id)
        {
          return _context.GRN.Any(e => e.GRN_ID == id);
        }
    }
}
