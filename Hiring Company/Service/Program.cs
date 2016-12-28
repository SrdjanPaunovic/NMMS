using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ServiceContract;
using System.Data.Entity;
using Service.Access;
using Service.Data;

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
			// set |DataDirectory| in App.config
			string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
			string path = System.IO.Path.GetDirectoryName(executable);
			path = path.Substring(0, path.LastIndexOf("bin")) + "DB";
			AppDomain.CurrentDomain.SetData("DataDirectory", path);

			// update database
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<AccessDB, Configuration>());

			User user = new User();
			user.Name = "savo";
			user.Surname = "oroz";

			HirinigCompanyDB.Instance.AddUser(user);

			User user1 = new User();
			user1.Name = "savo1";
			user1.Surname = "oroz1";
			HirinigCompanyDB.Instance.AddUser(user1);

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
