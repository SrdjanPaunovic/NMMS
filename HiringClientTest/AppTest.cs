using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Client;
using ServiceContract;
using Common.Entities;

namespace HiringClientTest
{
   [TestFixture]
    public class AppTest
    {
       private Client.App appUnderTest;

       [OneTimeSetUp]
       public void SetupTest()
       {
           appUnderTest = new App("test");
       }
       [Test]
       public void ConstructorTest1()
       {
           Assert.Throws<InvalidOperationException>(() => new App("test"));
       }

       [Test]
       public void ConstructorTest2()
       {
           Assert.Throws<InvalidOperationException>(() => new App());
       }


    }
}
