using Client;
using Common.Entities;
using NUnit.Framework;
using System;
using System.ServiceModel;
using TechTalk.SpecFlow;

namespace HiringBDD.StepDefinitions
{
	[Binding]
	public class EmployeeWorkTimeSteps
	{
		private static HiringClientProxy proxy = new HiringClientProxy(new NetTcpBinding(), "net.tcp://localhost:4000/IHiringContract");
		private static DateTime newDateTime = DateTime.Now;

		[When(@"I change my work time")]
		public void WhenIChangeMyWorkTime()
		{
			User admin = proxy.GetUser("admin");
			Assert.AreNotEqual(null, admin, "1");
			admin.StartTime = newDateTime;
			proxy.UpdateUser(admin);
		}

		[Then(@"My work time should be successfully changed")]
		public void ThenMyWorkTimeShouldBeSuccessfullyChanged()
		{
			User admin = proxy.GetUser("admin");
			Assert.AreNotEqual(null, admin);
			Assert.AreEqual(newDateTime, admin.StartTime);
		}
	}
}
