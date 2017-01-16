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
    public class TeamTest
    {
        private Team teamTest;

        [OneTimeSetUp]
        public void SetupTest()
        {
            this.teamTest = new Team();
        }


        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => new Team());
        }



        [Test]
        public void IdPropertyTest()
        {
            int id = 10;
            teamTest.Id = id;
            Assert.AreEqual(id, teamTest.Id);
        }

        [Test]
        public void DevelopersPropertyTest()
        {
            List<OcUser> list = new List<OcUser>();
            for (int i = 0; i < 10; i++)
            {
                OcUser ocuser = new OcUser("user" + i + 1, "password", Role.developer);
                list.Add(ocuser);
            }
            teamTest.Developers = list;
            Assert.AreEqual(list, teamTest.Developers);
        }
        [Test]
        public void TeamLeadPropertyTest()
        {
            OcUser teamlead = new OcUser() { Role = Role.TL };
            teamTest.Developers =new List<OcUser>();
            teamTest.Developers.Add(teamlead);
            teamTest.TeamLead = teamlead;
            Assert.AreEqual(teamlead, teamTest.TeamLead);
        }
        
        [Test]
        public void ProjectsPropertyTest()
        {
            List<OcProject> list = new List<OcProject>();
            for (int i = 0; i < 10; i++)
            {
                OcProject ocProject = new OcProject();
                list.Add(ocProject);
            }
            teamTest.Projects = list;
            Assert.AreEqual(list, teamTest.Projects);

        }
    }
}
