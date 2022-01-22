using BasicModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YuanHai.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        [HttpGet()]
        [ProducesDefaultResponseType(typeof(PageModel<ArticleModel>))]
        public IActionResult GetArticleList([FromQuery] PageModel pageModel)
        {
            throw new NotImplementedException();
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
