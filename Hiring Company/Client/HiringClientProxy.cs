using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;
using System.ServiceModel;


namespace Client
{
	public class HiringClientProxy:ChannelFactory<IHiringContract>,IHiringContract
	{
		IHiringContract factory;
		public HiringClientProxy(NetTcpBinding binding, string address)
			:base(binding,address)
		{
			factory = this.CreateChannel();
		}



		public bool LogIn(string username, string password)
		{
			bool result = false;

			try
			{
				result = factory.LogIn(username, password);
			}
			catch (Exception e)
			{
				//TODO log
			}
			return result;
		}

		public bool LogOut(string username)
		{
			bool result = false;

			try
			{
				result = factory.LogOut(username);
			}
			catch (Exception e)
			{
				//TODO log
			}
			return result;
		}

		public bool UserRegister(Common.Entities.User user)
		{
			throw new NotImplementedException();
		}

		public List<Common.Entities.User> LoginUsersOverview()
		{
			throw new NotImplementedException();
		}
	}
}
