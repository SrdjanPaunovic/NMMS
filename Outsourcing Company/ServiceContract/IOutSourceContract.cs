using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ServiceContract
{
	[ServiceContract]
	public interface IOutSourceContract
	{
		[OperationContract]
		void Write();
		[OperationContract]
		void Read();
	}
}
