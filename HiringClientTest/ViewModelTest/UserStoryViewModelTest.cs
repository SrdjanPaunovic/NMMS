using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
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
using System.Threading;

namespace HiringClientTest.ViewModelTest
{
    [TestFixture]   

  public  class UserStoryViewModelTest
    {

        private  UserStoryViewModel userStoryViewModelUnderTest;
        private UserStory userStory;
        private Common.Entities.Task taks;

        [OneTimeSetUp]
        public void SetupTest()
        {
            App.Proxy = Substitute.For<IHiringContract>();
            App.Proxy.GetTasksFromUserStory(new UserStory()).ReturnsForAnyArgs(new List<Common.Entities.Task>());
            App.Proxy.UpdateUserStory(new UserStory()).ReturnsForAnyArgs(true);
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


        [Test,RequiresSTA]
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
       
        [Test,RequiresSTA]
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
