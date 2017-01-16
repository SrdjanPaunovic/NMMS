namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using System.Threading;

    using Common;
    using Common.Entities;

    using Service.Access;

    using ServiceContract;

    public class Program
    {
        #region Fields
        private static string baseAddress;
        private static string companyName;
        private static DuplexChannelFactory<IHiring2OutSourceContract> factory;
        private static InstanceContext instanceContext = new InstanceContext(new OutSurce2HiringProxy());
        private static Company myOutSourceCompany;
        private static Thread checkProjectsThread;
        private static Thread checkTimeThread;
        private static Thread checkPasswordThread;
        private static IHiring2OutSourceContract proxy;
        private static ServiceHost host;
        #endregion

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

        public static DuplexChannelFactory<IHiring2OutSourceContract> Factory
        {
            get
            {
                return factory;
            }

            set
            {
                factory = value;
            }
        }

        public static InstanceContext InstanceContext
        {
            get
            {
                return instanceContext;
            }

            set
            {
                instanceContext = value;
            }
        }

        public static Company MyOutSourceCompany
        {
            get
            {
                return myOutSourceCompany;
            }

            set
            {
                myOutSourceCompany = value;
            }
        }
        #endregion

        private static void StartCheckingProjectsThread()
        {
            checkProjectsThread = new Thread(new ThreadStart(CheckingProjects));
            checkProjectsThread.Start();
            LogHelper.GetLogger().Info("Checking projects thread started.");

        }

        private static void StopCheckingProjectsThread()
        {

            checkProjectsThread.Abort();
            LogHelper.GetLogger().Info("Checking projects thread started.");

        }

        private static void Start()
        {

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;

            string address = "net.tcp://localhost:5000/IOutSourceContract";
            host = new ServiceHost(typeof(OutsourcingCompanyService));
            host.AddServiceEndpoint(typeof(IOutsourcingContract), binding, address);

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
            CompanyName = Console.ReadLine();

            //OcUser user1 = new OcUser("admin1", "admin1", Role.developer);
            //user1.Name = "savo1";
            //user1.Surname = "oroz1";

            //Project project = new Project();

            //  user.Project = project;
            //  user1.Project = project;

            //OutsourcingCompanyDB.Instance.AddUser(user1);
            //OutsourcingCompanyDB.Instance.AddProject(project);

            //  HirinigCompanyDB.Instance.AddCompany(company);
            //OcUser user = new OcUser("admin", "admin", Role.CEO);
            //user.Name = "savo";
            //user.Surname = "oroz";
            //OutsourcingCompanyDB.Instance.AddUser(user);

            /*Console.WriteLine("Enter ip adress of hiring company");
            string ipAdress = Console.ReadLine();*/

            //Team t1 = new Team();
            //t1.Name = "SaaS";
            //OutsourcingCompanyDB.Instance.AddTeam(t1);

            //Team t2 = new Team();
            //t2.Name = "Network Viewer";
            //t2.TeamLead = user1;
            //OutsourcingCompanyDB.Instance.AddTeam(t2);

            OcUser user = new OcUser("ceo", "123", Role.CEO);
            user.Name = "Nemanja";
            user.Surname = "Zerajic";
            OutsourcingCompanyDB.Instance.AddUser(user);

            OcUser user1 = new OcUser("po", "123", Role.PO);
            user1.Name = "Milos";
            user1.Surname = "Gagovic";
            OutsourcingCompanyDB.Instance.AddUser(user1);

            OcUser user2 = new OcUser("dev", "123", Role.developer);
            user2.Name = "Marko";
            user2.Surname = "Bogdanovic";
            OutsourcingCompanyDB.Instance.AddUser(user2);

            OcUser user3 = new OcUser("tl", "123", Role.TL);
            user3.Name = "Srdjan";
            user3.Surname = "Paunovic";
            OutsourcingCompanyDB.Instance.AddUser(user3);

            OcUser h = new OcUser("teamlead", "teamlead", Role.TL);
            h.Name = "tl1";
            h.Surname = "tl1";
            OutsourcingCompanyDB.Instance.AddUser(h);

            OcUser d1 = new OcUser("d1", "dev", Role.developer);
            d1.Name = "d1";
            d1.Surname = "d1";
            OutsourcingCompanyDB.Instance.AddUser(d1);

            OcUser d2 = new OcUser("d2", "dev", Role.developer);
            d2.Name = "d2";
            d2.Surname = "d2";
            OutsourcingCompanyDB.Instance.AddUser(d2);

            OcUser d3 = new OcUser("d3", "dev", Role.developer);
            d3.Name = "d3";
            d3.Surname = "d3";
            OutsourcingCompanyDB.Instance.AddUser(d3);

            MyOutSourceCompany = new Company(CompanyName);

            Console.WriteLine("Enter Hiring Company IP adress");
            string insertAdress = Console.ReadLine();
            Console.WriteLine("WCFService service is started.");
            Console.WriteLine("Press <enter> to stop service...");

            BaseAddress = "net.tcp://" + insertAdress + ":8000/Service";
            Factory = new DuplexChannelFactory<IHiring2OutSourceContract>(
                InstanceContext,
                new NetTcpBinding(SecurityMode.None),
                new EndpointAddress(BaseAddress));
            proxy = Factory.CreateChannel();

            OcUser scrumMaster = new OcUser()
            {
                Name = "Slobo",
                Surname = "Milosevic",
                Username = "slobo",
                Password = "slobo",
                Role = Role.SM,
                MailAddress = "gagovicmilosgagovic@gmail.com"
            };
            OcProject project = new OcProject() { Name = "NMMS", EndTime = DateTime.Now };
            OutsourcingCompanyDB.Instance.AddProject(project);
            OutsourcingCompanyDB.Instance.AddUser(scrumMaster);

            try
            {
                proxy.Introduce(new Company(CompanyName));
            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("Couldn't introduce to hiring company." + e.ToString());
            }
        }

        private static void Stop()
        {


            proxy.CloseCompany(MyOutSourceCompany);

            host.Close();

        }


        public static void CheckingProjects()
        {
            OcUser scrumMaster = OutsourcingCompanyDB.Instance.GetScrumMaster();
            DateTime currentTime = DateTime.Now;

            while (true)
            {
                List<OcProject> projects = OutsourcingCompanyDB.Instance.GetAllProjects();
                foreach (var project in projects)
                {
                    var dayDistance = (project.EndTime - currentTime).TotalDays;
                    if (dayDistance < 10)
                    {
                        List<UserStory> userStories = OutsourcingCompanyDB.Instance.GetUserStoryFromProject(project);
                        int closedStories = 0;
                        int countStories = userStories.Count;

                        foreach (var userStory in userStories)
                        {
                            if (userStory.State == StoryState.Closed)
                            {
                                closedStories++;
                            }
                        }
                        if (closedStories == 0)
                        {
                            // TODO salji mejl
                            using (MailHelper helper = new MailHelper())
                            {
                                helper.SendMail(scrumMaster.MailAddress, "Warning! The project  " + project.Name + "  may not be completed ! ");
                            }
                        }
                        else
                        {
                            float result = (countStories / 100) * closedStories;
                            if (result < 80)
                            {
                                using (MailHelper helper = new MailHelper())
                                {
                                    helper.SendMail(scrumMaster.MailAddress, "Warning! The project" + project.Name + " may not be completed !");
                                }
                            }
                        }
                    }
                    var sleepTime = DateTime.Today.AddDays(1).AddHours(1).Subtract(DateTime.Now); // jednom dnevno
                    Thread.Sleep(sleepTime); // svaki dan provjerava;
                }
            }
        }

        public static void CheckingWorkingTime()
        {

            while (true)
            {
                Thread.Sleep(600000);
                List<OcUser> result = OutsourcingCompanyDB.Instance.GetAllUsers();
                List<User> users = new List<User>();
                foreach (var user in result)
                {
                    users.Add(user as User);
                }
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
                List<OcUser> result = OutsourcingCompanyDB.Instance.GetAllUsers();
                List<User> users = new List<User>();
                foreach (var user in result)
                {
                    users.Add(user as User);
                }
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
            checkTimeThread = new Thread(new ThreadStart(CheckingWorkingTime));
            checkTimeThread.Start();
            LogHelper.GetLogger().Info("CheckingPassword thread started.");

        }
        private static void StopCheckingTimeTherad()
        {
            checkTimeThread.Abort();
            LogHelper.GetLogger().Info("CheckingPassword thread aborted.");

        }

        private static void StartCheckingPasswordThread()
        {
            checkPasswordThread = new Thread(new ThreadStart(CheckingPassword));
            checkPasswordThread.Start();
            LogHelper.GetLogger().Info("StartCheckingPasswordThread thread started.");


        }
        private static void StopCheckingPasswordThread()
        {
            checkPasswordThread.Abort();
            LogHelper.GetLogger().Info("StartCheckingPasswordThread thread aborted.");

        }

        public static void Main(string[] args)
        {
            Start();
            StartCheckingProjectsThread();
            StartCheckingTimeTherad();
            StartCheckingPasswordThread();
            Console.WriteLine("Pres any key to shut down service.");
            Console.ReadLine();
            StopCheckingProjectsThread();
            StopCheckingTimeTherad();
            StopCheckingPasswordThread();
            Stop();

        }

    }
}