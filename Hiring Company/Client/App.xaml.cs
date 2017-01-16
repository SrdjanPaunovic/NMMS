using Common.Entities;
using ServiceContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly static string HostAddress = "net.tcp://localhost:4000/IHiringContract";
        private CompanyType companyType = CompanyType.HIRING;
        private static IHiringContract proxy;
        private static User loggedUser;


        public static User LoggedUser
        {
            get { return loggedUser; }
            set { loggedUser = value; }
        }

        public static IHiringContract Proxy
        {
            get
            {
                return proxy;
            }

            set
            {
                proxy = value;
            }
        }

        public CompanyType CompanyType
        {
            get
            {
                return companyType;
            }

            set
            {
                companyType = value;
            }
        }
        static App()
        {
            Proxy = new HiringClientProxy(new NetTcpBinding(), HostAddress);
        }

        public App()
        {

            log4net.Config.XmlConfigurator.Configure();
            Exit += App_Exit;
        }
        public void App_Exit(object sender, ExitEventArgs e)
        {

            //Proxy.Close();

        }

        public App(string test) { }

        /* public static IHiringContract Proxy
         {
             get
             {
                 //if (Proxy.State != CommunicationState.Opened)
                 //{
                 //    Proxy= new HiringClientProxy(new NetTcpBinding(), HostAddress);
                 //}
                 return Proxy;
             }
             set
             {
                 Proxy = value;
             }
         }*/
    }
}
