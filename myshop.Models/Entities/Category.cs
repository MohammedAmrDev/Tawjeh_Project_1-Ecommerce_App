using System.ComponentModel.DataAnnotations;

namespace myshop.Models.Entities
{
    public class Category : IGenericEntity, ISoftDeletableEntity
    {
		public int Id { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdatedAt { get; set; }

		public bool IsDeleted { get; set; }

		public string Name { get; set; }
        public string Description { get; set; }
    }
}
