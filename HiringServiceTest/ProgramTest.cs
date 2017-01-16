using Common.Entities;
using HiringCompanyService;
using NSubstitute;
using NUnit.Framework;
using Service;
using Service.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringServiceTest
{
    [TestFixture]
   public  class ProgramTest
    {
        [OneTimeSetUp]
        public void SetupTest()
        {
            HiringCompanyDB.Instance = Substitute.For<IHiringCompanyDB>();

            HiringCompanyDB.Instance.AddUser(new User()).ReturnsForAnyArgs(true);

        }
        /*
       [Test]
       public void MainTest()
       {
           string[] arg = { "", "" };
           Assert.Throws<ArgumentOutOfRangeException>(() => Program.Main(arg));
       }
        */
    }
}
