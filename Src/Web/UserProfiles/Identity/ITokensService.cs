namespace TreniniDotNet.Web.UserProfiles.Identity
{
    public interface ITokensService
    {
        string CreateToken(string subject);
    }
}