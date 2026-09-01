using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Interfaces;
using System.Security.Claims;

namespace myshop.Web.Controllers
{
	[Authorize]
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public async Task<IActionResult> Index()
		{
			var ordersResponses = await _orderService.GetOrdersAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
			return View(ordersResponses);
		}

		public async Task<IActionResult> Details(int id)
		{
			var orderResponse = await _orderService.FindOrderAsync(id);
			return View(orderResponse);
		}
	}
}
