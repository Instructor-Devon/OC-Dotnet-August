using System.ComponentModel.DataAnnotations;

namespace ManyToManyz
{
    public class LogUser
    {
        [Display(Name="Email")]
        public string LogEmail {get;set;}
        [Display(Name="Password")]
        public string LogPassword {get;set;}
    }
}