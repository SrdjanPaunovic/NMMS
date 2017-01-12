using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Common.Entities;
using HiringCompanyService;
using Service.Access;
using NSubstitute;

namespace HiringServiceTest
{
	[TestFixture]
	public class HiringCompanyServiceTest
	{
		private HiringService serviceUnderTest;

		[OneTimeSetUp]
		public void SetupTest()
		{
			serviceUnderTest = new HiringService();
			HiringCompanyDB.Instance = Substitute.For<IHiringCompanyDB>();

			HiringCompanyDB.Instance.LogIn("admin","admin").Returns(true);
			HiringCompanyDB.Instance.LogIn("admin","ad").Returns(false);
			HiringCompanyDB.Instance.LogOut("admin").Returns(true);
			HiringCompanyDB.Instance.LogOut("pero").Returns(false);
			HiringCompanyDB.Instance.UserRegister(new User() { Username = "pero" }).Returns(true);
			HiringCompanyDB.Instance.UserRegister(new User() { Username = "jovisa" }).Returns(false);
			HiringCompanyDB.Instance.GetUser("pero").Returns(new User() { Name = "Pero" });
			HiringCompanyDB.Instance.AddUser(new User() { Name = "Voja", Surname = "Seselj" }).Returns(true);
			HiringCompanyDB.Instance.AddUser(new User() { Name = "Toma", Surname = "Diploma" }).Returns(false);
			HiringCompanyDB.Instance.UpdateUser(new User() { Name = "Mika" }).Returns(true);
			HiringCompanyDB.Instance.UpdateUser(new User() { Name = "Pera" }).Returns(false);

			HiringCompanyDB.Instance.RemoveUser(new User() { Name = "Mika" }).Returns(true);
			HiringCompanyDB.Instance.RemoveUser(new User() { Name = "Pera" }).Returns(false);

			HiringCompanyDB.Instance.AddProject(new Project() { Name = "GeRuDok" }).Returns(true);
			HiringCompanyDB.Instance.AddProject(new Project() { Name = "Abb" }).Returns(false);


			HiringCompanyDB.Instance.UpdateProject(new Project(){Name="NMMS"}).Returns(true);
			HiringCompanyDB.Instance.UpdateProject(new Project() { Name = "GMS" }).Returns(false);

			HiringCompanyDB.Instance.GetAllUsers().Returns(new List<User>(){new User(){Name="Voja"}});
			HiringCompanyDB.Instance.GetAllProjects().Returns(new List<Project>() { new Project() { Name = "NMMS" } });
			HiringCompanyDB.Instance.LoginUsersOverview().Returns(new List<User>() { new User() { Name = "Seselj" } });
			HiringCompanyDB.Instance.GetTasksFromUserStory(new UserStory() { Name = "us1" }).Returns(new List<Common.Entities.Task>() { new Common.Entities.Task() });
			HiringCompanyDB.Instance.GetProjectFromUserStory(new UserStory() { Name = "us1" }).Returns(new Project() { Name = "NMMS" });
			HiringCompanyDB.Instance.GetAllCompanies().Returns(new List<Company>() { new Company() { Name = "Nis" } });
			HiringCompanyDB.Instance.GetUserStoryFromProject(new Project() { Name = "NMMS" }).Returns(new List<UserStory>(){new UserStory(){Name="us1"}});
			HiringCompanyDB.Instance.UpdateUserStory(new UserStory() { Name = "us" }).Returns(true);
			HiringCompanyDB.Instance.UpdateUserStory(new UserStory() { Name = "us1" }).Returns(false);
			HiringCompanyDB.Instance.ModifyCompanyToPartner(new Company() { Name = "DMS" }).Returns(true);
			HiringCompanyDB.Instance.ModifyCompanyToPartner(new Company() { Name = "NIS" }).Returns(false);


			
		}

		[Test]
		public void ModifyCompanyToPartnerTestOk()
		{
			Company company = new Company() { Name = "DMS" };
			bool result = serviceUnderTest.ModifyCompany(company);
			Assert.IsTrue(result);

		}
		[Test]
		public void ModifyCompanyToPartnerTestFault()
		{
			Company company = new Company() { Name = "NIS" };
			bool result = serviceUnderTest.ModifyCompany(company);
			Assert.IsFalse (result);

		}

		[Test]
		public void UpdateUserStoryFault()
		{
			UserStory userStory = new UserStory() { Name = "us1" };
			bool result = serviceUnderTest.UpdateUserStory(userStory);
			Assert.IsFalse(result);

		}

		[Test]
		public void UpdateUserStoryOk()
		{
			UserStory userStory = new UserStory() { Name = "us" };
			bool result = serviceUnderTest.UpdateUserStory(userStory);
			Assert.IsTrue(result);

		}
		[Test]
		public void GetUserStoryFromProjectTest()
		{
			Project project=new Project() { Name = "NMMS" };
			List<UserStory> expectedUserStories = new List<UserStory>() { new UserStory() { Name = "us1" } };
			List<UserStory> list = serviceUnderTest.GetUserStoryFromProject(project);
			Assert.AreEqual(expectedUserStories, list);
		}
		[Test]
		public void GetAllCompaniesTest()
		{
			List<Company> expectedCompanies = new List<Company>() { new Company() { Name = "Nis" } };
			List<Company> companies = serviceUnderTest.GetAllCompanies();
			Assert.AreEqual(companies, expectedCompanies);
		}
		[Test]
		public void LogInTestOk()
		{
			bool result=serviceUnderTest.LogIn("admin", "admin");
			Assert.IsTrue(result);
		}
		[Test]
		public void LogInTestFault()
		{
			bool result = serviceUnderTest.LogIn("admin", "ad");
			Assert.IsFalse(result);
		}

