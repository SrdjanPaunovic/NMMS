using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ServiceContract;
namespace Client
{
	public class OutSClientProxy : ChannelFactory<IOutsourcingCompanyService>, IOutsourcingCompanyService
	{
		IOutsourcingCompanyService factory;
		public OutSClientProxy(NetTcpBinding binding, string address)
			: base(binding, address)
		{
			factory = this.CreateChannel();
		}

		public OutSClientProxy(NetTcpBinding binding, EndpointAddress address)
			: base(binding, address)
		{
			factory = this.CreateChannel();
		}

		
	}
}

