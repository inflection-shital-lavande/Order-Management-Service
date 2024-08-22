using Microsoft.EntityFrameworkCore;
using order_management.auth;
using order_management.database.models;
using order_management.domain_types.enums;
using Order_Management.src.database.models;
using System.Linq;


namespace order_management.database;


public  class OrderManagementContext : DbContext
{
    public OrderManagementContext()
    {
    }

    public OrderManagementContext(DbContextOptions<OrderManagementContext> options)
        : base(options)
    {
    }
    

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Cart> Carts { get; set; }

    public DbSet<Coupon> Coupons { get; set; }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerAddress> CustomerAddresses { get; set; }

    public DbSet<Merchant> Merchants { get; set; }


    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderCoupon> OrderCoupons { get; set; }
    public DbSet<OrderHistory> OrderHistorys { get; set; }
    public DbSet<OrderLineItem> OrderLineItems { get; set; }

    public DbSet<OrderType> OrderTypes { get; set; }

    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<FileMetadata> FileMetadatas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("addresses");

            entity.Property(e => e.Id)
                .HasColumnType("char(36)")
                .IsRequired();

            entity.Property(e => e.AddressLine1)
                .HasMaxLength(512)
                .IsRequired();

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

           // //One-to-many relationship with Merchant
            entity.HasMany(e => e.Merchants)
           .WithOne(e => e.Address)
           .HasForeignKey(e => e.AddressId)
           .OnDelete(DeleteBehavior.SetNull);


        });
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("carts");

            entity.Property(e => e.Id)
                .HasColumnType("char(36)")
                .IsRequired();

            entity.Property(e => e.CustomerId)
                .HasColumnType("char(36)");

            entity.Property(e => e.TotalItemsCount)
                .HasDefaultValue(0);

            entity.Property(e => e.TotalTax)
                .HasDefaultValue(0.0f);

            entity.Property(e => e.TotalDiscount)
                .HasDefaultValue(0.0f);

            entity.Property(e => e.TotalAmount)
                .HasDefaultValue(0.0f);

            entity.Property(e => e.CartToOrderTimestamp)
                .HasColumnType("datetime");

            entity.Property(e => e.AssociatedOrderId)
                .HasColumnType("char(36)");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime");

            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime");

            // One-to-many relationship with Order
            entity.HasMany(e => e.Orders)
                .WithOne(e => e.Cart)
                .HasForeignKey(e => e.AssociatedCartId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-many relationship with OrderLineItem
            entity.HasMany(e => e.OrderLineItems)
                .WithOne(e => e.Cart)
                .HasForeignKey(e => e.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.ToTable("coupons");

            entity.Property(e => e.Id)
                .HasColumnType("char(36)")
                .IsRequired();

            entity.Property(e => e.Name)
                .HasMaxLength(64);

            entity.Property(e => e.Description)
                .HasMaxLength(1024);

            entity.Property(e => e.CouponCode)
                .HasMaxLength(64);

            entity.Property(e => e.CouponType)
                .HasMaxLength(64);

            entity.Property(e => e.Discount)
                 .HasColumnType("float");

            entity.Property(e => e.DiscountType)
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            entity.Property(e => e.DiscountPercentage)
                .HasColumnType("float");

            entity.Property(e => e.DiscountMaxAmount)
                .HasColumnType("float");

            entity.Property(e => e.StartDate)
              .HasColumnType("datetime");

            entity.Property(e => e.EndDate)
              .HasColumnType("datetime");

            entity.Property(e => e.MaxUsage)
              .HasColumnType("int");

            entity.Property(e => e.MaxUsagePerUser)
             .HasColumnType("int");

            entity.Property(e => e.MaxUsagePerOrder)
             .HasColumnType("int");

            entity.Property(e => e.MinOrderAmount)
             .HasColumnType("float");

            entity.Property(e => e.IsActive)
           .HasColumnType("bit");

            entity.Property(e => e.IsDeleted)
           .HasColumnType("bit");


            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime");

            entity.Property(e => e.CreatedBy)
                 .HasColumnType("char(36)")
                .IsUnicode(false);

            
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime");

            // Configure relationships with OrderCoupon
            entity.HasMany(c => c.OrderCoupons)
                .WithOne()
                .HasForeignKey(oc => oc.CouponId)
                .OnDelete(DeleteBehavior.Cascade);
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

           
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.ToTable("customer_addresses");

            entity.HasKey(e => new { e.CustomerId, e.AddressId });

            entity.Property(e => e.Id)
                .HasColumnType("char(36)")
                .IsRequired();

            entity.Property(e => e.AddressType)
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion<string>();

            entity.Property(e => e.IsFavorite);

            entity.HasOne(e => e.Customer)
                .WithMany(e => e.CustomerAddresses)
                .HasForeignKey(e => e.CustomerId);

            entity.HasOne(e => e.Address)
                .WithMany(e => e.CustomerAddresses)
                .HasForeignKey(e => e.AddressId);
        });
        modelBuilder.Entity<Merchant>(entity =>
        {
            entity.ToTable("merchants");

            entity.Property(e => e.Id)
                .HasColumnType("char(36)")
                .IsRequired();

            entity.Property(e => e.ReferenceId)
                .HasColumnType("char(36)");

            entity.Property(e => e.Name)
                .HasMaxLength(512)
                .IsRequired();

            entity.Property(e => e.Email)
                .HasMaxLength(512);

            entity.Property(e => e.Phone)
                .HasMaxLength(64);

            entity.Property(e => e.Logo)
                .HasMaxLength(512);

            entity.Property(e => e.WebsiteUrl)
                .HasMaxLength(512);

            entity.Property(e => e.TaxNumber)
                .HasMaxLength(64);

            entity.Property(e => e.GSTNumber)
                .HasMaxLength(64);

            entity.Property(e => e.AddressId)
                .HasColumnType("char(36)");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

           

            
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            

            entity.Property(e => e.Id)
                .HasColumnType("int")
                .IsRequired();

            entity.Property(e => e.Name)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            entity.Property(e => e.Email)
                .HasColumnType("nvarchar(MAX)");

            entity.Property(e => e.Password)
                .HasColumnType("nvarchar(MAX)");
        });


       



    
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("orders");

            entity.Property(e => e.Id)
                .HasColumnType("char(36)")
                .IsRequired();

            entity.Property(e => e.DisplayCode)
                .HasMaxLength(36)
                .IsRequired();

            entity.Property(e => e.OrderStatus)
                .IsRequired();

            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(64)
                .IsRequired();

            entity.Property(e => e.AssociatedCartId)
                .HasColumnType("char(36)");

            entity.Property(e => e.TotalItemsCount)
                .HasDefaultValue(0);

            entity.Property(e => e.OrderDiscount)
                .HasDefaultValue(0.0);

            entity.Property(e => e.TipApplicable)
                .HasDefaultValue(false);

            entity.Property(e => e.TipAmount)
                .HasDefaultValue(0.0);

            entity.Property(e => e.TotalTax)
                .HasDefaultValue(0.0);

            entity.Property(e => e.TotalDiscount)
                .HasDefaultValue(0.0);

            entity.Property(e => e.TotalAmount)
                .HasDefaultValue(0.0);

            entity.Property(e => e.Notes)
                .HasMaxLength(1024);

            entity.Property(e => e.CustomerId)
                .HasColumnType("char(36)");

            entity.Property(e => e.ShippingAddressId)
                .HasColumnType("char(36)");

            entity.Property(e => e.BillingAddressId)
                .HasColumnType("char(36)");

            entity.Property(e => e.OrderTypeId)
                .HasColumnType("char(36)");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime");

            // Relationships

            // One Cart to Many Orders
            entity.HasOne(e => e.Cart)
                .WithMany(c => c.Orders)
                .HasForeignKey(e => e.AssociatedCartId)
                .OnDelete(DeleteBehavior.SetNull);

            // One Customer to Many Orders
            entity.HasOne(e => e.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            // One Order to One OrderHistory
            entity.HasOne(e => e.OrderHistory)
                .WithOne(h => h.Order)
                .HasForeignKey<OrderHistory>(h => h.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many Orders to One OrderType
            entity.HasOne(e => e.OrderType)
                .WithMany(t => t.Orders)
                .HasForeignKey(e => e.OrderTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            // One Order to Many OrderLineItems
            entity.HasMany(e => e.OrderLineItems)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);



            // One Order to Many PaymentTransactions
            entity.HasMany(e => e.PaymentTransactions)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationships with OrderCoupon
            entity.HasMany(e => e.OrderCoupons)
                .WithOne(oc => oc.Order)
                .HasForeignKey(oc => oc.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderCoupon>(entity =>
        {
            entity.ToTable("order_coupons");

            entity.HasKey(oc => oc.Id);

            entity.Property(oc => oc.Code)
                .HasMaxLength(64)
                .IsRequired();

            entity.Property(oc => oc.CouponId)
                .IsRequired();

            entity.Property(oc => oc.OrderId)
                .IsRequired();

            entity.Property(oc => oc.DiscountValue)
                .HasDefaultValue(0.0)
                .IsRequired();

            entity.Property(oc => oc.DiscountPercentage)
                .HasDefaultValue(0.0)
                .IsRequired();

            entity.Property(oc => oc.DiscountMaxAmount)
                .HasDefaultValue(0.0)
                .IsRequired();

            entity.Property(oc => oc.Applied)
                .HasDefaultValue(true)
                .IsRequired();

            entity.Property(oc => oc.AppliedAt);

            entity.Property(oc => oc.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            entity.Property(oc => oc.UpdatedAt);
        });

       
        modelBuilder.Entity<OrderHistory>(entity =>
        {
            entity.ToTable("order_histories");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.OrderId)
                .IsRequired()
                .HasMaxLength(36);

            entity.Property(e => e.PreviousStatus)
                .IsRequired();

            entity.Property(e => e.Status)
                .IsRequired();

            entity.Property(e => e.UpdatedByUserId)
                .HasMaxLength(36);

            entity.Property(e => e.Timestamp)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<OrderLineItem>(entity =>
        {
            entity.ToTable("order_line_items");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasMaxLength(512)
                .IsRequired();

            entity.Property(e => e.Quantity)
                .IsRequired()
                .HasDefaultValue(0);

            entity.Property(e => e.UnitPrice)
                .IsRequired()
                .HasDefaultValue(0.0);

            entity.Property(e => e.Discount)
                .IsRequired()
                .HasDefaultValue(0.0);

            entity.Property(e => e.Tax)
                .IsRequired()
                .HasDefaultValue(0.0);

            entity.Property(e => e.ItemSubTotal)
                .IsRequired()
                .HasDefaultValue(0.0);

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime");

            // Configure one-to-many relationship with Cart
            entity.HasOne(e => e.Cart)
                .WithMany(c => c.OrderLineItems)
                .HasForeignKey(e => e.CartId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure one-to-many relationship with Order
            entity.HasOne(e => e.Order)
                .WithMany(o => o.OrderLineItems)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        // OrderType entity configuration
        modelBuilder.Entity<OrderType>(entity =>
        {
            entity.ToTable("order_types");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasMaxLength(64);

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime");

            // Configure one-to-many relationship with Order
            entity.HasMany(e => e.Orders)
                .WithOne(o => o.OrderType)
                .HasForeignKey(o => o.OrderTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PaymentTransaction>(entity =>
        {
            entity.ToTable("payment_transactions");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(64);

            entity.Property(e => e.PaymentStatus)
                .IsRequired()
                .HasDefaultValue(PaymentStatusTypes.UNKNOWN);

            entity.Property(e => e.PaymentAmount)
                .IsRequired()
                .HasDefaultValue(0.0);

            entity.Property(e => e.IsRefund)
                .IsRequired()
                .HasDefaultValue(false);

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime");

            // Configure one-to-many relationship with Order
            entity.HasOne(e => e.Order)
                .WithMany(o => o.PaymentTransactions)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        //OnModelCreatingPartial(modelBuilder);
    }

   // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
