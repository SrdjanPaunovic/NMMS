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
		[OneTimeSetUp]
		public void SetupTest()
		{
            
            clientUnderTest = new HiringClientProxy();
			clientUnderTest.factory = Substitute.For<IHiringContract>();

			clientUnderTest.factory.LogIn("admin", "admin").Returns(true);
			clientUnderTest.factory.LogIn("admin", "ad").Returns(false);
            clientUnderTest.factory.LogIn("ex", "ex").Returns((x) => { throw new Exception(); });
			clientUnderTest.factory.LogOut("admin").Returns(true);
			clientUnderTest.factory.LogOut("pero").Returns(false);
            clientUnderTest.factory.LogOut("ex").Returns(x => { throw new Exception(); });
			clientUnderTest.factory.UserRegister(Arg.Is<User>(x=>x.Username=="pero")).Returns(true);
            clientUnderTest.factory.UserRegister(Arg.Is<User>(x => x.Username != "pero")).Returns(false);
			clientUnderTest.factory.GetUser("pero").Returns(new User() { Name = "Pero" });
            clientUnderTest.factory.GetUser("ex").Returns(x => { throw new Exception(); });
			clientUnderTest.factory.AddUser(Arg.Is<User>(x=>x.Name=="Voja" && x.Surname=="Seselj")).Returns(true);
            clientUnderTest.factory.AddUser(Arg.Is<User>(x => x.Name != "Voja" && x.Name != "ex")).Returns(false);
			clientUnderTest.factory.AddUser(Arg.Is<User>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });

            clientUnderTest.factory.UpdateUser(Arg.Is<User>(x => x.Name == "Mika")).Returns(true);
            clientUnderTest.factory.UpdateUser(Arg.Is<User>(x => x.Name != "Mika" && x.Name!="ex")).Returns(false);
            clientUnderTest.factory.UpdateUser(Arg.Is<User>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });

            clientUnderTest.factory.When(x => x.UpdateUser(Arg.Is<User>(f => f.Name == "ex"))).Do(k => { throw new Exception(); });

            clientUnderTest.factory.RemoveUser(Arg.Is<User>(x => x.Name == "Mika")).Returns(true);
            clientUnderTest.factory.RemoveUser(Arg.Is<User>(x => x.Name != "Mika" && x.Name!="ex")).Returns(false);
			clientUnderTest.factory.RemoveUser(Arg.Is<User>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });

            clientUnderTest.factory.AddProject(Arg.Is<Project>(x=>x.Name=="GeRuDok")).Returns(true);
            clientUnderTest.factory.AddProject(Arg.Is<Project>(x => x.Name != "GeRuDok"  && x.Name!="Excp")).Returns(false);
			clientUnderTest.factory.AddProject(Arg.Is<Project>(p=>p.Name=="Excp")).Returns((x) => { throw new Exception(); });

            clientUnderTest.factory.UpdateProject(Arg.Is<Project>(x=>x.Name=="NMMS")).Returns(true);
            clientUnderTest.factory.UpdateProject(Arg.Is<Project>(x => x.Name != "NMMS" && x.Name!="ex")).Returns(false);
            clientUnderTest.factory.UpdateProject(Arg.Is<Project>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });

            clientUnderTest.factory.GetAllUsers().Returns(new List<User>() { new User() { Name = "Voja" }, new User() { Name = "Slobo" } });
            clientUnderTest.factory.GetAllProjects().Returns(new List<Project>() { new Project() { Name = "NMMS" }, new Project() { Name = "AGMS" } });

            clientUnderTest.factory.LoginUsersOverview().Returns(new List<User>() { new User() { Name = "Seselj" }, new User() { Name = "Slobo" } });

            clientUnderTest.factory.GetTasksFromUserStory(Arg.Is<UserStory>(x => x.Name == "us1")).Returns(new List<Common.Entities.Task>() { new Common.Entities.Task() { Name = "taks1" }, new Common.Entities.Task() { Name = "taks2" } });
			clientUnderTest.factory.GetTasksFromUserStory(Arg.Is<UserStory>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });           
			clientUnderTest.factory.GetProjectFromUserStory(Arg.Is<UserStory>(x=>x.Name=="us")).Returns(new Project() { Name = "NMMS" });
           
			clientUnderTest.factory.GetAllCompanies().Returns(new List<Company>() { new Company() { Name = "Nis" }, new Company() { Name = "dms" } });
            clientUnderTest.factory.GetProjectFromUserStory(Arg.Is<UserStory>(x => x.Name == "ex")).Returns((x) => { throw new Exception(); });
            clientUnderTest.factory.GetUserStoryFromProject(Arg.Is<Project>(x => x.Name == "NMMS")).Returns(new List<UserStory>() { new UserStory() { Name = "us1" }, new UserStory() { Name = "us2" } });
            clientUnderTest.factory.GetUserStoryFromProject(Arg.Is<Project>(x => x.Name == "ex")).Returns(x=>{throw new Exception();});
            clientUnderTest.factory.UpdateUserStory(Arg.Is<UserStory>(x=>x.Name=="us")).Returns(true);
            clientUnderTest.factory.UpdateUserStory(Arg.Is<UserStory>(x => x.Name != "us" && x.Name!="ex")).Returns(false);
			clientUnderTest.factory.UpdateUserStory(Arg.Is<UserStory>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });
			clientUnderTest.factory.AddProject(Arg.Is<Project>(p => p.Name == "Excp")).Returns((x) => { throw new Exception(); });

			clientUnderTest.factory.AnswerToUserStory(Arg.Is<Company>(x => x.Name == "dms"),Arg.Is<Project>(y=>y.Name=="dms"),Arg.Is<UserStory>(z=>z.Name=="us")).Returns(true);
			clientUnderTest.factory.AnswerToUserStory(Arg.Is<Company>(x => x.Name == "ex"), Arg.Is<Project>(y => y.Name == "ex"), Arg.Is<UserStory>(z => z.Name == "ex")).Returns(p => { throw new Exception(); });
			clientUnderTest.factory.AnswerToUserStory(Arg.Is<Company>(x => x.Name != "dms" && x.Name!="ex"), Arg.Is<Project>(y => y.Name != "dms" && y.Name!="ex"), Arg.Is<UserStory>(z => z.Name != "us" && z.Name!="ex")).Returns(false);
			
			clientUnderTest.factory.SendProject(Arg.Is<Company>(x => x.Name == "dms"), Arg.Is<Project>(y => y.Name == "dms")).Returns(true);
			clientUnderTest.factory.SendProject(Arg.Is<Company>(x => x.Name != "dms" && x.Name != "ex"), Arg.Is<Project>(y => y.Name != "dms" && y.Name != "ex")).Returns(false);
			clientUnderTest.factory.SendProject(Arg.Is<Company>(x => x.Name == "ex"), Arg.Is<Project>(y => y.Name == "ex")).Returns(x => { throw new Exception(); });

			clientUnderTest.factory.SendRequest(Arg.Is<Company>(x => x.Name == "dms")).Returns(true);
			clientUnderTest.factory.SendRequest(Arg.Is<Company>(x => x.Name != "dms" && x.Name != "ex")).Returns(false);
			clientUnderTest.factory.SendRequest(Arg.Is<Company>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });

		}


		[Test]
		public void SendRequestTestException()
		{
			Company company = new Company() { Name = "ex" };
			Assert.DoesNotThrow(() => clientUnderTest.SendRequest(company));
		}

		[Test]
		public void SendRequestTestFault()
		{
			Company company = new Company() { Name = "dms1" };
			bool result = clientUnderTest.SendRequest(company);
			Assert.IsFalse(result);
		}


		[Test]
		public void SendRequestTestOk()
		{
			Company company = new Company() { Name = "dms" };
			bool result = clientUnderTest.SendRequest(company);
			Assert.IsTrue(result);			
		}

		[Test]
		public void SendProjectTestOk()
		{
			Company company = new Company() { Name = "dms" };
			Project project = new Project() { Name = "dms" };
			bool result = clientUnderTest.SendProject(company, project);
			Assert.IsTrue(result);
		}

		[Test]
		public void SendProjectTestFault()
		{
			Company company = new Company() { Name = "dms1" };
			Project project = new Project() { Name = "dms1" };
			bool result = clientUnderTest.SendProject(company, project);
			Assert.IsFalse(result);
		}
		[Test]
		public void SendProjectTestException()
		{
			Company company = new Company() { Name = "ex" };
			Project project = new Project() { Name = "ex" };
			Assert.DoesNotThrow(() => clientUnderTest.SendProject(company, project));
		}



		[Test]
		public void LoginUsersOverviewTestException()
		{
			Assert.Throws<NotImplementedException>(() => clientUnderTest.LoginUsersOverview());
		}

		[Test]
		public void AnswerToUserStoryTestOk()
		{
			Company company = new Company() { Name = "dms" };
			Project project = new Project() { Name = "dms" };
			UserStory userStory = new UserStory() { Name = "us" };
			bool result = clientUnderTest.AnswerToUserStory(company, project, userStory);
			Assert.IsTrue(result);
		}
		[Test]
		public void AnswerToUserStoryTestFault()
		{
			Company company = new Company() { Name = "dms1" };
			Project project = new Project() { Name = "dms1" };
			UserStory userStory = new UserStory() { Name = "us1" };
			bool result = clientUnderTest.AnswerToUserStory(company, project, userStory);
			Assert.IsFalse(result);
		}

		[Test]
		public void AnswerToUserStoryTestException()
		{
			Company company = new Company() { Name = "ex" };
			Project project = new Project() { Name = "ex" };
			UserStory userStory = new UserStory() { Name = "ex" };
			Assert.DoesNotThrow(() => clientUnderTest.AnswerToUserStory(company, project, userStory));
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
			Assert.DoesNotThrow(()=> clientUnderTest.UpdateUserStory(userStory));

		}

		[Test]
		public void GetUserStoryFromProjectTest()
		{
			Project project = new Project() { Name = "NMMS" };
			List<UserStory> actualUserStories = new List<UserStory>() { new UserStory() { Name = "us1" }, new UserStory() { Name = "us2"  }};
			List<UserStory> expected = clientUnderTest.GetUserStoryFromProject(project);
            Assert.AreEqual(expected[0].Name, actualUserStories[0].Name);
		}
        [Test]
        public void GetUserStoryFromProjectTestException()
        {
            Project project = new Project() { Name = "ex" };
            Assert.DoesNotThrow(() => clientUnderTest.GetUserStoryFromProject(project));
        }
		[Test]
		public void GetAllCompaniesTest()
		{
			List<Company> actualCompanies = new List<Company>() { new Company() { Name = "Nis" },new Company(){Name="dms"} };
			List<Company> companies = clientUnderTest.GetAllCompanies();
            Assert.AreEqual( companies[0].Name,actualCompanies[0].Name);
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
		public void UserRegisterTestOk()
		{
			User user = new User() { Username = "pero" };
			//bool result = clientUnderTest.UserRegister(user);
            Assert.Throws<NotImplementedException>(() => clientUnderTest.UserRegister(user));
		}
		[Test]
		public void UserRegisterTestFault()
		{
			User user = new User() { Username = "jovisa" };
			//bool result = clientUnderTest.UserRegister(user);
            // kad se implementira onda otkomentarisi
            Assert.Throws<NotImplementedException>(() => clientUnderTest.UserRegister(user));

		}

		[Test]
		public void ModifyCompanyTestException()
		{
			Company company = new Company();
			Assert.Throws<NotImplementedException>(() => clientUnderTest.ModifyCompany(company));
		}

		[Test]
		public void GetUserTest()
		{
			User result = clientUnderTest.GetUser("pero");
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
        public void AddUserTestException()
        {
            User user = new User() { Name = "ex", Surname = "ex" };
            Assert.DoesNotThrow(() => clientUnderTest.AddUser(user));
        }

		[Test]
		public void UpdateUserTestOk()
		{
			User user = new User() { Name = "Mika" };
			bool result = clientUnderTest.UpdateUser(user);
			Assert.IsTrue(result);

		}

		[Test]
		public void UpdateUserTestFault()
		{
			User user = new User() { Name = "Pera" };
			bool result = clientUnderTest.UpdateUser(user);
			Assert.IsFalse(result);

		}
        [Test]
        public void UpdateUserTestException()
        {
            User user = new User() { Name = "ex" };
			Assert.DoesNotThrow(() => clientUnderTest.UpdateUser(user));

        }


		[Test]
		public void RemoveUserTestOk()
		{
			User user = new User() { Name = "Mika" };
			bool result = clientUnderTest.RemoveUser(user);
			Assert.IsTrue(result);
		}

		[Test]
		public void RemoveUserTestFault()
		{
			User user = new User() { Name = "Pera" };
			bool result = clientUnderTest.RemoveUser(user);
			Assert.IsFalse(result);

		}

        public void RemoveUserTestException()
        {
            User user = new User() { Name = "ex" };
            Assert.DoesNotThrow(() => clientUnderTest.RemoveUser(user));
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
			Assert.DoesNotThrow(() => clientUnderTest.AddProject(project));			
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
        public void UpdateProjectTestException()
        {
            Project project = new Project() { Name = "ex" };
            Assert.DoesNotThrow(() => clientUnderTest.UpdateProject(project));
        }

		[Test]
		public void GetAllUsersTest()
		{
            List<User> actual = new List<User>() { new User() { Name = "Voja" }, new User() { Name = "Slobo" } };
			List<User> expected = clientUnderTest.GetAllUsers();
			CollectionAssert.AreEqual(expected[0].Name, actual[0].Name);
		}

		[Test]
		public void GetAllProjectsTest()
		{
            List<Project> actual = new List<Project>() { new Project() { Name = "NMMS" }, new Project() { Name = "AGMS" } };
			List<Project> expected = clientUnderTest.GetAllProjects();
			CollectionAssert.AreEqual(expected[0].Name, actual[0].Name);
		}
        /*
         * Nije jos implementirana metoda
         * 
		[Test]
		public void LoginUseraOverviewTest()
		{
            List<User> expectedList = new List<User>() { new User() { Name = "Seselj" }, new User() { Name = "Slobo" }  };
			List<User> list = clientUnderTest.LoginUsersOverview();
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
			Project expectedProject = new Project() { Name = "NMMS" };
			Project project = clientUnderTest.GetProjectFromUserStory(us);
			Assert.AreEqual(project.Name, expectedProject.Name);
		}

		[Test]
		public void GetProjectFromUserStoryTestException()
		{
			UserStory us = new UserStory() { Name = "ex" };
			Project expectedProject = new Project() { Name = "NMMS" };
			Assert.DoesNotThrow(()=>clientUnderTest.GetProjectFromUserStory(us));
		}
		

	}
}
