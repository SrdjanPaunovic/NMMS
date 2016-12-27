using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ServiceContract
{
	[ServiceContract]
	public interface IHiringContract
	{
		[OperationContract]
		bool LogIn(string username, string password);

		[OperationContract]
		bool LogOut(string username, string password);

	}
}
