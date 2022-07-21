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
        private readonly ITokenService _tokenService;
        public UserRepository(AppFootballTurfDbContext context,
            ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<ReturnCreateUser> CreateUser(CreateUserDto createUser, User userCurrent)
        {
            userCurrent = await CreateNewUser(createUser.Name);

            var generateToken = _tokenService.GenerateJwtToken(userCurrent);
            ReturnCreateUser returnCreateUser = new ReturnCreateUser()
            {
                Name = userCurrent.Name,
                Token = generateToken,
            };
            return returnCreateUser;
        }

        private async Task<User> CreateNewUser(string userName)
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = userName,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetUserById(string userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == Guid.Parse(userId));
            return user;
        }
    }
}
