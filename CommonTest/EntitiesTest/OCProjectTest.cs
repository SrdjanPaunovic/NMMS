﻿using Common.Entities;
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
        private Project proj;

        [OneTimeSetUp]
        public void SetupTest()
        {
            this.oprjectTest = new OcProject();
            this.proj = new Project();
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => new OcProject());
        }

        [Test]
        public void ConstructorTestParam()
        {
            Assert.DoesNotThrow(() => new OcProject(proj));
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
