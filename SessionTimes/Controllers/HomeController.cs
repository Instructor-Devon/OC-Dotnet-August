using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SessionTimes.Models;
using Microsoft.AspNetCore.Http;

namespace SessionTimes.Controllers
{
    public class HomeController : Controller
    {
        private int? SessionScore
        {
            get { return HttpContext.Session.GetInt32("score"); }
            set 
            { 
                // value
                if(value != null)
                    HttpContext.Session.SetInt32("score", (int)value);
                
            }
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            var friends = DbConnector.Query("SELECT * FROM friends;");
            // First time user experience
            // check to see if we have "score" in session
            int? sessionScore = HttpContext.Session.GetInt32("score");
            if(SessionScore == null)
            {
                // if not set "score" in session to 0
                SessionScore = 0;
            }
            // provide session values to our view (with ViewBag)
            ViewBag.Score = SessionScore;
            // if so cool
        
            return View();
        }
        [HttpGet("/click")]
        public IActionResult Click()
        {
            // we want to increment score by 1
            // get the score from session (in a var)
            // NULL CHECK
            if(SessionScore == null)
            {
                return Json(0);
            }
            // increment variable
            SessionScore++;
            // store it back
            return Json(SessionScore);
        }
        [HttpGet("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
