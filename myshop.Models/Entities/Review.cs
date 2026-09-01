namespace myshop.Models.Entities
{
	public class Review : IGenericEntity
	{
		public int Id { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdatedAt { get; set; }

		public ushort Rate { get; set; }
		public string? Comment { get; set; }
		public Guid UserId { get; set; }
		public int ProductId { get; set; }
	}
}
