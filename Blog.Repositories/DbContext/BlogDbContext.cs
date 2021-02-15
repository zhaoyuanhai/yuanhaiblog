using Blog.Repositories.Abstractions.Entities;
using Blog.Repositories.DbContext.Configures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Blog.Repositories.DbContext
{
    internal class BlogDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticelEntitiesConfig());
            modelBuilder.ApplyConfiguration(new AuthorEntitiesConfig());
             
            base.OnModelCreating(modelBuilder);
        }
    }
}
