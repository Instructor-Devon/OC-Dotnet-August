using Microsoft.AspNetCore.Mvc;
using FakeReddit.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace FakeReddit.Controllers
{
    [Route("votes")]
    public class VotesController : Controller
    {
        private int? SessionUser
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
            set { HttpContext.Session.SetInt32("UserId", (int)value); }
        }
        MyContext dbContext;
        public VotesController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("{postId}/vote/{isUpvote}")]
        public IActionResult Vote(int postId, bool isUpvote)
        {
            
            Vote newVote = new Vote()
            {
                PostId = postId,
                IsUpvote = isUpvote,
                UserId = (int)SessionUser
            };

            dbContext.Votes.Add(newVote);
            dbContext.SaveChanges();
            return RedirectToAction("Index", "Posts");
        }

        [HttpGet("delete/{postId}")]
        public IActionResult Delete(int postId)
        {
            // query for thing
            Vote toDel = dbContext.Votes.FirstOrDefault(v => v.PostId == postId && v.UserId == (int)SessionUser);
            dbContext.Votes.Remove(toDel);
            dbContext.SaveChanges();
            return RedirectToAction("Index", "Posts");
        }
    }
}