﻿using System.Threading.Tasks;
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

namespace OutsourcingClientTest.ViewModelTest
{
    [TestFixture]   

   public  class UserStoryViewModelTest
    {


        private UserStoryViewModel userStoryViewModelUnderTest;
        private UserStory userStory;
        private Common.Entities.Task taks;

        [OneTimeSetUp]
        public void SetupTest()
        {
            App.Proxy = Substitute.For<IOutsourcingContract>();
            App.Proxy.GetTasksFromUserStory(new UserStory()).ReturnsForAnyArgs(new List<Common.Entities.Task>());
            App.Proxy.UpdateUserStory(new UserStory()).ReturnsForAnyArgs(true);
            App.Proxy.GetProjectFromUserStory(new UserStory()).ReturnsForAnyArgs(new OcProject());
            App.Proxy.GetTasksFromUserStory(new UserStory()).ReturnsForAnyArgs(new List<Common.Entities.Task>(){new Common.Entities.Task()});
            UserStory userStory = new UserStory() { Name = "testUs" };
            taks = new Common.Entities.Task();
            userStory.Tasks.Add(taks);
            userStoryViewModelUnderTest = new UserStoryViewModel(userStory);
        }

        [Test]
        public void SaveCommandPropertyTest()
        {
            Assert.IsNotNull(userStoryViewModelUnderTest.CancelCommand);
        }

        [Test]
        public void AddTaskCommandPropertyTest()
        {
            Assert.IsNotNull(userStoryViewModelUnderTest.AddTaskCommand);
        }

        [Test]
        public void CancelCommandPropertyTest()
        {
            Assert.IsNotNull(userStoryViewModelUnderTest.SaveCommand);
        }


        [Test, RequiresSTA]
        public void SaveCommandTest1()
        {
            UserControl userControl = new UserControl();
            Window parentWindow = new Window();
            parentWindow.Content = userControl;

            Assert.DoesNotThrow(() => userStoryViewModelUnderTest.SaveCommand.Execute(userControl));

        }

        [Test, RequiresSTA]
        public void SaveCommandTest2()
        {
            userStoryViewModelUnderTest.UserStory.Id = 2;
            UserControl userControl = new UserControl();
            Window parentWindow = new Window();
            parentWindow.Content = userControl;

            Assert.DoesNotThrow(() => userStoryViewModelUnderTest.SaveCommand.Execute(userControl));

        }

        [Test, RequiresSTA]
        public void CancelCommandTest1()
        {
            UserControl userControl = new UserControl();
            Window parentWindow = new Window();

            parentWindow.Content = userControl;
            parentWindow.ShowDialog();

            Assert.DoesNotThrow(() => userStoryViewModelUnderTest.CancelCommand.Execute(userControl));

        }

        [Test]
        public void AddTaskTest1()
        {
            string param = "name";
            Assert.DoesNotThrow(() => userStoryViewModelUnderTest.AddTaskCommand.Execute(param));
        }

        [Test]
        public void AddTaskTest2()
        {
            string param = String.Empty;
            Assert.DoesNotThrow(() => userStoryViewModelUnderTest.AddTaskCommand.Execute(param));
        }

        [Test]
        public void DeleteTaskTest()
        {
            Assert.DoesNotThrow(() => userStoryViewModelUnderTest.DeleteTaskCommand.Execute(taks));

        }
    }
}
