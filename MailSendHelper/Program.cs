using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSendHelper
{
	class Program
	{


		static void Main(string[] args)
		{
			check = new Thread(new ThreadStart(CheckingUsers));

		}

		public static void CheckingUsers()
		{

			Object locker = new Object();
			while (true)
			{
				lock (locker)
				{



				}
				Thread.Sleep(1000 * 60 * 10 * 3);
			}

		}
	}
}
