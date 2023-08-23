using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Data
{
    public class ApplicationRoles : IdentityRole
    {
        public enum Roles
        {
            Admin,
        }
    }
}
