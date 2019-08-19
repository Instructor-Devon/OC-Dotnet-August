using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFIntro.Models;
using Microsoft.AspNetCore.Hosting;

namespace EFIntro.Controllers
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
            // SELECT * FROM Users WHERE UserId = 1;
            //                                            input   return value
            User someUser = dbContext.Users.FirstOrDefault(user => user.UserId == 1);
            // SELECT * FROM Users ORDER BY CreatedAt DESC LIMIT 5;
            List<User> result = dbContext.Users
                .OrderByDescending(user => user.CreatedAt)
                .ToList();
    
            return View(result);
        }
        [HttpGet("new")]
        public IActionResult New() => View();

        [HttpPost("create")]
        public IActionResult Create(User newUser)
        {
            

            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("user/{userid}")]
        // localhost:5000/23445
        public IActionResult Show(int userid)
        {
            User somebody = dbContext.Users.FirstOrDefault(u => u.UserId == userid);
            // make sure somebody is a user
            if(somebody == null)
                // lets get outta here!
                return RedirectToAction("Index");

            return View(somebody);

        }
        [HttpPost("user/{userId}/update")]
        public IActionResult Update(User user, int userId)
        {
            // query for the thing to update from the DB
            User toUpdate = dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            // make sure this gal is in db
            if(toUpdate == null)
                return RedirectToAction("Index");
            // update that object with your changes

            // or to be cool...
            // toUpdate.Update(user);

            toUpdate.FirstName = user.FirstName;
            toUpdate.LastName = user.LastName;
            toUpdate.Email = user.Email;
            toUpdate.UpdatedAt = DateTime.Now;

            // apply those changes
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("user/{userId}/delete")]
        public IActionResult Delete(int userId)
        {
            // query for user in db
            User toDelete = dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            // make sure this gal is in db
            if(toDelete == null)
                return RedirectToAction("Index");

            // stage the delete
            dbContext.Users.Remove(toDelete);

            // perform the delete
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
