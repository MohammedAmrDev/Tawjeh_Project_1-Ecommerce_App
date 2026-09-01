using Microsoft.EntityFrameworkCore;
using myshop.DAL.Data;
using myshop.DAL.Interfaces;
using myshop.Models.Entities;

namespace myshop.DAL.Repositories
{
	public class SoftDeleteRepository<T> : GenericRepository<T>, ISoftDeleteRepository<T> where T : class, IGenericEntity, ISoftDeletableEntity
	{
		private readonly ApplicationDbContext _context;

		public SoftDeleteRepository(ApplicationDbContext context) : base(context) =>
			_context = context;

		public void Restore(T entity)
		{
			_context.Update(entity);
			entity.IsDeleted = false;
		}
	}
}
