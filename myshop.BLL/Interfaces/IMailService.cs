using myshop.Models.DTOs;

namespace myshop.BLL.Interfaces
{
	public interface IMailService
	{
		Task SendMailAsync(string mailTo, string subject, string body);
		Task SendWelcomeEmailAsync(string userName, string mailTo);
		Task SendOrderConfirmationEmailAsync(string customerName, string mailTo);
	}
}
