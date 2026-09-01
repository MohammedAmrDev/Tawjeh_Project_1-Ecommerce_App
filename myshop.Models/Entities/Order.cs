namespace myshop.Models.Entities
{
	public class Order : IGenericEntity
	{
		public int Id { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdatedAt { get; set; }

		public Guid? UserId { get; set; }
		public string CustomerName { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string? OrderNotes { get; set; }

		public double Subtotal { get; set; }
		public double Shipping { get; set; }
		public double Tax { get; set; }
		public double Total { get; set; }

		public string Status { get; set; } = "Pending";

		public List<OrderItem> OrderItems { get; set; } = new();
	}
}
