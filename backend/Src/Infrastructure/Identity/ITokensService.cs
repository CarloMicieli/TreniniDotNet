namespace TreniniDotNet.Infrastructure.Identity
{
    public interface ITokensService
    {
        string CreateToken(string subject);
    }
}
