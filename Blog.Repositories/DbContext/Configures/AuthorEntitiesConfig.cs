using Blog.Repositories.DbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Blog.Repositories.DbContext.Config;

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
        }
    }
}