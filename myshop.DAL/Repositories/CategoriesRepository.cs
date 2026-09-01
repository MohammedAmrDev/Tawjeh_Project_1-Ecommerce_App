using Microsoft.EntityFrameworkCore;
using myshop.DAL.Data;
using myshop.DAL.Interfaces;
using myshop.Models.Entities;

namespace myshop.DAL.Repositories
{
	internal class CategoriesRepository : SoftDeleteRepository<Category>, ICategoriesRepository
	{
		private readonly ApplicationDbContext _context;
		public CategoriesRepository(ApplicationDbContext context) : base(context)
			=> _context = context;

		public async Task<List<Category>> GetAllAsync() =>
			await _context.Categories.AsNoTracking().ToListAsync();
	}
}
