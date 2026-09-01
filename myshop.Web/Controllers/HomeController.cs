using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Interfaces;
using myshop.Models.DTOs;
using myshop.Models.ViewModels;
using System.Security.Claims;

namespace myshop.Web.Controllers
{
    [AllowAnonymous]
	public class HomeController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IProductsService _productsService;
        private readonly ICartService _cartService;
        private readonly IReviewService _reviewService;


		public HomeController(ICategoriesService categoriesService, IProductsService productsService, ICartService cartService, IReviewService reviewService)
        {
			_categoriesService = categoriesService;
			_productsService = productsService;
			_cartService = cartService;
            _reviewService = reviewService;
		}

		public async Task<IActionResult> Index(string? searchBy, int? categoryId, string? sortBy, bool isDesc, int pageIndex = 0, int length = 5)
        {
            List<CategoryResponse> categories = await _categoriesService.GetCategoriesAsync();
            var (Data, countBeforePagination) = await _productsService.GetAllProducts(searchBy, categoryId, sortBy, isDesc, pageIndex, length);
            int totalPages = (int)Math.Ceiling(countBeforePagination / (double)length);

			HomeViewModel homeViewModel = new HomeViewModel
            {
				Categories = categories,
				Products = Data,
                CurretPageIndex = pageIndex,
                PagesNumber = totalPages,
				PageIndexsList = GetPageIndexsList(totalPages, pageIndex + 1)
			};

            // For The _Layout Page
            var cartItemsCount = await _cartService.GetCountAsync();
			ViewData["CartItemsNum"] = cartItemsCount == 0 ? "" : cartItemsCount;


			return View(homeViewModel);
        }

		public async Task<IActionResult> ProductDetails(int id)
        {
            var productResponse = await _productsService.GetProductByIdAsync(id);
            var productReviewDetails = await _reviewService.GetProductReviewDetails(id);

			ViewBag.AverageRating = productReviewDetails.AverageRating;
            ViewBag.ReviewCount = productReviewDetails.ReviewCount;

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reviewResponse = await _reviewService.GetCurrentUserReview(userId, id);
			ViewBag.CurrentUserReview = reviewResponse;

			return View(productResponse);
        }

        private List<int?> GetPageIndexsList(int totalPages, int current)
        {
            var indexsList = new List<int?>();

            if (totalPages <= 7)
            {
                for (int i = 1; i <= totalPages; i++) indexsList.Add(i);
                return indexsList;
            }

            indexsList.Add(1);

            if (current >= 4) indexsList.Add(null);
			int start = Math.Max(2, current- 1);
            int end = Math.Min(current + 1, totalPages - 1);
            for (int i = start; i <= end; i++) indexsList.Add(i);

			if (current <= totalPages - 3) indexsList.Add(null);

            indexsList.Add(totalPages);

            return indexsList;
		}
	}
}