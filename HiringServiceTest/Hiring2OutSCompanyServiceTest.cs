using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Common.Entities;
using HiringCompanyService;
using Service.Access;
using NSubstitute;
using Service;
using Common;
using System.ServiceModel;

namespace HiringServiceTest
{
	[TestFixture]
	public class Hiring2OutSCompanyServiceTest
	{
		private Hiring2OutSCompanyService serviceUnderTest;


		[OneTimeSetUp]
		public void SetupTest()
		{
			serviceUnderTest = new Hiring2OutSCompanyService();
			HiringCompanyDB.Instance = Substitute.For<IHiringCompanyDB>();

			HiringCompanyDB.Instance.AddCompany(Arg.Is<Company>(x => x.Name == "dms")).Returns(true);
			HiringCompanyDB.Instance.AddCompany(Arg.Is<Company>(x => x.Name != "dms")).Returns(false);

			HiringCompanyDB.Instance.RemoveCompany(Arg.Is<Company>(x => x.Name == "dms")).Returns(true);
			HiringCompanyDB.Instance.RemoveCompany(Arg.Is<Company>(x => x.Name != "dms")).Returns(false);

			HiringCompanyDB.Instance.ModifyCompanyToPartner(Arg.Is<Company>(x => x.Name == "dms")).Returns(true);
			HiringCompanyDB.Instance.ModifyCompanyToPartner(Arg.Is<Company>(x => x.Name != "dms")).Returns(false);

			HiringCompanyDB.Instance.AddUserStory(Arg.Is<UserStory>(x => x.Name == "us")).Returns(true);
			HiringCompanyDB.Instance.AddUserStory(Arg.Is<UserStory>(x => x.Name != "us")).Returns(false);

			HiringCompanyDB.Instance.UpdateProject(Arg.Is<Project>(x => x.Name =="adms")).Returns(true);
			HiringCompanyDB.Instance.UpdateProject(Arg.Is<Project>(x => x.Name != "adms")).Returns(false);




		}
        /*
		[Test]
		public void IntroduceTestOk()
		{
			Company company = new Company() { Name = "dms" };
			bool result = serviceUnderTest.Introduce(company);
			Assert.IsTrue(result);
		}*/

		[Test]
		public void IntroduceTestFault()
		{
			Company company = new Company() { Name = "eps" };
			Assert.Throws<NullReferenceException>(()=>serviceUnderTest.Introduce(company));
		}

		[Test]
		public void AnswerToRequestTest1()
		{
			Company company = new Company() { Name = "dms",State=Common.Entities.State.CompanyState.NoPartner};
			bool result = serviceUnderTest.AnswerToRequest(company);
			Assert.IsTrue(result);

		}

		[Test]
		public void AnswerToRequestTest2()
		{
			Company company = new Company() { Name = "dms", State = Common.Entities.State.CompanyState.Partner };

			bool result = serviceUnderTest.AnswerToRequest(company);

			Assert.IsTrue(result);
		}

		[Test]
		public void AnswerToRequestTest1Fault()
		{
			Company company = new Company() { Name = "ems", State = Common.Entities.State.CompanyState.NoPartner };
			bool result = serviceUnderTest.AnswerToRequest(company);
			Assert.IsFalse(result);

		}

		[Test]
		public void AnswerToRequestTest2Fault()
		{
			Company company = new Company() { Name = "ems", State = Common.Entities.State.CompanyState.Partner };

			bool result = serviceUnderTest.AnswerToRequest(company);

			Assert.IsFalse(result);
		}
		[Test]
		public void CloseCompanyTestOk()
		{
			Company company = new Company() { Name = "dms" };
			bool result = serviceUnderTest.CloseCompany(company);
			Assert.IsTrue(result);

		}

		[Test]
		public void CloseCompanyTestFault()
		{
			Company company = new Company() { Name = "ems" };
			bool result = serviceUnderTest.CloseCompany(company);
			Assert.IsFalse(result);
		}


		[Test]
		public void SendUserStoryTestOk()
		{
			Company company=new Company();
			UserStory userStrory=new UserStory(){Name="us"};
			Project project = new Project();

			bool result = serviceUnderTest.SendUserStory(company, userStrory, project);
			Assert.IsTrue(result);

		}

		[Test]
		public void SendUserStoryTestFault()
		{
			Company company = new Company();
			UserStory userStrory = new UserStory() { Name = "us1" };
			Project project = new Project();
			bool result = serviceUnderTest.SendUserStory(company, userStrory, project);
			Assert.IsFalse(result);
		}

		[Test]
		public void AnswerToProjectTestOk()
		{
			Company company = new Company();
			Project project = new Project() { Name = "adms" };
			bool result=serviceUnderTest.AnswerToProject(company, project);
			Assert.IsTrue(result);
		}

		[Test]
		public void AnswerToProjectTestFault()
		{
			Company company = new Company();
			Project project = new Project() { Name = "agms" };
			bool result=serviceUnderTest.AnswerToProject(company, project);
			Assert.IsFalse(result);
		}


	}
}
