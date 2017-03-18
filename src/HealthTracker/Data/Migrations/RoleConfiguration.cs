using System.Data.Entity.Migrations;
using HealthTracker.Data;
using HealthTracker.Data.Model;
using HealthTracker.Features.Users;

namespace HealthTracker.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(HealthTrackerContext context) {

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.SYSTEM
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.PRODUCT
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.DEVELOPMENT
            });

            context.SaveChanges();
        }
    }
}
