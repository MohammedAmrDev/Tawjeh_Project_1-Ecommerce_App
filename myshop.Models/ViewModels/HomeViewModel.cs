using myshop.Models.DTOs;

namespace myshop.Models.ViewModels
{
	public class HomeViewModel
	{
		public List<CategoryResponse> Categories { get; set; }
		public List<ProductResponse> Products { get; set; }
		public int CurretPageIndex { get; set; }
		public int PagesNumber { get; set; }
		public List<int?> PageIndexsList { get; set; } = new();
	}
}