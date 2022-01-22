using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YuanHai.Blog.Api.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [Authorize]
    [Route("user")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 用户登录 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/login")]
        public async Task<IActionResult> Login()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/logout")]
        public async Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
