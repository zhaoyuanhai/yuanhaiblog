using Blog.Repositories.Abstractions.Entities;
using Blog.Services.Abstractions;
using Blog.Services.Abstractions.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YuanHai.Blog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IArticleService _articleService;

        public IEnumerable<ArticleModel> ArticleModels { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        public async Task OnGet(int pageIndex = 1, int pageSize = 10)
        {
            var result = await _articleService.GetArticlePageList(new BasicModel.PageModel()
            {
                Index = pageIndex,
                Size = pageSize
            });

            this.ArticleModels = result.Data;

            if (!this.ArticleModels.Any())
            {
                var articleid = Guid.Parse("3A82C623-A6FD-441B-BF89-F0A06B771639");

                var authors = new List<AuthorEntities>()
                {
                   new AuthorEntities(){
                        Id = Guid.Parse("F7197771-D918-43EC-BF68-B039303313BE"),
                        FirstName = "张",
                        LastName = "三",
                        ArticleId = articleid
                   }
                };

                var article = new ArticleEntities()
                {
                    Id = articleid,
                    Title = "C# 学习指南",
                    Content = "XXXXXXXXXXXXXXXXXXXXXXXXXx",
                    CreateTime = DateTime.Now,
                    AuthorEntities = authors
                };

                if (await _articleService.AddArticles(article))
                {
                    Console.WriteLine("添加成功");
                }
            }
        }
    }
}