using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Common.Entities;


namespace CommonTest.EntitiesTest
{
    [TestFixture]
    public class UserStoryTest
    {
        private UserStory userStoryUnderTest;

        [OneTimeSetUp]
        public void SetupTest()
        {
            userStoryUnderTest = new UserStory();

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
            userStoryUnderTest.Id = id;
            Assert.AreEqual(id, userStoryUnderTest.Id);
        }

        [Test]
        public void NamePropertyTest()
        {
            string name = " user story";
            userStoryUnderTest.Name = name;
            Assert.AreEqual(name, userStoryUnderTest.Name);
        }

        [Test]
        public void AcceptanceCriteriaPropertyTest()
        {
            string acceptanceCriteria = " 1.  2.  4.";
            userStoryUnderTest.AcceptanceCriteria = acceptanceCriteria;
            Assert.AreEqual(acceptanceCriteria, userStoryUnderTest.AcceptanceCriteria);
        }

        [Test]
        public void StartTimePropertyTest()
        {
            DateTime startTime = DateTime.Now;
            userStoryUnderTest.StartTime = startTime;
            Assert.AreEqual(startTime, userStoryUnderTest.StartTime);
        }

        [Test]
        public void EndTimePropertyTest()
        {
            DateTime endTime = DateTime.Now;
            userStoryUnderTest.EndTime = endTime;
            Assert.AreEqual(endTime, userStoryUnderTest.EndTime);
        }

        [Test]
        public void ProjectPropertyTest()
        {
            Project project = new Project();
            userStoryUnderTest.Project = project;
            Assert.AreEqual(project, userStoryUnderTest.Project);
        }

        [Test]
        public void StatePropertyTest()
        {
            StoryState state = StoryState.Active;
            userStoryUnderTest.State = state;
            Assert.AreEqual(state, userStoryUnderTest.State);
        }

    }
}
