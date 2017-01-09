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
	public interface IOutsourcingCompanyService
	{
        [OperationContract ]
        bool AddUser(OcUser user);
        [OperationContract]
        bool AddCompany(Company company);
        [OperationContract]
        bool AddProject(OcProject project);
        [OperationContract]
        bool AddUserStory(UserStory userStory);
        [OperationContract]
        bool AddTask(Common.Entities.Task task);

        [OperationContract]
        bool AddTeam(Team team);
        [OperationContract]
        bool LogIn(string username, string password);
        [OperationContract]
        bool LogOut(string username);

        [OperationContract]
        bool UserRegister(OcUser user);
        [OperationContract]
        List<OcUser> LoginUsersOverview();

        [OperationContract]
        bool AnswerToRequest(Company company);

        [OperationContract]
        List<Company> GetAllCompanies();

		[OperationContract]
		List<OcProject> GetAllProjects();

		[OperationContract]
		OcUser GetUser(string username);

		[OperationContract]
		bool UpdateUser(OcUser user);
	}
}
