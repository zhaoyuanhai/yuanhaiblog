using Blog.IRepositories;
using Blog.Services.Abstractions;
using Blog.Services.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Services
{
    public class ArticleService : IArticleService
    {
        readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<bool> AddArticles(params Repositories.Abstractions.Entities.ArticleEntities[] articles)
        {
            return await _articleRepository.AddArticles(articles);
        }

        public async Task<IEnumerable<ArticleModel>> GetArticlePageList(PageingModel pageModel)
        {
            var entities = await _articleRepository.QueryArticle(pageModel.PageIndex, pageModel.PageSize);
            var models = new List<ArticleModel>();
            foreach (var item in entities)
            {
                models.Add(new ArticleModel()
                {
                    Id = item.Id,
                    Content = item.Content,
                    CreatTime = item.CreateTime,
                    IsPublish = item.IsPublish,
                    Title = item.Title,
                    UpdateTime = item.UpdateTime
                });
            }

            return models;
        }
    }
}
