using System;
using Microsoft.EntityFrameworkCore;

namespace EFIntro.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) {}

        // Our table records
        public DbSet<User> Users {get;set;}
    }
}