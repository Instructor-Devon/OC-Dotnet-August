using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToManyz.Models;

namespace OneToManyz.Controllers
{
    [Route("posts")]
    public class PostsController : Controller
    {
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