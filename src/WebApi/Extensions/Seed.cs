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
            var owner1 = await context.Users
                .Include(u=>u.MainTurfs)
                .Where(u => u.Role == UserRole.ProductOwner)
                .OrderBy(u=>u.Username)
                .FirstOrDefaultAsync();
            var mainTurf1=(new MainTurf
            {
                Name = "Sân lớn 1",
                Address = "Đường 1",
                Longitude="106.6333",
                Latitude="10.8167",
                Turfs = new List<Turf>()
            });
            var turf1 = new Turf
            {
                Name = "Sân nhỏ 1",
                Type = TurfType.FiveASide,
                Images = new List<Image>()
            };
            var turf2 = new Turf
            {
                Name = "Sân nhỏ 2",
                Type = TurfType.SevenASide,
                Images = new List<Image>()
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
            turf1.Images.AddRange(new Image[] {im1, im2});
            turf2.Images.AddRange(new Image[] {im3, im4});
            mainTurf1.Turfs.AddRange(new Turf[] {turf1, turf2});
            owner1.MainTurfs.Add(mainTurf1);
            await context.SaveChangesAsync();
        }

        if (!context.Schedules.Any())
        {
            var turf = await context.Turfs.OrderBy(t => t.Name).FirstOrDefaultAsync();
            var customer = await context.Users.Where(u => u.Role == UserRole.Customer)
                .OrderBy(u => u.Username).FirstOrDefaultAsync();
            context.Schedules.AddRange(new Schedule[]
            {
                new Schedule
                {
                    Start = DateTime.Today.ToUniversalTime(),
                    End = DateTime.Today.AddMinutes(120).ToUniversalTime(),
                    Status = ScheduleStatus.Pending,
                    Turf = turf,
                    Customer = customer
                },
                new Schedule
                {
                    Start = DateTime.Today.AddHours(9).ToUniversalTime(),
                    End=DateTime.Today.AddHours(12).ToUniversalTime(),
                    Status = ScheduleStatus.Pending,
                    Turf= turf,
                    Customer = customer
                },
                new Schedule
                {
                    Start=DateTime.Today.AddHours(20).ToUniversalTime(),
                    End = DateTime.Today.AddHours(22).ToUniversalTime(),
                    Status = ScheduleStatus.Booked,
                    Turf = turf,
                    Customer = customer
                }
            });
            await context.SaveChangesAsync();
        }
    }
}