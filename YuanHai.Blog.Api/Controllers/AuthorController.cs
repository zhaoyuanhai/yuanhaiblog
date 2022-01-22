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
    public class AuthorController : ControllerBase
    {
        [HttpGet()]
        [ProducesDefaultResponseType(typeof(PageModel<AuthorModel>))]
        public IActionResult GetAuthorList([FromQuery] PageModel pageModel)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(AuthorModel))]
        public IActionResult GetAuthor()
        {
            throw new NotImplementedException();
        }

        [HttpPost()]
        public IActionResult AddAuthor()
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor()
        {
            throw new NotImplementedException();
        }
    }
}
