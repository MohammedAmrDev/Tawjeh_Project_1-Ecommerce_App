using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myshop.Models.Entities;

namespace myshop.DAL.Data.Configurations
{
	internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
	{
		public void Configure(EntityTypeBuilder<Review> builder)
		{
			builder.Property(p => p.Comment).HasMaxLength(150);
			builder.ToTable(t => t.HasCheckConstraint(
				name: "Rate_Range",
				sql: "[Rate] >= 0 AND [Rate] <= 5"
			));
		}
	}
}
