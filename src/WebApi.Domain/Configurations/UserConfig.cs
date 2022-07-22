using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(u => u.Id);
        entity.HasMany(u => u.Turfs)
            .WithOne(t => t.Owner)
            .HasForeignKey(t => t.OwnerId);
        entity.HasMany(u => u.Schedules)
            .WithOne(s => s.Customer)
            .HasForeignKey(s => s.CustomerId);
    }
}