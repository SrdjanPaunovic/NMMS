﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace OutsourcingClientTest.ViewModelTest
{
    [TestFixture, RequiresSTA]   

    public class ProjectDialogViewModelTest
    {
        private ProjectDialogViewModel projectDialogUnderTest;
        private UserStory userStory;
        private OcProject project;



        [OneTimeSetUp]
        public void SetupTest()
        {
            ProjectDialogViewModel.Proxy = Substitute.For<IOutsourcingContract>();
            ProjectDialogViewModel.Proxy.AddUser(new OcUser()).ReturnsForAnyArgs(true);
            ProjectDialogViewModel.Proxy.GetUserStoryFromProject(new OcProject()).ReturnsForAnyArgs(new List<UserStory>());
            App.Proxy = Substitute.For<IOutsourcingContract>();
            App.Proxy.UpdateProject(new OcProject()).ReturnsForAnyArgs(true);
            App.Proxy.AddProject(new OcProject()).ReturnsForAnyArgs(false);
            App.Proxy.GetUserStoryFromProject(new OcProject()).ReturnsForAnyArgs(new List<UserStory>());
            App.Proxy.GetProjectFromUserStory(new UserStory()).ReturnsForAnyArgs(new OcProject());
            App.Proxy.AddUser(new OcUser()).ReturnsForAnyArgs(true);
            App.Proxy.GetAllUsersWithoutTeam().Returns(new List<OcUser>() { new OcUser() { Role = Role.developer }, new OcUser() { Role = Role.TL } });
            project = new OcProject();
            userStory = new UserStory();
            project.UserStories.Add(userStory);
            projectDialogUnderTest = new ProjectDialogViewModel(project);



        }


        [Test]
        public void ConstructorTest1()
        {
            Assert.DoesNotThrow(() => new ProjectDialogViewModel());
        }
        [Test, RequiresSTA]
        public void ConstructorTest2()
        {
            Assert.DoesNotThrow(() => new ProjectDialogViewModel(project));

        }

       
        [Test]
        public void SaveCommandTest1()
        {
            UserControl userControl = new UserControl();
            Window parentWindow = new Window();
            parentWindow.Content = userControl;

            Assert.DoesNotThrow(() => projectDialogUnderTest.SaveCommand.Execute(userControl));

        }


        [Test]
        public void SaveCommandTest2()
        {
            UserControl userControl = new UserControl();
            Window parentWindow = new Window();
            parentWindow.ShowDialog();
            parentWindow.Content = userControl;

            Assert.DoesNotThrow(() => projectDialogUnderTest.SaveCommand.Execute(userControl));

        }

        [Test]
        public void SaveCommandTest3()
        {
            UserControl userControl = new UserControl();
            Window parentWindow = new Window();
            parentWindow.ShowDialog();
            parentWindow.Content = userControl;
            Assert.DoesNotThrow(() => projectDialogUnderTest.SaveCommand.Execute(userControl));

        }

        [Test]
        public void AddStoryTest1()
        {
            Assert.DoesNotThrow(() => projectDialogUnderTest.AddStoryCommand.Execute("us1"));

        }
        [Test]
        public void AddStoryTest2()
        {
            Assert.DoesNotThrow(() => projectDialogUnderTest.AddStoryCommand.Execute(String.Empty));

        }

        [Test]
        public void EditUserStoryTest()
        {
            UserStory param = new UserStory();
            Assert.DoesNotThrow(() => projectDialogUnderTest.EditStoryCommand.Execute(param));
        }


        [Test]
        public void DeleteUserStoryTest()
        {
            Assert.DoesNotThrow(() => projectDialogUnderTest.DeleteStoryCommand.Execute(userStory));
        }
    }
}
