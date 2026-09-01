using myshop.Models.DTOs;
using myshop.Models.ViewModels;

namespace myshop.BLL.Interfaces
{
	public interface IProductsService
	{
		Task<(List<ProductResponse> data, int recordsTotal, int recordsFiltered)> GetAllProducts(string? search, string? orderBy, string? orderDir, int? start, int? length);
		Task<(List<ProductResponse> Data, int countBeforePagination)> GetAllProducts(string? searchBy, int? categoryId, string? sortBy, bool isDsc, int pageIndex, int length);
		Task CreateProduct(ProductViewModel productViewModel);
		Task<ProductResponse?> GetProductByIdAsync(int id);
		Task UpdateProduct(ProductViewModel productViewModel);
		Task<bool> DeleteProductAsync(int id);
		Task RestoreProductAsync(int id);
	}
}