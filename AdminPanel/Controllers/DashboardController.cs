using AdminPanel.Data;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        public readonly ApplicationDbContext _db;

        public DashboardController(ILogger<DashboardController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var numberOfUsers = _db.CreatedUsers.Count();
            ViewData["NumberOfUsers"] = numberOfUsers;
            return View();
        }
    }
}
