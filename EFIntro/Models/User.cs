using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFIntro.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}
        [Required]
        public string FirstName {get;set;}
        [Required]
        public string LastName {get;set;}
        [Required]
        [EmailAddress]
        public string Email {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        public void Update(User formObject)
        {
            this.FirstName = formObject.FirstName;
            this.LastName = formObject.LastName;
            this.Email = formObject.Email;
            this.UpdatedAt = DateTime.Now;
        }
    }
}