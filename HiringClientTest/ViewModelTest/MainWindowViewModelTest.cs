using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Client.ViewModel;
using Common;
using System.Collections.ObjectModel;
using Common.Entities;
using NSubstitute;
using ServiceContract;

namespace HiringClientTest.ViewModelTest
{
    [TestFixture, RequiresSTA]
	public class MainWindowViewModelTest
	{
		private MainWindowViewModel clientViewModelUnderTest;

		[OneTimeSetUp]
		public void SetupTest()
		{
			clientViewModelUnderTest = new MainWindowViewModel("test");
			clientViewModelUnderTest.proxy = Substitute.For<IHiringContract>();
			clientViewModelUnderTest.proxy.AddProject(Arg.Any<Project>()).Returns(true);
            clientViewModelUnderTest.proxy.LogIn("a", "a").ReturnsForAnyArgs(true);
            clientViewModelUnderTest.proxy.GetAllUsers().Returns(new List<User>(){new User(){Name="seselj"}});
            clientViewModelUnderTest.proxy.LogOut("a").ReturnsForAnyArgs(true);
            clientViewModelUnderTest.proxy.GetAllCompanies().Returns(new List<Company>() { new Company() { Name = "Nis" } });
            clientViewModelUnderTest.proxy.GetAllProjects().Returns(new List<Project>() { new Project() { Name = "adms" } });
            clientViewModelUnderTest.proxy.SendProject(new Company(),new Project()).ReturnsForAnyArgs(true);
            clientViewModelUnderTest.proxy.SendRequest(new Company()).ReturnsForAnyArgs(true);
        }   


