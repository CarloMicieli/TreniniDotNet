using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Database.Testing
{
    public sealed class DatabaseTestHelpers
    {
        public DatabaseArrange Arrange { get; }
        public DatabaseSetup Setup { get; }
        public DatabaseAssert Assert { get; }

        public DatabaseTestHelpers(IConnectionProvider connectionProvider)
        {
            Arrange = new DatabaseArrange(connectionProvider);
            Setup = new DatabaseSetup(connectionProvider);
            Assert = new DatabaseAssert(connectionProvider);
        }
    }
}
