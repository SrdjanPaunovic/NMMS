using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
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
		public void DevelopersPropertyChanged()
		{
			List<User> developers = new List<User>();
			developers.Add(new User());
			projectUnderTest.Develpers = developers;
			Assert.AreEqual(developers, projectUnderTest.Develpers);
		}

	}
}
