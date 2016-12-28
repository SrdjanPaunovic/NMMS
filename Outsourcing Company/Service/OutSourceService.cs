using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;
namespace Service
{
	public class OutSourceService : IOutSourceContract
	{
		public void Write()
		{
			Console.WriteLine("I write...");
		}

		public void Read()
		{
			Console.WriteLine("I read...");
		}
	}
}
