using Blog.IRepositories;
using Blog.Repositories.Abstractions.Entities;
using Blog.Services;
using Blog.Services.Extensions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace YuanHai.Blog.UnitTest.Blog.Service
{
    public class ArticleServiceTest
    {
        [Fact]
        public async Task MyTest()
        {
            var articleRespositoryMock = new Mock<IArticleRepository>();
            var mapperConfiguration = new AutoMapper.MapperConfiguration((configuration) =>
            {
                configuration.AddProfile<ModelProfile>();
            });

            var mapper = mapperConfiguration.CreateMapper();

            var articleService = new ArticleService(articleRespositoryMock.Object, mapper);

            await articleService.AddArticles(new ArticleEntities());

            Assert.True(true);
        }

        [Fact]
        public async Task ss()
        {
            await Task.CompletedTask;
            Assert.True(true);
        }
    }
}
