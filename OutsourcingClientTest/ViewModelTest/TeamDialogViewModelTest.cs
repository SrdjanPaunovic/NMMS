using System.Threading.Tasks;
using NSubstitute;
using NUnit;
using Common;
using NUnit.Framework;
using Client.ViewModel;
using ServiceContract;
using Common.Entities;
using Client;
using System.Windows.Controls;
using System.Windows;
using System.Threading;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

namespace OutsourcingClientTest.ViewModelTest
{
    public class TeamDialogViewModelTest
    {

        private TeamDialogViewModel teamDialogUnderTest;

        [OneTimeSetUp]
        public void SetupTest()
        {
            App.proxy = Substitute.For<IOutsourcingContract>();
            App.proxy.UpdateProject(new OcProject()).ReturnsForAnyArgs(true);
            App.proxy.AddProject(new OcProject()).ReturnsForAnyArgs(false);
            App.proxy.GetUserStoryFromProject(new OcProject()).ReturnsForAnyArgs(new List<UserStory>());
            App.proxy.GetProjectFromUserStory(new UserStory()).ReturnsForAnyArgs(new OcProject());
            App.proxy.AddUser(new OcUser()).ReturnsForAnyArgs(true);
            App.proxy.AddTeam(new Team()).ReturnsForAnyArgs(true);
            App.proxy.GetAllUsersWithoutTeam().Returns(new List<OcUser>() { new OcUser() { Role = Role.developer }, new OcUser() { Role = Role.TL } });
            teamDialogUnderTest = new TeamDialogViewModel();
            teamDialogUnderTest.Team.Developers = new List<OcUser>() { new OcUser() { Role = Role.developer } };
            
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => new TeamDialogViewModel());

        }

        [Test]
        public void DevelopersPropertyTest()
        {
            ObservableCollection<OcUser> developers = new ObservableCollection<OcUser>();
            teamDialogUnderTest.Developers = developers;
            Assert.AreEqual(developers, teamDialogUnderTest.Developers);

        }


        [Test]
        public void TeamLeadsPropertyTest()
        {
            ObservableCollection<OcUser> teamLeads = new ObservableCollection<OcUser>();
            teamDialogUnderTest.TeamLeads = teamLeads;
            Assert.AreEqual(teamLeads, teamDialogUnderTest.TeamLeads);

        }


        [Test]
        public void TeamDevelopersPropertyTest()
        {
            ObservableCollection<OcUser> teamDevelopers = new ObservableCollection<OcUser>();
            teamDialogUnderTest.TeamDevelopers = teamDevelopers;
            Assert.AreEqual(teamDevelopers, teamDialogUnderTest.TeamDevelopers);

        }
        

        [Test]
        public void TeamLeadPropertyTest()
        {
            OcUser lead = new OcUser();
            teamDialogUnderTest.TeamLead = lead;
            Assert.AreEqual(lead, teamDialogUnderTest.TeamLead);
        }

        [Test]
        public void TeamPropertyTest()
        {
            Team team = new Team();
            teamDialogUnderTest.Team = team;
            Assert.AreEqual(team, teamDialogUnderTest.Team);
        }

        [Test]
        public void CloseCommandProperty()
        {
            Assert.IsNotNull(teamDialogUnderTest.CloseCommand);
        }

        [Test]
        public void SaveCommandProperty()
        {
            Assert.IsNotNull(teamDialogUnderTest.SaveCommand);
        }


        [Test,RequiresSTA]
        public void CloseTest()
        {
            Window win = new Window();
            Assert.DoesNotThrow(() => teamDialogUnderTest.CloseCommand.Execute(win));
        }

        [Test, RequiresSTA]
        public void SaveTest()
        {
            teamDialogUnderTest.TeamDevelopers = new ObservableCollection<OcUser>() { new OcUser() { Role = Role.developer } };
            Window param = new Window();
            teamDialogUnderTest.TeamLead = new OcUser() { Role = Role.TL };
            Assert.DoesNotThrow(() => teamDialogUnderTest.SaveCommand.Execute(param));
        }


    }
}
