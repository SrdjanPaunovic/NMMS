using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Client;
using ServiceContract;
using Common.Entities;

namespace HiringClientTest
{
	[TestFixture]
	public class HiringClientProxyTest
	{
		private HiringClientProxy clientUnderTest;
		private Project proj = new Project() { Name = "Abb" };
		[OneTimeSetUp]
		public void SetupTest()
		{
			clientUnderTest = new HiringClientProxy();
			clientUnderTest.factory = Substitute.For<IHiringContract>();
			clientUnderTest.factory.LogIn("admin","admin").Returns(true);
		    clientUnderTest.factory.LogIn("admin","ad").Returns(false);
			clientUnderTest.factory.LogOut("admin").Returns(true);
			clientUnderTest.factory.LogOut("pero").Returns(false);
			clientUnderTest.factory.UserRegister(new User() { Username = "pero" }).Returns(true);
		    clientUnderTest.factory.UserRegister(new User() { Username = "jovisa" }).Returns(false);
			clientUnderTest.factory.GetUser("pero").Returns(new User() { Name = "Pero" });
			clientUnderTest.factory.AddUser(new User() { Name = "Voja", Surname = "Seselj" }).Returns(true);
			clientUnderTest.factory.AddUser(new User() { Name = "Toma", Surname = "Diploma" }).Returns(false);
		    clientUnderTest.factory.UpdateUser(new User() { Name = "Mika" }).Returns(true);
			clientUnderTest.factory.UpdateUser(new User() { Name = "Pera" }).Returns(false);

			clientUnderTest.factory.LogIn("admin", "admin").Returns(true);
			clientUnderTest.factory.LogIn("admin", "ad").Returns(false);
			clientUnderTest.factory.LogOut("admin").Returns(true);
			clientUnderTest.factory.LogOut("pero").Returns(false);
			clientUnderTest.factory.UserRegister(new User() { Username = "pero" }).Returns(true);
			clientUnderTest.factory.UserRegister(new User() { Username = "jovisa" }).Returns(false);
			clientUnderTest.factory.GetUser("pero").Returns(new User() { Name = "Pero" });
			clientUnderTest.factory.AddUser(new User() { Name = "Voja", Surname = "Seselj" }).Returns(true);
			clientUnderTest.factory.AddUser(new User() { Name = "Toma", Surname = "Diploma" }).Returns(false);
			clientUnderTest.factory.UpdateUser(new User() { Name = "Mika" }).Returns(true);
			clientUnderTest.factory.UpdateUser(new User() { Name = "Pera" }).Returns(false);
			clientUnderTest.factory.RemoveUser(new User() { Name = "Mika" }).Returns(true);
			clientUnderTest.factory.RemoveUser(new User() { Name = "Pera" }).Returns(false);

			clientUnderTest.factory.AddProject(new Project() { Name = "GeRuDok" }).Returns(true);

			clientUnderTest.factory.AddProject(Arg.Is<Project>(p=>p.Name=="Excp")).Returns((x) => { throw new Exception(); });
			



			clientUnderTest.factory.UpdateProject(new Project() { Name = "NMMS" }).Returns(true);
			clientUnderTest.factory.UpdateProject(new Project() { Name = "GMS" }).Returns(false);
			clientUnderTest.factory.GetAllUsers().Returns(new List<User>() { new User() { Name = "Voja" } });
			clientUnderTest.factory.GetAllProjects().Returns(new List<Project>() { new Project() { Name = "NMMS" } });
			clientUnderTest.factory.LoginUsersOverview().Returns(new List<User>() { new User() { Name = "Seselj" } });
			clientUnderTest.factory.GetTasksFromUserStory(new UserStory() { Name = "us1" }).Returns(new List<Common.Entities.Task>() { new Common.Entities.Task() });
			clientUnderTest.factory.GetProjectFromUserStory(new UserStory() { Name = "us1" }).Returns(new Project() { Name = "NMMS" });
			clientUnderTest.factory.GetAllCompanies().Returns(new List<Company>() { new Company() { Name = "Nis" } });
			clientUnderTest.factory.GetUserStoryFromProject(new Project() { Name = "NMMS" }).Returns(new List<UserStory>() { new UserStory() { Name = "us1" } });
			clientUnderTest.factory.UpdateUserStory(new UserStory() { Name = "us" }).Returns(true);
			clientUnderTest.factory.UpdateUserStory(new UserStory() { Name = "us1" }).Returns(false);
			//clientUnderTest.factory.ModifyCompanyToPartner(new Company() { Name = "DMS" }).Returns(true);
			//clientUnderTest.factory.ModifyCompanyToPartner(new Company() { Name = "NIS" }).Returns(false);
						
		}

		public void UpdateUserStoryFault()
		{
			UserStory userStory = new UserStory() { Name = "us1" };
			bool result = clientUnderTest.UpdateUserStory(userStory);
			Assert.IsFalse(result);

		}

