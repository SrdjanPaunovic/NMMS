using Client;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HiringClientTest
{
    [TestFixture]
    public class MainWindowTest
    {
        

        [Test]
        public void ConstructorTest()
        {
            Assert.Throws<InvalidOperationException>(() => new MainWindow());
        }

       

         
    }
}
