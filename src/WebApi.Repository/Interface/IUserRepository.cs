using System.Text;
using WebApi.Domain.Entities;
using WebApi.Repository.Dtos;

namespace WebApi.Repository.Interface
{
    public interface IUserRepository
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User?> GetUserById(string userId);
        Task<bool> CheckUserExistAsync(string username);
        Task SaveChangesAsync();
        Task<User> GetUserByUsernameAsync(string username);
        void AddUser(User user);
    }
}
