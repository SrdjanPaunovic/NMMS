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
    public class TaskTest
    {
        private Common.Entities.Task taskTest;

        [OneTimeSetUp]
        public void SetupTest()
        {
            this.taskTest = new Common.Entities.Task();
        }


        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => new Common.Entities.Task());
        }



        [Test]
        public void IdPropertyTest()
        {
            int id = 10;
            taskTest.Id = id;
            Assert.AreEqual(id, taskTest.Id);
        }


        [Test]
        public void UserStoryPropertyTest()
        {
            UserStory userStory = new UserStory();
            taskTest.UserStory = userStory;
            Assert.AreEqual(userStory, taskTest.UserStory);
        }



        [Test]
        public void NamePropertyTest()
        {
            string name = "NewTask";
            taskTest.Name = name;
            Assert.AreEqual(name, taskTest.Name);
        }

        [Test]
        public void DescriptionPropertyTest()
        {
            string desc = "Desc Task";
            taskTest.Description = desc;
            Assert.AreEqual(desc, taskTest.Description);

        }

    }
}
