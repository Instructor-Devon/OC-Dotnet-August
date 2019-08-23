using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FakeReddit.Models;
using Microsoft.AspNetCore.Http;
using FakeReddit.Filters;
using System.Collections.Generic;

namespace FakeReddit.Controllers
{
    [Route("posts")]
    // localhost:5000/posts
    public class PostsController : Controller
    {
        private int? SessionUser
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
            set { HttpContext.Session.SetInt32("UserId", (int)value); }
        }
        private List<Post> posts
        {
            get
            {
                return dbContext.Posts
                    .Include(p => p.Creator)
                    .Include(p => p.Votes)
                    // ThenInclude() to get user name from user_id
                        .ThenInclude(v => v.Voter)
                    .ToList();
            }
        }
        private MyContext dbContext;
        public PostsController(MyContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.UserId = SessionUser;
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
            ViewBag.UserId = SessionUser;
            return View("Index", posts);
        }
        [HttpGet("delete/{postId}")]
        public IActionResult Delete(int postId)
        {
            // query for thing
            Post toDel = dbContext.Posts.FirstOrDefault(p => p.PostId == postId);
            dbContext.Posts.Remove(toDel);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}