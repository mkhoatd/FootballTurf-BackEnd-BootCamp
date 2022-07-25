using Microsoft.EntityFrameworkCore;
using WebApi.Persistence;
using WebApi.Domain.Entities;
using WebApi.Domain.Enum;

namespace WebApi.Extensions;

public static class Seed
{
    public static async Task SeedData(AppFootballTurfDbContext context)
    {
        if (!context.Users.Any())
        {
            var customers = new List<User>
            {
                User.CreateCustomer("customer1", "password").UpdatePhoneNumber("1111111111"),
                User.CreateCustomer("customer2", "password").UpdatePhoneNumber("1111111112"),
                User.CreateCustomer("customer3", "password").UpdatePhoneNumber("1111111113")
            };
            context.Users.AddRange(customers);
            var productOwners = new List<User>
            {
                User.CreateProductOwner("owner1", "password").UpdatePhoneNumber("1111111114"),
                User.CreateProductOwner("owner2", "password").UpdatePhoneNumber("1111111115"),
                User.CreateProductOwner("owner3", "password").UpdatePhoneNumber("1111111116")
            };
            context.AddRange(productOwners);
            await context.SaveChangesAsync();
        }

        if (!context.MainTurfs.Any())
        {
            var owner1 = context.Users
                .Include(u=>u.MainTurfs)
                .Where(u => u.Role == UserRole.ProductOwner)
                .OrderBy(u=>u.Username)
                .FirstOrDefault();
            var mainTurf1=(new MainTurf
            {
                Name = "Sân lớn 1",
                Address = "Đường 1",
                Longitude="106.6333",
                Latitude="10.8167",
                Turfs = new HashSet<Turf>()
            });
            var turf1 = new Turf
            {
                Name = "Sân nhỏ 1",
                Type = TurfType.FiveASide,
                Images = new HashSet<Image>()
            };
            var turf2 = new Turf
            {
                Name = "Sân nhỏ 2",
                Type = TurfType.SevenASide,
                Images = new HashSet<Image>()
            };
            var im1 = new Image
            {
                Link = "https://picsum.photos/id/237/200/300"
            };
            var im2 = new Image
            {
                Link = "https://picsum.photos/id/0/200/300"
            };
            var im3 = new Image
            {
                Link = "https://picsum.photos/id/1/200/300"
            };
            var im4 = new Image
            {
                Link = "https://picsum.photos/id/2/200/300"
            };
            turf1.Images.Add(im1);
            turf1.Images.Add(im2);
            turf2.Images.Add(im3);
            turf2.Images.Add(im4);
            mainTurf1.Turfs.Add(turf1);
            mainTurf1.Turfs.Add(turf2);
            owner1.MainTurfs.Add(mainTurf1);
        }
    }
}