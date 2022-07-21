using WebApi.Domain.Entities;

namespace WebApi.Repository.Interface
{
    public interface ITokenService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GenerateJwtToken(User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string? ValidateJwtToken(string token);
    }
}
