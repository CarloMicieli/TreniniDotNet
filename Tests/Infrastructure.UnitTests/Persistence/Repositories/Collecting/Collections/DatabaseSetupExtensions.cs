using TreniniDotNet.Infrastructure.Database.Testing;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Collections
{
    public static class DatabaseSetupExtensions
    {
        public static void WithoutAnyCollection(this DatabaseSetup databaseSetup)
        {
            databaseSetup.TruncateTable(Tables.CollectionItems);
            databaseSetup.TruncateTable(Tables.Collections);
        }
    }
}