using myshop.Models.Entities;

namespace myshop.DAL.Interfaces
{
	public interface ICategoriesRepository : ISoftDeleteRepository<Category>
	{
		Task<List<Category>> GetAllAsync();
	}
}
