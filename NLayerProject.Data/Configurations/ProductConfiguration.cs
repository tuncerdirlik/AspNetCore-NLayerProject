using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerProject.Core.Models;

namespace NLayerProject.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Id).UseIdentityColumn();

            builder.Property(k => k.Name).IsRequired().HasMaxLength(200);
            builder.Property(k => k.Stock).IsRequired();
            builder.Property(k => k.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(k => k.InnerBarcode).HasMaxLength(50);

            builder.ToTable("Products");
        }
    }
}
