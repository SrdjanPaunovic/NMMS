using Common.Entities;
using NSubstitute;
using NUnit.Framework;
using Service;
using Service.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsourcingServiceTest
{
	[TestFixture]
	public class OutsourcingServiceTest
	{
		private OutsourcingCompanyService serviceUnderTest;

		static List<Team> expectedTeams = new List<Team> { new Team() { Name = "T1", Id = 1 }, new Team() { Name = "T2", Id = 2 } };
		static List<OcUser> expectedUsersWithoutTeam = new List<OcUser> { new OcUser() { Id = 1, Name = "Meca" } };
		static List<OcUser> expectedUsers = new List<OcUser> { new OcUser() { Id = 1, Name = "Zeki" }, new OcUser() { Id = 2, Name = "Srki" }, new OcUser() { Id = 3, Name = "Miki" }, new OcUser() { Id = 4, Name = "Maki" } };

		[OneTimeSetUp]
		public void SetupTest()
		{
			serviceUnderTest = new OutsourcingCompanyService();
			OutsourcingCompanyDB.Instance = Substitute.For<IOutsourcingCompanyDB>();

			OutsourcingCompanyDB.Instance.GetAllTeams().Returns(expectedTeams);
			OutsourcingCompanyDB.Instance.GetAllUsersWithoutTeam().Returns(expectedUsersWithoutTeam);
			OutsourcingCompanyDB.Instance.GetAllUsers().Returns(expectedUsers);
			OutsourcingCompanyDB.Instance.AddTeam(Arg.Is<Team>(x => x.Name == "RocketTeam")).Returns(true);
		}

		[Test]
		public void GetAllTeams()
		{
			List<Team> actualTeams = serviceUnderTest.GetAllTeams();
			Assert.AreEqual(expectedTeams[0].Name, actualTeams[0].Name);
		}

		[Test]
		public void AddTeam()
		{
			bool success = serviceUnderTest.AddTeam(new Team() { Name = "RocketTeam" });
			Assert.AreEqual(success, true);
		}

		[Test]
		public void GetAllUsersWithoutTeam()
		{
			List<OcUser> actualUsersWithoutTeam = serviceUnderTest.GetAllUsersWithoutTeam();
			Assert.AreEqual(expectedUsersWithoutTeam[0].Name, actualUsersWithoutTeam[0].Name);
		}
	}
}
