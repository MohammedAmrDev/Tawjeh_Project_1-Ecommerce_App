using myshop.Models.Entities;

namespace myshop.Models.DTOs
{
	public class ReviewRequest
	{
		public int? Id { get; set; }
		public Guid UserId { get; set; }
		public int ProductId { get; set; }
		public ushort Rate { get; set; }
		public string Comment { get; set; } = string.Empty;
	}

	public class ReviewResponse
	{
		public int Id { get; set; }
		public string Comment { get; set; } = string.Empty;
		public ushort Rate { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
	}

	public class ProductReviewDetailsResponse
	{
		public double AverageRating { get; set; }
		public int ReviewCount { get; set; }
		public ReviewResponse? ReviewResponse { get; set; }
	}

	public static class ReviewExtensions
	{
		public static ReviewResponse ToResponse(this Review review)
		{
			return new ReviewResponse
			{
				Id = review.Id,
				Comment = review.Comment ?? "",
				Rate = review.Rate,
				CreatedAt = review.CreatedAt,
			};
		}

		public static Review ToEntity(this ReviewRequest reviewRequest)
		{
			return new Review
			{
				UserId = reviewRequest.UserId,
				ProductId = reviewRequest.ProductId,
				Rate = reviewRequest.Rate,
				Comment = reviewRequest.Comment,
				CreatedAt = DateTimeOffset.Now,
			};
		}
	}
}
