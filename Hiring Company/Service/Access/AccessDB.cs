
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using Common;

namespace Service.Access
{
	public class AccessDB:DbContext
	{
		public AccessDB() : base("HiringDB") {
			LogHelper.GetLogger().Info("AccessDB initialized");
		}
		public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Common.Entities.Task> Tasks { get; set; }


	}
}
