using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFIntro.Models;

namespace EFIntro.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext {get;}
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            // SELECT * FROM Users
            var users = dbContext.Users.ToList();

            // SELECT * FROM Users WHERE UserId = 1;
            //                                            input   return value
            User someUser = dbContext.Users.FirstOrDefault(user => user.UserId == 1);
            // SELECT * FROM Users ORDER BY CreatedAt DESC LIMIT 5;
            List<User> result = dbContext.Users
                .OrderByDescending(user => user.CreatedAt)
                .Take(5)
                .ToList();
    
            return View(users);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
