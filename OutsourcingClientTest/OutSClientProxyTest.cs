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

namespace OutsourcingClientTest
{
    [TestFixture]
    public class OutSClientProxyTest
    {
        private OutSClientProxy clientUnderTest;
        [OneTimeSetUp]
        public void SetupTest()
        {

            clientUnderTest = new OutSClientProxy();

            clientUnderTest.Factory = Substitute.For<IOutsourcingContract>();

            clientUnderTest.Factory.LogIn("admin", "admin").Returns(true);
            clientUnderTest.Factory.LogIn("admin", "ad").Returns(false);
            clientUnderTest.Factory.LogIn("ex", "ex").Returns((x) => { throw new Exception(); });
            clientUnderTest.Factory.LogOut("admin").Returns(true);
            clientUnderTest.Factory.LogOut("pero").Returns(false);
            clientUnderTest.Factory.LogOut("ex").Returns(x => { throw new Exception(); });
            clientUnderTest.Factory.GetUser("pero").Returns(new OcUser() { Name = "Pero" });
            clientUnderTest.Factory.GetUser("ex").Returns(x => { throw new Exception(); });
            clientUnderTest.Factory.AddUser(Arg.Is<OcUser>(x => x.Name == "Voja" && x.Surname == "Seselj")).Returns(true);
            clientUnderTest.Factory.AddUser(Arg.Is<OcUser>(x => x.Name != "Voja" && x.Name != "ex")).Returns(false);
            clientUnderTest.Factory.AddUser(Arg.Is<OcUser>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });
            clientUnderTest.Factory.UpdateUser(Arg.Is<OcUser>(x => x.Name == "Mika")).Returns(true);
            clientUnderTest.Factory.UpdateUser(Arg.Is<OcUser>(x => x.Name != "Mika" && x.Name != "ex")).Returns(false);
            clientUnderTest.Factory.UpdateUser(Arg.Is<OcUser>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });
            clientUnderTest.Factory.When(x => x.UpdateUser(Arg.Is<OcUser>(f => f.Name == "ex"))).Do(k => { throw new Exception(); });
            clientUnderTest.Factory.RemoveUser(Arg.Is<OcUser>(x => x.Name == "Mika")).Returns(true);
            clientUnderTest.Factory.RemoveUser(Arg.Is<OcUser>(x => x.Name != "Mika" && x.Name != "ex")).Returns(false);
            clientUnderTest.Factory.RemoveUser(Arg.Is<OcUser>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });
            clientUnderTest.Factory.AddProject(Arg.Is<OcProject>(x => x.Name == "GeRuDok")).Returns(true);
            clientUnderTest.Factory.AddProject(Arg.Is<OcProject>(x => x.Name != "GeRuDok" && x.Name != "Excp")).Returns(false);
            clientUnderTest.Factory.AddProject(Arg.Is<OcProject>(p => p.Name == "Excp")).Returns((x) => { throw new Exception(); });
            clientUnderTest.Factory.UpdateProject(Arg.Is<OcProject>(x => x.Name == "NMMS")).Returns(true);
            clientUnderTest.Factory.UpdateProject(Arg.Is<OcProject>(x => x.Name != "NMMS" && x.Name != "ex")).Returns(false);
            clientUnderTest.Factory.UpdateProject(Arg.Is<OcProject>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });
            clientUnderTest.Factory.GetAllUsers().Returns(new List<OcUser>() { new OcUser() { Name = "Voja" }, new OcUser() { Name = "Slobo" } });
            clientUnderTest.Factory.GetAllProjects().Returns(new List<OcProject>() { new OcProject() { Name = "NMMS" }, new OcProject() { Name = "AGMS" } });
            clientUnderTest.Factory.GetTasksFromUserStory(Arg.Is<UserStory>(x => x.Name == "us1")).Returns(new List<Common.Entities.Task>() { new Common.Entities.Task() { Name = "taks1" }, new Common.Entities.Task() { Name = "taks2" } });
            clientUnderTest.Factory.GetTasksFromUserStory(Arg.Is<UserStory>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });
            clientUnderTest.Factory.GetProjectFromUserStory(Arg.Is<UserStory>(x => x.Name == "us")).Returns(new OcProject() { Name = "NMMS" });
            clientUnderTest.Factory.GetAllCompanies().Returns(new List<Company>() { new Company() { Name = "Nis" }, new Company() { Name = "dms" } });
            clientUnderTest.Factory.GetProjectFromUserStory(Arg.Is<UserStory>(x => x.Name == "ex")).Returns((x) => { throw new Exception(); });
            clientUnderTest.Factory.GetUserStoryFromProject(Arg.Is<OcProject>(x => x.Name == "NMMS")).Returns(new List<UserStory>() { new UserStory() { Name = "us1" }, new UserStory() { Name = "us2" } });
            clientUnderTest.Factory.GetUserStoryFromProject(Arg.Is<OcProject>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });
            clientUnderTest.Factory.UpdateUserStory(Arg.Is<UserStory>(x => x.Name == "us")).Returns(true);
            clientUnderTest.Factory.UpdateUserStory(Arg.Is<UserStory>(x => x.Name != "us" && x.Name != "ex")).Returns(false);
            clientUnderTest.Factory.UpdateUserStory(Arg.Is<UserStory>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });
            clientUnderTest.Factory.AddProject(Arg.Is<OcProject>(p => p.Name == "Excp")).Returns((x) => { throw new Exception(); });
            clientUnderTest.Factory.AddCompany(Arg.Is<Company>(x => x.Name == "dms")).Returns(true);
            clientUnderTest.Factory.AddCompany(Arg.Is<Company>(x => x.Name != "dms")).Returns(false);
            clientUnderTest.Factory.AddTeam(Arg.Is<Team>(x => x.Name == "rto")).Returns(true);
            clientUnderTest.Factory.AddTeam(Arg.Is<Team>(x => x.Name != "rto")).Returns(false);
            clientUnderTest.Factory.GetAllTeams().ReturnsForAnyArgs(new List<Team>() { new Team() { Name = "rto" } });
            clientUnderTest.Factory.AnswerToRequest(Arg.Is<Company>(x => x.Name == "dms")).Returns(true);
            clientUnderTest.Factory.AnswerToRequest(Arg.Is<Company>(x => x.Name != "dms")).Returns(false);
            clientUnderTest.Factory.SendUserStory(new Company(), new UserStory(), new Project()).ReturnsForAnyArgs(true);
            clientUnderTest.Factory.AnswerToProject(new Company(), new Project()).ReturnsForAnyArgs(true);
            clientUnderTest.Factory.ModifyCompany(Arg.Is<Company>(x => x.Name == "dms")).Returns(x => { throw new Exception(); });
            clientUnderTest.Factory.ChangeCompanyState(new Company(),State.CompanyState.NoPartner).ReturnsForAnyArgs(true);
            clientUnderTest.Factory.RemoveCompany(new Company()).ReturnsForAnyArgs(true);
            clientUnderTest.Factory.GetAllUsersWithoutTeam().Returns(new List<OcUser>() { new OcUser() { Name = "slobo" } });
            clientUnderTest.Factory.RemoveProject(Arg.Is<OcProject>(x => x.Name == "nmms")).Returns(true);
            clientUnderTest.Factory.RemoveProject(Arg.Is<OcProject>(x => x.Name != "nmms")).Returns(x => { throw new Exception(); });
            clientUnderTest.Factory.GetAllUserStories().Returns(new List<UserStory>());

            

        }

        [Test]
        public void GetAllUserStories()
        {
            Assert.DoesNotThrow(() => clientUnderTest.GetAllUserStories());

        }
        [Test]
        public void RemoveProject2()
        {
            Assert.DoesNotThrow(() => clientUnderTest.RemoveProject(new OcProject() { Name = "aa" }));
        }
        [Test]
        public void RemoveProject1()
        {
            bool result = clientUnderTest.RemoveProject(new OcProject() { Name = "nmms" });
            Assert.IsTrue(result);
        }

        [Test]
        public void GetAllUsersWithoutTeamTest()
        {
            List<OcUser> users = clientUnderTest.GetAllUsersWithoutTeam();
            OcUser user = users.FirstOrDefault();
            string name = "slobo";
            Assert.AreEqual(user.Name, name);
        }

        [Test ]
        public void RemoveCompanyTest()
        {
            bool result = clientUnderTest.RemoveCompany(new Company());
            Assert.IsTrue(result);

        }


        [Test]
        public void ChangeCompanyStateTest()
        {
            bool result = clientUnderTest.ChangeCompanyState(new Company(), State.CompanyState.NoPartner);
            Assert.IsTrue(result);
        }


        [Test]
        public void ModifyCompanyTesFault()
        {
            Assert.IsFalse(clientUnderTest.ModifyCompany(new Company() { Name = "ex" }));
        }
        [Test]
        public void ModifyCompanyTesException()
        {
            Assert.Throws<Exception>(()=>clientUnderTest.ModifyCompany(new Company() { Name = "dms" }));
        }

        [Test]
        public void AnswerToProjectTest()
        {
            bool result = clientUnderTest.AnswerToProject(new Company(), new Project());
            Assert.IsTrue(result);
        }

        [Test]
        public void SendUserStoryTest()
        {
            bool result = clientUnderTest.SendUserStory(new Company(), new UserStory(), new Project());
            Assert.IsTrue(result);
        }
        [Test]
        public void AnswerToRequestTest1()
        {
            bool result = clientUnderTest.AnswerToRequest(new Company() { Name = "Nis" });
            Assert.IsFalse(result);
        }

        [Test]
        public void AnswerToRequestTest2()
        {
            bool result = clientUnderTest.AnswerToRequest(new Company() { Name = "dms" });
            Assert.IsTrue(result);
        }

        [Test]
        public void GetAllTeamsTest1()
        {
            Assert.DoesNotThrow(() => clientUnderTest.GetAllTeams());
        }

        [Test]
        public void GetAllTeamsTest2()
        {
            List<Team> teams = clientUnderTest.GetAllTeams();
            Team team = teams.FirstOrDefault();
            string name = "rto";
            Assert.AreEqual(team.Name, name);

        }

        [Test]
        public void GetAllTeamsTest3()
        {
            List<Team> teams = clientUnderTest.GetAllTeams();
            Team team = teams.FirstOrDefault();
            string name = "rta";
            Assert.AreNotEqual(team.Name, name);

        }

        [Test]
        public void AddTeamTest1()
        {
            bool result = clientUnderTest.AddTeam(new Team() { Name = "rto" });
            Assert.IsTrue(result);
        }

        [Test]
        public void AddTeamTest2()
        {
            bool result = clientUnderTest.AddTeam(new Team() { Name = "rta" });
            Assert.IsFalse(result);
        }
        [Test]
        public void AddCompanyTest1()
        {
            bool result = clientUnderTest.AddCompany(new Company() { Name = "dms" });
            Assert.IsTrue(result);
        }

        [Test]
        public void AddCompanyTest2()
        {
            bool result = clientUnderTest.AddCompany(new Company() { Name = "nis" });
            Assert.IsFalse(result);
        }

        [Test]
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
        public void UpdateUserStoryException()
        {
            UserStory userStory = new UserStory() { Name = "ex" };
            Assert.DoesNotThrow(() => clientUnderTest.UpdateUserStory(userStory));

        }

        [Test]
        public void GetUserStoryFromProjectTest()
        {
            OcProject project = new OcProject() { Name = "NMMS" };
            List<UserStory> actualUserStories = new List<UserStory>() { new UserStory() { Name = "us1" }, new UserStory() { Name = "us2" } };
            List<UserStory> expected = clientUnderTest.GetUserStoryFromProject(project);
            Assert.AreEqual(expected[0].Name, actualUserStories[0].Name);
        }
        [Test]
        public void GetUserStoryFromProjectTestException()
        {
            OcProject project = new OcProject() { Name = "ex" };
            Assert.DoesNotThrow(() => clientUnderTest.GetUserStoryFromProject(project));
        }
        [Test]
        public void GetAllCompaniesTest()
        {
            List<Company> actualCompanies = new List<Company>() { new Company() { Name = "Nis" }, new Company() { Name = "dms" } };
            List<Company> companies = clientUnderTest.GetAllCompanies();
            Assert.AreEqual(companies[0].Name, actualCompanies[0].Name);
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
        public void LogInTestException()
        {
            Assert.DoesNotThrow(() => clientUnderTest.LogIn("ex", "ex"));
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
        public void LogOutTestException()
        {
            Assert.DoesNotThrow(() => clientUnderTest.LogOut("ex"));
        }



    

        [Test]
        public void GetUserTest()
        {
            OcUser result = clientUnderTest.GetUser("pero");
            string name = "Pero";
            Assert.AreEqual(result.Name, name);
        }
        [Test]
        public void GetUserTestException()
        {
            Assert.DoesNotThrow(() => clientUnderTest.GetUser("ex"));
        }

        [Test]
        public void AddUserTestOk()
        {
            OcUser user = new OcUser() { Name = "Voja", Surname = "Seselj" };
            bool result = clientUnderTest.AddUser(user);
            Assert.IsTrue(result);
        }

        [Test]
        public void AddUserTestFault()
        {
            OcUser user = new OcUser() { Name = "Toma", Surname = "Diploma" };
            bool result = clientUnderTest.AddUser(user);
            Assert.IsFalse(result);
        }

        [Test]
        public void AddUserTestException()
        {
            OcUser user = new OcUser() { Name = "ex", Surname = "ex" };
            Assert.DoesNotThrow(() => clientUnderTest.AddUser(user));
        }

        [Test]
        public void UpdateUserTestOk()
        {
            OcUser user = new OcUser() { Name = "Mika" };
            bool result = clientUnderTest.UpdateUser(user);
            Assert.IsTrue(result);

        }

        [Test]
        public void UpdateUserTestFault()
        {
            OcUser user = new OcUser() { Name = "Pera" };
            bool result = clientUnderTest.UpdateUser(user);
            Assert.IsFalse(result);

        }
        [Test]
        public void UpdateUserTestException()
        {
            OcUser user = new OcUser() { Name = "ex" };
            Assert.DoesNotThrow(() => clientUnderTest.UpdateUser(user));

        }


        [Test]
        public void RemoveUserTestOk()
        {
            OcUser user = new OcUser() { Name = "Mika" };
            bool result = clientUnderTest.RemoveUser(user);
            Assert.IsTrue(result);
        }

        [Test]
        public void RemoveUserTestFault()
        {
            OcUser user = new OcUser() { Name = "Pera" };
            bool result = clientUnderTest.RemoveUser(user);
            Assert.IsFalse(result);

        }

        public void RemoveUserTestException()
        {
            OcUser user = new OcUser() { Name = "ex" };
            Assert.DoesNotThrow(() => clientUnderTest.RemoveUser(user));
        }

        [Test]
        public void AddProjectTestOk()
        {
            OcProject project = new OcProject() { Name = "GeRuDok" };
            bool result = clientUnderTest.AddProject(project);
            Assert.IsTrue(result);
        }
        [Test]
        public void AddProjectTestFault()
        {
            OcProject project = new OcProject() { Name = "Abb" };
            bool result = clientUnderTest.AddProject(project);
            Assert.IsFalse(result);
        }
        [Test]

        public void AddProjectTestException()
        {
            OcProject project = new OcProject() { Name = "Excp" };
            Assert.DoesNotThrow(() => clientUnderTest.AddProject(project));
        }

        [Test]
        public void UpdateProjectTestOk()
        {
            OcProject project = new OcProject() { Name = "NMMS" };
            bool result = clientUnderTest.UpdateProject(project);
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateProjectTestFault()
        {
            OcProject project = new OcProject() { Name = "GMS" };
            bool result = clientUnderTest.UpdateProject(project);
            Assert.IsFalse(result);
        }
        [Test]
        public void UpdateProjectTestException()
        {
            OcProject project = new OcProject() { Name = "ex" };
            Assert.DoesNotThrow(() => clientUnderTest.UpdateProject(project));
        }

        [Test]
        public void GetAllUsersTest()
        {
            List<OcUser> actual = new List<OcUser>() { new OcUser() { Name = "Voja" }, new OcUser() { Name = "Slobo" } };
            List<OcUser> expected = clientUnderTest.GetAllUsers();
            CollectionAssert.AreEqual(expected[0].Name, actual[0].Name);
        }

        [Test]
        public void GetAllProjectsTest()
        {
            List<OcProject> actual = new List<OcProject>() { new OcProject() { Name = "NMMS" }, new OcProject() { Name = "AGMS" } };
            List<OcProject> expected = clientUnderTest.GetAllProjects();
            CollectionAssert.AreEqual(expected[0].Name, actual[0].Name);
        }
        /*
         * Nije jos implementirana metoda
         * 
		[Test]
		public void LoginUseraOverviewTest()
		{
            List<OcUser> expectedList = new List<OcUser>() { new OcUser() { Name = "Seselj" }, new OcUser() { Name = "Slobo" }  };
			List<OcUser> list = clientUnderTest.LoginUsersOverview();
			Assert.AreEqual(list, expectedList);
		}*/



        [Test]
        public void GetTasksFromUserStoryTest()
        {
            UserStory us = new UserStory() { Name = "us1" };
            List<Common.Entities.Task> actualList = new List<Common.Entities.Task>() { new Common.Entities.Task() { Name = "taks1" }, new Common.Entities.Task() { Name = "taks2" } };
            var list = clientUnderTest.GetTasksFromUserStory(us);
            Assert.AreEqual(list[0].Name, actualList[0].Name);
        }
        [Test]
        public void GetTasksFromUserStoryException()
        {
            UserStory us = new UserStory() { Name = "ex" };
            Assert.DoesNotThrow(() => clientUnderTest.GetTasksFromUserStory(us));
        }


        [Test]
        public void GetProjectFromUserStoryTest()
        {
            UserStory us = new UserStory() { Name = "us" };
            OcProject expectedProject = new OcProject() { Name = "NMMS" };
            OcProject project = clientUnderTest.GetProjectFromUserStory(us);
            Assert.AreEqual(project.Name, expectedProject.Name);
        }

        [Test]
        public void GetProjectFromUserStoryTestException()
        {
            UserStory us = new UserStory() { Name = "ex" };
            OcProject expectedProject = new OcProject() { Name = "NMMS" };
            Assert.DoesNotThrow(() => clientUnderTest.GetProjectFromUserStory(us));
        }


    }
}
