using NUnit.Framework;
using Service.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringServiceTest.Access
{
	[TestFixture]
	public class ConfigurationTest
	{

		[Test]
		public void ConstructorTest()
		{
			Assert.DoesNotThrow(() => new Configuration());
		}
	}
}
