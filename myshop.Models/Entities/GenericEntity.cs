namespace myshop.Models.Entities
{
	public abstract class GenericEntity
	{
        public int Id { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdatedAt { get; set; }
	}
}
