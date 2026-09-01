using Microsoft.EntityFrameworkCore;
using myshop.DAL.Data;
using myshop.DAL.Interfaces;
using myshop.Models.Entities;

namespace myshop.DAL.Repositories
{
	public class ReviewRepository : GenericRepository<Review>, IReviewRepository
	{
		private readonly ApplicationDbContext _context;

		public ReviewRepository(ApplicationDbContext context) : base(context) =>
			_context = context;

		public async Task<(int ReviewCount, double AverageRating)> GetReviewDetailsQuery(int productId)
		{
			int reviewCount = await _context.Reviews.CountAsync(r => r.ProductId == productId);
			if (reviewCount == 0) return (0, 0);

			int ratingSum = await _context.Reviews.Where(r => r.ProductId == productId).SumAsync(r => r.Rate);
			double averageRating = ratingSum / reviewCount;
			return (reviewCount, averageRating);
		}

		public async Task<Review?> GetCurrentUserReview(Guid userId, int productId) =>
			await _context.Reviews.FirstOrDefaultAsync(r => r.UserId == userId && r.ProductId == productId);
	}
}
