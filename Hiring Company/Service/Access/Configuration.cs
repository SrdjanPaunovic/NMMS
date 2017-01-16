using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Service.Access
{
    public class Configuration : DbMigrationsConfiguration<AccessDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "HiringDB";
            LogHelper.GetLogger().Info("Configuration initialized");

        }
    }
}
