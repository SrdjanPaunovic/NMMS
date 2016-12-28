using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;

namespace Service.Access
{
	public class AccessDB : DbContext
	{
		public AccessDB() : base("OutSourceDB") { }

		public DbSet<OSUserAction> Actions { get; set; }
	}
}
