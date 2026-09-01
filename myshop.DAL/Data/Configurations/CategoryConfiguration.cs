using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myshop.Models.Entities;

namespace myshop.DAL.Data.Configurations
{
	internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.Property(c => c.Name).IsRequired();
			builder.HasQueryFilter(c => !c.IsDeleted);
		}
	}
}
