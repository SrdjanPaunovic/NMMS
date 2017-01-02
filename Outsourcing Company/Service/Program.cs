using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Data.Entity;
using Service.Access;

namespace Service
{
	class Program
	{
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();
			binding.Security.Mode = SecurityMode.Transport;
			binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;

			string address = "net.tcp://localhost:9999/WCFService";
			ServiceHost host = new ServiceHost(typeof(OutsourcingCompanyService));
			host.AddServiceEndpoint(typeof(IOutsourcingCompanyService), binding, address);

			host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
			host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

			host.Open();

			Console.WriteLine("WCFService service is started.");
			Console.WriteLine("Press <enter> to stop service...");

			string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
			string path = System.IO.Path.GetDirectoryName(executable);
			path = path.Substring(0, path.LastIndexOf("bin")) + "DB";
			AppDomain.CurrentDomain.SetData("DataDirectory", path);

			// update database
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<AccessDB, Configuration>());

			// test database context
			UsersDB.Instance.AddAction(new OSUserAction
			{
			  Username = "admin",
			  Password = "admin123"
			});

			UsersDB.Instance.AddAction(new OSUserAction
			{
				Username = "user1",
				Password = ""
			});

			Console.ReadLine();
			host.Close();
		}
	}
}
