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

namespace HiringClientTest.ViewTest
{
    [TestFixture,RequiresSTA]
    public  class ProfileDialogTest
    {

        private ProfileDialog dialogUnderTest;

        [Test]
        public void ConstructorTest1()
        {
            Assert.DoesNotThrow(() => dialogUnderTest = new ProfileDialog());
        }

        [Test]
        public void ConstructorTest2()
        {
            Assert.DoesNotThrow(() => dialogUnderTest = new ProfileDialog("username"));
        }

    }
}
