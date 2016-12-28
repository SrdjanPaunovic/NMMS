using Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Access
{
	public class HirinigCompanyDB
	{
		public static HirinigCompanyDB hirinigCompanyDB;

		public static HirinigCompanyDB Instance
		{
			get
			{
				if (hirinigCompanyDB == null)
				{
					hirinigCompanyDB = new HirinigCompanyDB();
				}
				return hirinigCompanyDB;
			}
			set
			{
				if (hirinigCompanyDB == null)
				{
					hirinigCompanyDB = value;
				}
			}
		}

		public bool AddUser(User user)
		{
			using(var db=new AccessDB()){

				db.Users.Add(user);
				int i = db.SaveChanges();
				if (i > 0)
				{
					return true;
				}
				return false;
			}
		}
		
	}
}
