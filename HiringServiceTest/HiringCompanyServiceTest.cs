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


            HiringCompanyDB.Instance.LogIn("admin", "admin").Returns(true);
            HiringCompanyDB.Instance.LogIn("admin", "ad").Returns(false);
            HiringCompanyDB.Instance.LogOut("admin").Returns(true);
            HiringCompanyDB.Instance.LogOut("pero").Returns(false);
            HiringCompanyDB.Instance.GetUser("pero").Returns(new User() { Name = "Pero" });
            HiringCompanyDB.Instance.AddUser(Arg.Is<User>(x => x.Name == "Voja" && x.Surname == "Seselj")).Returns(true);
            HiringCompanyDB.Instance.AddUser(Arg.Is<User>(x => x.Name != "Voja" && x.Surname == "Seselj")).Returns(false);
            HiringCompanyDB.Instance.UpdateUser(Arg.Is<User>(x => x.Name == "Mika")).Returns(true);
            HiringCompanyDB.Instance.UpdateUser(Arg.Is<User>(x => x.Name != "Mika")).Returns(false);
            HiringCompanyDB.Instance.LogIn("admin", "admin").Returns(true);
            HiringCompanyDB.Instance.LogIn("admin", "ad").Returns(false);
            HiringCompanyDB.Instance.LogOut("admin").Returns(true);
            HiringCompanyDB.Instance.LogOut("pero").Returns(false);
            HiringCompanyDB.Instance.GetUser("pero").Returns(new User() { Name = "Pero" });
            HiringCompanyDB.Instance.AddUser(Arg.Is<User>(x => x.Name == "Voja" && x.Surname == "Seselj")).Returns(true);
            HiringCompanyDB.Instance.AddUser(Arg.Is<User>(x => x.Name != "Voja" && x.Surname == "Seselj")).Returns(false);
            HiringCompanyDB.Instance.UpdateUser(Arg.Is<User>(x => x.Name == "Mika")).Returns(true);
            HiringCompanyDB.Instance.UpdateUser(Arg.Is<User>(x => x.Name != "Mika")).Returns(false);
            HiringCompanyDB.Instance.RemoveUser(Arg.Is<User>(x => x.Name == "Mika")).Returns(true);
            HiringCompanyDB.Instance.RemoveUser(Arg.Is<User>(x => x.Name != "Mika")).Returns(false);
            HiringCompanyDB.Instance.AddProject(Arg.Is<Project>(x => x.Name == "GeRuDok")).Returns(true);
            HiringCompanyDB.Instance.AddProject(Arg.Is<Project>(x => x.Name != "GeRuDok" && x.Name!="Excp")).Returns(false);
            HiringCompanyDB.Instance.UpdateProject(Arg.Is<Project>(x => x.Name == "NMMS")).Returns(true);
            HiringCompanyDB.Instance.UpdateProject(Arg.Is<Project>(x => x.Name != "NMMS")).Returns(false);
            HiringCompanyDB.Instance.GetAllUsers().Returns(new List<User>() { new User() { Name = "Voja" }, new User() { Name = "Slobo" } });
            HiringCompanyDB.Instance.GetAllProjects().Returns(new List<Project>() { new Project() { Name = "NMMS" }, new Project() { Name = "AGMS" } });  
            HiringCompanyDB.Instance.GetTasksFromUserStory(Arg.Is<UserStory>(x=>x.Name=="us1")).Returns(new List<Common.Entities.Task>() { new Common.Entities.Task() { Name = "taks1" }, new Common.Entities.Task() { Name = "taks2" } });

            HiringCompanyDB.Instance.GetProjectFromUserStory(Arg.Is<UserStory>(x => x.Name == "us1")).Returns(new Project() { Name = "NMMS" });

            HiringCompanyDB.Instance.GetAllCompanies().Returns(new List<Company>() { new Company() { Name = "Nis" }, new Company() { Name = "dms" } });
            HiringCompanyDB.Instance.GetUserStoryFromProject(Arg.Is<Project>(x => x.Name == "NMMS")).Returns(new List<UserStory>() { new UserStory() { Name = "us1" }, new UserStory() { Name = "us2" } });
            HiringCompanyDB.Instance.UpdateUserStory(Arg.Is<UserStory>(x => x.Name == "us")).Returns(true);
            HiringCompanyDB.Instance.UpdateUserStory(Arg.Is<UserStory>(x => x.Name != "us")).Returns(false);           

			HiringCompanyDB.Instance.ModifyCompanyToPartner(Arg.Is<Company>(x=>x.Name=="DMS")).Returns(true);
            HiringCompanyDB.Instance.ModifyCompanyToPartner(Arg.Is<Company>(x => x.Name != "DMS")).Returns(false);
			HiringCompanyDB.Instance.ChangeCompanyState(new Company(),State.CompanyState.NoPartner).ReturnsForAnyArgs(true);

		}


		[Test]
		public void AnswerToUserStoryTest()
		{
			Project project = new Project();
			Company company = new Company();
			UserStory userStory=new UserStory();
			bool result = serviceUnderTest.AnswerToUserStory(company, project, userStory);
			Assert.IsTrue(result);
		}

		[Test]
		public void SendProjectTestOk()
		{
			Project project=new Project();
			Company company=new Company();
			bool result = serviceUnderTest.SendProject(company, project);
			Assert.IsTrue(result);
		}

		[Test]
		public void SendRequestTestOk() { 
			bool result=serviceUnderTest.SendRequest(new Company());
			Assert.IsFalse(result);
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
            Project project = new Project() { Name = "NMMS" };
            List<UserStory> actualUserStories = new List<UserStory>() { new UserStory() { Name = "us1" }, new UserStory() { Name = "us2" } };
            List<UserStory> expected = serviceUnderTest.GetUserStoryFromProject(project);
            Assert.AreEqual(expected[0].Name, actualUserStories[0].Name);
		}
		[Test]
		public void GetAllCompaniesTest()
		{
            List<Company> actualCompanies = new List<Company>() { new Company() { Name = "Nis" }, new Company() { Name = "dms" } };
			List<Company> companies = serviceUnderTest.GetAllCompanies();
            Assert.AreEqual(companies[0].Name, actualCompanies[0].Name);
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
			Assert.IsFalse(result);

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
            List<User> actual = new List<User>() { new User() { Name = "Voja" }, new User() { Name = "Slobo" } };
			List<User> expected = serviceUnderTest.GetAllUsers();
			CollectionAssert.AreEqual(expected[0].Name,actual[0].Name);
		}

		[Test]
		public void GetAllProjectsTest()
		{
            List<Project> actualList = new List<Project>() { new Project() { Name = "NMMS" }, new Project() { Name = "AGMS" } };
			List<Project> list = serviceUnderTest.GetAllProjects();
            Assert.AreEqual(list[0].Name, actualList[0].Name);
		}

		
        
		[Test]
		public void GetTesksFromUserStoryTest()
		{
			UserStory us = new UserStory() { Name = "us1" };
            List<Common.Entities.Task> actualList = new List<Common.Entities.Task>() { new Common.Entities.Task() { Name = "taks1" }, new Common.Entities.Task() { Name = "taks2" } };
			var list = serviceUnderTest.GetTasksFromUserStory(us);
            Assert.AreEqual(list[0].Name, actualList[0].Name);

		}

		[Test]
		public void GetProjectFromUserStoryTest()
		{
			UserStory us = new UserStory() { Name = "us1" };
			Project actualProject = new Project() { Name = "NMMS" };
			Project expected = serviceUnderTest.GetProjectFromUserStory(us);
			Assert.AreEqual(expected.Name, actualProject.Name);
		}

		

	}
}
