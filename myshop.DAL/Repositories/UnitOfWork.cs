using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using myshop.DAL.Data;
using myshop.DAL.Interfaces;
using myshop.Models.Entities;

namespace myshop.DAL.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		public ICategoriesRepository Categories { get; }
		public IProductsRepository Products { get; }
		public IOrderRepository Orders { get; }
		public IReviewRepository Reviews { get; }


		private readonly ApplicationDbContext _context;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			Categories = new CategoriesRepository(_context);
			Products = new ProductsRepository(_context);
			Orders = new OrderRepository(_context);
			Reviews = new ReviewRepository(_context);
		}

		public async Task<int> SaveChangesAsync()
		{
			foreach (var entry in _context.ChangeTracker.Entries())
			{
				if (entry.State == EntityState.Deleted && entry.Entity is ISoftDeletableEntity entity)
				{
					entry.State = EntityState.Modified;
					entity.IsDeleted = true;
				}
			}
			return await _context.SaveChangesAsync();
		}
	}
}
