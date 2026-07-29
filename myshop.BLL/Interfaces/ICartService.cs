using myshop.Models.DTOs;
using myshop.Models.Entities;

namespace myshop.BLL.Interfaces
{
	public interface ICartService
	{
		Task<int> GetCountAsync();
		Task<List<CartItemResponse>> GetCart();
		Task<int> AddItem(CartItem item);
		Task<bool> RemoveItem(int id);
		Task<int> IncreaseQuantity(int id);
		Task<int> DecreaseQuantity(int id);
		void ClearCart();
		Task<double> GetTotalPrice();
	}
}
