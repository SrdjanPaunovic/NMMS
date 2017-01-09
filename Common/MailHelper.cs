using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Common.Entities;

namespace Common
{
	public class MailHelper:IDisposable
	{
		private Thread check;
		private Object locker;

		Dictionary<User, bool> usersWorkingTimeCheck = new Dictionary<User, bool>();
		Dictionary<User, bool> usersPasswordCheck = new Dictionary<User, bool>();

		DateTime currentTime;

		public MailHelper(List<User> allUsers)
		{
			foreach (var user in allUsers)
			{
				usersWorkingTimeCheck.Add(user, false);
				usersPasswordCheck.Add(user, false);
			}

		}
				
		private  void SendMail(string address, string messageContent)
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

		public  void CheckWorkingTime()
		{
			currentTime = DateTime.Now;
			foreach (var user in usersWorkingTimeCheck.Keys)
			{
				if (user.StartTime < currentTime && user.IsAuthenticated == false && usersWorkingTimeCheck[user] == false)
				{
					string message = " Kasnjenje ";// TODO prosiriti malo
					SendMail(user.MailAddress, message); 
					usersWorkingTimeCheck[user] = true;
				}				
			}
		}

		public void CheckPassword()
		{
			currentTime = DateTime.Now;

			foreach (var user in usersPasswordCheck.Keys)
			{

				var distance = (currentTime - user.Password_changed).TotalDays;
				string message="Password last time chanded "+user.Password_changed.ToString();
				if (distance > 180 && usersPasswordCheck[user] == false)
				{
					SendMail(user.MailAddress, message);
					usersPasswordCheck[user] = true;
				}
			}
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
