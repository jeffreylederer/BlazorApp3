namespace BlazerApp3.Services
{
    public interface ISecurityService
    {
        string GetSha256Hash(string input);
    }
}
