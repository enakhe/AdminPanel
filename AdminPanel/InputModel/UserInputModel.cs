#nullable disable

using System.ComponentModel.DataAnnotations;

namespace AdminPanel.InputModel
{
    public class UserInputModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Date created")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
