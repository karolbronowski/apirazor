using FinalLabProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalLabProject.Infrastructure.Data.Configurations;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasIndex(a => a.UserId)
            .IsUnique();

        builder.Property(a => a.UserId)
            .HasMaxLength(450) // Identity default string PK length
            .IsRequired();

        builder.Property(a => a.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.OwnsOne(a => a.Username, u =>
        {
            u.Property(un => un.Value)
                .HasColumnName("Username")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(a => a.Email, e =>
        {
            e.Property(em => em.Value)
                .HasColumnName("Email")
                .HasMaxLength(255)
                .IsRequired();
        });

        builder.Property(a => a.Bio)
            .HasMaxLength(1000);

        builder.OwnsOne(a => a.PayoutTier, pt =>
        {
            pt.Property(p => p.Tier)
                .HasColumnName("PayoutTier")
                .IsRequired();
        });
    }
}