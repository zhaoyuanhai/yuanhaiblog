using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YuanHai.Blog.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("article/")]
    public class ArticleController : Controller
    {
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("list")]
        public async Task<IActionResult> List()
        {
            throw new NotImplementedException();
        }
    }
}
