using myshop.BLL.Interfaces;
using myshop.DAL.Interfaces;
using myshop.Models.DTOs;
using myshop.Models.Entities;

namespace myshop.BLL.Services
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _uow;

		public OrderService(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public async Task<OrderResponse> AddOrderAsync(DeliveryInfoDTO deliveryInfo, double totalPrice, List<CartItemResponse> cartItems, string? userId)
		{
			Order orderEntity = new Order
			{
				UserId = userId is not null ? Guid.Parse(userId) : null,
				CustomerName = deliveryInfo.CustomerName,
				Phone = deliveryInfo.Phone,
				Address = deliveryInfo.Address,
				City = deliveryInfo.City,
				OrderNotes = deliveryInfo.OrderNotes,

				Subtotal = totalPrice,
				Shipping = 0,
				Tax = 0,
				Total = totalPrice,

				OrderItems = cartItems.Select(c => new OrderItem
				{
					ProductId = c.ProductId,
					ProductName = c.ProductName,
					ImageURL = c.ImageURL,
					Price = c.Price,
					Quantity = c.Quantity,
				}).ToList(),
			};



			int generatedId = await _uow.Orders.Add(orderEntity);
			await _uow.SaveChangesAsync();
			return orderEntity.ToResponse();
		}

		public async Task<OrderResponse?> FindOrderAsync(int id)
		{
			var order = await _uow.Orders.GetByIdAsync(id, o => o.OrderItems);
			return order?.ToResponse();
		}

		public async Task<List<OrderResponse>> GetOrdersAsync(string? userId)
		{
			if (userId is null)
				return new List<OrderResponse>();
			var orders = await _uow.Orders.GetAllAsync(Guid.Parse(userId));
			return orders.Select(o => o.ToResponse()).ToList();
		}

		public async Task UpdateToPaidAsync(int id)
		{

			Order? order = await _uow.Orders.GetByIdAsync(id, o => o.OrderItems);

			if (order is not null)
			{
				order.Status = "Paid";
				_uow.Orders.Update(order);
				await _uow.SaveChangesAsync();
			}
		}
	}
}
