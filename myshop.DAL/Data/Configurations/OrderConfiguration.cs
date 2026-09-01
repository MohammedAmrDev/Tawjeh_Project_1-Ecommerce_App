using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myshop.Models.Entities;
using myshop.Models.IdentityEntities;

namespace myshop.DAL.Data.Configurations
{
	internal class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(p => p.Name).IsRequired();
			builder.Property(p => p.ImageURL).HasAnnotation("DisplayName", "Image");
			builder.Property(p => p.Price).IsRequired();
			builder.Property(p => p.CategoryId).IsRequired().HasAnnotation("DisplayName", "Category");
			builder.HasQueryFilter(p => !p.IsDeleted);
		}
	}
}
