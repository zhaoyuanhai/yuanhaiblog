using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Abstractions.Models
{
    public class ArticleModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public bool IsPublish { get; set; }
    }
}
