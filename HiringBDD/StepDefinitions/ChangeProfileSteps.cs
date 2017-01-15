using Client;
using Common.Entities;
using NUnit.Framework;
using System;
using System.ServiceModel;
using TechTalk.SpecFlow;

namespace HiringBDD.StepDefinitions
{
	[Binding]
	public class ChangeProfileSteps
	{
		private static HiringClientProxy proxy = new HiringClientProxy(new NetTcpBinding(), "net.tcp://localhost:4000/IHiringContract");

		[When(@"I change my profile")]
		public void WhenIChangeMyProfile()
		{
			User admin = proxy.GetUser("admin");
			admin.Username = "kica";
			proxy.UpdateUser(admin);
		}

		[Then(@"My profile should be changed")]
		public void ThenMyProfileShouldBeChanged()
		{
			User kica = proxy.GetUser("kica");
			Assert.AreNotEqual(null, kica);
		}
	}
}
