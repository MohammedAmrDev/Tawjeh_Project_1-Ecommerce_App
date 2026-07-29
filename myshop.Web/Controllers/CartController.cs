using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Interfaces;
using myshop.Models.Entities;

namespace myshop.Web.Controllers
{
	[AllowAnonymous]
	public class CartController : Controller
	{
		private readonly ICartService _cartService;

		public CartController(ICartService cartService)
		{
			_cartService = cartService;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _cartService.GetCart());
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CartItem cartItem)
		{
			int count = await _cartService.AddItem(cartItem);
			return Json(new { count });
		}

		[HttpPost]
		public async Task<IActionResult> IncreaseQuantity(int id)
		{
			int quantity = await _cartService.IncreaseQuantity(id);
			return Json(new { quantity });
		}

		[HttpPost]
		public async Task<IActionResult> DecreaseQuantity(int id)
		{
			int quantity = await _cartService.DecreaseQuantity(id);
			return Json(new { quantity });
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			bool isDelete = await _cartService.RemoveItem(id);
			return Json(new { success = isDelete });
		}

		[HttpDelete]
		public IActionResult Clear()
		{
			_cartService.ClearCart();
			return Json(new {});
		}
	}
}
