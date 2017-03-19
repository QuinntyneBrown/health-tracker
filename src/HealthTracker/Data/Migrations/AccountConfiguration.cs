using HealthTracker.Data.Model;
using System.Data.Entity.Migrations;
using System.Linq;

namespace HealthTracker.Data.Migrations
{
    public class AccountConfiguration
    {
        public static void Seed(HealthTrackerContext context) {
            
            var tenant = context.Tenants.Single(x => x.Name == "Default");
            
            context.Accounts.AddOrUpdate(x => x.Firstname, new Account()
            {
                Firstname = "Quinntyne",
                Lastname = "Brown",
                TenantId = tenant.Id
            });

            context.SaveChanges();
        }
    }
}
