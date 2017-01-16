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
using System.Threading;
using System.Net;
using System.Net.Sockets;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]


namespace HiringCompanyService
{
    public class Program
    {

        private static ServiceHost host;
        private static ServiceHost hostForOutS;
        private static string baseAddress;
        private static Company myHiringCompany = new Company("test_name");
        private static string companyName;
        private static Thread checkTimeThread;
        private static Thread checkPasswordThread;

        #region Properties
        public static string BaseAddress
        {
            get
            {
                return baseAddress;
            }

            set
            {
                baseAddress = value;
            }
        }

        public static Company MyHiringCompany
        {
            get
            {
                return myHiringCompany;
            }

            set
            {
                myHiringCompany = value;
            }
        }

        public static string CompanyName
        {
            get
            {
                return companyName;
            }

            set
            {
                companyName = value;
            }
        }

        public static Thread CheckTimeThread
        {
            get
            {
                return checkTimeThread;
            }

            set
            {
                checkTimeThread = value;
            }
        }

        public static Thread CheckPasswordThread
        {
            get
            {
                return checkPasswordThread;
            }

            set
            {
                checkPasswordThread = value;
            }
        }
#endregion Properties

        public static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            Start();

            StartCheckingTimeTherad();
            StartCheckingPasswordThread();
            Console.ReadKey(true);
            StopCheckingTimeTherad();
            StopCheckingPasswordThread();
            Stop();
        }

        private static void Start()
        {

            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("bin")) + "DB";
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AccessDB, Configuration>());

            /* User user1 = new User("admin1", "admin1", Role.developer);
                user1.Name = "savo1";
                user1.Surname = "oroz1";
            
                Project project = new Project();


              //  user.Project = project;
              //  user1.Project = project;


                HiringCompanyDB.Instance.AddUser(user1);
                HiringCompanyDB.Instance.AddProject(project);

                //  HirinigCompanyDB.Instance.AddCompany(company);
*/
            User user = new User("ceo", "123", Role.CEO);
            user.Name = "Nemanja";
            user.Surname = "Zerajic";
            HiringCompanyDB.Instance.AddUser(user);

            User user1 = new User("po", "123", Role.PO);
            user1.Name = "Milos";
            user1.Surname = "Gagovic";
            HiringCompanyDB.Instance.AddUser(user1);

            User user2 = new User("dev", "123", Role.developer);
            user2.Name = "Marko";
            user2.Surname = "Bogdanovic";
            HiringCompanyDB.Instance.AddUser(user2);

            //Company c1 = new Company("C1");
            //Company c2 = new Company();
            //Company c3 = new Company();
            //c2.Name = "C2";
            //c2.State = State.CompanyState.Requested;
            //c3.Name = "C3";
            //c3.State = State.CompanyState.Partner;

            //HiringCompanyDB.Instance.AddCompany(c1);
            //HiringCompanyDB.Instance.AddCompany(c2);
            //HiringCompanyDB.Instance.AddCompany(c3);


            host = new ServiceHost(typeof(HiringService));
            host.AddServiceEndpoint(typeof(IHiringContract),
                new NetTcpBinding(),
                new Uri("net.tcp://localhost:4000/IHiringContract"));
            host.Open();
            host.Faulted += Host_Faulted;

            Console.WriteLine("Insert compnany name");
            CompanyName = Console.ReadLine();
            MyHiringCompany = new Company(CompanyName);


            BaseAddress = "net.tcp://" + GetLocalIPAddress() + ":8000/Service";
            hostForOutS = new ServiceHost(typeof(Service.Hiring2OutSCompanyService), new Uri(BaseAddress));
            hostForOutS.AddServiceEndpoint(typeof(IHiring2OutSourceContract), new NetTcpBinding(SecurityMode.None), "");
            hostForOutS.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            hostForOutS.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            hostForOutS.Open();
            LogHelper.GetLogger().Info("Hiring Service host opened : net.tcp://localhost:4000/IHiringContract ");


            Console.WriteLine("Host opened");
            Console.WriteLine("Server is ready ! ");

        }

        private static void Host_Faulted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void Stop()
        {
            host.Close();
            LogHelper.GetLogger().Info("Hiring Service host closed.");

        }

        public static void CheckingWorkingTime()
        {

            while (true)
            {
                Thread.Sleep(600000);
                List<User> users = HiringCompanyDB.Instance.GetAllUsers();
                using (MailHelper helper = new MailHelper(users))
                {
                    helper.CheckWorkingTime();

                }

            }

        }



        public static void CheckingPassword()
        {

            while (true)
            {
                List<User> users = HiringCompanyDB.Instance.GetAllUsers();
                using (MailHelper helper = new MailHelper(users))
                {
                    helper.CheckPassword();

                }
                var sleepTime = DateTime.Today.AddDays(1).AddHours(1).Subtract(DateTime.Now); // jednom dnevno
                Thread.Sleep(sleepTime);

            }
        }


        private static void StartCheckingTimeTherad()
        {
            CheckTimeThread = new Thread(new ThreadStart(CheckingWorkingTime));
            CheckTimeThread.Start();
            LogHelper.GetLogger().Info("CheckingPassword thread started.");

        }

        private static void StopCheckingTimeTherad()
        {
            CheckTimeThread.Abort();
            LogHelper.GetLogger().Info("CheckingPassword thread aborted.");

        }

        private static void StartCheckingPasswordThread()
        {
            CheckPasswordThread = new Thread(new ThreadStart(CheckingPassword));
            CheckPasswordThread.Start();
            LogHelper.GetLogger().Info("StartCheckingPasswordThread thread started.");


        }
        private static void StopCheckingPasswordThread()
        {
            CheckPasswordThread.Abort();
            LogHelper.GetLogger().Info("StartCheckingPasswordThread thread aborted.");

        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }


    }
}
