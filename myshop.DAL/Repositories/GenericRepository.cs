using Microsoft.EntityFrameworkCore;
using myshop.DAL.Data;
using myshop.DAL.Interfaces;
using myshop.Models.Entities;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace myshop.DAL.Repositories
{
	public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, IGenericEntity
	{
		private readonly ApplicationDbContext _context;
		private readonly DbSet<T> _dbSet;

		public GenericRepository(ApplicationDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		#region Querying_Data
		public async Task<int> GetCountAsync() =>
			await _dbSet.IgnoreQueryFilters().CountAsync();

		public async Task<List<T>> GetAllForDataTableAsync(Expression<Func<T, bool>>? filter, string? orderBy, string? orderDir, int start, int length, Expression<Func<T, object>>? include = null)
		{
			var query = _dbSet.AsNoTracking().AsQueryable().IgnoreQueryFilters();

			if (include is not null)
				query = query.Include(include);

			if (filter is not null)
				query = query.Where(filter);

			if (orderBy is not null)
				query = query.OrderBy(orderBy + " " + (orderDir ?? "asc"));

			query = query.Skip(start).Take(length);

			return await query.ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
		{
			var query = _dbSet.AsNoTracking().AsQueryable().IgnoreQueryFilters();
			foreach (var include in includes)
			{
				query = query.Include(include);
			}
			return await query.FirstOrDefaultAsync(e => e.Id == id);
		}
		public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
		{
			var query = _dbSet.AsNoTracking().AsQueryable().IgnoreQueryFilters();
			foreach (var include in includes)
			{
				query = query.Include(include);
			}
			return await query.ToListAsync();
		}
		#endregion

		#region Create_Update_Delete
		public async Task CreateAsync(T entity)
		{
			entity.CreatedAt = DateTimeOffset.UtcNow;
			await _dbSet.AddAsync(entity);
		}
		
		public void Update(T newEntity)
		{
			newEntity.UpdatedAt = DateTimeOffset.UtcNow;
			_dbSet.Update(newEntity);
		}

		public void Delete(T entity)
		{
			_dbSet.Remove(entity);
		}
		#endregion
	}
}
