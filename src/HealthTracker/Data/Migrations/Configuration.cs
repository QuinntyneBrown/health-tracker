namespace HealthTracker.Migrations
{
    using Data;
    using Data.Helpers;
    using Data.Migrations;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<HealthTrackerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HealthTrackerContext context)
        {
            TenantConfiguration.Seed(context);
            RoleConfiguration.Seed(context);
            UserConfiguration.Seed(context);
            AccountConfiguration.Seed(context);
            ProfileConfiguration.Seed(context);
        }
    }

    public class DbConfiguration : System.Data.Entity.DbConfiguration
    {
        public DbConfiguration()
        {
            AddInterceptor(new SoftDeleteInterceptor());
        }
    }
}
