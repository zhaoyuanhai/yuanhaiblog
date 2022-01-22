using Blog.Repositories.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Blog.Repositories.DbContext.Config;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blog.Repositories.DbContext.Configures
{
    internal class AuthorEntitiesConfig : IEntityTypeConfiguration<AuthorEntities>
    {
        public void Configure(EntityTypeBuilder<AuthorEntities> builder)
        {
            builder.ToTable(ToTableName(nameof(AuthorEntities)));

            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.Name);
            builder.Property(x => x.FirstName).HasMaxLength(20);
            builder.Property(x => x.LastName).HasMaxLength(20);

            builder.HasOne<ArticleEntities>()
                .WithMany(b => b.AuthorEntities)
                .HasForeignKey(nameof(AuthorEntities.ArticleId));
        }
    }
}