using Microsoft.Extensions.DependencyInjection;
using myshop.BLL.Interfaces;
using myshop.BLL.Services;

namespace myshop.BLL
{
	public static class ConfiguringBusinessLogic
	{
		public static IServiceCollection ConfiguringBusinessLogicLayer(this IServiceCollection services)
		{
			services.AddScoped<IProductsService, ProductsService>();
			services.AddScoped<ICategoriesService, CategoriesService>();
			services.AddScoped<IFileService, LocalFileService>();
			services.AddScoped<IUserManagementService, UserManagementService>();
			services.AddScoped<ICartService, CartService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddTransient<IMailService, MailKitService>();
			services.AddTransient<IReviewService, ReviewService>();
			return services;
		}
	}
}
