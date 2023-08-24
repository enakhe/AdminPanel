using AdminPanel.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminPanel.ApiController
{
    [Route("[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        public readonly ApplicationDbContext _db;
        public readonly UserManager<ApplicationUser> _userManager;

        public UserApiController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // POST api/<UserApiController>
        [HttpPost]
        [Route("username")]
        public async Task<JsonResult> Post(string username)
        {
            if(username != null)
            {
                if (username.Length < 6)
                {
                    return new JsonResult("Username is to short");
                }
                var user = await _db.CreatedUsers.FirstOrDefaultAsync(user => user.Username == username);
                if (user != null)
                {
                    return new JsonResult("Error, a user with that username already exits");
                }
                return new JsonResult("Username is valid");
            }
            return new JsonResult("");
        }
    }
}
