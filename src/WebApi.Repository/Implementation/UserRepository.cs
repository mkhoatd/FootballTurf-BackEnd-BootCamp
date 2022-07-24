using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
using WebApi.Persistence;
using WebApi.Repository.Dtos;
using WebApi.Repository.Interface;

namespace WebApi.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AppFootballTurfDbContext _context;
        public UserRepository(AppFootballTurfDbContext context)
        {
            _context = context;
        }
        
        public async Task<User?> GetUserById(string userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == Guid.Parse(userId));
            return user;
        }

        public async Task<bool> CheckUserExistAsync(string username)
        {
            var isExist= await _context.Users.AnyAsync(x => x.Username == username);
            return isExist;
        }
    
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }
    }
}
