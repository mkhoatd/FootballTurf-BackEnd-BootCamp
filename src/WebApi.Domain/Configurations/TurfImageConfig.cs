using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Configurations;

public class TurfImageConfig : IEntityTypeConfiguration<TurfImage>
{
    public void Configure(EntityTypeBuilder<TurfImage> entity)
    {
        entity.HasKey(tf => new {tf.TurfId, tf.ImageId});
        entity.HasOne(tf => tf.Image)
            .WithMany(i => i.TurfImages)
            .HasForeignKey(tf => tf.ImageId);
        entity.HasOne(tf => tf.Turf)
            .WithMany(t => t.TurfImages)
            .HasForeignKey(tf => tf.TurfId);
    }
}