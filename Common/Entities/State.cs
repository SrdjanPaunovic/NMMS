using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class State
	{
		public enum CompanyState
		{
			NoPartner=0,
			RequestSent=1,
			Partner=2
		}
	}
}
