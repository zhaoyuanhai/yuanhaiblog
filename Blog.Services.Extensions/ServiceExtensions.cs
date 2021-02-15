using Blog.Repositories.Extensions;
using Blog.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IArticleService, ArticleService>();

            services.AddRepositories(configuration);

            return services;
        }
    }
}
