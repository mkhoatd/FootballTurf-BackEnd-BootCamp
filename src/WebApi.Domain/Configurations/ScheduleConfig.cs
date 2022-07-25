using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Configurations;

public class ScheduleConfig : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> entity)
    {
        entity.HasKey(s => s.Id);
        entity.HasOne(s => s.Turf)
            .WithMany(t => t.Schedules)
            .HasForeignKey(s => s.TurfId);
        entity.HasOne(s => s.Customer)
            .WithMany(u => u.Schedules)
            .HasForeignKey(s => s.CustomerId);
    }
}