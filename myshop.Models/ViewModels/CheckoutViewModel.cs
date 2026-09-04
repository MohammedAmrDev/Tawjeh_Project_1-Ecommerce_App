using myshop.Models.DTOs;
using myshop.Models.Enums;

namespace myshop.Models.ViewModels
{
	public class CheckoutViewModel
	{
		public List<CartItemResponse> CartItems { get; set; } = new();
		public double TotalPrice { get; set; }
		public DeliveryInfoViewModel DeliveryInfo { get; set; } = new();
		public PaymentTypeEnum PaymentMethod { get; set; } = PaymentTypeEnum.COD;
	}
}
