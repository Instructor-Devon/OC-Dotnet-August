using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManyToManyz.Models;
using Microsoft.AspNetCore.Http;
using ManyToManyz.Filters;

namespace ManyToManyz.Controllers
{
    [Route("posts")]
    [LoggedIn]
    public class PostsController : Controller
    {
        private int? SessionUser
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
            set { HttpContext.Session.SetInt32("UserId", (int)value); }
        }
        private MyContext dbContext;
        public PostsController(MyContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            var posts = dbContext.Posts
                .Include(p => p.Creator)
                .Include(p => p.Votes)
                // ThenInclude() to get user name from user_id
                    .ThenInclude(v => v.Voter)
                .ToList();
            ViewBag.Users = dbContext.Users.ToList();
            return View(posts);
        }
        [HttpGet("new")]
        public IActionResult NewPost()
        {
            var users = dbContext.Users.ToList();
            ViewBag.Users = users;
            return View();
        }
        [HttpPost("create")]
        public IActionResult CreatePost(Post newPost)
        {
            if(ModelState.IsValid)
            {
                dbContext.Posts.Add(newPost);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Users = dbContext.Users.ToList();
            return View("Index", dbContext.Posts.ToList());
        }
    }
}