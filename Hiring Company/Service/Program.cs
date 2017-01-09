using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ServiceContract;
using System.Data.Entity;
using Service.Access;
using Common.Entities;
using Common;
using System.ServiceModel.Description;

namespace HiringCompanyService
{
	class Program
	{

		private static ServiceHost host;
		private static ServiceHost hostForOutS;
		public static string baseAddress;
		public static Company myHiringCompany;
		public static string companyName;

		static void Main(string[] args)
		{
			Start();
			Console.ReadKey(true);
			Stop();
		}

		private static void Start()
		{
			string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
			string path = System.IO.Path.GetDirectoryName(executable);
			path = path.Substring(0, path.LastIndexOf("bin")) + "DB";
			AppDomain.CurrentDomain.SetData("DataDirectory", path);
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<AccessDB, Configuration>());

			#region Test
			/*
                

                User user1 = new User("admin1", "admin1", Role.developer);
                user1.Name = "savo1";
                user1.Surname = "oroz1";
            
                Project project = new Project();


              //  user.Project = project;
              //  user1.Project = project;
			

                HiringCompanyDB.Instance.AddUser(user1);
                HiringCompanyDB.Instance.AddProject(project);
			
                //  HirinigCompanyDB.Instance.AddCompany(company);
			*/
			#endregion
			User user = new User("admin", "admin", Role.CEO);
			user.Name = "savo";
			user.Surname = "oroz";
			HiringCompanyDB.Instance.AddUser(user);

			Company c1 = new Company("C1");
			Company c2 = new Company();
			Company c3 = new Company();
			c2.Name = "C2";
			c2.State = State.CompanyState.Requested;
			c3.Name = "C3";
			c3.State = State.CompanyState.Partner;
			HiringCompanyDB.Instance.AddCompany(c1);
			HiringCompanyDB.Instance.AddCompany(c2);
			HiringCompanyDB.Instance.AddCompany(c3);


			host = new ServiceHost(typeof(HiringCompanyService));
			host.AddServiceEndpoint(typeof(IHiringContract),
				new NetTcpBinding(),
				new Uri("net.tcp://localhost:4000/IHiringContract"));
			host.Open();

			Console.WriteLine("Insert compnany name");
			companyName = Console.ReadLine();
			myHiringCompany = new Company(companyName);


			baseAddress = "net.tcp://localhost:8000/Service";
			hostForOutS = new ServiceHost(typeof(Service.Hiring2OutSCompanyService), new Uri(baseAddress));
			hostForOutS.AddServiceEndpoint(typeof(IHiring2OutSourceContract), new NetTcpBinding(SecurityMode.None), "");
			hostForOutS.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
			hostForOutS.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
			hostForOutS.Open();

			Console.WriteLine("Host opened");
			Console.WriteLine("Server is ready ! ");

		}

		private static void Stop()
		{

			host.Close();
		}
	}
}
