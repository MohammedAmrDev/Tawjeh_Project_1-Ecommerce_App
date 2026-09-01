using myshop.Models.DTOs;

namespace myshop.BLL.Interfaces
{
	public interface IOrderService
	{
		Task<OrderResponse> AddOrderAsync(CheckoutRequest checkoutRequest, double totalPrice, List<CartItemResponse> cartItems, string? userId);
		Task<OrderResponse?> FindOrderAsync(int id);
		Task<List<OrderResponse>> GetOrdersAsync(string? userId);
		Task UpdateToPaidAsync(int id);
	}
}
