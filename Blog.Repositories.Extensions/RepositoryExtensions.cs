using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddService(configuration);

            return serviceCollection;
        }
    }
}
