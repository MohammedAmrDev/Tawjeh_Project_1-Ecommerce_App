using myshop.Models.Entities;
using System.Linq.Expressions;

namespace myshop.DAL.Interfaces
{
	public interface IProductsRepository : IGenericRepository<Product>, ISoftDeleteRepository<Product>
	{
		Task<(List<Product> Data, int countBeforePagination)> GetAllAsync(string? sortBy, bool isDsc, int pageIndex, int length, params Expression<Func<Product, bool>>?[] predicates);
	}
}