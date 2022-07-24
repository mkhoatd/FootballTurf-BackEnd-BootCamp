using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Configurations;

public class MainTurfConfig:IEntityTypeConfiguration<MainTurf>
{
    public void Configure(EntityTypeBuilder<MainTurf> entity)
    {
        entity.HasKey(t => t.Id);
        entity.HasMany(t => t.Turfs)
            .WithOne(t => t.MainTurf)
            .HasForeignKey(t => t.MainTurfId);
        entity.HasOne(t => t.Owner)
            .WithMany(o => o.MainTurfs)
            .HasForeignKey(t => t.OwnerId);

    }
}