using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curmudgeon.Models
{
    public class CurmudgeonDbContext : IdentityDbContext <ApplicationUser>
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public CurmudgeonDbContext(DbContextOptions options) : base(options)
        {
           
        }

        
    }
}