		[Test]
		public void LogOutTestOk()
		{
		bool result=serviceUnderTest.LogOut("admin");
		Assert.IsTrue(result);
		}
		[Test]
		public void LogOutTestFault()
		{
			bool result = serviceUnderTest.LogOut("pero");
			Assert.IsFalse(result);
		}
		[Test]
		public void UserRegisterTestOk()
		{
			User user = new User() { Username = "pero" };
			bool result=serviceUnderTest.UserRegister(user);
			Assert.IsTrue(result);
		}
		[Test]
		public void UserRegisterTestFault()
		{
			User user = new User() { Username = "jovisa" };
			bool result = serviceUnderTest.UserRegister(user);
			Assert.IsFalse(result);
		}

		[Test]
		public void GetUserTest()
		{
			User result = serviceUnderTest.GetUser("pero");
			string name="Pero";
			Assert.AreEqual(result.Name, name);
		}
		[Test]
		public void AddUserTestOk()
		{
			User user = new User() { Name = "Voja", Surname = "Seselj" };
			bool result=serviceUnderTest.AddUser(user);
			Assert.IsTrue(result);
		}

		[Test]
		public void AddUserTestFault()
		{
			User user = new User() { Name = "Toma", Surname = "Diploma" };
			bool result = serviceUnderTest.AddUser(user);
			Assert.IsFalse(result);
		}

		[Test]
		public void UpdateUserOk()
		{
			User user = new User() { Name = "Mika" };
			bool result = serviceUnderTest.UpdateUser(user);
			Assert.IsTrue(result);

		}

		[Test]
		public void UpdateUserFault()
		{
			User user = new User() { Name = "Pera" };
			bool result = serviceUnderTest.UpdateUser(user);
			Assert.IsTrue(result);

		}

		[Test]
		public void RemoveUserOk()
		{
			User user = new User() { Name = "Mika" };
			bool result = serviceUnderTest.RemoveUser(user);
			Assert.IsTrue(result);
		}

		[Test]
		public void RemoveUserFault()
		{
			User user = new User() { Name = "Pera" };
			bool result = serviceUnderTest.RemoveUser(user);
			Assert.IsFalse(result);

		}

		[Test]
		public void AddProjectTestOk()
		{
			Project project = new Project() { Name = "GeRuDok" };
			bool result = serviceUnderTest.AddProject(project);
			Assert.IsTrue(result);
		}
		[Test]
		public void AddProjectTestFault()
		{
			Project project = new Project() { Name = "Abb" };
			bool result = serviceUnderTest.AddProject(project);
			Assert.IsFalse(result);
		}

		[Test]
		public void UpdateProjectTestOk()
		{
			Project project = new Project() { Name = "NMMS" };
			bool result = serviceUnderTest.UpdateProject(project);
			Assert.IsTrue(result);			
		}

		[Test]
		public void UpdateProjectTestFault()
		{
			Project project = new Project() { Name = "GMS" };
			bool result = serviceUnderTest.UpdateProject(project);
			Assert.IsFalse(result);
		}

		[Test]
		public void GetAllUsersTest()
		{
			List<User> expectedList=new List<User>(){new User(){Name="Voja"}};
			List<User> list = serviceUnderTest.GetAllUsers();
			Assert.AreEqual(list,expectedList);
		}

		[Test]
		public void GetAllProjectsTest()
		{
			List<Project> expectedList = new List<Project>() { new Project() { Name = "NMMS" } };
			List<Project> list = serviceUnderTest.GetAllProjects();
			Assert.AreEqual(list, expectedList);
		}

		[Test]
		public void LoginUseraOverviewTest()
		{
			List<User> expectedList = new List<User>() { new User() { Name = "Seselj" } };
			List<User> list = serviceUnderTest.LoginUsersOverview();
			Assert.AreEqual(list, expectedList);
		}
		[Test]
		public void GetTesksFromUserStoryTest()
		{
			UserStory us = new UserStory() { Name = "us1" };
			List<Common.Entities.Task> expectedList=new List<Common.Entities.Task>() { new Common.Entities.Task() };
			var list = serviceUnderTest.GetTasksFromUserStory(us);
			Assert.AreEqual(list, expectedList);
		}

		[Test]
		public void GetProjectFromUserStoryTest()
		{
			UserStory us = new UserStory() { Name = "us1" };
			Project expectedProject = new Project() { Name = "NMMS" };
			Project project = serviceUnderTest.GetProjectFromUserStory(us);
			Assert.AreEqual(project, expectedProject);
		}

		

	}
}
