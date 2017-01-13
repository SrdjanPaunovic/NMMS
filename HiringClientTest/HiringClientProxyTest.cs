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
            clientUnderTest.factory.AddUser(Arg.Is<User>(x => x.Name != "Voja" && x.Surname == "Seselj")).Returns(false);
            clientUnderTest.factory.UpdateUser(Arg.Is<User>(x => x.Name == "Mika")).Returns(true);
            clientUnderTest.factory.UpdateUser(Arg.Is<User>(x => x.Name != "Mika" && x.Name!="ex")).Returns(false);
            clientUnderTest.factory.UpdateUser(Arg.Is<User>(x => x.Name == "ex")).Returns(x => { throw new Exception(); });

            clientUnderTest.factory.RemoveUser(Arg.Is<User>(x => x.Name == "Mika")).Returns(true);
            clientUnderTest.factory.RemoveUser(Arg.Is<User>(x => x.Name != "Mika")).Returns(false);
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
            clientUnderTest.factory.GetProjectFromUserStory(Arg.Is<UserStory>(x=>x.Name=="us")).Returns(new Project() { Name = "NMMS" });
            clientUnderTest.factory.GetAllCompanies().Returns(new List<Company>() { new Company() { Name = "Nis" }, new Company() { Name = "dms" } });
            clientUnderTest.factory.GetProjectFromUserStory(Arg.Is<UserStory>(x => x.Name == "ex")).Returns((x) => { throw new Exception(); });
            clientUnderTest.factory.GetUserStoryFromProject(Arg.Is<Project>(x => x.Name == "NMMS")).Returns(new List<UserStory>() { new UserStory() { Name = "us1" }, new UserStory() { Name = "us2" } });
             clientUnderTest.factory.GetUserStoryFromProject(Arg.Is<Project>(x => x.Name == "ex")).Returns(x=>{throw new Exception();});
            clientUnderTest.factory.UpdateUserStory(Arg.Is<UserStory>(x=>x.Name=="us")).Returns(true);
            clientUnderTest.factory.UpdateUserStory(Arg.Is<UserStory>(x => x.Name != "us")).Returns(false);
			
						
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
			List<UserStory> expectedUserStories = new List<UserStory>() { new UserStory() { Name = "us1" }, new UserStory() { Name = "us2"  }};
			List<UserStory> list = clientUnderTest.GetUserStoryFromProject(project);
			Assert.AreEqual(expectedUserStories, list);
		}
        [Test]
        public void GetUserStoryFromProjectTestException()
        {
            Project project = new Project() { Name = "ex" };
            Assert.Throws<Exception>(() => clientUnderTest.GetUserStoryFromProject(project));
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
            Assert.Throws<Exception>(() => clientUnderTest.LogIn("ex", "ex"));
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
            Assert.Throws<Exception>(() => clientUnderTest.LogOut("ex"));
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
		public void GetUserTest()
		{
			User result = clientUnderTest.GetUser("pero");
			string name = "Pero";
			Assert.AreEqual(result.Name, name);
		}
        public void GetUserTestException()
        {
            Assert.Throws<Exception>(() => clientUnderTest.GetUser("ex"));
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
            Assert.Throws<Exception>(() => clientUnderTest.AddUser(user));
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
            Assert.Throws<Exception>(() => clientUnderTest.UpdateUser(user));

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
            User user = new User() { Name = "Pera" };
            Assert.Throws<Exception>(() => clientUnderTest.RemoveUser(user));
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

        public void UpdateProjectTestException()
        {
            Project project = new Project() { Name = "ex" };
            Assert.Throws<Exception>(() => clientUnderTest.UpdateProject(project));
        }

		[Test]
		public void GetAllUsersTest()
		{
            List<User> expectedList = new List<User>() { new User() { Name = "Voja" }, new User() { Name = "Slobo" } };
			List<User> list = clientUnderTest.GetAllUsers();
			CollectionAssert.AreEqual(list, expectedList);
		}

		[Test]
		public void GetAllProjectsTest()
		{
            List<Project> expectedList = new List<Project>() { new Project() { Name = "NMMS" }, new Project() { Name = "AGMS" } };
			List<Project> list = clientUnderTest.GetAllProjects();
			CollectionAssert.AreEqual(list, expectedList);
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
		public void GetTesksFromUserStoryTest()
		{
			UserStory us = new UserStory() { Name = "us1" };
            List<Common.Entities.Task> expectedList = new List<Common.Entities.Task>() { new Common.Entities.Task() { Name = "taks1" }, new Common.Entities.Task() { Name = "taks2" } };
			var list = clientUnderTest.GetTasksFromUserStory(us);
			CollectionAssert.AreEqual(list, expectedList);
		}
        [Test]
        public void GetTesksFromUserStoryTestException()
        {
            UserStory us = new UserStory() { Name = "ex" };
            Assert.Throws<Exception>(() => clientUnderTest.GetTasksFromUserStory(us));
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
