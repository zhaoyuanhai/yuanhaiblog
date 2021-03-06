﻿using Blog.IRepositories;
using Blog.Repositories.Abstractions.Entities;
using Blog.Repositories.DbContext;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repositories
{
    internal class ArticleRepository : IArticleRepository
    {
        readonly BlogDbContext _dbContext;

        public ArticleRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddArticles(params ArticleEntities[] articles)
        {
            await _dbContext.Set<ArticleEntities>().AddRangeAsync(articles);

            return _dbContext.SaveChanges() == articles.Length;
        }

        public async Task<IEnumerable<ArticleEntities>> QueryArticle(int pageIndex, int pageSize)
        {
            var articleQuery = _dbContext.Set<ArticleEntities>();

            var result = articleQuery
                .OrderBy(x => x.UpdateTime)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).AsNoTracking().ToList();

            return await Task.FromResult(result);
        }
    }
}
