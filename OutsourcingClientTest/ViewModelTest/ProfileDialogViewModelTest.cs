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
using System.Windows.Controls;
using System.Windows;

namespace OutsourcingClientTest.ViewModelTest
{
    [TestFixture, RequiresSTA]

    public class ProfileDialogViewModelTest
    {
        ProfileDialogViewModel profileDialogUnderTest;

        [OneTimeSetUp]
        public void SetupTest()
        {
            profileDialogUnderTest = new ProfileDialogViewModel();
            profileDialogUnderTest.proxy = Substitute.For<IOutsourcingContract>();
            profileDialogUnderTest.proxy.AddUser(new OcUser()).ReturnsForAnyArgs(true);
            App.proxy = Substitute.For<IOutsourcingContract>();

        }
        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => new ProfileDialogViewModel());
        }

        [Test]
        public void SaveCommandTest1()
        {
            UserControl userControl = new UserControl();
            Window parentWindow = new Window();
            parentWindow.Content = userControl;
            profileDialogUnderTest.User.Id = 0;
            Assert.Throws<InvalidOperationException>(() => profileDialogUnderTest.SaveCommand.Execute(userControl));

        }


        [Test]
        public void SaveCommandTest2()
        {
            profileDialogUnderTest.User.Id = 1;
            object param = new object();
            UserControl userControl = new UserControl();
            Window parentWindow = new Window();
            parentWindow.ShowDialog();
            parentWindow.Content = userControl;

            Assert.DoesNotThrow(() => profileDialogUnderTest.SaveCommand.Execute(userControl));

        }
    }
}
