using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;
using Common.Entities;
using Common;

namespace Service.Access
{
    public class AccessDB : DbContext
    {
        public AccessDB()
            : base("OutSourceDB")
        {
            LogHelper.GetLogger().Info("AccessDB initialized");
        }

        public DbSet<OcUser> Users { get; set; }
        public DbSet<Team> Teams { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<OcProject> Projects { get; set; }

        public DbSet<UserStory> UserStories { get; set; }

        public DbSet<Common.Entities.Task> Tasks { get; set; }
    }
}
