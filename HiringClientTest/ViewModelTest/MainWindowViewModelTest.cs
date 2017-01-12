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
	[TestFixture]
	public class MainWindowViewModelTest
	{
		private MainWindowViewModel clientViewModelUnderTest;

		[OneTimeSetUp]
		public void SetupTest()
		{
			clientViewModelUnderTest = new MainWindowViewModel("test");
			clientViewModelUnderTest.proxy = Substitute.For<IHiringContract>();


			//clientViewModelUnderTest.proxy.

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
			clientViewModelUnderTest.ShowProfileCommand.Execute(new Object());
		}


	}
}
