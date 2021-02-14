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

            services.AddScoped<IArticleRepository, ArticleRepository>();

            return services;
        }
    }
}
