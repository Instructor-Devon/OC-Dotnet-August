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
        public IActionResult Index()
        {
            List<Dictionary<string, object>> result = DbConnector.Query("SELECT * FROM users");
            return View(result);
        }
        [HttpPost("create")]
        public IActionResult Create(string first_name, string last_name)
        {
            // Chief O'Brian
            string insert = $"INSERT INTO users (first_name, last_name) VALUES ('{first_name}', '{last_name}');";
            DbConnector.Execute(insert);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
