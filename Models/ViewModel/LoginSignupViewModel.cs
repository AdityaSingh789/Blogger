using System.ComponentModel.DataAnnotations;

namespace Blogger.Models.ViewModel
{
    public class LoginSignupViewModel
    { 

        public string Username { get; set; }
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool IsRemember { get; set; }
    }
}
