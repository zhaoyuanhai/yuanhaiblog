using Blog.Repositories.DbContext.Configures;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repositories.DbContext
{
    internal class BlogDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        internal BlogDbContext(DbContextOptions<BlogDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticelEntitiesConfig());
            modelBuilder.ApplyConfiguration(new AuthorEntitiesConfig());
        }
    }
}
