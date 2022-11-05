using EllepolEnt.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EllepolEnt.Controllers
{
    public class Rptview : Controller
    {
        private readonly EllepolEntContext _context;

        public Rptview(EllepolEntContext context)
        {
            _context = context;
        }

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

        public IActionResult Report()
        {
            return View();
        }
    }
}
