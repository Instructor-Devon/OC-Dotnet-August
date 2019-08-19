using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneToManyz.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace OneToManyz.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("register")]
        public IActionResult Register(LogRegViewModel model)
        {
            User newUser = model.RegUser;
            if(ModelState.IsValid)
            {
                // we want to ensure email is unique to db
                bool notUnique = dbContext.Users.Any(a => a.Email == newUser.Email);
                if(notUnique)
                {
                    // we have an error!
                    ModelState.AddModelError("Email", "Email is in use");
                    return View("Index");
                }
                // We have to hash password first!
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                string hash = hasher.HashPassword(newUser, newUser.Password);
                newUser.Password = hash;

                // We are ready to add user to db
                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
                return RedirectToAction("Success");
            }
            return View("Index");
        }
        [HttpGet("user/{id}")]
        public IActionResult Show(int id)
        {
            // grab a user, inlcude their posts
            var user = dbContext.Users.Include(u => u.CreatedPosts)
                .FirstOrDefault(u => u.UserId == id);
            return View(user);
            
        }
        [HttpGet("posts/new")]
        public IActionResult NewPost()
        {
            var users = dbContext.Users.ToList();
            ViewBag.Users = users;
            return View();
        }
        [HttpPost("login")]
        public IActionResult Login(LogRegViewModel model)
        {
            LogUser user = model.LogUser;
            if(ModelState.IsValid)
            {
                // check if email exists in db (grab a user)
                User check = dbContext.Users.FirstOrDefault(u => u.Email == user.LogEmail);
                // if check is null, no email exists
                if(check == null)
                {
                    ModelState.AddModelError("LogUser.LogEmail", "Invalid Email/Password");
                    return View("Index");
                }

                // check.Password needs to match user.Password
                PasswordHasher<LogUser> hasher = new PasswordHasher<LogUser>();
                var result = hasher.VerifyHashedPassword(user, check.Password, user.LogPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("LogUser.LogEmail", "Invalid Email/Password");
                    return View("Index");
                }
                // WE ARE OK TO SET USER IN SESSION
                long MAX_LONG = Int64.MaxValue;
                HttpContext.Session.SetInt32("long", (int)MAX_LONG);

                HttpContext.Session.SetInt32("UserId", check.UserId);
                return RedirectToAction("Success");
            }
            return View("Index");

        }
        [HttpGet("success")]
        public string Success()
        {
            return "S U C C E S S";
        }
        [HttpGet("posts")]
        public IActionResult AllPosts()
        {
            var posts = dbContext.Posts
                .Include(p => p.Creator)
                .ToList();
            return View(posts);
        }
    }
}
