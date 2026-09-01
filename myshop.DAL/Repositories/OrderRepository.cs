using Microsoft.EntityFrameworkCore;
using myshop.DAL.Data;
using myshop.DAL.Interfaces;
using myshop.Models.Entities;

namespace myshop.DAL.Repositories
{
	public class OrderRepository : GenericRepository<Order>, IOrderRepository
	{
		private readonly ApplicationDbContext _context;

		public OrderRepository(ApplicationDbContext context) :base(context) =>
			_context = context;

		public async Task<int> Add(Order order)
		{
			order.CreatedAt = DateTimeOffset.Now;
			var addedOrder = await _context.Orders.AddAsync(order);
			return addedOrder.Entity.Id;
		}

		public async Task<List<Order>> GetAllAsync(Guid userId) =>
			await _context.Orders.AsNoTracking().Where(o => o.UserId == userId).Include(o => o.OrderItems).ToListAsync();
	}
}
