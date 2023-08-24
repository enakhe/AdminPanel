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
            var user = await _userManager.GetUserAsync(User);
            ViewData["StatusMessage"] = statusMessage;
            var UserList = await _db.CreatedUsers.Where(createdUser => createdUser.AdminId == user.Id).ToListAsync();
            return View(UserList);
        }

        [HttpGet]
        public IActionResult Create(string returnUrl = null)
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
                var user = await _userManager.GetUserAsync(User);

                if (userInputModel.Username == null || userInputModel.FirstName == null || userInputModel.LastName == null || userInputModel.Password == null)
                {
                    ViewData["ErrorMessage"] = "Error, add required fields";
                    return View();
                }

                var existedUser = CheckUsername(userInputModel.Username);
                if (existedUser)
                {
                    ViewData["ErrorMessage"] = "Error, a user with that username already exits or username too short";
                    return View();
                }

                var passwordValidation = PasswordValidator(userInputModel.Password);
                if (passwordValidation == false)
                {
                    ViewData["ErrorMessage"] = "Error, invalid password";
                    return View();
                }

                CreatedUser newUser = new()
                {
                    Username = userInputModel.Username,
                    FirstName = Regex.Replace(userInputModel.FirstName, "^[a-z]", c => c.Value.ToUpper()),
                    LastName = Regex.Replace(userInputModel.LastName, "^[a-z]", c => c.Value.ToUpper()),
                    Password = userInputModel.Password,

                    AdminId = user.Id
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

        [HttpGet]
        public async Task<IActionResult> Edit(string id, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var thisCreatedUser = await _db.CreatedUsers.FirstOrDefaultAsync(createdUser => createdUser.Id == id);
                return View(thisCreatedUser);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel userInputModel, string id, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (userInputModel.Username == null || userInputModel.FirstName == null || userInputModel.LastName == null || userInputModel.Password == null)
                {
                    ViewData["ErrorMessage"] = "Error, add required fields";
                    return View();
                }

                if (userInputModel.Username.Length < 6)
                {
                    ViewData["ErrorMessage"] = "Error, username too short";
                    return View();
                }

                var passwordValidation = PasswordValidator(userInputModel.Password);
                if (passwordValidation == false)
                {
                    ViewData["ErrorMessage"] = "Error, invalid password";
                    return View();
                }

                var thisCreatedUser = await _db.CreatedUsers.FirstOrDefaultAsync(createdUser => createdUser.Id == id);

                thisCreatedUser.Username = userInputModel.Username;
                thisCreatedUser.LastName = Regex.Replace(userInputModel.LastName, "^[a-z]", c => c.Value.ToUpper());
                thisCreatedUser.FirstName = Regex.Replace(userInputModel.FirstName, "^[a-z]", c => c.Value.ToUpper());
                thisCreatedUser.Password = userInputModel.Password;

                _db.Entry(thisCreatedUser).State = EntityState.Modified;

                await _db.SaveChangesAsync();
                ViewData["StatusMessage"] = "Successfully updated user";
                return RedirectToAction("Index", new
                {
                    statusMessage = ViewData["StatusMessage"]
                });
            }
            return View();
        }

        private bool CheckUsername(string username)
        {
            if (username.Length < 6)
            {
                return true;
            }
            var user = _db.CreatedUsers.FirstOrDefault(user => user.Username == username);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        private bool PasswordValidator(string password)
        {
            var regexItem = new Regex("^[`!@#$%&*]*$");
            var regexItemNum = new Regex(@"^\d+$");

            bool isValid = false;

            if (password.Length >= 6)
            {
                isValid = true;
            } else
            {
                isValid = false;
            }

            foreach (char c in password)
            {
                if (regexItem.IsMatch(c.ToString()))
                {
                    isValid =  true;
                }
                else
                {
                    isValid = false;
                }
            }

            foreach (char c in password)
            {
                if (Regex.IsMatch(c.ToString(), @"^\d+$"))
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}
