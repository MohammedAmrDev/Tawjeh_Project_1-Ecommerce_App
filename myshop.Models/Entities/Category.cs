namespace myshop.Models.Entities
{
    public class Category : GenericEntity, ISoftDeletableEntity
    {
		public bool IsDeleted { get; set; }

		public string Name { get; set; }
        public string Description { get; set; }
    }
}
