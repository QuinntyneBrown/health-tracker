using System.Data.Entity.Migrations;
using HealthTracker.Data.Model;
using System.Linq;
using HealthTracker.Security;

namespace HealthTracker.Data.Migrations
{
    public class ProfileConfiguration
    {
        public static void Seed(HealthTrackerContext context) {

            var systemRole = context.Roles.First(x => x.Name == Roles.SYSTEM);
            var tenant = context.Tenants.Single(x => x.Name == "Default");
            var account = context.Accounts.Single(x => x.Firstname== "Quinntyne");
            
            context.Profiles.AddOrUpdate(x => x.Name, new Profile()
            {
                Name = "Quinntyne",
                AccountId = account.Id,                
                TenantId = tenant.Id
            });

            context.SaveChanges();
        }
    }
}
