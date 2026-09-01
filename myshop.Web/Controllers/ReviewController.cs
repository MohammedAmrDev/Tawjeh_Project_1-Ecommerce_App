using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Interfaces;
using myshop.Models.DTOs;
using System.Security.Claims;

namespace myshop.Web.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/reviews")]
	public class ReviewController : ControllerBase
	{
		private readonly IReviewService _reviewService;

		public ReviewController(IReviewService reviewService) =>
			_reviewService = reviewService;

		[HttpPost]
		public async Task<IActionResult> AddReview(ReviewRequest reviewRequest)
		{
			string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userId is null)
				return Unauthorized();

			reviewRequest.UserId = Guid.Parse(userId);
			await _reviewService.AddReviewAsync(reviewRequest);
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateReview(int id, ReviewRequest reviewRequest)
		{
			string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userId is null)
				return Unauthorized();
			reviewRequest.Id = id;
			reviewRequest.UserId = Guid.Parse(userId);
			await _reviewService.EditReview(reviewRequest);
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteReview(int id)
		{
			await _reviewService.DeleteReview(id);
			return Ok();
		}
	}
}
