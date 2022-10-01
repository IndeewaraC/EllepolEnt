using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EllepolEnt.Data;
using EllepolEnt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace EllepolEnt.Controllers
{
    public class LoginController : Controller
    {

        private readonly EllepolEntContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public LoginController(EllepolEntContext context, UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        // GET: Login
        public async Task<IActionResult> Index()
        {
              return View(await _context.Login.ToListAsync());
        }

        // GET: Login/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var sex = HttpContext.Session.GetString("Uname");
            if (id == null || _context.Login == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            List<SelectListItem> useridlist = new();
            List<UserDetails> userid = _context.UserDetails.ToList();
            userid.ForEach (x => useridlist.Add(new() { Value = x.UserId.ToString(), Text = x.Name.ToString() })) ;
            ViewBag.listofuserids = useridlist;
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,UserName,Password")] Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(login);
        }

        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Login == null)
            {
                return NotFound();
            }

            var login = await _context.Login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            return View(login);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,UserName,Password")] Login login)
        {
            if (id != login.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.UserID))
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
            return View(login);
        }

        // GET: Login/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Login == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);

        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Login == null)
            {
                return Problem("Entity set 'EllepolEntContext.Login'  is null.");
            }
            var login = await _context.Login.FindAsync(id);
            if (login != null)
            {
                _context.Login.Remove(login);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult LoginUser()
        {
            return View();
        }
        public ActionResult AccessDinied()
        {
            return View();
        }
        public ActionResult LogoutUser()
        {
            HttpContext.Session.Remove("options");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginUser(string UserName, string password)
        {
            if (ModelState.IsValid)
            {
                var data = _context.Login.FirstOrDefault(s => s.UserName.Equals(UserName) && s.Password.Equals(password));

                if (data != null)
                {
                    var userRoleid = _context.UserDetails.FirstOrDefault(s => s.UserId.Equals(data.UserID)).RoleID;
                    var userRolename = _context.Role.FirstOrDefault(s => s.RoleId.Equals(userRoleid)).RoleName;
                    HttpContext.Session.SetString("Uname",UserName);
                    HttpContext.Session.SetString("Role", userRolename);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        private bool LoginExists(int id)
        {
          return _context.Login.Any(e => e.UserID == id);
        }

        //public static string GetMD5(string str)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] fromData = Encoding.UTF8.GetBytes(str);
        //    byte[] targetData = md5.ComputeHash(fromData);
        //    string byte2String = null;

        //    for (int i = 0; i < targetData.Length; i++)
        //    {
        //        byte2String += targetData[i].ToString("x2");

        //    }
        //    return byte2String;
        //}

    }
}
