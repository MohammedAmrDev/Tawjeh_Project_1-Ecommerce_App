namespace myshop.Models.Entities
{
	public interface ISoftDeletableEntity
	{
		public bool IsDeleted { get; set; }
	}
}
