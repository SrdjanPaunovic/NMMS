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
	public class CompanyTest
	{
		private Company companyUnderTest;

		[OneTimeSetUp]
		public void SetupTest(){
			companyUnderTest = new Company();
		}

		[Test]
		public void ConstructorTest()
		{
			Assert.DoesNotThrow(() => new User());
		}

		[Test]
		public void IdPropertyTest()
		{
			int id = 100;
			companyUnderTest.Id = id;
			Assert.AreEqual(id, companyUnderTest.Id);
		}

		[Test]
		public void NamePropertyTest()
		{
			string name = "ExportInport";
			companyUnderTest.Name = name;
			Assert.AreEqual(name, companyUnderTest.Name);

		}

		[Test]
		public void StatePropertyTest()
		{
			State.CompanyState state = Common.Entities.State.CompanyState.Partner;
			companyUnderTest.State = state;
			Assert.AreEqual(state, companyUnderTest.State);
		}
	}
}
