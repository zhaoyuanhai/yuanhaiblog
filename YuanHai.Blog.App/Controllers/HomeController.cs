using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YuanHai.Blog.App.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.RecordNotFound(1);

            return Content("");
        }
    }

    internal static class LoggerExtensions
    {
        public static void RecordNotFound(this ILogger logger, int id) => NotFound(logger, id, null);

        private static Action<ILogger, int, Exception> NotFound => LoggerMessage.Define<int>(LogLevel.Error, new EventId(123, nameof(NotFound)), "");
    }
}
