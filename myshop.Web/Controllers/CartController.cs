using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Interfaces;
using myshop.Models.Entities;
using System.Diagnostics;

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
			double totalPrice = await _cartService.GetTotalPrice();
			ViewData["totalPrice"] = totalPrice;
			return View(await _cartService.GetCart());
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CartItem cartItem)
		{
			int count = await _cartService.AddItem(cartItem);
			double totalPrice = await _cartService.GetTotalPrice();
			return Json(new { count, totalPrice });
		}

		[HttpPost]
		public async Task<IActionResult> IncreaseQuantity(int id)
		{
			int quantity = await _cartService.IncreaseQuantity(id);
			double totalPrice = await _cartService.GetTotalPrice();

			return Json(new { quantity, totalPrice });
		}

		[HttpPost]
		public async Task<IActionResult> DecreaseQuantity(int id)
		{
			int quantity = await _cartService.DecreaseQuantity(id);
			double totalPrice = await _cartService.GetTotalPrice();

			return Json(new { quantity, totalPrice });
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			bool isDelete = await _cartService.RemoveItem(id);
			double totalPrice = await _cartService.GetTotalPrice();

			return Json(new { success = isDelete, totalPrice });
		}

		[HttpDelete]
		public async Task<IActionResult> Clear()
		{
			_cartService.ClearCart();
			double totalPrice = await _cartService.GetTotalPrice();

			return Json(new { totalPrice });
		}
	}
}
