using myshop.BLL.Interfaces;
using myshop.DAL.Interfaces;
using myshop.Models.DTOs;
using myshop.Models.Entities;

namespace myshop.BLL.Services
{
	public class ReviewService : IReviewService
	{
		private readonly IUnitOfWork _uow;

		public ReviewService(IUnitOfWork uow)
		{
			_uow = uow;
		}

		#region Getting_Data
		public async Task<ProductReviewDetailsResponse> GetProductReviewDetails(int id)
		{
			var (ReviewCount, AverageRating) = await _uow.Reviews.GetReviewDetailsQuery(id);

			return new ProductReviewDetailsResponse
			{
				ReviewCount = ReviewCount,
				AverageRating = AverageRating
			};
		}

		public async Task<ReviewResponse?> GetCurrentUserReview(string? userId, int productId)
		{
			if (userId is null) return null;
			var review = await _uow.Reviews.GetCurrentUserReview(Guid.Parse(userId), productId);
			if (review is null) return null;
			return review.ToResponse();
		}
		#endregion

		#region Add_Update_Delete
		public async Task AddReviewAsync(ReviewRequest reviewRequest)
		{
			var review = reviewRequest.ToEntity();
			await _uow.Reviews.CreateAsync(review);
			await _uow.SaveChangesAsync();
		}

		public async Task EditReview(ReviewRequest reviewRequest)
		{
			var review = reviewRequest.ToEntity();
			if (reviewRequest.Id is not null)
			{
				review.Id = reviewRequest.Id.Value;
				_uow.Reviews.Update(review);
				await _uow.SaveChangesAsync();
			}
		}

		public async Task DeleteReview(int id)
		{
			var reviewEntity = new Review { Id = id };
			_uow.Reviews.Delete(reviewEntity);
			await _uow.SaveChangesAsync();
		}
		#endregion
	}
}
