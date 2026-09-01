using myshop.Models.DTOs;

namespace myshop.BLL.Interfaces
{
	public interface IReviewService
	{
		Task AddReviewAsync(ReviewRequest reviewRequest);
		Task EditReview(ReviewRequest reviewRequest);
		Task DeleteReview(int id);
		Task<ReviewResponse?> GetCurrentUserReview(string? userId, int productId);
		Task<ProductReviewDetailsResponse> GetProductReviewDetails(int id);
	}
}
