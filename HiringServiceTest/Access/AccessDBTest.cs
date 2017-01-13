using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Access;
using System.Collections.Generic;
using System.Data.Entity;
using Common.Entities;

namespace HiringServiceTest.Access
{
	[TestFixture]
	public class AccessDBTest
	{
		private AccessDB contextUnderTest;

		
		[OneTimeSetUp]
		public void SetupTest()
		{
			this.contextUnderTest = new AccessDB();
		}

		[Test]
		public void UserPropertyTest()
		{
			Assert.DoesNotThrow(() => contextUnderTest.Users.FirstOrDefault());
		}

		[Test]
		public void CompaniesTest()
		{
			Assert.DoesNotThrow(() => contextUnderTest.Companies.FirstOrDefault());
		}

		[Test]
		public void UserStoriesTest()
		{
			Assert.DoesNotThrow(() => contextUnderTest.UserStories.FirstOrDefault());
		}
		[Test]
		public void ProjectTest()
		{
			Assert.DoesNotThrow(() => contextUnderTest.Projects.FirstOrDefault());
		}

		[Test]
		public void TasksPropertyTest()
		{
			Assert.DoesNotThrow(() => contextUnderTest.Tasks.FirstOrDefault());

		}
		

	}
}
