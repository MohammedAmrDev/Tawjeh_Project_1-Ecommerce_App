namespace myshop.DAL.Interfaces
{
	public interface IUnitOfWork
	{
		public ICategoriesRepository Categories { get; }
		public IProductsRepository Products { get; }
		public IOrderRepository Orders { get; }
		public IReviewRepository Reviews { get; }
		public Task<int> SaveChangesAsync();
	}
}
