using myshop.Models.Entities;

namespace myshop.Models.DTOs
{
	public class CartItem
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; } = 1;
		public string ImageURL { get; set; }
	}

	public class CartItemResponse
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public string ImageURL { get; set; }
	}

	public static class CartItemExtensions {
		public static CartItemResponse ToResponse(this CartItem cartItem)
		{
			return new CartItemResponse
			{
				ProductId = cartItem.ProductId,
				ProductName = cartItem.ProductName,
				Price = cartItem.Price,
				Quantity = cartItem.Quantity,
				ImageURL = cartItem.ImageURL,
			};
		}
	}
}
