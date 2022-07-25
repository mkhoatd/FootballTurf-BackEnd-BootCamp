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
                User.CreateCustomer("customer1", "password").UpdatePhoneNumber("1111111111").UpdateName("Customer 1"),
                User.CreateCustomer("customer2", "password").UpdatePhoneNumber("1111111112").UpdateName("Customer 2"),
                User.CreateCustomer("customer3", "password").UpdatePhoneNumber("1111111113").UpdateName("Customer 3")
            };
            context.Users.AddRange(customers);
            var productOwners = new List<User>
            {
                User.CreateProductOwner("owner1", "password").UpdatePhoneNumber("1111111114").UpdateName("Owner 1"),
                User.CreateProductOwner("owner2", "password").UpdatePhoneNumber("1111111115").UpdateName("Owner 2"),
                User.CreateProductOwner("owner3", "password").UpdatePhoneNumber("1111111116").UpdateName("Owner 3")
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
            var mainTurf1=new MainTurf
            {
                Name = "Sân lớn 1",
                Address = "Đường 1",
                Longitude="106.6333",
                Latitude="10.8167",
                Turfs = new List<Turf>(),
                ImageLinks= new List<string>(),
            };
            var mainTurf2 = new MainTurf
            {
                Name = "Sân lớn 2",
                Address = "Đường 2",
                Longitude = "169.96",
                Latitude = "45",
                Turfs = new List<Turf>(),
                ImageLinks = new List<string>()
            };
            mainTurf1.ImageLinks.AddRange(new []
            {
                "https://picsum.photos/id/1003/200/300",
                "https://picsum.photos/id/1004/200/300"
            });
            mainTurf2.ImageLinks.AddRange(new []
            {
                "https://picsum.photos/id/1019/200/300",
                "https://picsum.photos/id/102/200/300"
            });
            var turf1 = new Turf
            {
                Name = "Sân nhỏ 1",
                Type = TurfType.FiveASide,
                ImageLinks = new List<string>()
            };
            var turf2 = new Turf
            {
                Name = "Sân nhỏ 2",
                Type = TurfType.SevenASide,
                ImageLinks = new List<string>()
            };
            var turf3 = new Turf
            {
                Name = "Sân nhỏ 3",
                Type = TurfType.SevenASide,
                ImageLinks = new List<string>()
            };
            turf1.ImageLinks.AddRange(new []
            {
                "https://picsum.photos/id/237/200/300", 
                "https://picsum.photos/id/0/200/300"
            });
            turf2.ImageLinks.AddRange(new []
            {
                "https://picsum.photos/id/1/200/300", 
                "https://picsum.photos/id/2/200/300"
            });
            turf3.ImageLinks.AddRange(new []
            {
                "https://picsum.photos/id/1024/200/300", 
                "https://picsum.photos/id/1025/200/300"
            });
            mainTurf1.Turfs.AddRange(new [] {turf1, turf2});
            mainTurf2.Turfs.Add(turf3);
            owner1.MainTurfs.AddRange(new [] {mainTurf1, mainTurf2});
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