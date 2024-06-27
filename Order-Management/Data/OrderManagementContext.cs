using Microsoft.EntityFrameworkCore;
using Order_Management.app.database.models;

namespace Order_Management.Data
{
    public partial class OrderManagementContext : DbContext
    {
        public OrderManagementContext()
        {
        }

        public OrderManagementContext(DbContextOptions<OrderManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("addresses");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .HasColumnType("char(36)")
                    .IsRequired()
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.AddressLine1).HasMaxLength(512);
                entity.Property(e => e.AddressLine2).HasMaxLength(512);
                entity.Property(e => e.City).HasMaxLength(64);
                entity.Property(e => e.Country).HasMaxLength(64);
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.State).HasMaxLength(64);
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
                entity.Property(e => e.ZipCode).HasMaxLength(64);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
