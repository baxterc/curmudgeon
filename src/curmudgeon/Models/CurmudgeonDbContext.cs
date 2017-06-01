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
        public CurmudgeonDbContext()
        {

        }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<PostTag> PostTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CurmudgeonDb;integrated security = True");
        }

        public CurmudgeonDbContext(DbContextOptions options) : base(options)
        {
           
        }

        
    }
}
