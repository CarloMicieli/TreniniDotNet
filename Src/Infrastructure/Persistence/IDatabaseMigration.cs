namespace TreniniDotNet.Infrastructure.Persistence
{
    public interface IDatabaseMigration
    {
        void Up();
        void Down(long version);
    }
}