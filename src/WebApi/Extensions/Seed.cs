using Microsoft.EntityFrameworkCore;
using WebApi.Persistence;
using WebApi.Domain.Entities;
using WebApi.Domain.Enum;
using WebApi.Helpers;

namespace WebApi.Extensions;

public static class Seed
{
    public static async Task SeedData(AppFootballTurfDbContext context)
    {
        if (!context.Users.Any())
        {
            var customers = new List<User>
            {
                UserHelper.CreateRandomCustomer(1),
                UserHelper.CreateRandomCustomer(2),
                UserHelper.CreateRandomCustomer(3),
                UserHelper.CreateRandomCustomer(4),
                UserHelper.CreateRandomCustomer(5),
                UserHelper.CreateRandomCustomer(6),
            };
            context.Users.AddRange(customers);
            var productOwners = new List<User>
            {
                UserHelper.CreateRandomProductOwner(1),
                UserHelper.CreateRandomProductOwner(2),
                UserHelper.CreateRandomProductOwner(3),
                UserHelper.CreateRandomProductOwner(4),
                UserHelper.CreateRandomProductOwner(5),
                UserHelper.CreateRandomProductOwner(6),
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
                Name = "Sân Bóng Đá Mini Cỏ Nhân Tạo Hồng Bảy",
                Address = "399/45 Bình Thành, Bình Hưng Hoà B, Bình Tân, Thành phố Hồ Chí Minh",
                Longitude= "106.58636497606",
                Latitude= "10.81326010",
                Turfs = new List<Turf>(),
                ImageLinks= new List<string>()
                {
                 "https://firebasestorage.googleapis.com/v0/b/freshfood-6bb13.appspot.com/o/Test%2Fsan3-1.jpg?alt=media&token=095bca4e-ec72-4f94-a0fd-85e9421906f0",
                 "https://firebasestorage.googleapis.com/v0/b/freshfood-6bb13.appspot.com/o/Test%2Fsan6.jpg?alt=media&token=dd280e4b-9ef3-4caf-9315-cdfa1f27950f"
                },
            };
            var mainTurf2 = new MainTurf
            {
                Name = "Sân Bóng Đá Cỏ Nhân Tạo Vườn Keo",
                Address = "99 Tân Thới Hiệp 22, Tân Thới Hiệp, Quận 12, Thành phố Hồ Chí Minh",
                Longitude = "106.640805581",
                Latitude = "10.8656916",
                Turfs = new List<Turf>(),
                ImageLinks = new List<string>()
                {
                    "https://firebasestorage.googleapis.com/v0/b/freshfood-6bb13.appspot.com/o/Test%2Fsan7.png?alt=media&token=61b8d4d6-ff21-40ea-9f3a-5fbb5671e904",
                    "https://firebasestorage.googleapis.com/v0/b/freshfood-6bb13.appspot.com/o/Test%2Fsan7-2.jpg?alt=media&token=5af4d737-e27e-4375-af44-c50aef2c9c64"
                }
            };
            var mainTurf3 = new MainTurf
            {
                Name = "Sân Chảo Lửa",
                Address = "30 Phan Thúc Duyện, Phường 4, Tân Bình, Thành phố Hồ Chí Minh",
                Longitude = "106.6606704",
                Latitude = "10.8048043",
                Turfs = new List<Turf>(),
                ImageLinks = new List<string>()
                {
                    "https://firebasestorage.googleapis.com/v0/b/freshfood-6bb13.appspot.com/o/Test%2Fchaolua1.jpg?alt=media&token=7399364f-56b8-461f-832b-867e5b42ee75",
                    "https://firebasestorage.googleapis.com/v0/b/freshfood-6bb13.appspot.com/o/Test%2Fchaolua2.jpg?alt=media&token=1f80fd03-a100-47cf-b106-b1ce1f5d0a83"
                }
            };
            var mainTurf4 = new MainTurf
            {
                Name = "Sân Cỏ Nhân Tạo Trường CĐ Công Nghệ Thủ Đức",
                Address = "53 Võ Văn Ngân, P. Linh Chiểu, Q. Thủ Đức, Thành phố Hồ Chí Minh",
                Longitude = "106.75715",
                Latitude = "10.8513",
                Turfs = new List<Turf>(),
                ImageLinks = new List<string>()
                {
                    "https://firebasestorage.googleapis.com/v0/b/freshfood-6bb13.appspot.com/o/Test%2Fsan2-1.jpg?alt=media&token=2f487f86-65da-428c-a46a-936895757a81",
                    "https://firebasestorage.googleapis.com/v0/b/freshfood-6bb13.appspot.com/o/Test%2Fsan2.jpg?alt=media&token=268b8d09-1e97-4537-b452-f8e4c40df7e9"
                }
            };

            var mainTurf5 = new MainTurf
            {
                Name = "Sân Bóng Đá Cỏ Nhân Tạo Hợp Thành",
                Address = "213 Bình Quới, Phường 28, Bình Thạnh, Thành phố Hồ Chí Minh",
                Longitude = "106.65725",
                Latitude = "10.5533",
                Turfs = new List<Turf>(),
                ImageLinks = new List<string>()
                {
                    "https://firebasestorage.googleapis.com/v0/b/freshfood-6bb13.appspot.com/o/Test%2Fsan3.jpg?alt=media&token=1c9b9d19-5fa5-4708-a337-a6966af9a657",
                    "https://firebasestorage.googleapis.com/v0/b/freshfood-6bb13.appspot.com/o/Test%2Fsan3-1.jpg?alt=media&token=095bca4e-ec72-4f94-a0fd-85e9421906f0"
                }
            };

            var mainTurf6 = new MainTurf
            {
                Name = "Sân bóng đá mini cỏ nhân tạo Thành Phát",
                Address = "1017 Bình Quới, Phường 28, Bình Thạnh, Thành phố Hồ Chí Minh",
                Longitude = "106.2131",
                Latitude = "10.1122",
                Turfs = new List<Turf>(),
                ImageLinks = new List<string>()
                {
                    "https://firebasestorage.googleapis.com/v0/b/freshfood-6bb13.appspot.com/o/Test%2Fsan4-1.jpg?alt=media&token=ebcae86a-dea2-4969-878f-0877451a5ebf",
                    "https://firebasestorage.googleapis.com/v0/b/freshfood-6bb13.appspot.com/o/Test%2Fsan4.jpg?alt=media&token=08f21ce5-67cc-4c86-8ea0-792ab58a3be9",
                }
            };
            var mainTurf7 = new MainTurf
            {
                Name = "Sân Cỏ Nhân Tạo Công Viên Thể Thao Q.2",
                Address = "QQP7+27J, Phường Bình Trưng Tây, Quận 2, Thành phố Hồ Chí Minh",
                Longitude = "106.4412",
                Latitude = "10.6221",
                Turfs = new List<Turf>(),
                ImageLinks = new List<string>()
                {
                    "https://firebasestorage.googleapis.com/v0/b/freshfood-6bb13.appspot.com/o/Test%2Fsan5.jpg?alt=media&token=f5e028d8-4631-41ea-a837-b82eb3f0524a",
                    "https://firebasestorage.googleapis.com/v0/b/freshfood-6bb13.appspot.com/o/Test%2Fsan5-1.jpg?alt=media&token=b4ddf8e5-3ad8-4dbb-95ac-2374cffb6747",
                }
            };
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
            var turf4 = new Turf
            {
                Name = "Sân nhỏ 4",
                Type = TurfType.SevenASide,
                ImageLinks = new List<string>()
            };
            var turf5 = new Turf
            {
                Name = "Sân nhỏ 5",
                Type = TurfType.SevenASide,
                ImageLinks = new List<string>()
            };
            var turf6 = new Turf
            {
                Name = "Sân nhỏ 6",
                Type = TurfType.FiveASide,
                ImageLinks = new List<string>(),
            };
            var turf7 = new Turf
            {
                Name = "Sân nhỏ 7",
                Type = TurfType.FiveASide,
                ImageLinks = new List<string>()
            };
            var turf8 = new Turf
            {
                Name = "Sân nhỏ 8",
                Type = TurfType.SevenASide,
                ImageLinks = new List<string>()
            };
            var turf9 = new Turf
            {
                Name = "Sân nhỏ 9",
                Type = TurfType.SevenASide,
                ImageLinks = new List<string>()
            };
            var turf10 = new Turf
            {
                Name = "Sân nhỏ 10",
                Type = TurfType.FiveASide,
                ImageLinks = new List<string>()
            };
            var turf11 = new Turf
            {
                Name = "Sân nhỏ 11",
                Type = TurfType.SevenASide,
                ImageLinks = new List<string>()
            };
            mainTurf1.Turfs.AddRange(new [] {turf1, turf2});
            mainTurf2.Turfs.Add(turf3);
            mainTurf3.Turfs.AddRange(new []{turf7, turf8,turf9});
            mainTurf4.Turfs.AddRange(new []{turf10});
            mainTurf5.Turfs.AddRange(new []{turf11});
            mainTurf6.Turfs.AddRange(new []{turf4, turf5});
            mainTurf7.Turfs.AddRange(new []{turf4});
            owner1.MainTurfs.AddRange(new [] {mainTurf1, mainTurf2, mainTurf3, mainTurf4, mainTurf5, mainTurf6, mainTurf7});
            await context.SaveChangesAsync();
        }

        if (!context.Schedules.Any())
        {
            var turfs = await context.Turfs.OrderBy(t => t.Name).ToListAsync();
            var customers = await context.Users.Where(u => u.Role == UserRole.Customer)
                .OrderBy(u => u.Username).ToListAsync();
            var a = DatetimeHelper.CreateRandomStartAndEndTime(27);
            var sche1 = new Schedule
            {
                Start = a.Item1,
                End = a.Item2,
                Status = ScheduleStatus.Booked,
                Turf = turfs[0],
                Customer = customers[0],
            };
            a = DatetimeHelper.CreateRandomStartAndEndTime(27);
            var sche2=new Schedule
            {
                Start = a.Item1,
                End = a.Item2,
                Status = ScheduleStatus.Pending,
                Turf = turfs[2],
                Customer = customers[0],
            };
            a = DatetimeHelper.CreateRandomStartAndEndTime(26);
            var sche3=new Schedule
            {
                Start = a.Item1,
                End = a.Item2,
                Status = ScheduleStatus.Booked,
                Turf = turfs[1],
                Customer = customers[1],
            };
            a = DatetimeHelper.CreateRandomStartAndEndTime(29);
            var sche4=new Schedule
            {
                Start = a.Item1,
                End = a.Item2,
                Status = ScheduleStatus.Booked,
                Turf = turfs[3],
                Customer = customers[2],
            };
            a = DatetimeHelper.CreateRandomStartAndEndTime(29);
            var sche5=new Schedule
            {
                Start = a.Item1,
                End = a.Item2,
                Status = ScheduleStatus.Pending,
                Turf = turfs[2],
                Customer = customers[2],
            };
            a = DatetimeHelper.CreateRandomStartAndEndTime(28);
            var sche6=new Schedule
            {
                Start = a.Item1,
                End = a.Item2,
                Status = ScheduleStatus.Booked,
                Turf = turfs[2],
                Customer = customers[1],
            };
            a = DatetimeHelper.CreateRandomStartAndEndTime(27);
            var sche7=new Schedule
            {
                Start = a.Item1,
                End = a.Item2,
                Status = ScheduleStatus.Pending,
                Turf = turfs[5],
                Customer = customers[5],
            };
            a = DatetimeHelper.CreateRandomStartAndEndTime(29);
            var sche8=new Schedule
            {
                Start = a.Item1,
                End = a.Item2,
                Status = ScheduleStatus.Booked,
                Turf = turfs[3],
                Customer = customers[2],
            };
            context.Schedules.AddRange(new []{sche1,sche2,sche3,sche4,sche5,sche6,sche7,sche8});
            await context.SaveChangesAsync();
        }
    }
}