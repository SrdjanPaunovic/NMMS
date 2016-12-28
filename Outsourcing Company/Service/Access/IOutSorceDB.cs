using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;
namespace Service.Access
{
	public interface IOutSorceDB
	{
		bool AddAction(OSUserAction action);
		bool RemoveAction(OSUserAction action);
		bool UpdateAction(OSUserAction action);
	}
}
