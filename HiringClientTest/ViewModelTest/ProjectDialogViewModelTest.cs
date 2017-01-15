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

namespace HiringClientTest.ViewModelTest
{
    [TestFixture, RequiresSTA]   
  public   class ProjectDialogViewModelTest
    {

        private ProjectDialogViewModel projectDialogUnderTest;
        private UserStory userStory;
        private Project project;

        [OneTimeSetUp]
        public void SetupTest()
        {
            ProjectDialogViewModel.proxy = Substitute.For<IHiringContract>();
            ProjectDialogViewModel.proxy.AddUser(new User()).ReturnsForAnyArgs(true);
            ProjectDialogViewModel.proxy.GetUserStoryFromProject(new Project()).ReturnsForAnyArgs(new List<UserStory>());
            App.Proxy = Substitute.For<IHiringContract>();
            App.Proxy.UpdateProject(new Project()).ReturnsForAnyArgs(true);
            App.Proxy.AddProject(new Project()).ReturnsForAnyArgs(false);

            project = new Project();
            userStory = new UserStory();
            project.UserStories.Add(userStory);
            projectDialogUnderTest = new ProjectDialogViewModel(project);

            

        }


        [Test]
        public void ConstructorTest1()
        {
            Assert.DoesNotThrow(() => new ProjectDialogViewModel());
        }
        [Test,RequiresSTA]
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
            projectDialogUnderTest.IsEditing = false;
            Assert.DoesNotThrow(() => projectDialogUnderTest.SaveCommand.Execute(userControl));

        }

        [Test]
        public void AddStoryTest1()
        {
         Assert.DoesNotThrow(()=> projectDialogUnderTest.AddStoryCommand.Execute("us1"));

        }
        [Test]
        public void AddStoryTest2()
        {
           Assert.DoesNotThrow(()=>projectDialogUnderTest.AddStoryCommand.Execute(String.Empty));

        }

        [Test]
        public void EditUserStoryTest()
        {
            UserStory param = new UserStory();
             Assert.DoesNotThrow(()=>projectDialogUnderTest.EditStoryCommand.Execute(param));
        }


        [Test]
        public void DeleteUserStoryTest()
        {
            Assert.DoesNotThrow(() => projectDialogUnderTest.DeleteStoryCommand.Execute(userStory));
        }
    }
}
