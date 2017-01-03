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
        public readonly string HostAddress = "net.tcp://localhost:4000/IHiringContract";
        private HiringClientProxy proxy;

        public App()
        {
            proxy = new HiringClientProxy(new NetTcpBinding(), HostAddress);
        }

        public HiringClientProxy Proxy
        {
            get
            {
                if (proxy.State != CommunicationState.Opened)
                {
                    proxy= new HiringClientProxy(new NetTcpBinding(), HostAddress);
                }
                return proxy;
            }
        }
    }
}
