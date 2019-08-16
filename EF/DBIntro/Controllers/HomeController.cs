using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DBIntro.Models;

namespace DBIntro.Controllers
{
    public class HomeController : Controller
    {
        private List<Dictionary<string, object>> AllUsers
        {
            get
            {
                return DbConnector.Query("SELECT * FROM users;");
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("create")]
        public IActionResult Create(string first_name, string last_name)
        {
            // Chief O'Brian
            // "Obrian'); DROP TABLE users;
            string insert = $"INSERT INTO users (first_name, last_name) VALUES ('{first_name}', '{last_name}');";
            DbConnector.Execute(insert);
            return RedirectToAction("Users");
        }
        [HttpGet("/click")]
        public IActionResult Click()
        {
            return PartialView("UserPartial", AllUsers);
        }
        [HttpGet("users")]
        public IActionResult Users()
        {
            return PartialView("UserPartial", AllUsers);       
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
