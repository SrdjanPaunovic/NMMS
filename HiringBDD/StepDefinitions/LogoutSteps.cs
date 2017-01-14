using Client;
using NUnit.Framework;
using System;
using System.ServiceModel;
using TechTalk.SpecFlow;

namespace HiringBDD.StepDefinitions
{
    [Binding]
    public class LogoutSteps
    {
		private HiringClientProxy proxy = new HiringClientProxy(new NetTcpBinding(), "net.tcp://localhost:4000/IHiringContract");
		private bool result;

		[Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
			proxy.LogIn("admin", "admin");
		}

		[When(@"I click log out")]
        public void WhenIClickLogOut()
        {
			result = proxy.LogOut("admin");
        }
        
        [Then(@"I should be successfully logged out")]
        public void ThenIShouldBeSuccessfullyLoggedOut()
        {
			Assert.AreEqual(true, result);
        }
    }
}
