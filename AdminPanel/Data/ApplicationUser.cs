#nullable disable

using AdminPanel.Models;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() 
        {
            this.CreatedUsers = new HashSet<CreatedUser>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<CreatedUser> CreatedUsers { get; set; }
    }
}
