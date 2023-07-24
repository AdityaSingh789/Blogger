using Blogger.Data;
using Blogger.Models.Accounts;
using Blogger.Models.ViewModel;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IDNTCaptchaValidatorService _validatorService;
     

        public AccountController(ApplicationContext context,IDNTCaptchaValidatorService validatorService)
        {
            this._context = context;
            this._validatorService=validatorService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()//get the user login data 
        {
            return View();
        }
        [HttpPost]
       // [ValidateDNTCaptcha(ErrorMessage ="Please Enter the correct captcha",CaptchaGeneratorLanguage =Language.English,CaptchaGeneratorDisplayMode =DisplayMode.ShowDigits)]
        public IActionResult Login(LoginSignupViewModel model)//get the user login data 
        {
            if (ModelState.IsValid)
            {
                if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
                {
                    TempData["captchaError"] = "Please Enter the valid captcha";
                    return View(model);
                }
                var user=_context.Users.Where(e=>e.Username == model.Username).FirstOrDefault();
                if (user != null)
                {
                    bool isValid=(user.Username == model.Username && DecryptPassword(user.Password) == model.Password);
                    if (isValid)
                    {
                        var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name,model.Username) },//storing username in claim name,Identity
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);//cookies based authentication
                        HttpContext.Session.SetString("Username", model.Username);
                        return RedirectToAction("Index","Blog");
                    }
                    else
                    {
                        TempData["errorPassword"] = "Invalid Password";
                        return View(model);
                    }
                }
                else
                {
                    TempData["errorUserName"] = "Username not found";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
           
        }


        [AcceptVerbs("Post", "Get")]
        public IActionResult UserNameExist(string userName)
        {
            var data = _context.Users.Where(e => e.Username == userName).SingleOrDefault();
            if (data != null)
            {
                return Json($"Username {userName} already in use!");
            }
            else
            {
                return Json(true);
            }
        }
        public IActionResult SignUp()//get the form data 
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new User()
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = EncryptPassword(model.Password),
                    Mobile = model.Mobile,
                    Gender = model.Gender,
                    City = model.City,
                    Country = model.Country,
                    IsActive = model.IsActive,
                };
                _context.Users.Add(data);
                _context.SaveChanges();
                TempData["successMessage"] = "You are eligible to login,Please fill own credential's then Login!";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["errorMessage"] = "Empty form can't be submitted";
                return View(model);
            }
        }

        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
                string encryptedPassword = Convert.ToBase64String(storePassword);
                return encryptedPassword;
            }

        }

        public static string DecryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPassword = Convert.FromBase64String(password);
                string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);       
                return decryptedPassword;
            }

        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedCookies = Request.Cookies.Keys;
            foreach (var cookie in storedCookies)
            {
                Response.Cookies.Delete(cookie);
            }   
            return RedirectToAction("Login", "Account");
        }
    }
}
