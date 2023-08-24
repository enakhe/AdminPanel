#nullable disable

using System.ComponentModel.DataAnnotations;

namespace AdminPanel.InputModel
{
    public class UserViewModel
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
