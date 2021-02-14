using Blog.Repositories.DbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Blog.Repositories.DbContext.Config;

namespace Blog.Repositories.DbContext.Configures
{
    internal class ArticelEntitiesConfig : IEntityTypeConfiguration<ArticelEntities>
    {
        public void Configure(EntityTypeBuilder<ArticelEntities> builder)
        {
            builder.ToTable(ToTableName(nameof(ArticelEntities)));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(100);
        }
    }
}