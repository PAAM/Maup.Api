using Maup.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Maup.Infrastructure.Data
{
    public partial class MUPContext : DbContext
    {
        public MUPContext()
        {
        }

        public MUPContext(DbContextOptions<MUPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Owner> Owners { get; set; } = null!;
        public virtual DbSet<Property> Properties { get; set; } = null!;
        public virtual DbSet<PropertyImage> PropertyImages { get; set; } = null!;
        public virtual DbSet<PropertyTrace> PropertyTraces { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(entity =>
            {
                entity.ToTable("Owner");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                .HasColumnName("IdOwner");


                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Photo).HasColumnType("image");
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("Property");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                .HasColumnName("IdProperty");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.IdOwnerNavigation)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.IdOwner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Property__IdOwne__628FA481");
            });

            modelBuilder.Entity<PropertyImage>(entity =>
            {
                entity.ToTable("PropertyImage");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                .HasColumnName("IdPropertyImage");

                entity.Property(e => e.File)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPropertyNavigation)
                    .WithMany(p => p.PropertyImages)
                    .HasForeignKey(d => d.IdProperty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PropertyI__IdPro__693CA210");
            });

            modelBuilder.Entity<PropertyTrace>(entity =>
            {
                entity.ToTable("PropertyTrace");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                .HasColumnName("IdPropertyTrace");

                entity.Property(e => e.DateSale).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Tax).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Value).HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.IdPropertyNavigation)
                    .WithMany(p => p.PropertyTraces)
                    .HasForeignKey(d => d.IdProperty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PropertyT__IdPro__6383C8BA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
