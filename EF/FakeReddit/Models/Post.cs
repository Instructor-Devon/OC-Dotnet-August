using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using FakeReddit.Models;

namespace FakeReddit.Models
{
    public class Post
    {
        [Key]
        [Column("id")]
        public int PostId {get;set;}
        [Column("user_id")]
        public int UserId {get;set;}
        [Column("content")]
        [Required]
        public string Content {get;set;}
        [Column("created_at")]
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        [Column("updated_at")]
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public int Karma
        {
            get
            {
                return this.Votes.Count(v => v.IsUpvote) - this.Votes.Count(v => !v.IsUpvote);
            }
        }

        // Navigation Properties    
        public User Creator {get;set;}
        public List<Vote> Votes {get;set;}
    }
}