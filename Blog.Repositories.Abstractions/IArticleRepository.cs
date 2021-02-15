using Blog.Repositories.Abstractions.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.IRepositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<ArticleEntities>> QueryArticle(int pageIndex, int pageSize);
    
        Task<bool> AddArticles(params ArticleEntities[] articles);
    }
}
