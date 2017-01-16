using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NSubstitute;
using NUnit.Framework;
using Client.ViewModel;
using ServiceContract;
using Common.Entities;
using System.Collections.ObjectModel;
using Client;

namespace OutsourcingClientTest.ViewModelTest
{
    [TestFixture, RequiresSTA]
    public class MainWindowViewModelTest
    {
        private MainWindowViewModel clientViewModelUnderTest;

        [OneTimeSetUp]
        public void SetupTest()
        {
            clientViewModelUnderTest = new MainWindowViewModel("test");
            clientViewModelUnderTest.Proxy = Substitute.For<IOutsourcingContract>();
            clientViewModelUnderTest.Proxy.AddProject(Arg.Any<OcProject>()).Returns(true);
            clientViewModelUnderTest.Proxy.LogIn("a", "a").ReturnsForAnyArgs(true);
            clientViewModelUnderTest.Proxy.GetAllUsers().Returns(new List<OcUser>() { new OcUser() { Name = "seselj" } });
            clientViewModelUnderTest.Proxy.LogOut("a").ReturnsForAnyArgs(true);
            clientViewModelUnderTest.Proxy.GetAllCompanies().Returns(new List<Company>() { new Company() { Name = "Nis" } });
            clientViewModelUnderTest.Proxy.GetAllProjects().Returns(new List<OcProject>() { new OcProject() { Name = "adms" } });
            clientViewModelUnderTest.LoggedUser = new OcUser() { Username = "slobo", Password = "slobo", Name = "Slobo", Surname = "Milosevic" };
            clientViewModelUnderTest.Proxy.AnswerToRequest(new Company()).ReturnsForAnyArgs(true);
            clientViewModelUnderTest.Proxy.GetProjectFromUserStory(new UserStory()).ReturnsForAnyArgs(new OcProject());
            clientViewModelUnderTest.Proxy.GetAllUsersWithoutTeam().ReturnsForAnyArgs(new List<OcUser>() { new OcUser() });
            //App.proxy = Substitute.For<IOutsourcingContract>();
            App.LoggedUser = new OcUser() { Username = "slobo", Password = "slobo", Name = "Slobo", Surname = "Milosevic" };
            
           
        }

        [Test]
        public void UpdateData1()
        {
            clientViewModelUnderTest.CurrentState = MainWindowViewModel.WindowState.COMPANIES;
            Assert.DoesNotThrow(()=>clientViewModelUnderTest.UpdateData());
        }

        [Test]
        public void UpdateData2()
        {
            clientViewModelUnderTest.CurrentState = MainWindowViewModel.WindowState.EMPLOYEES;
            Assert.DoesNotThrow(() => clientViewModelUnderTest.UpdateData());
        }

        [Test]
        public void UpdateData3()
        {
            clientViewModelUnderTest.CurrentState = MainWindowViewModel.WindowState.LOGIN;
            Assert.DoesNotThrow(() => clientViewModelUnderTest.UpdateData());
        }

        [Test]
        public void UpdateData4()
        {
            clientViewModelUnderTest.CurrentState = MainWindowViewModel.WindowState.PROJECTS;
            Assert.DoesNotThrow(() => clientViewModelUnderTest.UpdateData());
        }

        [Test]
        public void UpdateData5()
        {
            clientViewModelUnderTest.CurrentState = MainWindowViewModel.WindowState.TEAMS;
            Assert.DoesNotThrow(() => clientViewModelUnderTest.UpdateData());
        }

        [Test]
        public void TeamsPropertyTest()
        {
            ObservableCollection<Team> teams = new ObservableCollection<Team>();
            clientViewModelUnderTest.Teams = teams;
            Assert.AreEqual(teams, clientViewModelUnderTest.Teams);
        }


        [Test]
        public void ProjectsPropertyTest()
        {
            ObservableCollection<Project> projects = new ObservableCollection<Project>();
            clientViewModelUnderTest.Projects = projects;
            Assert.AreEqual(projects, clientViewModelUnderTest.Projects);
        }

        [Test]
        public void NonSentUSPropertyTest()
        {
            ObservableCollection<UserStory> nonSentUSjects = new ObservableCollection<UserStory>();
            clientViewModelUnderTest.NonSentUS = nonSentUSjects;
            Assert.AreEqual(nonSentUSjects, clientViewModelUnderTest.NonSentUS);
        }



