using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Repositories.Abstractions.Entities
{
    [Table("Articel")]
    public class ArticleEntities
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual ICollection<AuthorEntities> AuthorEntities { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public DateTime PublishTime { get; set; }

        public bool IsPublish { get; set; }

        public bool IsDeleted { get; set; }
    }
}
