using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
namespace Service.Access
{
	public class HiringCompanyDB
	{
		public static HiringCompanyDB hirinigCompanyDB;

		public static HiringCompanyDB Instance
		{
			get
			{
				if (hirinigCompanyDB == null)
				{
					hirinigCompanyDB = new HiringCompanyDB();
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

        public bool AddCompany(Company company)
        {
            using (var db = new AccessDB())
            {
                db.Companies.Add(company);
                int i = db.SaveChanges();
                if (i > 0)
                {
                    return true;
                }
                return  false;

            }
        }
        public bool AddProject(Project project)
        {
            using (var db = new AccessDB())
            {
                db.Projects.Add(project);
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
