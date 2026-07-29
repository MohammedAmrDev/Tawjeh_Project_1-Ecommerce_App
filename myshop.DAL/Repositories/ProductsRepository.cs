using Microsoft.EntityFrameworkCore;
using myshop.DAL.Data;
using myshop.DAL.Interfaces;
using myshop.Models.Entities;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace myshop.DAL.Repositories
{
	internal class ProductsRepository : GenericRepository<Product>, IProductsRepository
	{
		private readonly ApplicationDbContext _context;
		public ProductsRepository(ApplicationDbContext context) : base(context) => _context = context;

		public async Task<Product?> GetByIdAsync(int id, params Expression<Func<Product, object>>[] includes)
		{
			IQueryable<Product> query = _context.Products;

			foreach (var include in includes)
			{
				query = query.Include(include);
			}

			return await query.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<(List<Product>, int)> GetAllAsync(string? sortBy, bool isDesc, int pageIndex, int length, params Expression<Func<Product, bool>>?[] predicates)
		{
			var query = _context.Products.AsNoTracking().AsQueryable();

			foreach (var predicate in predicates)
				if (predicate is not null)
					query = query.Where(predicate);
			
			if (sortBy is not null)
			{
				var sortDir = isDesc ? "desc" : "asc";
				query = query.OrderBy($"{sortBy} {sortDir}");
			}

			var countBeforePagination = await query.CountAsync();

			query =	query.Skip(pageIndex * length).Take(length);

			return (await query.Include(p => p.Category).ToListAsync(), countBeforePagination);
		}
	}
}
