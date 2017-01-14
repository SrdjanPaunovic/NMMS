using Client;
using Common.Entities;
using NUnit.Framework;
using Service.Access;
using System;
using System.ServiceModel;
using TechTalk.SpecFlow;

namespace HiringBDD.StepDefinitions
{
	[Binding]
	public class LoginSteps
	{
		private string username;
		private string password;
		private static bool result;

		[Given(@"User admin exist")]
		public void GivenUserAdminExist()
		{
			HiringClientProxy proxy = new HiringClientProxy(new NetTcpBinding(), "net.tcp://localhost:4000/IHiringContract");
			User admin = proxy.GetUser("admin");
			if(admin != null)
			{
				proxy.AddUser(new User("admin", "admin", Role.developer));
			}
		}

		[Given(@"I enter valid ""(.*)"" or ""(.*)""")]
		public void GivenIEnterValidOr(string p0, string p1)
		{
			username = p0;
			password = p1;
		}

		[Given(@"I enter invalid ""(.*)"" or ""(.*)""")]
		public void GivenIEnterInvalidOr(string p0, string p1)
		{
			username = p0;
			password = p1;
		}

		[When(@"I click Log in")]
		public void WhenIClickLogIn()
		{
			HiringClientProxy proxy = new HiringClientProxy(new NetTcpBinding(), "net.tcp://localhost:4000/IHiringContract");
			result = proxy.LogIn(username, password);
		}

		[Then(@"I should be successfully logged in")]
		public void ThenIShouldBeSuccessfullyLoggedIn()
		{
			Assert.AreEqual(true, result);
		}

		[Then(@"I should get an login error")]
		public void ThenIShouldGetAnLoginError()
		{
			Assert.AreEqual(false, result);
		}
	}
}
