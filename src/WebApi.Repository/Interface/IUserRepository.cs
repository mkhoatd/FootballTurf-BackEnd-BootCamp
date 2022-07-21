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
        /// <param name="createUser"></param>
        /// <returns></returns>
        Task<ReturnCreateUser> CreateUser(CreateUserDto createUser, User userCurrent);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User?> GetUserById(string userId);
    }
}
