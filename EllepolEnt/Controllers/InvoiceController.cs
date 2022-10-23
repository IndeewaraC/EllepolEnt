using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EllepolEnt.Data;
using EllepolEnt.Models;
using System.Xml.Linq;

namespace EllepolEnt.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly EllepolEntContext _context;

        public InvoiceController(EllepolEntContext context)
        {
            _context = context;
        }

        // GET: Invoice
        public async Task<IActionResult> Index()
        {
              return View(await _context.Invoice.ToListAsync());
        }

        // GET: Invoice/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Invoice == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .FirstOrDefaultAsync(m => m.Invoice_Number == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoice/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Invoice_Number,Item_Id,Item_Name,Item_Price,qty")] Invoice invoice)
        {
            var inv = invoice.Invoice_Number.ToString();
            string? invoicenum = _context.Invoice.OrderByDescending(e => e.Invoice_Number).FirstOrDefault().ToString();
            var itmId = invoice.Item_Id.ToString();
            var qtyy = invoice.qty.ToString();
            var dty = DateTime.UtcNow.Date.ToString();
            var outprice = invoice.Item_Price.ToString();

            if (inv == invoicenum)
            {
                ViewBag.Message = string.Format("Invoice Number Already Exsists");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var stockrecord = _context.Stock.FirstOrDefault(e => e.Itemid == itmId);
                    if (stockrecord != null && stockrecord.Available_Stock >= int.Parse(qtyy.ToString()))
                    {
                        stockrecord.Available_Stock -= Convert.ToInt32(qtyy);
                        _context.Add(invoice);
                        var stockoutRecord = new Stock_Out();
                        stockoutRecord.stockoutDate = Convert.ToDateTime(dty).Date;
                        stockoutRecord.ItemId = itmId;
                        stockoutRecord.Stockout = float.Parse(qtyy.ToString());
                        stockoutRecord.StockoutPrice = float.Parse(outprice.ToString());
                        _context.Stock_Out.Add(stockoutRecord);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ViewBag.Message = string.Format("Invoice Number Already Exsists");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Invoice == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Invoice_Number,Item_Id,ItemName,ItemPrice,qty")] Invoice invoice)
        {
            if (id != invoice.Invoice_Number)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Invoice_Number))
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
            return View(invoice);
        }

        // GET: Invoice/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Invoice == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .FirstOrDefaultAsync(m => m.Invoice_Number == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Invoice == null)
            {
                return Problem("Entity set 'EllepolEntContext.Invoice'  is null.");
            }
            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoice.Remove(invoice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(string id)
        {
          return _context.Invoice.Any(e => e.Invoice_Number == id);
        }
    }
}
