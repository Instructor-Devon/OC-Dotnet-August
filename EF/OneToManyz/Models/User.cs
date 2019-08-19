using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace OneToManyz.Models
{
    public class User
    {
        [Key]
        [Column("id")]
        public int UserId {get;set;}

        [Column("first_name", TypeName="VARCHAR(45)")]
        [Display(Name="First Name")]
        [Required]
        public string FirstName {get;set;}

        [Column("last_name", TypeName="VARCHAR(45)")]
        [Required]
        [Display(Name="Last Name")]
        public string LastName {get;set;}

        [Column("email", TypeName="VARCHAR(45)")]
        [Required]
        [EmailAddress]
        public string Email {get;set;}

        [Required]
        [Column("password", TypeName="VARCHAR(255)")]
        public string Password {get;set;}

        [NotMapped]
        [Compare("Password")]
        public string Confirm {get;set;}
        [Column("dob")]
        public DateTime DOB {get;set;} = DateTime.Now;

        [Column("created_at")]
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        
        [Column("updated_at")]
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        // Navigation Properties
        public List<Post> CreatedPosts {get;set;}
        public string FullName
        {
            get
            {
                return $"{this.FirstName} {this.LastName}";
            }
        }
        public void Update(User formObject)
        {
            this.FirstName = formObject.FirstName;
            this.LastName = formObject.LastName;
            this.Email = formObject.Email;
            this.UpdatedAt = DateTime.Now;
        }
    }
}