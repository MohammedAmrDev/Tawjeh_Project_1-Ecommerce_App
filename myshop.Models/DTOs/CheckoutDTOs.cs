using System.ComponentModel.DataAnnotations;

namespace myshop.Models.DTOs
{
	public class CheckoutRequest
	{
		[Required(ErrorMessage = "Customer name is required.")]
		[StringLength(50)]
		[Display(Name = "Full Name")]
		public string CustomerName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Phone number is required.")]
		[Phone(ErrorMessage = "Please enter a valid phone number.")]
		public string Phone { get; set; } = string.Empty;

		[Required(ErrorMessage = "Email number is required.")]
		[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Address is required.")]
		[StringLength(100)]
		public string Address { get; set; } = string.Empty;

		[Required(ErrorMessage = "City is required.")]
		[StringLength(100)]
		public string City { get; set; } = string.Empty;

		[StringLength(100)]
		[Display(Name = "Order Notes (Optional)")]
		public string? OrderNotes { get; set; }
	}
}
