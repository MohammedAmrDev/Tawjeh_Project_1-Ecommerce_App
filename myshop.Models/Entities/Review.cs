namespace myshop.Models.Entities
{
	public class Review : GenericEntity
	{
		public ushort Rate { get; set; }
		public string? Comment { get; set; }
		public Guid UserId { get; set; }
		public int ProductId { get; set; }
	}
}