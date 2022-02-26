using System.ComponentModel.DataAnnotations;

namespace ECommerceSite.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        //null! = null forgiving operator - for removing warnings
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        //remember ? nullable makes phone and username OPTIONAL
        public string? Phone { get; set; }

        public string? Username { get; set; }
    }

    //used to only pull out needed data from member to register with, in that view
    //this is done for security purproses, so other unused properties cannot be tamprered with
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

    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
