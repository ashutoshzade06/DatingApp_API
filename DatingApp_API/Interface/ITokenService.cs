namespace DatingApp_API
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
