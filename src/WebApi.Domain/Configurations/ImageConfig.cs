using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Configurations;

public class ImageConfig : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> entity)
    {
        entity.HasKey(i => i.Id);
        entity.HasOne(i => i.Turf)
            .WithMany(t => t.Images)
            .HasForeignKey(i => i.TurfId);
    }
}