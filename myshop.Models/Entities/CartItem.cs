namespace myshop.Models.Entities
{
	public class CartItem
	{
		public int ProductId {  get; set; }
		public string ProductName {  get; set; }
		public double Price {  get; set; }
		public int Quantity { get; set; } = 1;
		public string ImageURL {  get; set; }
	}
}
