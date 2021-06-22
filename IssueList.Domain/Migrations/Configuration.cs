using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using IssueList.Domain;


namespace IssueList.Domain.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<IssueListContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IssueListContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
