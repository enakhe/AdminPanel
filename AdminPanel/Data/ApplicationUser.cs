#nullable disable

using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }
}
