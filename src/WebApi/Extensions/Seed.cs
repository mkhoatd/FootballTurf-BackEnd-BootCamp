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
            //mainTurf1.ImageLinks.AddRange(new []
            //{
            //    "https://picsum.photos/id/1003/200/300",
            //    "https://picsum.photos/id/1004/200/300"
            //});
            //mainTurf2.ImageLinks.AddRange(new []
            //{
            //    "https://picsum.photos/id/1019/200/300",
            //    "https://picsum.photos/id/102/200/300"
            //});
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
            owner1.MainTurfs.AddRange(new [] {mainTurf1, mainTurf2, mainTurf3, mainTurf4, mainTurf5, mainTurf6, mainTurf7});
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