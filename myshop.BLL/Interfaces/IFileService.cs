using Microsoft.AspNetCore.Http;

namespace myshop.BLL.Interfaces
{
	public interface IFileService
	{
		string UploadFile(IFormFile file);
		bool DeleteFile(string imageURL);
	}
}
