using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ServiceContract;

namespace HiringCompanyService
{
	class Program
	{

		private static ServiceHost host;

		static void Main(string[] args)
		{
			Start();
			Console.ReadKey(true);
			Stop();

		}

		private static void Start()
		{
			host = new ServiceHost(typeof(HiringCompanyService));
			host.AddServiceEndpoint(typeof(IHiringContract),
				new NetTcpBinding(),
				new Uri("net.tcp://localhost:4000/IHiringContract"));
			host.Open();
			Console.WriteLine("Server is ready ! ");

		}

		private static void Stop()
		{

			host.Close();
		}
	}
}
