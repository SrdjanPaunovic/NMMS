using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSendHelper
{
	class MailHelper
	{

		public static void SendMail(string address, string messageContent)
		{
			using (SmtpClient smtpClient = new SmtpClient())
			{
				using (MailMessage message = new MailMessage())
				{
					message.Subject = "Test";
					message.Body = messageContent;
					message.To.Add(new MailAddress(address));
					message.IsBodyHtml = false;
					try
					{
						smtpClient.Send(message);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
					}

				}
			}
			Console.WriteLine("Mail sent");
		}
	}
}
