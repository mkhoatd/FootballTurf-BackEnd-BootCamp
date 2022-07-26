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
                .Include(u => u.MainTurfs)
                .Where(u => u.Role == UserRole.ProductOwner)
                .OrderBy(u => u.Username)
                .FirstOrDefaultAsync();
            var mainTurf1 = new MainTurf
            {
                Name = "Sân Mỹ Đình",
                Address = "1 Lê Đức Thọ, Mỹ Đình, Nam Từ Liêm, Hà Nội",
                Latitude = "21.1",
                Longitude = "105.45",
                Turfs = new List<Turf>(),
                ImageLinks = new List<string>(),
            };
            var mainTurf2 = new MainTurf
            {
                Name = "Sân Hàng Đẫy",
                Address = "9 Trịnh Hoài Đức, Cát Linh, Đống Đa, Hà Nội, Việt Nam",
                Latitude = "21.1",
                Longitude = "105.49",
                Turfs = new List<Turf>(),
                ImageLinks = new List<string>()
            };
            var mainTurf3 = new MainTurf
            {
                Name = "Sân Đồng Nai",
                Address = "Đường Phạm Văn Khoai, Tân Hiệp, Biên Hòa, Đồng Nai, Việt Nam",
                Latitude = "10.961917",
                Longitude = "106.862806",
                Turfs = new List<Turf>(),
                ImageLinks = new List<string>()
            };
            var mainTurf4 = new MainTurf
            {
                Name = "Sân Cao Lãnh",
                Address = "Đường Lê Duẩn, Phường Mỹ Phú, Cao Lãnh, Đồng Tháp, Việt Nam",
                Latitude = "10.28",
                Longitude = "105.37",
                Turfs = new List<Turf>(),
                ImageLinks = new List<string>()
            };
            var mainTurf5 = new MainTurf
            {
                Name = "Sân Thanh Hóa",
                Address = "37 Lê Quý Đôn, Phường Ba Đình, Thành phố Thanh Hóa, Thanh Hóa, Việt Nam",
                Latitude = "19.47",
                Longitude = "105.46",
                Turfs = new List<Turf>(),
                ImageLinks = new List<string>()
            };
            var mainTurf6 = new MainTurf
            {
                Name = "Sân Pleiku",
                Address = "Quang Trung, P.Tây Sơn, Thành phố Pleiku, Gia Lai",
                Latitude = "13.979162",
                Longitude = "108.004901",
                Turfs = new List<Turf>(),
                ImageLinks = new List<string>()
            };
            var mainTurf7 = new MainTurf
            {
                Name = "Sân Long An",
                Address = "44B Trương Định, Phường 2, Tân An, Long An, Việt Nam",
                Latitude = "10.53616",
                Longitude = "106.408199",
                Turfs = new List<Turf>(),
                ImageLinks = new List<string>()
            };
            mainTurf1.ImageLinks.AddRange(new[]
            {
                "https://images.unsplash.com/photo-1509077613385-f89402467146?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=627&q=80"
            });
            mainTurf2.ImageLinks.AddRange(new[]
            {
                "https://images.unsplash.com/photo-1521221680442-c2850b60ee46?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=735&q=80"
            });
            mainTurf3.ImageLinks.Add("https://upload.wikimedia.org/wikipedia/commons/1/13/Hang_Day.jpg");
            mainTurf4.ImageLinks.Add("https://images.unsplash.com/photo-1493538706211-316874a1e8b4?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1424&q=80");
            mainTurf5.ImageLinks.Add("https://images.unsplash.com/photo-1519743375942-b497d66b1e8f?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1548&q=80");
            mainTurf6.ImageLinks.Add("https://images.unsplash.com/photo-1591695440087-aed745e5955e?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1767&q=80");
            mainTurf7.ImageLinks.Add("https://images.unsplash.com/photo-1511204579483-e5c2b1d69acd?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1558&q=80");
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
            turf1.ImageLinks.AddRange(new[]
            {
                "https://picsum.photos/id/237/200/300",
                "https://picsum.photos/id/0/200/300"
            });
            turf2.ImageLinks.AddRange(new[]
            {
                "https://picsum.photos/id/1/200/300",
                "https://picsum.photos/id/2/200/300"
            });
            turf3.ImageLinks.AddRange(new[]
            {
                "https://picsum.photos/id/1024/200/300",
                "https://picsum.photos/id/1025/200/300"
            });
            mainTurf1.Turfs.AddRange(new[] { turf1, turf2 });
            mainTurf2.Turfs.Add(turf3);
            owner1.MainTurfs.AddRange(new[] { mainTurf1, mainTurf2 });
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
                    Start = DateTime.Today.AddHours(20).ToUniversalTime(),
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


