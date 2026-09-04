using myshop.Models.DTOs;
using myshop.Models.ViewModels;

namespace myshop.BLL.Interfaces
{
	public interface IOrderService
	{
		Task<OrderResponse> AddOrderAsync(DeliveryInfoDTO deliveryInfo, double totalPrice, List<CartItemResponse> cartItems, string? userId);
		Task<OrderResponse?> FindOrderAsync(int id);
		Task<List<OrderResponse>> GetOrdersAsync(string? userId);
		Task UpdateToPaidAsync(int id);
	}
}
