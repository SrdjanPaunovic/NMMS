using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;

namespace Service.Access
{
	public class UsersDB: IOutSorceDB
	{
		private static IOutSorceDB myDB;

		public static IOutSorceDB Instance
		{
			get
			{
				if (myDB == null)
					myDB = new UsersDB();

				return myDB;
			}
			set
			{
				if (myDB == null)
					myDB = value;
			}
		}

		public bool AddAction(OSUserAction action)
		{
			using (var access = new AccessDB())
			{
				access.Actions.Add(action);
				int i = access.SaveChanges();

				if (i > 0)
					return true;
				return false;
			}
		}

		public bool RemoveAction(OSUserAction action)
		{
			throw new NotImplementedException();
		}

		public bool UpdateAction(OSUserAction action)
		{
			throw new NotImplementedException();
		}
	}
}
