using MailKit.Net.Smtp; // MailKit is the library used to send mail messages
using Microsoft.Extensions.Options;
using MimeKit; // Mimekit is the library used to build mail messages, and download with MailKit (because MailKit dependes on it)
using MimeKit.Text;
using myshop.Models.DTOs;
using myshop.Models.Settings;
using myshop.BLL.Interfaces;

namespace myshop.BLL.Services
{
	public class MailKitService : IMailService
	{
		private readonly MailSettings _mailSettings;

		public MailKitService(IOptions<MailSettings> mailSettings)
		{
			_mailSettings = mailSettings.Value;
		}

		public async Task SendMailAsync(string mailTo, string subject, string body)
		{
			var message = new MimeMessage();

			var from = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email);
			message.From.Add(from);

			var to = new MailboxAddress("Test", mailTo);
			message.To.Add(to);

			message.Subject = subject;
			message.Body = new TextPart(TextFormat.Html)
			{
				Text = body
			};

			using var smtp = new SmtpClient();
			await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port);
			await smtp.SendAsync(message);
			await smtp.DisconnectAsync(true);
		}

		#region Specific_Emails
		public async Task SendWelcomeEmailAsync(string userName, string mailTo)
		{
			string body = WelcomeEmailTemplate(userName);
			await SendMailAsync(mailTo, "Welcome", body);
		}

		public async Task SendOrderConfirmationEmailAsync(string customerName, string mailTo)
		{
			string body = OrderConfirmationEmailTemplate(customerName);
			await SendMailAsync(mailTo, "Order Confirmation", body);
		}
		#endregion

		#region Email_Templates
		public static string WelcomeEmailTemplate(string userName)
		{
			return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <title>Welcome</title>
            </head>
            <body style='margin:0;padding:0;background-color:#f4f4f7;font-family:Arial,sans-serif;'>
                <table width='100%' cellpadding='0' cellspacing='0' style='background-color:#f4f4f7;padding:30px 0;'>
                    <tr>
                        <td align='center'>
                            <table width='600' cellpadding='0' cellspacing='0' style='background:#ffffff;border-radius:8px;overflow:hidden;'>
                                <!-- Header -->
                                <tr>
                                    <td style='background-color:#1f2937;padding:24px;text-align:center;'>
                                        <h1 style='color:#ffffff;margin:0;font-size:22px;'>MyShop</h1>
                                    </td>
                                </tr>
                                <!-- Body -->
                                <tr>
                                    <td style='padding:32px;color:#333333;font-size:15px;line-height:1.6;'>
                                        <h2 style='margin-top:0;'>Welcome, {userName}!</h2>
                                        <p>Thanks for creating an account with MyShop. We're excited to have you on board.</p>
                                        <p>Start browsing our latest products and enjoy exclusive member deals.</p>
                                        <a href='#' style='display:inline-block;padding:12px 24px;background:#2563eb;color:#ffffff;
                                            text-decoration:none;border-radius:6px;margin-top:12px;font-weight:bold;'>
                                            Start Shopping
                                        </a>
                                    </td>
                                </tr>
                                <!-- Footer -->
                                <tr>
                                    <td style='background-color:#f4f4f7;padding:16px;text-align:center;font-size:12px;color:#888888;'>
                                        &copy; {DateTime.Now.Year} MyShop. All rights reserved.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";
		}

		public static string OrderConfirmationEmailTemplate(string userName)
		{
			return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <title>Welcome</title>
            </head>
            <body style='margin:0;padding:0;background-color:#f4f4f7;font-family:Arial,sans-serif;'>
                <table width='100%' cellpadding='0' cellspacing='0' style='background-color:#f4f4f7;padding:30px 0;'>
                    <tr>
                        <td align='center'>
                            <table width='600' cellpadding='0' cellspacing='0' style='background:#ffffff;border-radius:8px;overflow:hidden;'>
                                <!-- Header -->
                                <tr>
                                    <td style='background-color:#1f2937;padding:24px;text-align:center;'>
                                        <h1 style='color:#ffffff;margin:0;font-size:22px;'>MyShop</h1>
                                    </td>
                                </tr>
                                <!-- Body -->
                                <tr>
                                    <h2>Thanks for your order, {{customerName}}!</h2>
                                    <p>Your order has been confirmed.</p>
                                </tr>
                                <!-- Footer -->
                                <tr>
                                    <td style='background-color:#f4f4f7;padding:16px;text-align:center;font-size:12px;color:#888888;'>
                                        &copy; {DateTime.Now.Year} MyShop. All rights reserved.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";
		}
		#endregion
	}
}
