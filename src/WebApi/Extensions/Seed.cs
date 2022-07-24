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
                User.CreateCustomer("customer1", "password"),
                User.CreateCustomer("customer2", "password"),
                User.CreateCustomer("customer3", "password")
            };
            context.Users.AddRange(customers);
            var productOwners = new List<User>
            {
                User.CreateProductOwner("owner1", "password"),
                User.CreateProductOwner("owner2", "password"),
                User.CreateProductOwner("owner3", "password")
            };
            context.AddRange(productOwners);
            await context.SaveChangesAsync();
        }

        if (!context.Turfs.Any())
        {
            var owner1 = await context.Users.Where(u => u.Role == UserRole.ProductOwner)
                .Include(u => u.Turfs)
                .FirstOrDefaultAsync();
            owner1.Turfs.Add(new Turf()
            {
                Name = "Turf 1",
                Address = "Address 1",
                Latitude = "43",
                Longitude = "105",
                Type = TurfType.San7
            });
            await context.SaveChangesAsync();
        }
    }
}