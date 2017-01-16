using Common.Entities;
using NSubstitute;
using NUnit.Framework;
using Service;
using Service.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsourcingServiceTest
{
	[TestFixture]
	public class OutsourcingServiceTest
	{
		private OutsourcingCompanyService serviceUnderTest;

		static List<Team> expectedTeams = new List<Team> { new Team() { Name = "T1", Id = 1 }, new Team() { Name = "T2", Id = 2 } };
		static List<OcUser> expectedUsersWithoutTeam = new List<OcUser> { new OcUser() { Id = 1, Name = "Meca" } };
		static List<OcUser> expectedUsers = new List<OcUser> { new OcUser() { Id = 1, Name = "Zeki" }, new OcUser() { Id = 2, Name = "Srki" }, new OcUser() { Id = 3, Name = "Miki" }, new OcUser() { Id = 4, Name = "Maki" } };

		[OneTimeSetUp]
		public void SetupTest()
		{
			serviceUnderTest = new OutsourcingCompanyService();
			OutsourcingCompanyDB.Instance = Substitute.For<IOutsourcingCompanyDB>();

			OutsourcingCompanyDB.Instance.GetAllTeams().Returns(expectedTeams);
			OutsourcingCompanyDB.Instance.GetAllUsersWithoutTeam().Returns(expectedUsersWithoutTeam);
			OutsourcingCompanyDB.Instance.GetAllUsers().Returns(expectedUsers);
			OutsourcingCompanyDB.Instance.AddTeam(Arg.Is<Team>(x => x.Name == "RocketTeam")).Returns(true);
            OutsourcingCompanyDB.Instance.LogIn("admin", "admin").Returns(true);
            OutsourcingCompanyDB.Instance.LogIn("admin", "ad").Returns(false);
            OutsourcingCompanyDB.Instance.LogIn("ex", "ex").Returns((x) => { throw new Exception(); });
            OutsourcingCompanyDB.Instance.LogOut("admin").Returns(true);
            OutsourcingCompanyDB.Instance.LogOut("pero").Returns(false);
            OutsourcingCompanyDB.Instance.LogOut("ex").Returns(x => { throw new Exception(); });
            OutsourcingCompanyDB.Instance.GetUser("pero").Returns(new OcUser() { Name = "Pero" });
            OutsourcingCompanyDB.Instance.GetUser("ex").Returns(x => { throw new Exception(); });
            OutsourcingCompanyDB.Instance.AddUser(Arg.Is<OcUser>(x => x.Name == "Voja" && x.Surname == "Seselj")).Returns(true);
            OutsourcingCompanyDB.Instance.AddUser(Arg.Is<OcUser>(x => x.Name != "Voja" && x.Name != "ex")).Returns(false);
            OutsourcingCompanyDB.Instance.AddUser(Arg.Is<OcUser>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });
            OutsourcingCompanyDB.Instance.UpdateUser(Arg.Is<OcUser>(x => x.Name == "Mika")).Returns(true);
            OutsourcingCompanyDB.Instance.UpdateUser(Arg.Is<OcUser>(x => x.Name != "Mika" && x.Name != "ex")).Returns(false);
            OutsourcingCompanyDB.Instance.UpdateUser(Arg.Is<OcUser>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });
            OutsourcingCompanyDB.Instance.When(x => x.UpdateUser(Arg.Is<OcUser>(f => f.Name == "ex"))).Do(k => { throw new Exception(); });
            OutsourcingCompanyDB.Instance.RemoveUser(Arg.Is<OcUser>(x => x.Name == "Mika")).Returns(true);
            OutsourcingCompanyDB.Instance.RemoveUser(Arg.Is<OcUser>(x => x.Name != "Mika" && x.Name != "ex")).Returns(false);
            OutsourcingCompanyDB.Instance.RemoveUser(Arg.Is<OcUser>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });
            OutsourcingCompanyDB.Instance.AddProject(Arg.Is<OcProject>(x => x.Name == "GeRuDok")).Returns(true);
            OutsourcingCompanyDB.Instance.AddProject(Arg.Is<OcProject>(x => x.Name != "GeRuDok" && x.Name != "Excp")).Returns(false);
            OutsourcingCompanyDB.Instance.AddProject(Arg.Is<OcProject>(p => p.Name == "Excp")).Returns((x) => { throw new Exception(); });
            OutsourcingCompanyDB.Instance.UpdateProject(Arg.Is<OcProject>(x => x.Name == "NMMS")).Returns(true);
            OutsourcingCompanyDB.Instance.UpdateProject(Arg.Is<OcProject>(x => x.Name != "NMMS" && x.Name != "ex")).Returns(false);
            OutsourcingCompanyDB.Instance.UpdateProject(Arg.Is<OcProject>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });
            OutsourcingCompanyDB.Instance.GetAllUsers().Returns(new List<OcUser>() { new OcUser() { Name = "Voja" }, new OcUser() { Name = "Slobo" } });
            OutsourcingCompanyDB.Instance.GetAllProjects().Returns(new List<OcProject>() { new OcProject() { Name = "NMMS" }, new OcProject() { Name = "AGMS" } });
            OutsourcingCompanyDB.Instance.GetTasksFromUserStory(Arg.Is<UserStory>(x => x.Name == "us1")).Returns(new List<Common.Entities.Task>() { new Common.Entities.Task() { Name = "taks1" }, new Common.Entities.Task() { Name = "taks2" } });
            OutsourcingCompanyDB.Instance.GetTasksFromUserStory(Arg.Is<UserStory>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });
            OutsourcingCompanyDB.Instance.GetProjectFromUserStory(Arg.Is<UserStory>(x => x.Name == "us")).Returns(new OcProject() { Name = "NMMS" });
            OutsourcingCompanyDB.Instance.GetAllCompanies().Returns(new List<Company>() { new Company() { Name = "Nis" }, new Company() { Name = "dms" } });
            OutsourcingCompanyDB.Instance.GetProjectFromUserStory(Arg.Is<UserStory>(x => x.Name == "ex")).Returns((x) => { throw new Exception(); });
            OutsourcingCompanyDB.Instance.GetUserStoryFromProject(Arg.Is<OcProject>(x => x.Name == "NMMS")).Returns(new List<UserStory>() { new UserStory() { Name = "us1" }, new UserStory() { Name = "us2" } });
            OutsourcingCompanyDB.Instance.GetUserStoryFromProject(Arg.Is<OcProject>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });
            OutsourcingCompanyDB.Instance.UpdateUserStory(Arg.Is<UserStory>(x => x.Name == "us")).Returns(true);
            OutsourcingCompanyDB.Instance.UpdateUserStory(Arg.Is<UserStory>(x => x.Name != "us" && x.Name != "ex")).Returns(false);
            OutsourcingCompanyDB.Instance.UpdateUserStory(Arg.Is<UserStory>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });
            OutsourcingCompanyDB.Instance.AddProject(Arg.Is<OcProject>(p => p.Name == "Excp")).Returns((x) => { throw new Exception(); });
            OutsourcingCompanyDB.Instance.AddCompany(Arg.Is<Company>(x => x.Name == "dms")).Returns(true);
            OutsourcingCompanyDB.Instance.AddCompany(Arg.Is<Company>(x => x.Name != "dms")).Returns(false);
            OutsourcingCompanyDB.Instance.AddTeam(Arg.Is<Team>(x => x.Name == "rto")).Returns(true);
            OutsourcingCompanyDB.Instance.AddTeam(Arg.Is<Team>(x => x.Name != "rto")).Returns(false);
            OutsourcingCompanyDB.Instance.GetAllTeams().ReturnsForAnyArgs(new List<Team>() { new Team() { Name = "rto" } });
            OutsourcingCompanyDB.Instance.ChangeCompanyState(new Company(),State.CompanyState.NoPartner).ReturnsForAnyArgs(true);
            OutsourcingCompanyDB.Instance.RemoveCompany(new Company()).ReturnsForAnyArgs(true);
            OutsourcingCompanyDB.Instance.GetAllUsersWithoutTeam().Returns(new List<OcUser>() { new OcUser() { Name = "slobo" } });
            OutsourcingCompanyDB.Instance.RemoveProject(Arg.Is<OcProject>(x => x.Name == "nmms")).Returns(true);
            OutsourcingCompanyDB.Instance.RemoveProject(Arg.Is<OcProject>(x => x.Name != "nmms")).Returns(x => { throw new Exception(); });
		}

		[Test]
		public void GetAllTeams()
		{
			List<Team> actualTeams = serviceUnderTest.GetAllTeams();
			Assert.AreEqual("rto", actualTeams[0].Name);
		}

		[Test]
		public void AddTeam()
		{
			bool success = serviceUnderTest.AddTeam(new Team() { Name = "RocketTeam" });
            Assert.IsFalse(success);
		}

		[Test]
		public void GetAllUsersWithoutTeam()
		{
			List<OcUser> actualUsersWithoutTeam = serviceUnderTest.GetAllUsersWithoutTeam();
			Assert.AreEqual("slobo", actualUsersWithoutTeam[0].Name);
		}


        [Test]
        public void GetAllUserStories()
        {
            Assert.DoesNotThrow(() => serviceUnderTest.GetAllUserStories());

        }
        [Test]
        public void RemoveProjectException()
        {
            Assert.Throws<Exception>(() => serviceUnderTest.RemoveProject(new OcProject() { Name = "aa" }));
        }
        [Test]
        public void RemoveProject1()
        {
            bool result = serviceUnderTest.RemoveProject(new OcProject() { Name = "nmms" });
            Assert.IsTrue(result);
        }

        [Test]
        public void GetAllUsersWithoutTeamTest()
        {
            List<OcUser> users = serviceUnderTest.GetAllUsersWithoutTeam();
            OcUser user = users.FirstOrDefault();
            string name = "slobo";
            Assert.AreEqual(user.Name, name);
        }

        [Test]
        public void RemoveCompanyTest()
        {
            bool result = serviceUnderTest.RemoveCompany(new Company());
            Assert.IsTrue(result);

        }


        [Test]
        public void ChangeCompanyStateTest()
        {
            bool result = serviceUnderTest.ChangeCompanyState(new Company(), State.CompanyState.NoPartner);
            Assert.IsTrue(result);
        }


        [Test]
        public void ModifyCompanyTest()
        {
            Assert.DoesNotThrow(() => serviceUnderTest.ModifyCompany(new Company() { Name = "dms" }));
        }

        [Test]
        public void AnswerToProjectTest()
        {
            bool result = serviceUnderTest.AnswerToProject(new Company(), new Project());
            Assert.IsFalse(result);
        }

        [Test]
        public void SendUserStoryTest()
        {
            bool result = serviceUnderTest.SendUserStory(new Company(), new UserStory(), new Project());
            Assert.IsTrue(result);
        }
        [Test]
        public void AnswerToRequestTest1()
        {
            bool result = serviceUnderTest.AnswerToRequest(new Company() { Name = "Nis" });
            Assert.IsFalse(result);
        }

        [Test]
        public void AnswerToRequestTest2()
        {
            bool result = serviceUnderTest.AnswerToRequest(new Company() { Name = "dms" });
            Assert.IsFalse(result);
        }

        [Test]
        public void GetAllTeamsTest1()
        {
            Assert.DoesNotThrow(() => serviceUnderTest.GetAllTeams());
        }

        [Test]
        public void GetAllTeamsTest2()
        {
            List<Team> teams = serviceUnderTest.GetAllTeams();
            Team team = teams.FirstOrDefault();
            string name = "rto";
            Assert.AreEqual(team.Name, name);

        }

        [Test]
        public void GetAllTeamsTest3()
        {
            List<Team> teams = serviceUnderTest.GetAllTeams();
            Team team = teams.FirstOrDefault();
            string name = "rta";
            Assert.AreNotEqual(team.Name, name);

        }

        [Test]
        public void AddTeamTest1()
        {
            bool result = serviceUnderTest.AddTeam(new Team() { Name = "rto" });
            Assert.IsTrue(result);
        }

        [Test]
        public void AddTeamTest2()
        {
            bool result = serviceUnderTest.AddTeam(new Team() { Name = "rta" });
            Assert.IsFalse(result);
        }
        [Test]
        public void AddCompanyTest1()
        {
            bool result = serviceUnderTest.AddCompany(new Company() { Name = "dms" });
            Assert.IsTrue(result);
        }

        [Test]
        public void AddCompanyTest2()
        {
            bool result = serviceUnderTest.AddCompany(new Company() { Name = "nis" });
            Assert.IsFalse(result);
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
        public void UpdateUserStoryException()
        {
            UserStory userStory = new UserStory() { Name = "ex" };
            Assert.Throws<Exception>(() => serviceUnderTest.UpdateUserStory(userStory));

        }

        [Test]
        public void GetUserStoryFromProjectTest()
        {
            OcProject project = new OcProject() { Name = "NMMS" };
            List<UserStory> actualUserStories = new List<UserStory>() { new UserStory() { Name = "us1" }, new UserStory() { Name = "us2" } };
            List<UserStory> expected = serviceUnderTest.GetUserStoryFromProject(project);
            Assert.AreEqual(expected[0].Name, actualUserStories[0].Name);
        }
        [Test]
        public void GetUserStoryFromProjectTestException()
        {
            OcProject project = new OcProject() { Name = "ex" };
            Assert.Throws<Exception>(() => serviceUnderTest.GetUserStoryFromProject(project));
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
            bool result = serviceUnderTest.LogIn("admin", "admin");
            Assert.IsTrue(result);
        }
        [Test]
        public void LogInTestFault()
        {
            bool result = serviceUnderTest.LogIn("admin", "ad");
            Assert.IsFalse(result);
        }

        [Test]
        public void LogInTestException()
        {
            Assert.DoesNotThrow(() => serviceUnderTest.LogIn("ex", "ex"));
        }

        [Test]
        public void LogOutTestOk()
        {
            bool result = serviceUnderTest.LogOut("admin");
            Assert.IsTrue(result);
        }
        [Test]
        public void LogOutTestFault()
        {
            bool result = serviceUnderTest.LogOut("pero");
            Assert.IsFalse(result);
        }
        [Test]
        public void LogOutTestException()
        {
            Assert.Throws<Exception>(() => serviceUnderTest.LogOut("ex"));
        }



       

        [Test]
        public void GetUserTest()
        {
            OcUser result = serviceUnderTest.GetUser("pero");
            string name = "Pero";
            Assert.AreEqual(result.Name, name);
        }
        [Test]
        public void GetUserTestException()
        {
            Assert.Throws<Exception>(() => serviceUnderTest.GetUser("ex"));
        }

        [Test]
        public void AddUserTestOk()
        {
            OcUser user = new OcUser() { Name = "Voja", Surname = "Seselj" };
            bool result = serviceUnderTest.AddUser(user);
            Assert.IsTrue(result);
        }

        [Test]
        public void AddUserTestFault()
        {
            OcUser user = new OcUser() { Name = "Toma", Surname = "Diploma" };
            bool result = serviceUnderTest.AddUser(user);
            Assert.IsFalse(result);
        }

        [Test]
        public void AddUserTestException()
        {
            OcUser user = new OcUser() { Name = "ex", Surname = "ex" };
            Assert.Throws<Exception>(() => serviceUnderTest.AddUser(user));
        }

        [Test]
        public void UpdateUserTestOk()
        {
            OcUser user = new OcUser() { Name = "Mika" };
            bool result = serviceUnderTest.UpdateUser(user);
            Assert.IsTrue(result);

        }

        [Test]
        public void UpdateUserTestFault()
        {
            OcUser user = new OcUser() { Name = "Pera" };
            bool result = serviceUnderTest.UpdateUser(user);
            Assert.IsFalse(result);

        }
        [Test]
        public void UpdateUserTestException()
        {
            OcUser user = new OcUser() { Name = "ex" };
            Assert.Throws<Exception>(() => serviceUnderTest.UpdateUser(user));

        }


        [Test]
        public void RemoveUserTestOk()
        {
            OcUser user = new OcUser() { Name = "Mika" };
            bool result = serviceUnderTest.RemoveUser(user);
            Assert.IsTrue(result);
        }

        [Test]
        public void RemoveUserTestFault()
        {
            OcUser user = new OcUser() { Name = "Pera" };
            bool result = serviceUnderTest.RemoveUser(user);
            Assert.IsFalse(result);

        }

        public void RemoveUserTestException()
        {
            OcUser user = new OcUser() { Name = "ex" };
            Assert.DoesNotThrow(() => serviceUnderTest.RemoveUser(user));
        }

        [Test]
        public void AddProjectTestOk()
        {
            OcProject project = new OcProject() { Name = "GeRuDok" };
            bool result = serviceUnderTest.AddProject(project);
            Assert.IsTrue(result);
        }
        [Test]
        public void AddProjectTestFault()
        {
            OcProject project = new OcProject() { Name = "Abb" };
            bool result = serviceUnderTest.AddProject(project);
            Assert.IsFalse(result);
        }

        [Test]
        public void AddProjectTestException()
        {
            OcProject project = new OcProject() { Name = "Excp" };
            Assert.Throws<Exception>(() => serviceUnderTest.AddProject(project));
        }

        [Test]
        public void UpdateProjectTestOk()
        {
            OcProject project = new OcProject() { Name = "NMMS" };
            bool result = serviceUnderTest.UpdateProject(project);
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateProjectTestFault()
        {
            OcProject project = new OcProject() { Name = "GMS" };
            bool result = serviceUnderTest.UpdateProject(project);
            Assert.IsFalse(result);
        }
        [Test]
        public void UpdateProjectTestException()
        {
            OcProject project = new OcProject() { Name = "ex" };
            Assert.Throws<Exception>(() => serviceUnderTest.UpdateProject(project));
        }

        [Test]
        public void GetAllUsersTest()
        {
            List<OcUser> actual = new List<OcUser>() { new OcUser() { Name = "Voja" }, new OcUser() { Name = "Slobo" } };
            List<OcUser> expected = serviceUnderTest.GetAllUsers();
            CollectionAssert.AreEqual(expected[0].Name, actual[0].Name);
        }

        [Test]
        public void GetAllProjectsTest()
        {
            List<OcProject> actual = new List<OcProject>() { new OcProject() { Name = "NMMS" }, new OcProject() { Name = "AGMS" } };
            List<OcProject> expected = serviceUnderTest.GetAllProjects();
            CollectionAssert.AreEqual(expected[0].Name, actual[0].Name);
        }
        /*
         * Nije jos implementirana metoda
         * 
		[Test]
		public void LoginUseraOverviewTest()
		{
            List<OcUser> expectedList = new List<OcUser>() { new OcUser() { Name = "Seselj" }, new OcUser() { Name = "Slobo" }  };
			List<OcUser> list = serviceUnderTest.LoginUsersOverview();
			Assert.AreEqual(list, expectedList);
		}*/



        [Test]
        public void GetTasksFromUserStoryTest()
        {
            UserStory us = new UserStory() { Name = "us1" };
            List<Common.Entities.Task> actualList = new List<Common.Entities.Task>() { new Common.Entities.Task() { Name = "taks1" }, new Common.Entities.Task() { Name = "taks2" } };
            var list = serviceUnderTest.GetTasksFromUserStory(us);
            Assert.AreEqual(list[0].Name, actualList[0].Name);
        }
        [Test]
        public void GetTasksFromUserStoryException()
        {
            UserStory us = new UserStory() { Name = "ex" };
            Assert.Throws<System.Exception>(() => serviceUnderTest.GetTasksFromUserStory(us));
        }


        [Test]
        public void GetProjectFromUserStoryTest()
        {
            UserStory us = new UserStory() { Name = "us" };
            OcProject expectedProject = new OcProject() { Name = "NMMS" };
            OcProject project = serviceUnderTest.GetProjectFromUserStory(us);
            Assert.AreEqual(project.Name, expectedProject.Name);
        }

        [Test]
        public void GetProjectFromUserStoryTestException()
        {
            UserStory us = new UserStory() { Name = "ex" };
            OcProject expectedProject = new OcProject() { Name = "NMMS" };
            Assert.Throws<Exception>(() => serviceUnderTest.GetProjectFromUserStory(us));
        }
	}
}
