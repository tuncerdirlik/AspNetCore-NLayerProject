using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerProject.Core.Models;

namespace NLayerProject.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Id).UseIdentityColumn();

            builder.Property(k => k.Name).IsRequired().HasMaxLength(200);

            builder.ToTable("Categories");
        }
    }
}
