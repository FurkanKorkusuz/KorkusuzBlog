using Blog.Entity.Entity;
using Blog.Entity.Identity;
using Korkusuz.DAL.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korkusuz.DAL.Context
{
    public class BlogContext : IdentityDbContext<ApplicationUser>
    {
        public BlogContext() : base("BlogContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogContext, Configuration>("BlogContext"));
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
    }
}