        [Test]
        public void SendCompanyRequestTest1()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.SendCompanyRequestCommand.Execute(new Company()));
        }

        [Test]
        public void SendCompanyRequestTest2()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.SendCompanyRequestCommand.Execute(null));
        }

        [Test]
        public void SendProjectRequestTest1()
        {
            object[] param = { new Company(), new Project() };
            Assert.DoesNotThrow(() => clientViewModelUnderTest.SendProjectRequestCommand.Execute(param));
        }

        [Test]
        public void SendProjectRequestTest2()
        {
            object[] param = { null, null };
            Assert.DoesNotThrow(() => clientViewModelUnderTest.SendProjectRequestCommand.Execute(param));
        }

        [Test]
        public void SendProjectRequestTest3()
        {
            
            Assert.DoesNotThrow(() => clientViewModelUnderTest.SendProjectRequestCommand.Execute(null));
        }


        [Test]
        public void AddUserTest()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.AddUserCommand.Execute(new Object()));

        }
        [Test]
        public void DeleteUserTest1()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.DeleteUserCommand.Execute(new User()));
        }

        [Test]
        public void DeleteUserTest2()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.DeleteUserCommand.Execute(new User()));
        }

        [Test]
        public void EditUserProfileTest1() {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.EditUserProfileCommand.Execute(new User()));
        }
        [Test]
        public void EditUserProfileTest2()
        {
            Assert.Throws<NullReferenceException>(() => clientViewModelUnderTest.EditUserProfileCommand.Execute(null));
        }
        [Test]
        public void LogOutTest()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.LogOutCommand.Execute(new Object()));
        }
        [Test]
        public void ShowEmployeesTest()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.ShowEmployeesCommand.Execute(new Object()));
        }
        [Test]
        public void LoginClickTest1()
        {
            string[] param = { "a", "a" };
            Assert.DoesNotThrow(()=>clientViewModelUnderTest.LoginCommand.Execute(param));

        }
        [Test]
        public void LoginClickTest2()
        {
            Assert.Throws<NullReferenceException>(() => clientViewModelUnderTest.LoginCommand.Execute(null));

        }

        [Test]
        public void LoginClickTest3()
        {
            object[] param = { null, null };
            Assert.DoesNotThrow(() => clientViewModelUnderTest.LoginCommand.Execute(param));

        }
        [Test]
        public void SendProjectRequestCommandTest()
        {
            Assert.IsNotNull(clientViewModelUnderTest.SendProjectRequestCommand);
        }

        [Test]
        public void AddUserCommandTest()
        {
            Assert.IsNotNull(clientViewModelUnderTest.AddUserCommand);
        }

        [Test]
        public void DeleteUserCommandTest()
        {
            Assert.IsNotNull(clientViewModelUnderTest.DeleteUserCommand);
        }

        [Test]
        public void EditUserProfileCommandTest()
        {
            Assert.IsNotNull(clientViewModelUnderTest.EditUserProfileCommand);
        }
        [Test]
        public void NonSentProjectTest()
        {
            ObservableCollection<Project> projects = new ObservableCollection<Project>();
            clientViewModelUnderTest.NonSentProjects = projects;
            Assert.AreEqual(projects, clientViewModelUnderTest.NonSentProjects);
        }

        [Test]
        public void AcceptedProjectsTest()
        {
            ObservableCollection<Project> projects = new ObservableCollection<Project>();
            clientViewModelUnderTest.AcceptedProjects = projects;
            Assert.AreEqual(projects, clientViewModelUnderTest.AcceptedProjects);
        }

        [Test]
        public void AllEmployeesTest()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            clientViewModelUnderTest.AllEmployees = users;
            Assert.AreEqual(users, clientViewModelUnderTest.AllEmployees);
        }
        
		[Test]
		public void ConstructorTest()
		{
			Assert.DoesNotThrow(() => new MainWindowViewModel());
		}
        
		[Test]
		public void LoginCommandTest()
		{
			Assert.IsNotNull(clientViewModelUnderTest.LoginCommand);
		}

		[Test]
		public void DisplayProjectsCommandTest()
		{
			Assert.IsNotNull(clientViewModelUnderTest.DisplayProjectsCommand);
		}

		[Test]
		public void NewProjectCommandTest()
		{
			Assert.IsNotNull(clientViewModelUnderTest.NewProjectCommand);
		}

		[Test]
		public void EditProjectCommandTest()
		{
			Assert.IsNotNull(clientViewModelUnderTest.EditProjectCommand);
		}

		[Test]
		public void ShowProfileCommandTest()
		{
			Assert.IsNotNull(clientViewModelUnderTest.ShowProfileCommand);

		}

		[Test]
		public void ShowEmployeesCommandTest()
		{
			Assert.IsNotNull(clientViewModelUnderTest.ShowEmployeesCommand);

		}
		[Test]
		public void ShowCompaniesCommand()
		{
			Assert.IsNotNull(clientViewModelUnderTest.ShowCompaniesCommand);

		}



		[Test]
		public void DisplayCompaniesCommandTest()
		{
			Assert.IsNotNull(clientViewModelUnderTest.DisplayCompaniesCommand);
		}

		[Test]
		public void SendCompanyRequestCommandTest()
		{
			Assert.IsNotNull(clientViewModelUnderTest.SendCompanyRequestCommand);
		}

		[Test]
		public void ShowProjectsCommandTest()
		{
			Assert.IsNotNull(clientViewModelUnderTest.ShowProjectsCommand);

		}

		[Test]
		public void LoggedUsernameTest()
		{
			string name = "Seselj";

			clientViewModelUnderTest.LoggedUsername = name;
			Assert.AreEqual(name, clientViewModelUnderTest.LoggedUsername);

		}

		[Test]
		public void LogOutCommandTest()
		{
			Assert.IsNotNull(clientViewModelUnderTest.LogOutCommand);

		}

		[Test]
		public void CurrentStateTest()
		{

		}
		[Test]
		public void PartnerCompaniesTest()
		{
			ObservableCollection<Company> partnerCompanies = new ObservableCollection<Company>();
			clientViewModelUnderTest.PartnerCompanies = partnerCompanies;
			Assert.AreEqual(partnerCompanies, clientViewModelUnderTest.PartnerCompanies);
		}

		[Test]
		public void NonPartnerComapanies()
		{
			ObservableCollection<Company> nonPartnerCompanies = new ObservableCollection<Company>();
			clientViewModelUnderTest.NonPartnerCompanies = nonPartnerCompanies;
			Assert.AreEqual(nonPartnerCompanies, clientViewModelUnderTest.NonPartnerCompanies);
		}
		[Test]
		public void ProjectsTest()
		{
			ObservableCollection<Project> projects = new ObservableCollection<Project>();
			clientViewModelUnderTest.Projects = projects;
			Assert.AreEqual(projects, clientViewModelUnderTest.Projects);
		}

		[Test]
		public void WindowStateTest()
		{
			WindowState state = WindowState.LOGIN;
			clientViewModelUnderTest.CurrentState = state;
			Assert.AreEqual(state, clientViewModelUnderTest.CurrentState);
		}

		[Test]
		public void ShowCompaniesTest()
		{
			clientViewModelUnderTest.ShowCompaniesCommand.Execute(new Object());
			Assert.AreEqual(WindowState.COMPANIES, clientViewModelUnderTest.CurrentState);

		}

		[Test]
		public void ShowProjectsTest()
		{
			clientViewModelUnderTest.ShowProjectsCommand.Execute(new Object());
			Assert.AreEqual(WindowState.PROJECTS, clientViewModelUnderTest.CurrentState);

		}
        [Test]
        public void NewPrjectTest()
        {
           Assert.DoesNotThrow(()=> clientViewModelUnderTest.NewProjectCommand.Execute(new Object()));
        }

        //TODO rijesiti ovo, pravi pizdarije
        /*
        [Test]
        public void EditProjectTest()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.EditProjectCommand.Execute(new Project()));

        }*/
        [Test]
        public void FetchCompaniesTest()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.DisplayCompaniesCommand.Execute(new object()));

        }

        [Test]
        public void FetchProjectsTest()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.DisplayProjectsCommand.Execute(new object()));

        }


		/*
		[Test]
		public void LoginClickTest()
		{
			string[] array = { "voja", "seselj" };
			clientViewModelUnderTest.LoginCommand.Execute(array);

		}
		*/
        
		[Test]
		public void ShowProfileTest()
		{
			Assert.DoesNotThrow(()=>clientViewModelUnderTest.ShowProfileCommand.Execute(new Object()));
		}
        

        


	}
}
