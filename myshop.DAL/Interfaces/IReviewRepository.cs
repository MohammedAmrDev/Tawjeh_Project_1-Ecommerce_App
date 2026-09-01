using myshop.Models.Entities;

namespace myshop.DAL.Interfaces
{
	public interface IReviewRepository : IGenericRepository<Review>
	{
		Task<(int ReviewCount, double AverageRating)> GetReviewDetailsQuery(int id);
		Task<Review?> GetCurrentUserReview(Guid userId, int productId);
	}
}
