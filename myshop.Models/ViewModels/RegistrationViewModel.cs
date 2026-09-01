using myshop.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace myshop.Models.ViewModels
{
	public class RegistrationViewModel
	{
		[Required(ErrorMessage = "First name is required")]
		[MaxLength(40)]
		public string? FirstName { get; set; }

		[Required(ErrorMessage = "Last name is required")]
		[MaxLength(40)]
		public string? LastName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string? Password { get; set; }

		[Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
		[Required(ErrorMessage = "Confirm the password")]
		[DataType(DataType.Password)]
		public string? PasswordConfirm { get; set; }

		[Required(ErrorMessage = "Error occured when defining the user role")]
		public UserTypeEnum? Role { get; set; }
	}
}
