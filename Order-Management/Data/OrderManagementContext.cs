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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("addresses");

                entity.Property(e => e.Id)
                    .HasColumnType("char(36)")
                    .IsRequired();

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(512);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(512);

                entity.Property(e => e.City)
                    .HasMaxLength(64);

                entity.Property(e => e.Country)
                    .HasMaxLength(64);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(64);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(64);

                entity.HasMany(c => c.CustomerAddresses)
                    .WithOne(ca => ca.Address)
                    .HasForeignKey(ca => ca.AddressId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.Id)
                    .HasColumnType("char(36)")
                    .IsRequired();

                entity.Property(e => e.ReferenceId)
                    .HasColumnType("char(36)")
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(128);

                entity.Property(e => e.Email)
                    .HasMaxLength(512);

                entity.Property(e => e.PhoneCode)
                    .HasMaxLength(8);

                entity.Property(e => e.Phone)
                    .HasMaxLength(64);

                entity.Property(e => e.ProfilePicture)
                    .HasMaxLength(512);

                entity.Property(e => e.TaxNumber)
                    .HasMaxLength(64);

                entity.Property(e => e.DefaultShippingAddressId)
                    .HasColumnType("char(36)");


                entity.Property(e => e.DefaultBillingAddressId)
                    .HasColumnType("char(36)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime");

                entity.HasMany(c => c.CustomerAddresses)
                    .WithOne(ca => ca.Customer)
                    .HasForeignKey(ca => ca.CustomerId);
            });

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.ToTable("customer_addresses");

                entity.HasKey(e => e.Id); // Assuming Id is defined as primary key

                entity.Property(e => e.Id)
                    .HasColumnType("char(36)")
                    .IsRequired();

                entity.Property(e => e.CustomerId)
                    .HasColumnType("char(36)");

                entity.Property(e => e.AddressId)
                    .HasColumnType("char(36)");

                entity.Property(e => e.AddressType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasConversion<string>(); // Ensure enum is stored as string

                entity.Property(e => e.IsFavorite);

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
