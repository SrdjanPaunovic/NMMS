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
using ServiceContract;
using NSubstitute;
using Client.ViewModel;


namespace HiringClientTest.ViewTest
{
    [TestFixture]
    public class ProjectViewDialogTest
    {

        private ProjectViewDialog dialogUnderTest;

        [OneTimeSetUp]
        public void SetupTest()
        {
            ProjectDialogViewModel.proxy = Substitute.For<IHiringContract>();

            ProjectDialogViewModel.proxy.GetUserStoryFromProject(new Project()).ReturnsForAnyArgs(new List<UserStory>());

        }
        [Test, RequiresSTA]
        public void ConstructorTest1()
        {
            Assert.DoesNotThrow(() => dialogUnderTest = new ProjectViewDialog());

        }

        
        [Test,RequiresSTA]
        public void ConstructorTest2()
        {
            Assert.DoesNotThrow(() => dialogUnderTest = new ProjectViewDialog(new Project()));

        }


    }
}
