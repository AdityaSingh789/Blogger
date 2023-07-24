using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Blogger.Models.ViewModel
{
    public class SignUpUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter your username!")]
        [Remote(action: "UserNameExist", controller: "Account")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter your Email!")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter Valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your Mobile Number!")]
        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Mobile number is not valid.")]
        public long? Mobile { get; set; }

        [Required(ErrorMessage = "Enter your Password!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter your confirm password!")]
        [Compare("Password", ErrorMessage = ("password doesn't matches!"))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Gender Field can not be blank.")]
        [Display(Name = "Enter your Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "City Field can not be blank.")]
        [Display(Name = "Enter your City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country Field can not be blank.")]
        [Display(Name = "Enter your Country")]
        public string Country { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}

