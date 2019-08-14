using System.ComponentModel.DataAnnotations;
using System;

namespace EFIntro.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Email {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public static void Test()
        {
            string strId = "1";

            int id = Convert.ToInt32(strId);
        }
    }
}