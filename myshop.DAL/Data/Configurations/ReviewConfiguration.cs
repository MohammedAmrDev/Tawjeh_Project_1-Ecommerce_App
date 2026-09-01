using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myshop.Models.Entities;
using myshop.Models.IdentityEntities;

namespace myshop.DAL.Data.Configurations
{
	internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
	{
		public void Configure(EntityTypeBuilder<Review> builder)
		{
			
		}
	}
}
