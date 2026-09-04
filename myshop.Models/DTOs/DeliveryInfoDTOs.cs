using myshop.Models.ViewModels;

namespace myshop.Models.DTOs
{
	public class DeliveryInfoDTO
	{
		public string CustomerName { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string? OrderNotes { get; set; }
	}

	public static class DeliveryInfoExtensions
	{
		public static DeliveryInfoDTO ToDTO(this DeliveryInfoViewModel deliveryInfoViewModel)
		{
			return new DeliveryInfoDTO
			{
				CustomerName = deliveryInfoViewModel.CustomerName,
				Phone = deliveryInfoViewModel.Phone,
				Email = deliveryInfoViewModel.Email,
				Address = deliveryInfoViewModel.Address,
				City = deliveryInfoViewModel.City,
				OrderNotes = deliveryInfoViewModel.OrderNotes,
			};
		}
	}
}
