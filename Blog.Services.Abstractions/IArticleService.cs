using BasicModel;
using Blog.Repositories.Abstractions.Entities;
using Blog.Services.Abstractions.Models;
using System.Threading.Tasks;

namespace Blog.Services.Abstractions
{
    public interface IArticleService
    {
        Task<PageModel<ArticleModel>> GetArticlePageList(PageModel pageModel);

        Task<bool> AddArticles(params ArticleEntities[] article);
    }
}