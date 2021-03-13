using Blog.IRepositories;
using Blog.Repositories.Abstractions.Entities;
using Blog.Services;
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

            var articleService = new ArticleService(articleRespositoryMock.Object);

            await articleService.AddArticles(new ArticleEntities());

            Assert.True(true);
        }
    }
}
