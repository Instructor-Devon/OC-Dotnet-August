using Microsoft.EntityFrameworkCore;
namespace ManyToManyz.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options):base(options){}
        public DbSet<User> Users {get;set;}
        public DbSet<Post> Posts {get;set;}
        public DbSet<Vote> Votes {get;set;}
    }
}
