namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<HRONLib.HRONEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            DbMigrator migrator = new DbMigrator(this);

            if (migrator.GetPendingMigrations().Any())
            {
                migrator.Update();
            }
        }

        protected override void Seed(HRONLib.HRONEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