		[Test]
		public void UpdateUserStoryOk()
		{
			UserStory userStory = new UserStory() { Name = "us" };
			bool result = clientUnderTest.UpdateUserStory(userStory);
			Assert.IsTrue(result);

		}
		[Test]
		public void GetUserStoryFromProjectTest()
		{
			Project project = new Project() { Name = "NMMS" };
			List<UserStory> expectedUserStories = new List<UserStory>() { new UserStory() { Name = "us1" } };
			List<UserStory> list = clientUnderTest.GetUserStoryFromProject(project);
			Assert.AreEqual(expectedUserStories, list);
		}
		[Test]
		public void GetAllCompaniesTest()
		{
			List<Company> expectedCompanies = new List<Company>() { new Company() { Name = "Nis" } };
			List<Company> companies = clientUnderTest.GetAllCompanies();
			Assert.AreEqual(companies, expectedCompanies);
		}
		[Test]
		public void LogInTestOk()
		{
			bool result = clientUnderTest.LogIn("admin", "admin");
			Assert.IsTrue(result);
		}
		[Test]
		public void LogInTestFault()
		{
			bool result = clientUnderTest.LogIn("admin", "ad");
			Assert.IsFalse(result);
		}

		[Test]
		public void LogOutTestOk()
		{
			bool result = clientUnderTest.LogOut("admin");
			Assert.IsTrue(result);
		}
		[Test]
		public void LogOutTestFault()
		{
			bool result = clientUnderTest.LogOut("pero");
			Assert.IsFalse(result);
		}
		[Test]
		public void UserRegisterTestOk()
		{
			User user = new User() { Username = "pero" };
			bool result = clientUnderTest.UserRegister(user);
			Assert.IsTrue(result);
		}
		[Test]
		public void UserRegisterTestFault()
		{
			User user = new User() { Username = "jovisa" };
			bool result = clientUnderTest.UserRegister(user);
			Assert.IsFalse(result);
		}

		[Test]
		public void GetUserTest()
		{
			User result = clientUnderTest.GetUser("pero");
			string name = "Pero";
			Assert.AreEqual(result.Name, name);
		}
		[Test]
		public void AddUserTestOk()
		{
			User user = new User() { Name = "Voja", Surname = "Seselj" };
			bool result = clientUnderTest.AddUser(user);
			Assert.IsTrue(result);
		}

		[Test]
		public void AddUserTestFault()
		{
			User user = new User() { Name = "Toma", Surname = "Diploma" };
			bool result = clientUnderTest.AddUser(user);
			Assert.IsFalse(result);
		}

		[Test]
		public void UpdateUserOk()
		{
			User user = new User() { Name = "Mika" };
			bool result = clientUnderTest.UpdateUser(user);
			Assert.IsTrue(result);

		}

		[Test]
		public void UpdateUserFault()
		{
			User user = new User() { Name = "Pera" };
			bool result = clientUnderTest.UpdateUser(user);
			Assert.IsTrue(result);

		}

		[Test]
		public void RemoveUserOk()
		{
			User user = new User() { Name = "Mika" };
			bool result = clientUnderTest.RemoveUser(user);
			Assert.IsTrue(result);
		}

		[Test]
		public void RemoveUserFault()
		{
			User user = new User() { Name = "Pera" };
			bool result = clientUnderTest.RemoveUser(user);
			Assert.IsFalse(result);

		}

		[Test]
		public void AddProjectTestOk()
		{
			Project project = new Project() { Name = "GeRuDok" };
			bool result = clientUnderTest.AddProject(project);
			Assert.IsTrue(result);
		}
		[Test]
		public void AddProjectTestFault()
		{
			Project project = new Project() { Name = "Abb" };
			bool result = clientUnderTest.AddProject(project);
			Assert.IsFalse(result);
		}
		[Test]
		
		public void AddProjectTestException()
		{
			Project project = new Project() { Name = "Excp" };
			Assert.Throws<Exception>(() => clientUnderTest.AddProject(project));			
		}

		[Test]
		public void UpdateProjectTestOk()
		{
			Project project = new Project() { Name = "NMMS" };
			bool result = clientUnderTest.UpdateProject(project);
			Assert.IsTrue(result);
		}

		[Test]
		public void UpdateProjectTestFault()
		{
			Project project = new Project() { Name = "GMS" };
			bool result = clientUnderTest.UpdateProject(project);
			Assert.IsFalse(result);
		}

		[Test]
		public void GetAllUsersTest()
		{
			List<User> expectedList = new List<User>() { new User() { Name = "Voja" } };
			List<User> list = clientUnderTest.GetAllUsers();
			Assert.AreEqual(list, expectedList);
		}

		[Test]
		public void GetAllProjectsTest()
		{
			List<Project> expectedList = new List<Project>() { new Project() { Name = "NMMS" } };
			List<Project> list = clientUnderTest.GetAllProjects();
			Assert.AreEqual(list, expectedList);
		}

		[Test]
		public void LoginUseraOverviewTest()
		{
			List<User> expectedList = new List<User>() { new User() { Name = "Seselj" } };
			List<User> list = clientUnderTest.LoginUsersOverview();
			Assert.AreEqual(list, expectedList);
		}
		[Test]
		public void GetTesksFromUserStoryTest()
		{
			UserStory us = new UserStory() { Name = "us1" };
			List<Common.Entities.Task> expectedList = new List<Common.Entities.Task>() { new Common.Entities.Task() };
			var list = clientUnderTest.GetTasksFromUserStory(us);
			Assert.AreEqual(list, expectedList);
		}

		[Test]
		public void GetProjectFromUserStoryTest()
		{
			UserStory us = new UserStory() { Name = "us1" };
			Project expectedProject = new Project() { Name = "NMMS" };
			Project project = clientUnderTest.GetProjectFromUserStory(us);
			Assert.AreEqual(project, expectedProject);
		}

		

	}
}
