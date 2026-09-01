using myshop.Models.Entities;

namespace myshop.DAL.Interfaces
{
	public interface IOrderRepository : IGenericRepository<Order>
	{
		Task<int> Add(Order order);
		Task<List<Order>> GetAllAsync(Guid userId);
	}
}
