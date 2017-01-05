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

            string address = "net.tcp://localhost:9999/WCFService";
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

            /*Console.WriteLine("Enter ip adress of hiring company");
            string ipAdress = Console.ReadLine();*/

            myOutSourceCompany = new Company(companyName);
            Console.WriteLine("WCFService service is started.");
            Console.WriteLine("Press <enter> to stop service...");

            baseAddress = "net.tcp://localhost:8000/Service";
            factory = new DuplexChannelFactory<IHiring2OutSourceContract>(instanceContext, new NetTcpBinding(SecurityMode.None), new EndpointAddress(baseAddress));
            IHiring2OutSourceContract proxy = factory.CreateChannel();

          //  proxy.Introduce(new Company(companyName));

            Console.ReadLine();





            host.Close();
        }

    }
}
