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
using Common;
using Common.Entities;

namespace Service
{
    class Program
    {
        public static InstanceContext instanceContext = new InstanceContext(new OutSurce2HiringProxy());
        public static DuplexChannelFactory<IHiring2OutSourceContract> factory;
        public static Company myOutSourceCompany;
        public static string companyName;
        public static string baseAddress;

        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;

            string address = "net.tcp://localhost:5000/IOutSourceContract";
            ServiceHost host = new ServiceHost(typeof(OutsourcingCompanyService));
            host.AddServiceEndpoint(typeof(IOutsourcingCompanyService), binding, address);

            host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

            host.Open();



            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("bin")) + "DB";
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            // update database
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AccessDB, Configuration>());

            Console.WriteLine("Enter company name:");
            companyName = Console.ReadLine();

            #region test
            OcUser user1 = new OcUser("admin1", "admin1", Role.developer);
            user1.Name = "savo1";
            user1.Surname = "oroz1";

            Project project = new Project();


            //  user.Project = project;
            //  user1.Project = project;


            OutsourcingCompanyDB.Instance.AddUser(user1);
            //OutsourcingCompanyDB.Instance.AddProject(project);

            //  HirinigCompanyDB.Instance.AddCompany(company);
            OcUser user = new OcUser("admin", "admin", Role.CEO);
            user.Name = "savo";
            user.Surname = "oroz";
            OutsourcingCompanyDB.Instance.AddUser(user);

            /*Console.WriteLine("Enter ip adress of hiring company");
            string ipAdress = Console.ReadLine();*/

            Team t1 = new Team();
            t1.Name = "SaaS";
            //OutsourcingCompanyDB.Instance.AddTeam(t1);

            Team t2 = new Team();
            t2.Name = "Network Viewer";
            t2.TeamLead = user1;
            OutsourcingCompanyDB.Instance.AddTeam(t2);

            #endregion

            myOutSourceCompany = new Company(companyName);
            Console.WriteLine("WCFService service is started.");
            Console.WriteLine("Press <enter> to stop service...");

            baseAddress = "net.tcp://localhost:8000/Service";
            factory = new DuplexChannelFactory<IHiring2OutSourceContract>(instanceContext, new NetTcpBinding(SecurityMode.None), new EndpointAddress(baseAddress));
            IHiring2OutSourceContract proxy = factory.CreateChannel();

            try
            {
                proxy.Introduce(new Company(companyName));
            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("Couldn't introduce to hiring company.");
            }

            Console.ReadLine();
            proxy.CloseCompany(myOutSourceCompany);

			host.Close();
        }

    }
}
