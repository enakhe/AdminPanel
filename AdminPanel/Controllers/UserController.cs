#nullable disable

using Microsoft.AspNetCore.Mvc;
using AdminPanel.Data;
using AdminPanel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using AdminPanel.InputModel;

namespace AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        public readonly ApplicationDbContext _db;
        public readonly ILogger<UserController> _logger;
        public readonly UserManager<ApplicationUser> _userManager;

        public UserController(ApplicationDbContext db, ILogger<UserController> logger, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _logger = logger;
            _userManager = userManager;
        }

        public string ReturnUrl { get; set; }


        [HttpGet]
        public async Task<IActionResult> Index(string statusMessage, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ViewData["StatusMessage"] = statusMessage;
            var UserList = await _db.CreatedUsers.ToListAsync();
            return View(UserList);
        }

        [HttpGet]
        public  IActionResult Create(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel userInputModel, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                if (userInputModel.Username == null || userInputModel.FirstName == null || userInputModel.LastName == null || userInputModel.Password == null)
                {
                    ViewData["ErrorMessage"] = "Error, add required fields";
                    return View();
                }
                CreatedUser newUser = new()
                {
                    Username = userInputModel.Username,
                    FirstName = Regex.Replace(userInputModel.FirstName, "^[a-z]", c => c.Value.ToUpper()),
                    LastName  = Regex.Replace(userInputModel.LastName, "^[a-z]", c => c.Value.ToUpper()),
                    Password = userInputModel.Password
                };

                ValueTask<EntityEntry<CreatedUser>> result = _db.CreatedUsers.AddAsync(newUser);

                if (result.IsCompletedSuccessfully)
                {
                    await _db.SaveChangesAsync();
                    _logger.LogInformation("Successfully Added User");
                    ViewData["StatusMessage"] = "Successfully added user";
                    return RedirectToAction("Index", new
                    {
                        statusMessage = ViewData["StatusMessage"]
                    });
                }
            }
            return View();
        }
    }
}
