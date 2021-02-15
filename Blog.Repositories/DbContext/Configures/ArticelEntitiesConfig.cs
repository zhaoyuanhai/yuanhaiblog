using Blog.Repositories.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using static Blog.Repositories.DbContext.Config;

namespace Blog.Repositories.DbContext.Configures
{
    internal class ArticelEntitiesConfig : IEntityTypeConfiguration<ArticleEntities>
    {
        public void Configure(EntityTypeBuilder<ArticleEntities> builder)
        {
            builder.ToTable(ToTableName(nameof(ArticleEntities)));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(100);
            builder.Property(x => x.UpdateTime).HasDefaultValue(DateTime.Now);
        }
    }
}