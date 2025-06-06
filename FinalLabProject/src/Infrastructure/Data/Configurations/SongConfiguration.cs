using FinalLabProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalLabProject.Infrastructure.Data.Configurations;

public class SongConfiguration : IEntityTypeConfiguration<Song>
{
    public void Configure(EntityTypeBuilder<Song> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(s => s.ArtistId)
            .IsRequired();

        builder.Property(s => s.ListenedTimes)
            .HasDefaultValue(0)
            .IsRequired();

        // Audit fields
        builder.Property(s => s.Created)
            .IsRequired();

        builder.Property(s => s.LastModified)
            .IsRequired();

        builder.Property(s => s.CreatedBy)
            .HasMaxLength(100);

        builder.Property(s => s.LastModifiedBy)
            .HasMaxLength(100);
    }
}