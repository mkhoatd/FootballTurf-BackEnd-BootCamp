using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Configurations;

public class TurfConfig : IEntityTypeConfiguration<Turf>
{
    public void Configure(EntityTypeBuilder<Turf> entity)
    {
        entity.HasKey(t => t.Id);
        entity.HasOne(t => t.MainTurf)
            .WithMany(mt => mt.Turfs)
            .HasForeignKey(t => t.MainTurfId);
        entity.HasMany(t => t.Schedules)
            .WithOne(s => s.Turf)
            .HasForeignKey(s => s.TurfId);
        entity.HasMany(t=>t.Images)
            .WithOne(i=>i.Turf)
            .HasForeignKey(i=>i.TurfId);
    }
}