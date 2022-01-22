using BasicModel;
using Blog.Services.Abstractions;
using Blog.Services.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace YuanHai.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet()]
        [ProducesDefaultResponseType(typeof(PageModel<ArticleModel>))]
        public async Task<IActionResult> GetArticleList([FromQuery] PageModel pageModel)
        {
            var result = await _articleService.GetArticlePageList(pageModel);
            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(ArticleModel))]
        public IActionResult GetArticle()
        {
            throw new NotImplementedException();
        }

        [HttpPost()]
        public IActionResult AddArticle()
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateArticle()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteArticle()
        {
            throw new NotImplementedException();
        }

        [HttpGet("search/{keyword}")]
        public IActionResult SearchArticle(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
