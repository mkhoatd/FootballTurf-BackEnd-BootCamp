using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.Domain.Entities;

namespace WebApi.Persistence
{
    public class AppFootballTurfDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public IConfiguration Config { get; }

        public AppFootballTurfDbContext(DbContextOptions<AppFootballTurfDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();

                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName[6..]);
                }
            }
        }
        private static ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging(builder => builder.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information));
#pragma warning disable CS8603 // Possible null reference return.

            return serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
