using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ServiceContract;

namespace HiringCompanyService
{
	public class HiringCompanyService : IHiringContract
	{


		public bool LogIn(string username, string password)
		{
			throw new NotImplementedException();

		}

		public bool LogOut(string username, string password)
		{
			throw new NotImplementedException();

		}
	}
}
