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
		public readonly string HostAddress = "net.tcp://localhost:5000/IOutSourceContract";
		private static OcUser loggedUser;
        public static IOutsourcingContract proxy;

		public App()
		{
			proxy = new OutSClientProxy(new NetTcpBinding(), HostAddress);
			log4net.Config.XmlConfigurator.Configure();

		}

		public static OcUser LoggedUser
		{
			get
			{
				return loggedUser;
			}

			set
			{
				loggedUser = value;
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
