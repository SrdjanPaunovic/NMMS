using Common.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTest.EntitiesTest
{
    [TestFixture]
    public class OcUserTest
    {
        private OcUser ocUserUderTest;

        [OneTimeSetUp]
        public void SetupTest()
        {
            this.ocUserUderTest = new OcUser();
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => new OcUser());
        }

        [Test]
        public void ConstructorTestWithParams()
        {
            Assert.DoesNotThrow(() => new OcUser("ocuser","password",Role.CEO));
        }

        [Test]
        public void TeamPropertyTest()
        {
            Team team = new Team();
            ocUserUderTest.Team = team;
            Assert.AreEqual(team, ocUserUderTest.Team);
        }
    }
}
