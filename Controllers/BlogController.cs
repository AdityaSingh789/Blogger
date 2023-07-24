using Blogger.Data;
using Blogger.Models;
using Blogger.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Blogger.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BlogController(ApplicationContext context, IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            this._webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var result = _context.Blogs.ToList();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogViewModel model)//Creating a new blog data
        {
            if (ModelState.IsValid)
            {
                var blg = new Blog()
                {
                    UserName = model.UserName,
                    Tag = model.Tag,
                    Content = model.Content,
                    Title=model.Title,
                  
                };
                _context.Blogs.Add(blg);
                _context.SaveChanges();
                TempData["error"] = "Your blog is created successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Empty field can't Submit.";
                return View(model);
            }
            
        }

        public IActionResult Delete(int Id)//Deleting the data w.r.t ID
        {
            var blg = _context.Blogs.SingleOrDefault(e => e.Id == Id);
            _context.Blogs.Remove(blg);
            _context.SaveChanges();
            TempData["error"] = "Your blog is deleted successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)//Getting the data to edit
        {
            var user = _context.Blogs.SingleOrDefault(e => e.Id == id);
            var result = new BlogViewModel()
            {
                UserName = user.UserName,
                Tag = user.Tag,
                Content=user.Content,
                Title = user.Title,
            };
            return View(result);    
        }
        [HttpPost]
        public IActionResult Edit(BlogViewModel model)//Taking the edited data and updating it and showing on Blog page
        {
            var user = new Blog()
            {
                Id = model.Id,
                UserName = model.UserName,
                Tag = model.Tag,
                Content = model.Content,
                Title = model.Title,
            };
            _context.Blogs.Update(user);
            _context.SaveChanges();
            TempData["error"] = "Your blog is updated successfully!";
            return RedirectToAction("Index");
        }
    }
}
