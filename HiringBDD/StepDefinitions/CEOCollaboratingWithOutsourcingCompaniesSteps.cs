using Client;
using Common;
using Common.Entities;
using NSubstitute;
using NUnit.Framework;
using Service;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using TechTalk.SpecFlow;

namespace HiringBDD.StepDefinitions
{
	[Binding]
	public class CEOCollaboratingWithOutsourcingCompaniesSteps
	{
		private static HiringClientProxy proxy = new HiringClientProxy(new NetTcpBinding(), "net.tcp://localhost:4000/IHiringContract");
		private static List<Company> companies;

		[Given(@"CEO exists")]
		public void GivenCEOExists()
		{
			User ceo = proxy.GetUser("tesla");
			if (ceo == null)
			{
				proxy.AddUser(new User("tesla", "tesla", Role.CEO));
			}
		}

		[Given(@"Non-partner company exist")]
		public void GivenNon_PartnerCompanyExist()
		{
			proxy.RemoveAllCompanies();
			string baseAddress = "net.tcp://localhost:8000/Service";
			var factory = new DuplexChannelFactory<IHiring2OutSourceContract>(new InstanceContext(new OutSurce2HiringProxy()), new NetTcpBinding(SecurityMode.None), new EndpointAddress(baseAddress));
			IHiring2OutSourceContract proxy_c = factory.CreateChannel();
			proxy_c.Introduce(new Company("test"));
		}

		[Given(@"CEO get list of non-partner companies")]
		public void GivenCEOGetListOfNon_PartnerCompanies()
		{
			companies = proxy.GetAllCompanies();
			Assert.AreNotEqual(null, companies, "GetAllCompanies");
		}

		[When(@"CEO send request to one")]
		public void WhenCEOSendRequestToOne()
		{
			Company company = companies[0];
			Assert.AreNotEqual(null, company);
			proxy.SendRequest(company);
		}

		[Then(@"Request should be successfully sent")]
		public void ThenRequestShouldBeSuccessfullySent()
		{
			companies = proxy.GetAllCompanies();
			Assert.AreEqual(State.CompanyState.Requested, companies[0].State);
		}

		[Given(@"new project is created")]
		public void GivenNewProjectIsCreated()
		{
			Project newProject = new Project();
			newProject.Name = "testic";
			proxy.AddProject(newProject);
		}

		[When(@"CEO approve project")]
		public void WhenCEOApproveProject()
		{
			
		}

		[Then(@"Project should be successfully approved")]
		public void ThenProjectShouldBeSuccessfullyApproved()
		{
			ScenarioContext.Current.Pending();
		}
	}
}
