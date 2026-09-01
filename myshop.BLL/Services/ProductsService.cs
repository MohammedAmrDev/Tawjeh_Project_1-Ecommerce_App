using AutoMapper;
using myshop.BLL.Interfaces;
using myshop.DAL.Interfaces;
using myshop.Models.DTOs;
using myshop.Models.Entities;
using myshop.Models.ViewModels;

namespace myshop.BLL.Services
{
	public class ProductsService : IProductsService
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly IFileService _imageService;

		public ProductsService(IUnitOfWork uow, IMapper mapper, IFileService imageService)
		{
			_uow = uow;
			_mapper = mapper;
			_imageService = imageService;
		}

		public async Task<(List<ProductResponse> data, int recordsTotal, int recordsFiltered)> GetAllProducts(string? search, string? orderBy, string? orderDir, int? start, int? length)
		{
			List<Product> products = await _uow.Products.GetAllForDataTableAsync(
				string.IsNullOrEmpty(search) ? null : p => p.Name.ToLower().Contains(search.ToLower()) || p.Description.ToLower().Contains(search.ToLower()),
				orderBy,
				orderDir,
				start ?? 0,
				length ?? 5,
				x => x.Category
			);

			var data = products.Select(_mapper.Map<ProductResponse>).ToList();

			return (data, await _uow.Products.GetCountAsync(), data.Count());
		}

		public async Task<(List<ProductResponse>, int)> GetAllProducts(string? searchBy, int? categoryId, string? sortBy, bool isDsc, int pageIndex, int length)
		{
			var products = await _uow.Products.GetAllAsync(
				sortBy,
				isDsc,
				pageIndex,
				length,
				searchBy is null ? null : p => p.Name.ToLower().Contains(searchBy.ToLower()) || p.Description.ToLower().Contains(searchBy.ToLower()),
				categoryId is null ? null : p => p.CategoryId == categoryId
			);
			return (products.Data.Select(_mapper.Map<ProductResponse>).ToList(), products.countBeforePagination);
		}

		public async Task CreateProduct(ProductViewModel productViewModel)
		{
			Product product = _mapper.Map<Product>(productViewModel);
			string? ImageURL = _imageService.UploadFile(productViewModel.ImageFile); // ImageFile will not be null here because of the custom validation
			if (ImageURL != null)
				product.ImageURL = ImageURL;
			await _uow.Products.CreateAsync(product);
			await _uow.SaveChangesAsync();
		}

		public async Task<ProductResponse?> GetProductByIdAsync(int id)
		{
			Product? product = await _uow.Products.GetByIdAsync(id, x => x.Category);
			if (product == null)
				return null;
			return _mapper.Map<ProductResponse>(product);
		}

		public async Task UpdateProduct(ProductViewModel productViewModel)
		{
			if (productViewModel.ImageFile != null && productViewModel.ImageURL != null)
			{
				_imageService.DeleteFile(productViewModel.ImageURL);
				string? newImageURL = _imageService.UploadFile(productViewModel.ImageFile);
				productViewModel.ImageURL = newImageURL;
			}

			Product product = _mapper.Map<Product>(productViewModel);
			_uow.Products.Update(product);
			await _uow.SaveChangesAsync();
		}

		public async Task<bool> DeleteProductAsync(int id)
		{
			Product? productEntity = await _uow.Products.GetByIdAsync(id, x => x.Category);
			if (productEntity is null)
				return false;

			//_imageService.DeleteFile(productEntity.ImageURL); // Don't remove the uploaded images because of soft delete update
			_uow.Products.Delete(productEntity);
			await _uow.SaveChangesAsync();
			return true;
		}

		public async Task RestoreProductAsync(int id)
		{
			Product? productEntity = await _uow.Products.GetByIdAsync(id, x => x.Category);
			if (productEntity is not null)
			{
				_uow.Products.Restore(productEntity);
				await _uow.SaveChangesAsync();
			}
		}
	}
}
