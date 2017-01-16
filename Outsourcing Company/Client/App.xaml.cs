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



namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly static string HostAddress = "net.tcp://localhost:5000/IOutSourceContract";

        private static OcUser loggedUser;
        private static IOutsourcingContract proxy;

        public static IOutsourcingContract Proxy
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


        private CompanyType companyType = CompanyType.OUTSOURCING;

        static App()
        {
            if (proxy == null)
            {
                proxy = new OutSClientProxy(new NetTcpBinding(), HostAddress);
            }
        }

        public App()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static OcUser LoggedUser
        {
            get { return loggedUser; }
            set { loggedUser = value; }
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
        /*
       public OutSClientProxy Proxy
       {
           get
           {
               if (proxy.State != CommunicationState.Opened)
               {
                   proxy = new OutSClientProxy(new NetTcpBinding(), HostAddress);
               }
               return proxy;
           }
       }  */
    }
}
