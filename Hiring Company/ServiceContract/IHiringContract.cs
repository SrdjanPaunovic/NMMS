using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common.Entities;

namespace ServiceContract
{
	[ServiceContract]
	public interface IHiringContract
	{
		[OperationContract]
		bool LogIn(string username, string password);

		[OperationContract]
		bool LogOut(string username);

		[OperationContract]
		bool UserRegister(User user);

        [OperationContract]
        User GetUser(string username);

        [OperationContract]
        bool UpdateUser(User user);

		List<User> LoginUsersOverview();
	}
}
