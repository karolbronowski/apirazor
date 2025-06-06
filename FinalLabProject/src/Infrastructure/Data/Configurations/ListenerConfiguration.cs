using FinalLabProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalLabProject.Infrastructure.Data.Configurations;

public class ListenerConfiguration : IEntityTypeConfiguration<Listener>
{
    public void Configure(EntityTypeBuilder<Listener> builder)
    {
        builder.HasKey(l => l.Id);

        builder.HasIndex(l => l.UserId)
            .IsUnique();

        builder.Property(l => l.UserId)
            .HasMaxLength(450)
            .IsRequired();

        builder.Property(l => l.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.OwnsOne(l => l.Username, u =>
        {
            u.Property(un => un.Value)
                .HasColumnName("Username")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(l => l.Email, e =>
        {
            e.Property(em => em.Value)
                .HasColumnName("Email")
                .HasMaxLength(255)
                .IsRequired();
        });

        builder
            .HasMany(l => l.FavouriteSongs)
            .WithMany()
            .UsingEntity(j => j.ToTable("ListenerFavouriteSongs"));
    }
}