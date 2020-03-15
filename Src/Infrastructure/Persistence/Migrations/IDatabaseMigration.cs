namespace TreniniDotNet.Infrastracture.Persistence.Migrations
{
    public interface IDatabaseMigration
    {
        void Up();
        void Down(long version);
    }
}
