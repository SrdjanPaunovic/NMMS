using Client.ViewModel;
using Common.Entities;
using NSubstitute;
using NUnit.Framework;
using ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HiringClientTest.ViewModelTest
{
	[TestFixture]
	public class ProfileDialogViewModelTest
	{
		private ProfileDialogViewModel clientViewModelUnderTest;

		[OneTimeSetUp]
		public void SetupTest()
		{
			clientViewModelUnderTest = new ProfileDialogViewModel();
			clientViewModelUnderTest.proxy = Substitute.For<IHiringContract>();
		}

		[Test]
		public void ConstructorTest()
		{
			Assert.DoesNotThrow(() => new ProfileDialogViewModel());
		}

		[Test]
		public void SaveCommandTest()
		{
			Assert.IsNotNull(clientViewModelUnderTest.SaveCommand);
		}

		[Test]
		public void CancelCommandTest()
		{
			Assert.IsNotNull(clientViewModelUnderTest.CancelCommand);
		}

		[Test]
		public void UserTest()
		{
			User user = new User();

			clientViewModelUnderTest.User = user;
			Assert.AreEqual(user, clientViewModelUnderTest.User);

		}

		[Test]
		public void CancelClickTest()
		{
			clientViewModelUnderTest.CancelCommand.Execute(new UserControl());
		}

		[Test]
		public void SaveClickTest()
		{
			clientViewModelUnderTest.SaveCommand.Execute(new UserControl());
		}
	}
}
