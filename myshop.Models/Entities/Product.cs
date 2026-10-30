using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace myshop.Models.Entities
{
	public class Product : GenericEntity, ISoftDeletableEntity
    {
		public bool IsDeleted { get; set; }

		public string Name { get; set; }
        public string Description { get; set; }
		[ValidateNever]
		public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
    }
}
