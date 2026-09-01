using System.ComponentModel.DataAnnotations;

namespace myshop.Models.ViewModels
{
	public class PaymentViewModel
	{
		public int? OrderId { get; set; }

		[Required(ErrorMessage = "Holder name is required")]
		[Display(Name = "Holder Name")]
		public string HolderName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Card number is required")]
		[StringLength(16, ErrorMessage = "Card number length is 16")]
		[Display(Name = "Card Number")]
		public string CardNumber { get; set; } = string.Empty;

		[Required(ErrorMessage = "Expiry date is required")]
		public string Expiry { get; set; } = string.Empty;

		[Required(ErrorMessage = "Cvv is required")]
		public string Cvv { get; set; } = string.Empty;
	}
}
