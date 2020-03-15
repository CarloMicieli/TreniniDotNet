namespace TreniniDotNet.Infrastracture.Persistence
{
    public interface IDatabaseMigration
    {
        void Up();
        void Down(long version);
    }
}
