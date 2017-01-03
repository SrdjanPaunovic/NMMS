﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Common.Entities;

namespace CommonTest.EntitiesTest
{
	[TestFixture]
	public class UserTest
	{

		private User userUnderTest;


		[OneTimeSetUp]
		public void SetupTest()
		{
			this.userUnderTest = new User();
		}


		[Test]
		public void ConstructorTest()
		{
			Assert.DoesNotThrow(() => new User());
		}

	

		[Test]
		public void IdPropertyTest()
		{
			int id = 1;
			userUnderTest.Id = id;
			Assert.AreEqual(id, userUnderTest.Id);
		}


		[Test]
		public void UsernamePropertyTest()
		{
			string username = "jovisa";
			userUnderTest.Username = username;
			Assert.AreEqual(username, userUnderTest.Username);
		}



		[Test]
		public void NamePropertyTest()
		{
			string name = "Jovisa";
			userUnderTest.Name = name;
			Assert.AreEqual(name, userUnderTest.Name);
		}

		[Test]
		public void SurnamePropertyTest()
		{
			string surname = "Jovic";
			userUnderTest.Surname =surname;
			Assert.AreEqual(surname, userUnderTest.Surname);

		}

		[Test]
		public void PasswordPropertyTest()
		{
			string password = "123";
			userUnderTest.Password = password;
		}

		[Test]
		public void IsAuthenticatedPropertyTest()
		{
			bool isAuthenticated = true;
			userUnderTest.IsAuthenticated = isAuthenticated;
			Assert.AreEqual(isAuthenticated, userUnderTest.IsAuthenticated);
		}
		[Test]
		public void PasswordChangedPropertyChanged()
		{
			DateTime dt = DateTime.Now;
			userUnderTest.Password_changed = dt;
			Assert.AreEqual(dt, userUnderTest.Password_changed);
		}
		[Test]
		public void RolePropertyTest()
		{
			Roles.Role role = Roles.Role.CEO;
			userUnderTest.Role = role;
			Assert.AreEqual(role, userUnderTest.Role);
		}
		//[Test]
		//public void ProjectPropertyTest()
		//{
		//	Project project = new Project();
		//	userUnderTest.Project = project;
		//	Assert.AreEqual(project, userUnderTest.Project);
		//}
		[Test]
		public void PasswordChangedTest()
		{
			string password = "admin";
			userUnderTest.PasswordChange(password);
			Assert.AreEqual(password, userUnderTest.Password);

		}

	}
}
