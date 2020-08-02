using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Database.Testing
{
    public sealed class DatabaseTestHelpers
    {
        public DatabaseArrange Arrange { get; }
        public DatabaseSetup Setup { get; }
        public DatabaseAssert Assert { get; }

        public DatabaseTestHelpers(IDatabaseContext databaseContext)
        {
            Arrange = new DatabaseArrange(databaseContext);
            Setup = new DatabaseSetup(databaseContext);
            Assert = new DatabaseAssert(databaseContext);
        }
    }
}
