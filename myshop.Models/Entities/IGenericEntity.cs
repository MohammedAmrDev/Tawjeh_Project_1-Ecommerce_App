namespace myshop.Models.Entities
{
	public interface IGenericEntity
	{
        public int Id { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdatedAt { get; set; }
	}
}
