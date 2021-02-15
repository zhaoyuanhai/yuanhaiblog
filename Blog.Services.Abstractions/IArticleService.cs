using Blog.Repositories.Abstractions.Entities;
using Blog.Services.Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Services.Abstractions
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleModel>> GetArticlePageList(PageingModel pageModel);
      
        Task<bool> AddArticles(params ArticleEntities[] article);
    }
}