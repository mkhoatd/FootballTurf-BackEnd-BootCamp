using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.Domain.Configurations;
using WebApi.Domain.Entities;

namespace WebApi.Persistence
{
    public class AppFootballTurfDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TurfImage> TurfImages { get; set; }
        public DbSet<Turf> Turfs { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Image> Images { get; set; }

        public AppFootballTurfDbContext(DbContextOptions<AppFootballTurfDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new TurfImageConfig());
            builder.ApplyConfiguration(new TurfConfig());
            builder.ApplyConfiguration(new ScheduleConfig());
            builder.ApplyConfiguration(new ImageConfig());
            builder.ApplyConfiguration(new MainTurfConfig());
        }

    }
}
