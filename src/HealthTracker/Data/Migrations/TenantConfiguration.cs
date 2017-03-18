using System.Data.Entity.Migrations;
using HealthTracker.Data;
using HealthTracker.Data.Model;

namespace HealthTracker.Migrations
{
    public class TenantConfiguration
    {
        public static void Seed(HealthTrackerContext context) {

            context.Tenants.AddOrUpdate(x => x.Name, new Tenant()
            {
                Name = "Default"
            });

            context.SaveChanges();
        }
    }
}
