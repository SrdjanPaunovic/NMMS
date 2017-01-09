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

        [OperationContract]
        bool AddProject(Project project);

        [OperationContract]
        bool UpdateProject(Project project);

        [OperationContract]
        List<Project> GetAllProjects();

        [OperationContract]
        List<User> LoginUsersOverview();

        [OperationContract]
        bool SendRequest(string ipAdress, Company company);

        [OperationContract]
		List<UserStory> GetUserStoryFromProject(Project project);

        [OperationContract]
        bool UpdateUserStory(UserStory userStory);

        [OperationContract]
        List<Company> GetAllCompanies();
	}
}
