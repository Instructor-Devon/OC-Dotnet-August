using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OneToManyz.Models;

namespace OneToManyz.Models
{
    public class Post
    {
        [Key]
        [Column("id")]
        public int PostId {get;set;}
        [Column("user_id")]
        public int UserId {get;set;}
        [Column("content")]
        public string Content {get;set;}
        [Column("created_at")]
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        [Column("updated_at")]
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        // Navigation Properties    
        public User Creator {get;set;}
    }
}