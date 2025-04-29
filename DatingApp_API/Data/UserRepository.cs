﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp_API.Interface;
using Microsoft.EntityFrameworkCore;

namespace DatingApp_API.Data
{
    public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
    {
        public async Task<AppUser?> GetUserByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<AppUser?> GetUserByUsernameAsync(string username)
        {
            return await context.Users
                .Include(x => x.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUserAsync()
        {
            return await context.Users
                .Include(x => x.Photos)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            context.Entry(user).State = EntityState.Modified;

        }

        public async Task<IEnumerable<MemberDTO>> GetMembersAsync()
        {
           return await context.Users
                .ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<MemberDTO?> GetMemberAsync(string username)
        {
            return await context.Users
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

        }
    }
}
