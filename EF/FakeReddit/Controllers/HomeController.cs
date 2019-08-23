using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FakeReddit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FakeReddit.Controllers
{
    public class HomeController : Controller
    {
        public static string USER_KEY = "UserId";
        private int? SessionUser
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
            set { HttpContext.Session.SetInt32("UserId", (int)value); }
            // SessionUser = 1;
        }
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

                SessionUser = newUser.UserId;
                return RedirectToAction("Show", new {id=newUser.UserId});
            }
            return View("Index");
        }
        [HttpGet("user/{id}")]
        public IActionResult Show(int id)
        {
            // grab a user, inlcude their posts
            var user = dbContext.Users
                .Include(u => u.CreatedPosts)
                    .ThenInclude(p => p.Votes)
                    .ThenInclude(v => v.Voter)
                .FirstOrDefault(u => u.UserId == id);
            return View(user);
            
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
            
                SessionUser = check.UserId;
                return RedirectToAction("Index", "Posts");
                // return Redirect("/");
                // return RedirectToAction("Show", new {id=check.UserId});
            }
            return View("Index");

        }
        [HttpGet("success")]
        public string Success()
        {
            return "S U C C E S S";
        }
        
    }
}
