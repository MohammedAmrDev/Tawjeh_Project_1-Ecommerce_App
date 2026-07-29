using myshop.Models.Entities;
using System.Linq.Expressions;

namespace myshop.DAL.Interfaces
{
	public interface IProductsRepository : IGenericRepository<Product>
	{
		Task<Product?> GetByIdAsync(int id, params Expression<Func<Product, object>>[] includes);
		Task<(List<Product> Data, int countBeforePagination)> GetAllAsync(string? sortBy, bool isDsc, int pageIndex, int length, params Expression<Func<Product, bool>>?[] predicates);
	}
}