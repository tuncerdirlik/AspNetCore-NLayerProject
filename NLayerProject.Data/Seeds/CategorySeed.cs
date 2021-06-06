using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerProject.Core.Models;



namespace NLayerProject.Data.Seeds
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        private readonly int[] ids;
        public CategorySeed(int[] _ids)
        {
            ids = _ids;
        }

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = ids[0], Name = "Kalemler" },
                new Category { Id = ids[1], Name = "Defterler" }
            );
        }
    }
}
