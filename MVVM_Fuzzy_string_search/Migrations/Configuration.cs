namespace MVVM_Fuzzy_string_search.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVVM_Fuzzy_string_search.Models.ResultContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MVVM_Fuzzy_string_search.Models.ResultContext";
        }

        protected override void Seed(MVVM_Fuzzy_string_search.Models.ResultContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
