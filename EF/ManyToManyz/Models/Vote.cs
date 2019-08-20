using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManyToManyz.Models
{
    public class Vote
    {
        [Key]
        [Column("id")]
        public int VoteId {get;set;}
        [Column("user_id")]
        public int UserId {get;set;}
        [Column("post_id")]
        public int PostId {get;set;}
        [Column("is_upvote")]
        public bool IsUpvote {get;set;}

        // Navigation Properties
        public User Voter {get;set;}
        public Post Voted {get;set;}
    }
}