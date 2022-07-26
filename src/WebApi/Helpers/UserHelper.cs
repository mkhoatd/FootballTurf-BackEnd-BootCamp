using WebApi.Domain.Entities;
using WebApi.Domain.Enum;

namespace WebApi.Helpers;

public static class UserHelper
{
    public static User CreateRandomCustomer(int i)
    {
        var user = new User();
        user.Username="customer" + i.ToString();
        user.UpdatePassword("password");
        user.Name = RandomString(10);
        user.PhoneNumber = CreateRandomPhoneNumber();
        user.Role = UserRole.Customer;
        return user;
    }

    public static User CreateRandomProductOwner(int i)
    {
        var user = new User();
        user.Username="owner" + i.ToString();
        user.UpdatePassword("password");
        user.Name = RandomString(10);
        user.PhoneNumber = CreateRandomPhoneNumber();
        user.Role = UserRole.ProductOwner;
        return user;
    }
    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private static string CreateRandomPhoneNumber()
    {
        var R = new Random(Guid.NewGuid().GetHashCode());
        return ((long)R.Next (0, 100000 ) * (long)R.Next (0, 100000 )).ToString ().PadLeft (10, '0').ToString();
    }

}