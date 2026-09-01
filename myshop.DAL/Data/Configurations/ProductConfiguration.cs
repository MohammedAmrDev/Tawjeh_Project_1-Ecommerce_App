using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myshop.Models.Entities;
using myshop.Models.IdentityEntities;

namespace myshop.DAL.Data.Configurations
{
	internal class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			
		}
	}
}
