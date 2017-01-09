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
    public class OcProjectTest
    {
        private OcProject oprjectTest;

        [OneTimeSetUp]
        public void SetupTest()
        {
            this.oprjectTest = new OcProject();
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => new OcProject());
        }

        [Test]
        public void TeamPropertyTest()
        {
            Team team = new Team();
            oprjectTest.Team = team;
            Assert.AreEqual(team, oprjectTest.Team);
        }
    }
}
