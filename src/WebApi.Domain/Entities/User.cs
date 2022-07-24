using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using WebApi.Domain.Enum;

namespace WebApi.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public UserRole Role { get; set; }
        public string PhoneNumber { get; set; }
        public HashSet<Turf> Turfs { get; set; }

        public User() {}

        public User(string username, string password)
        {
            Username = username.ToLower();
            UpdatePassword(password);
        }

        public static User CreateCustomer(string username, string password)
        {
            var user=new User(username, password);
            user.Role = UserRole.Customer;
            return user;
        }

        public static User CreateProductOwner(string username, string password)
        {
            var user=new User(username, password);
            user.Role = UserRole.ProductOwner;
            return user;
        }
        public void UpdatePassword(string password)
        {
            using var hmac = new HMACSHA512();
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            PasswordSalt = hmac.Key;
        }
    }
}
