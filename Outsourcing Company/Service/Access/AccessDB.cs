using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;
using Common.Entities;

namespace Service.Access
{
	public class AccessDB : DbContext
	{
		public AccessDB() : base("OutSourceDB") { }

		public DbSet<OcUser> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Company> Comapanies { get; set; } // constract signed with

        public DbSet<OcProject> Projects { get; set; }
	}
}
