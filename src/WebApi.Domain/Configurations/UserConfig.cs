using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(u => u.Id);
        entity.HasMany(u => u.MainTurfs)
            .WithOne(mt => mt.Owner)
            .HasForeignKey(mt => mt.OwnerId);
    }
}