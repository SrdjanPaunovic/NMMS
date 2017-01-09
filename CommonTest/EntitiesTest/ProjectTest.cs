﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Common.Entities;

namespace CommonTest.EntitiesTest
{
	[TestFixture]
	public class ProjectTest
	{
		private Project projectUnderTest;

		[OneTimeSetUp]
		public void SetupTest()
		{
			projectUnderTest = new Project();
		}

		[Test]
		public void ConstructorTest()
		{
			Assert.DoesNotThrow(() => new Project());
		}

		[Test]
		public void IdPropertyTest()
		{
			int id = 1;
			projectUnderTest.Id = id;
			Assert.AreEqual(id, projectUnderTest.Id);
		}
        [Test]
        public void ProductOwnerPropertyTest()
        {
            User po = new User();
            po.Role = Role.PO;
            projectUnderTest.ProductOwner = po;
            Assert.AreEqual(po, projectUnderTest.ProductOwner);
        }

        [Test]
        public void NamePropertyTest()
        {
            string projectName = "extra project";
            projectUnderTest.Name = projectName;
            Assert.AreEqual(projectName, projectUnderTest.Name);
        }

        [Test]
        public void DescriptionPropertyTest()
        {
            string description = "description...";
            projectUnderTest.Description = description;
            Assert.AreEqual(description, projectUnderTest.Description);
        }

        [Test]
        public void StartTimePropertyTest()
        {
            DateTime startTime = DateTime.Now;
            projectUnderTest.StartTime = startTime;
            Assert.AreEqual(startTime, projectUnderTest.StartTime);
        }

        [Test]
        public void EndTimePropertyTest()
        {
            DateTime endTime = DateTime.Now;
            projectUnderTest.EndTime = endTime;
            Assert.AreEqual(endTime, projectUnderTest.EndTime);
        }

        [Test]
        public void DevelopCompanyPropertyTest()
        {
            Company developCompany = new Company();
            projectUnderTest.DevelopCompany = developCompany;
            Assert.AreEqual(developCompany, projectUnderTest.DevelopCompany);
        }

        [Test]
        public void UserStoriesPropertyTest()
        {
            List<UserStory> userStories = new List<UserStory>();
            userStories.Add(new UserStory());
            userStories.Add(new UserStory());
            foreach(var us in userStories)
            {
                projectUnderTest.UserStories.Add(us);

            }
            Assert.AreEqual(userStories, projectUnderTest.UserStories);
        }
		

	}
}
