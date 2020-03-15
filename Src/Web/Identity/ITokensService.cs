namespace TreniniDotNet.Web.Identity
{
    public interface ITokensService
    {
        string CreateToken(string subject);
    }
}