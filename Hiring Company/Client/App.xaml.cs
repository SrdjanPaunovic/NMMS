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
        public readonly string HostAddress = "net.tcp://localhost:4000/IHiringContract";
        public static IHiringContract Proxy;

        public  App()
        {
            Proxy = new HiringClientProxy(new NetTcpBinding(), HostAddress);

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
                //if (proxy.State != CommunicationState.Opened)
                //{
                //    proxy= new HiringClientProxy(new NetTcpBinding(), HostAddress);
                //}
                return proxy;
            }
            set
            {
                proxy = value;
            }
        }*/
    }
}
