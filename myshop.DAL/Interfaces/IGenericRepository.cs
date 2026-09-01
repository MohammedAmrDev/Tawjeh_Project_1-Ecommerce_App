using myshop.Models.Entities;
using System.Linq.Expressions;

namespace myshop.DAL.Interfaces
{
	public interface IGenericRepository<T> where T : class, IGenericEntity
	{
		Task<int> GetCountAsync();
		Task<List<T>> GetAllForDataTableAsync(Expression<Func<T, bool>>? filter, string? orderBy, string? orderDir, int start, int length, Expression<Func<T, object>>? include = null);
		Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
		Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
		Task CreateAsync(T entity);
		void Update(T newEntity);
		void Delete(T entity);
	}
}