using System.ComponentModel.DataAnnotations;

namespace ECommerceSite.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Username { get; set; }
    }

    //used to only pull out needed data from member to register with, in that view
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Compare(nameof(Email))]
        [Display(Name = "Confirm Email")] //displays as Confirm Email instead of property name
        public string ConfirmEmail { get; set; }

        [Required]
        [DataType(DataType.Password)] // writes bullet points when you type in password
        [StringLength(75, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof (Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
