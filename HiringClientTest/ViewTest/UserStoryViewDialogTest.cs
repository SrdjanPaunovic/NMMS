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
using Client.View;
using Client.ViewModel;


namespace HiringClientTest.ViewTest
{
    [TestFixture, RequiresSTA]

    public class UserStoryViewDialogTest
    {
        private UserStoryViewDialog dialogUnderTest;


        [OneTimeSetUp]
        public void SetupTest()
        {
            UserStoryViewModel.proxy = Substitute.For<IHiringContract>();
            UserStoryViewModel.proxy.GetTasksFromUserStory(new UserStory()).ReturnsForAnyArgs(new List<Common.Entities.Task>() { new Common.Entities.Task() { Name = "task1" } });
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => dialogUnderTest = new UserStoryViewDialog(new UserStory()));
        }
    }
}
