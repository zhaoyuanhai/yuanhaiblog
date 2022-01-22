using Blog.Repositories.DbContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Blog.IRepositories;

namespace Blog.Repositories
{
    public static class InjectionService
    {
        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<BlogDbContext>(build =>
            {
                build.UseSqlServer(configuration.GetConnectionString(nameof(BlogDbContext)));
            });

            services.AddDbContext<BlogDbContext>(build =>
            {
                build.UseSqlServer(configuration.GetConnectionString(nameof(BlogDbContext)));
            });

            var dbContext = services.BuildServiceProvider().GetRequiredService<BlogDbContext>();
            if (!dbContext.Database.IsInMemory())
            {
                var timeout = dbContext.Database.GetCommandTimeout();

                dbContext.Database.SetCommandTimeout(3000);

                dbContext.Database.Migrate();

                dbContext.Database.SetCommandTimeout(timeout);
            }

            services.AddScoped<IArticleRepository, ArticleRepository>();

            return services;
        }
    }
}
