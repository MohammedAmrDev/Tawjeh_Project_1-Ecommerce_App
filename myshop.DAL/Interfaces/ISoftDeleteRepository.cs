using myshop.Models.Entities;

namespace myshop.DAL.Interfaces
{
	public interface ISoftDeleteRepository<T> : IGenericRepository<T> where T : GenericEntity, ISoftDeletableEntity
	{
		void Delete(T entity);
		void Restore(T entity);
	}
}
