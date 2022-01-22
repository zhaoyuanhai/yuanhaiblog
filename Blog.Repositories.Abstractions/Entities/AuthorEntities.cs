using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Repositories.Abstractions.Entities
{
    [Table("Author")]
    public class AuthorEntities
    {
        public Guid Id { get; set; }

        public string Name
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
            set
            {
                var names = value.Split(' ');
                this.FirstName = names[0];
                this.LastName = names[1];
            }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid ArticleId { get; set; }
    }
}
