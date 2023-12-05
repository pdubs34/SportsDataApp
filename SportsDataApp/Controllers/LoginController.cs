using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsDataApp.Data;
using SportsDataApp.Models;
using SportsDataApp.ViewModels;

namespace SportsDataApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly SportsDataAppContext _context;

        public LoginController(SportsDataAppContext context)
        {
            _context = context;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
            return _context.LoginUser != null ?
                        View(await _context.LoginUser.ToListAsync()) :
                        Problem("Entity set 'SportsDataAppContext.User'  is null.");
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,PhoneNumber,EmailVerificationToken,PasswordResetToken")] LoginUser user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(string username, string password)
        {
            // Use FirstOrDefaultAsync to get the first matching user or null if not found
            int userId = await _context.LoginUser
                .Where(us => us.Name == username && us.Password == password)
                .Select(userId => userId.Id)
                .FirstOrDefaultAsync();

            // Check if the user was found
            if (userId != null)
            {
                // User found, you can create a ViewModel or use the User directly
                string id = userId.ToString();
                HttpContext.Session.SetString("userId", id);
                LoginUser user = new LoginUser(username,password);
                SampleViewModel sampleViewModel = new SampleViewModel(user);
                // Pass the User or the ViewModel to the View
                return View(sampleViewModel);
            }

            // User not found, handle accordingly (e.g., show an error message)
            return View("LoginError"); // Assuming "LoginError" is the name of an error view
        }
        private bool UserExists(int id)
        {
            return (_context.LoginUser?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
