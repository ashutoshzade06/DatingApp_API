using DatingApp_API.Helpers;

namespace DatingApp_API.Interface
{
    public interface IUserRepository
    {
        Task<AppUser?> GetUserByIdAsync(int id);
        Task<AppUser?> GetUserByUsernameAsync(string username);
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUserAsync();
        Task<PagedList<MemberDTO>> GetMembersAsync(UserParams userParams);
        Task<MemberDTO?> GetMemberAsync(string username);
    }
}