        [Test]
        public void ShowTeamsCommandTest()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.ShowTeamsCommand.Execute(new Object()));
        }

        [Test]
        public void AcceptCompanyRequestCommandTest1()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.AcceptCompanyRequestCommand.Execute(new Company()));
        }

        [Test]
        public void AcceptCompanyRequestCommandTest2()
        {
            Assert.Throws<NullReferenceException>(() => clientViewModelUnderTest.AcceptCompanyRequestCommand.Execute(null));
        }

        [Test]
        public void RejectCompanyRequestCommandTest()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.RejectCompanyRequestCommand.Execute(new Company ()));
        }


        //****
        [Test]
        public void AcceptProjectRequestCommandTest()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.AcceptProjectRequestCommand.Execute(new OcProject()));
        }

        [Test]
        public void RejectProjectRequestCommandTest()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.RejectProjectRequestCommand.Execute(new OcProject()));
        }

        [Test]
        public void UserStoryRequestCommandTest()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.UserStoryRequestCommand.Execute(new UserStory()));
        }



        [Test]
        public void AddUserTest()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.AddUserCommand.Execute(new Object()));

        }
        [Test]
        public void DeleteUserTest1()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.DeleteUserCommand.Execute(new OcUser()));
        }

        [Test]
        public void DeleteUserTest2()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.DeleteUserCommand.Execute(new OcUser()));
        }

        [Test]
        public void EditUserProfileTest1()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.EditUserProfileCommand.Execute(new OcUser()));
        }
        
        [Test]
        public void LogOutTest()
        {
            App.LoggedUser = new OcUser() { Username = "slobo", Password = "slobo", Name = "Slobo", Surname = "Milosevic" };

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
            Assert.DoesNotThrow(() => clientViewModelUnderTest.LoginCommand.Execute(param));

        }
        [Test]
        public void LoginClickTest2()
        {

            Assert.Throws<ArgumentNullException>(() => clientViewModelUnderTest.LoginCommand.Execute(null));

        }

        [Test]
        public void LoginClickTest3()
        {
            object[] param = { null, null };
            Assert.Throws<NullReferenceException>(() => clientViewModelUnderTest.LoginCommand.Execute(param));

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
        public void AcceptedProjectsTest()
        {
            ObservableCollection<OcProject> projects = new ObservableCollection<OcProject>();
            clientViewModelUnderTest.AcceptedProjects = projects;
            Assert.AreEqual(projects, clientViewModelUnderTest.AcceptedProjects);
        }

        [Test]
        public void AllEmployeesTest()
        {
            ObservableCollection<OcUser> users = new ObservableCollection<OcUser>();
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
        public void ShowProjectsCommandTest()
        {
            Assert.IsNotNull(clientViewModelUnderTest.ShowProjectsCommand);

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
        /*
        [Test]
        public void ProjectsTest()
        {
            ObservableCollection<OcProject> projects = new ObservableCollection<OcProject>();
            clientViewModelUnderTest.Projects = projects;
            Assert.AreEqual(projects, clientViewModelUnderTest.Projects);
        }
        */
        [Test]
        public void WindowStateTest()
        {
            MainWindowViewModel.WindowState state = MainWindowViewModel.WindowState.LOGIN;
            clientViewModelUnderTest.CurrentState = state;
            Assert.AreEqual(state, clientViewModelUnderTest.CurrentState);
        }

        [Test]
        public void ShowCompaniesTest()
        {
            clientViewModelUnderTest.ShowCompaniesCommand.Execute(new Object());
            Assert.AreEqual(MainWindowViewModel.WindowState.COMPANIES, clientViewModelUnderTest.CurrentState);

        }

        [Test]
        public void ShowProjectsTest()
        {
            clientViewModelUnderTest.ShowProjectsCommand.Execute(new Object());
            Assert.AreEqual(MainWindowViewModel.WindowState.PROJECTS, clientViewModelUnderTest.CurrentState);

        }
        [Test]
        public void NewPrjectTest()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.NewProjectCommand.Execute(new Object()));
        }

       
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




        [Test]
        public void ShowProfileTest()
        {
            Assert.DoesNotThrow(() => clientViewModelUnderTest.ShowProfileCommand.Execute(new Object()));
        }
        

        
    }
}
