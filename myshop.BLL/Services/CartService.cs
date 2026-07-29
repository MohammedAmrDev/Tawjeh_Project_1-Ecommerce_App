using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using myshop.BLL.Interfaces;
using myshop.Models.DTOs;
using myshop.Models.Entities;
using System.Text.Json;

namespace myshop.BLL.Services
{
	public class CartService : ICartService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly string _cartSessionName;
		public CartService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
		{
			_httpContextAccessor = httpContextAccessor;
			_cartSessionName = configuration.GetValue<string>("CartSessionName") ?? "ShopHub.Cart";
		}
		public async Task<int> GetCountAsync()
		{
			var cartList = await GetCartList();
			return cartList.Sum(i => i.Quantity);
		}

		public async Task<List<CartItemResponse>> GetCart()
		{
			var cartList = await GetCartList();
			return cartList.Select(c => c.ToResponse()).ToList();
		}

		public async Task<int> AddItem(CartItem item)
		{
			var cartList = await GetCartList();
			var existing = cartList.FirstOrDefault(i => i.ProductId == item.ProductId);

			if (existing is not null)
				existing.Quantity += item.Quantity;
			else cartList.Add(item);

			SaveCartList(cartList);
			return cartList.Sum(i => i.Quantity);
		}

		public async Task<bool> RemoveItem(int id)
		{
			var cartList = await GetCartList();
			bool isDeleted = cartList.RemoveAll(i => i.ProductId == id) >= 1;
			SaveCartList(cartList);
			return isDeleted;
		}

		public async Task<int> IncreaseQuantity(int id)
		{
			var cartList = await GetCartList();
			CartItem? target = cartList.FirstOrDefault(i => i.ProductId == id);
			if (target is null) return -1;
			target.Quantity++;
			SaveCartList(cartList);
			return target.Quantity;
		}

		public async Task<int> DecreaseQuantity(int id)
		{
			var cartList = await GetCartList();
			CartItem? target = cartList.FirstOrDefault(i => i.ProductId == id);
			if (target is null) return -1;
			if (target.Quantity == 0) return 0;

			target.Quantity--;

			SaveCartList(cartList);
			return target.Quantity;
		}

		public void ClearCart()
		{
			_httpContextAccessor.HttpContext?.Session.SetString(_cartSessionName, JsonSerializer.Serialize(new List<CartItem>()));
		}

		#region Helper_Methods
		private async Task<List<CartItem>> GetCartList()
		{
			var session = _httpContextAccessor.HttpContext?.Session;
			List<CartItem> cartList = new();

			if (session is null) return cartList;

			await session.LoadAsync();

			string? cartData = session.GetString(_cartSessionName);
			if (cartData is not null)
				cartList = JsonSerializer.Deserialize<List<CartItem>>(cartData) ?? new();
			return cartList;
		}

		private void SaveCartList(List<CartItem> cartList)
		{
			var session = _httpContextAccessor.HttpContext?.Session;
			if (session is null) return;
			session.SetString(_cartSessionName, JsonSerializer.Serialize(cartList));
		}
		#endregion
	}
}
