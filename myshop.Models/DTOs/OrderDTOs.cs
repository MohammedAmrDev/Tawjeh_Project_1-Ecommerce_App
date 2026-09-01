using myshop.Models.Entities;

namespace myshop.Models.DTOs
{
	public class OrderResponse
	{
		public int Id { get; set; }
		public Guid? UserId { get; set; }
		public string CustomerName { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string? OrderNotes { get; set; }

		public double Subtotal { get; set; }
		public double Shipping { get; set; }
		public double Tax { get; set; }
		public double Total { get; set; }

		public string Status { get; set; } = "Pending";
		public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

		public List<OrderItemResponse> OrderItems { get; set; } = new();
	}

	public class OrderItemResponse
	{
		public int Id { get; set; }

		public int OrderId { get; set; }

		public int ProductId { get; set; }
		public string ProductName { get; set; } = string.Empty;
		public string? ImageURL { get; set; }

		public double Price { get; set; }
		public int Quantity { get; set; }
	}

	public static class OrderExtensions
	{ 
		public static OrderResponse ToResponse(this Order order)
		{
			return new OrderResponse
			{
				Id = order.Id,
				UserId = order.UserId,
				CustomerName = order.CustomerName,
				Phone = order.Phone,
				Address = order.Address,
				City = order.City,
				OrderNotes = order.OrderNotes,

				Subtotal = order.Subtotal,
				Shipping = order.Shipping,
				Tax = order.Tax,
				Total = order.Total,

				Status = order.Status,
				CreatedAt = order.CreatedAt,

				OrderItems = order.OrderItems.Select(ToResponse).ToList(),
			};
		}

		public static OrderItemResponse ToResponse(this OrderItem orderItem)
		{
			return new OrderItemResponse
			{
				Id = orderItem.Id,
				OrderId = orderItem.OrderId,
				ProductId = orderItem.ProductId,
				ProductName = orderItem.ProductName,
				ImageURL = orderItem.ImageURL,
				Price = orderItem.Price,
				Quantity = orderItem.Quantity,
			};
		}
	}
}
