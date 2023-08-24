#nullable disable

using AdminPanel.Data;

namespace AdminPanel.Models
{
    public class CreatedUser
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; }
        public string AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public virtual ApplicationUser Admin { get; set; }
    }
}
