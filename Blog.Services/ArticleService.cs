using AutoMapper;
using BasicModel;
using Blog.IRepositories;
using Blog.Services.Abstractions;
using Blog.Services.Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Services
{
    public class ArticleService : ServiceBase, IArticleService
    {
        readonly IArticleRepository _articleRepository;
        readonly IMapper _mapper;

        public ArticleService(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddArticles(params Repositories.Abstractions.Entities.ArticleEntities[] articles)
        {
            return await _articleRepository.AddArticles(articles);
        }

        public async Task<PageModel<ArticleModel>> GetArticlePageList(PageModel pageModel)
        {
            var (entities, total) = await _articleRepository.QueryArticle(pageModel.Index, pageModel.Size);
            var models = _mapper.Map<List<ArticleModel>>(entities);
            pageModel.Total = total;
            var pageResult = new PageModel<ArticleModel>(pageModel, models);
            return pageResult;
        }
    }
}
