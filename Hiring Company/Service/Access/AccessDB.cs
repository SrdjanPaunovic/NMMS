using Service.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Access
{
	public class AccessDB:DbContext
	{
		public AccessDB() : base("HiringDB") { }
		public DbSet<User> Users { get; set; }
	}
}
