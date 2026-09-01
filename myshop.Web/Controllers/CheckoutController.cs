using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Interfaces;
using myshop.Models.DTOs;
using myshop.Models.Enums;
using myshop.Models.ViewModels;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace myshop.Web.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly ICartService _cartService;
		private readonly IOrderService _orderService;
		private readonly IMailService _mailService;
		private readonly IConfiguration _configuration;

		public CheckoutController(ICartService cartService, IOrderService orderService, IMailService mailService, IConfiguration configuration)
		{
			_cartService = cartService;
			_orderService = orderService;
			_mailService = mailService;
			_configuration = configuration;
		}

		public async Task<IActionResult> Index()
		{
			CheckoutViewModel checkoutViewModel = new CheckoutViewModel
			{
				CartItems = await _cartService.GetCart(),
				TotalPrice = await _cartService.GetTotalPriceAsync(),
			};

			return View(checkoutViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> ConfrimOrder(CheckoutViewModel checkoutViewModel)
		{
			var cartItems = await _cartService.GetCart();
			var totalPrice = await _cartService.GetTotalPriceAsync();

			if (!ModelState.IsValid)
			{
				checkoutViewModel.CartItems = cartItems;
				checkoutViewModel.TotalPrice = totalPrice;
				return View(nameof(Index), checkoutViewModel);
			}

			// Add order so it can be accessed from different actions
			string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var orderResponse = await _orderService.AddOrderAsync(checkoutViewModel.DeliveryInfo, totalPrice, cartItems, userId);
			_cartService.ClearCart();

			if (checkoutViewModel.PaymentMethod == PaymentTypeEnum.Strip)
			{
				//PaymentViewModel paymentViewModel = new PaymentViewModel { OrderId = orderResponse.Id };
				//return View(nameof(ProcessPayment), paymentViewModel);
				return RedirectToAction("ProcessPayment", new { id = orderResponse.Id });
			}

			await _mailService.SendOrderConfirmationEmailAsync(checkoutViewModel.DeliveryInfo.CustomerName, checkoutViewModel.DeliveryInfo.Email);
			return RedirectToAction(nameof(HomeController.Index), "Home");
		}

		[HttpGet]
		public async Task<IActionResult> ProcessPayment(int id)
		{
			OrderResponse? orderResponse = await _orderService.FindOrderAsync(id);

			if (orderResponse is null)
				return BadRequest("Error Occured");

			var origin = $"{Request.Scheme}://{Request.Host}";

			var lineItems = orderResponse.OrderItems.Select(item => new SessionLineItemOptions
			{
				PriceData = new()
				{
					UnitAmount = (long)(item.Price * 100),
					Currency = "usd",
					ProductData = new() { Name = item.ProductName },
				},
				Quantity = item.Quantity,
			}).ToList();

			StripeConfiguration.ApiKey = _configuration.GetValue<string>("Strip:SecretKey");
			var stripSessionService = new SessionService();
			var session = await stripSessionService.CreateAsync(new SessionCreateOptions
			{
				Mode = "payment",
				ClientReferenceId = orderResponse.Id.ToString(),
				SuccessUrl = Url.Action("SucessPayment", "Checkout", new { id = orderResponse.Id }, Request.Scheme) + "?session_id={CHECKOUT_SESSION_ID}",
				CancelUrl = Url.Action("Index", "Order", new {  }, Request.Scheme),
				LineItems = lineItems,
			});

			return Redirect(session.Url);
		}

		[HttpGet]
		public async Task<IActionResult> SucessPayment(int id, string session_id)
		{
			var service = new SessionService();
			Session session = service.Get(session_id);

			if (session.PaymentStatus == "paid")
			{
				await _orderService.UpdateToPaidAsync(id);
			}

			return RedirectToAction("Details", "Order", new { id });
		}
	}
}
