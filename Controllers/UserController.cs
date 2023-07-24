using Blogger.Data;
using Blogger.Models;
using Blogger.Models.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace Blogger.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationContext _context;

        public UserController(ApplicationContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserProfile()
        {
            string loggedInUserName=HttpContext.User.Identity.Name;
            User user = _context.Users.FirstOrDefault(e => e.Username == loggedInUserName);
            return View(user);
        }

        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadImage(ImagePath model)
        {
            if (model.ImageURL != null && model.ImageURL.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageURL.FileName;
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    model.ImageURL.CopyTo(stream);
                }
            }

            return RedirectToAction("UserProfile");
        }

        public IActionResult DisplayImage()
        {
            string fileName = "your_uploaded_image_filename.jpg";
            string imagePath = Path.Combine("/images", fileName);
            return View(imagePath);
        }


    }
}
