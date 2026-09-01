using myshop.Models.Entities;

namespace myshop.DAL.Interfaces
{
	public interface ISoftDeleteRepository<T> : IGenericRepository<T> where T : class, IGenericEntity, ISoftDeletableEntity
	{
		void Delete(T entity);
		void Restore(T entity);
	}
}
