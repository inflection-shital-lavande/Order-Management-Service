using Microsoft.EntityFrameworkCore;
using order_management.auth;
using order_management.database.models;
using order_management.domain_types.enums;
using Order_Management.src.database.models;
using System.Linq;


namespace order_management.database;


public class OrderManagementContext : DbContext
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
            //one to one relatinship

            entity.HasOne(e => e.Merchant)
                .WithOne(oh => oh.Addressess)
                .HasForeignKey<Merchant>(oh => oh.AddressId)
                .OnDelete(DeleteBehavior.Cascade);

            /* //One-to-many relationship with Merchant
             entity.HasMany(e => e.Merchants)
            .WithOne(e => e.Addressess)
            .HasForeignKey(e => e.AddressId)
            .OnDelete(DeleteBehavior.SetNull);*/


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
                .HasColumnType("int")
                .HasDefaultValue(0);

            entity.Property(e => e.TotalTax)
                .HasColumnType("float")
                .HasDefaultValue(0.0f);

            entity.Property(e => e.TotalDiscount)
                .HasColumnType("float")
                .HasDefaultValue(0.0f);

            entity.Property(e => e.TotalAmount)
                .HasColumnType("float")
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

            entity.HasOne(e => e.Customers)
                .WithMany(c => c.carts)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            /* // One-to-many relationship with Order
             entity.HasMany(e => e.Orders)
                 .WithOne(e => e.Carts)
                 .HasForeignKey(e => e.AssociatedCartId)
                 .OnDelete(DeleteBehavior.Cascade);

             // One-to-many relationship with OrderLineItem
             entity.HasMany(e => e.OrderLineItems)
                 .WithOne(e => e.Carts)
                 .HasForeignKey(e => e.CartId)
                 .OnDelete(DeleteBehavior.Cascade);*/
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

            /* // Configure relationships with OrderCoupon
             entity.HasMany(c => c.OrderCoupons)
                 .WithOne()
                 .HasForeignKey(oc => oc.CouponId)
                 .OnDelete(DeleteBehavior.Cascade);*/
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
                .HasMaxLength(512)
                .IsRequired();

            entity.Property(e => e.PhoneCode)
                .HasMaxLength(8)
                .IsRequired();

            entity.Property(e => e.Phone)
                .HasMaxLength(64)
                .IsRequired();

            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(512);

            entity.Property(e => e.TaxNumber)
                .HasMaxLength(64);

            entity.Property(e => e.DefaultShippingAddressId)
                .HasColumnType("char(36)")
                .IsRequired();


            entity.Property(e => e.DefaultBillingAddressId)
                .HasColumnType("char(36)")
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime");

          /*  modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.HasKey(ca => new { ca.CustomerId, ca.AddressId });
                entity.Property(ca => ca.AddressType).HasMaxLength(50).IsRequired();
                entity.Property(ca => ca.IsFavorite).IsRequired();
            });*/


        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.ToTable("customer_addresses");

            entity.Property(e => e.Id)
                 .IsRequired()
                 .HasColumnType("char(36)");

            entity.HasKey(ca => new { ca.CustomerId, ca.AddressId });

            entity.HasOne(e => e.Customers)
               .WithMany(c => c.CustomerAddresses)
               .HasForeignKey(e => e.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Addresses)
                .WithMany(a => a.CustomerAddresses)
                .HasForeignKey(e => e.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
            /* entity.HasKey(e => e.Id);

             entity.Property(e => e.Id)
                 .IsRequired()
                 .HasColumnType("char(36)");

             entity.Property(e => e.CustomerId)
                 .HasColumnType("char(36)");

             entity.Property(e => e.AddressId)
                 .HasColumnType("char(36)");*/

            entity.Property(e => e.AddressType)
                .IsRequired()
                .HasMaxLength(50);
               // .HasDefaultValue(AddressTypes.SHIPPING);

            entity.Property(e => e.IsFavorite)
                .HasColumnType("bit")
                .IsRequired();

           
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
            //one to one
            entity.HasOne(e => e.Addressess)
               .WithOne(oh => oh.Merchant)
               .HasForeignKey<Merchant>(oh => oh.AddressId)
               .OnDelete(DeleteBehavior.Cascade);



           /* entity.HasOne(e => e.Addressess)
                .WithMany(c => c.Merchants)
                .HasForeignKey(e => e.AddressId)
                .OnDelete(DeleteBehavior.SetNull);*/


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

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("char(36)");

            entity.Property(e => e.DisplayCode)
                .HasMaxLength(64)
                .HasColumnType("nvarchar(64)");

            entity.Property(e => e.OrderStatus)
                .IsRequired()
                .HasDefaultValue(OrderStatusTypes.DRAFT)
                .HasColumnType("nvarchar(64)");

            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(64)
                .HasColumnType("nvarchar(64)");

            entity.Property(e => e.AssociatedCartId)
                .HasColumnType("char(36)");

            entity.Property(e => e.TotalItemsCount)
                .HasDefaultValue(0)
                .HasColumnType("int");

            entity.Property(e => e.OrderDiscount)
                .HasDefaultValue(0.0)
                .HasColumnType("float(53)");

            entity.Property(e => e.TipApplicable)
                .HasDefaultValue(false)
                .HasColumnType("bit");

            entity.Property(e => e.TipAmount)
                .HasDefaultValue(0.0)
                .HasColumnType("float(53)");

            entity.Property(e => e.TotalTax)
                .HasDefaultValue(0.0)
                .HasColumnType("float(53)");

            entity.Property(e => e.TotalDiscount)
                .HasDefaultValue(0.0)
                .HasColumnType("float(53)");

            entity.Property(e => e.TotalAmount)
                .HasDefaultValue(0.0)
                .HasColumnType("float(53)");

            entity.Property(e => e.Notes)
                .HasMaxLength(1024)
                .HasColumnType("nvarchar(1024)");

            entity.Property(e => e.CustomerId)
                .HasColumnType("char(36)");

            entity.Property(e => e.ShippingAddressId)
                .HasColumnType("char(36)");

            entity.Property(e => e.BillingAddressId)
                .HasColumnType("char(36)");

            entity.Property(e => e.OrderTypeId)
                .HasColumnType("char(36)");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime");

            // Foreign key relationships
            entity.HasOne(e => e.Carts)
                .WithMany(c => c.Orders)
                .HasForeignKey(e => e.AssociatedCartId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Customers)
                .WithMany(c => c.Orders)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ShippingAddress)
                .WithMany(c => c.ShippingOrders)
                .HasForeignKey(e => e.ShippingAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.BillingAddress)
                .WithMany(c => c.BillingOrders)
                .HasForeignKey(e => e.BillingAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.OrderTypes)
                .WithMany(c => c.Orders)
                .HasForeignKey(e => e.OrderTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure one-to-one relationship with OrderHistory
            entity.HasOne(e => e.OrderHistorys)
                .WithOne(oh => oh.Orders)
                .HasForeignKey<OrderHistory>(oh => oh.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


        });

        modelBuilder.Entity<OrderCoupon>(entity =>
        {
           // base.OnModelCreating(modelBuilder);

            // Configure the many-to-many relationship using OrderCoupon as the join entity

           
            //entity.ToTable("order_Coupons");

            // Configure the properties in OrderCoupon
            //modelBuilder.Entity<OrderCoupon>()
            //    .Property(oc => oc.Id)
            //    .HasMaxLength(36)
            //    .HasDefaultValueSql("NEWID()"); // Generates a new GUID by default
            /* modelBuilder.Entity<OrderCoupon>()
            .HasKey(oc => oc.Id);*/


          /*  entity.Property(e => e.Id)
                  .HasColumnType("char(36)")
                  .IsRequired();
            // Code is not required, and has a max length of 64

            modelBuilder.Entity<OrderCoupon>()
                .Property(oc => oc.DiscountValue)
                .HasDefaultValue(0.0); // Default discount value is 0.0

            modelBuilder.Entity<OrderCoupon>()
                .Property(oc => oc.DiscountPercentage)
                .HasDefaultValue(0.0); // Default discount percentage is 0.0

            modelBuilder.Entity<OrderCoupon>()
                .Property(oc => oc.DiscountMaxAmount)
                .HasDefaultValue(0.0); // Default maximum discount amount is 0.0

            modelBuilder.Entity<OrderCoupon>()
                .Property(oc => oc.Applied)
                .HasDefaultValue(true); // Default applied value is true

            modelBuilder.Entity<OrderCoupon>()
                .Property(oc => oc.AppliedAt)
                .HasDefaultValueSql("NULL"); // Default applied at is null (can be nullable)

            modelBuilder.Entity<OrderCoupon>()
                .Property(oc => oc.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()"); // Set default value to the current UTC time

            modelBuilder.Entity<OrderCoupon>()
                .Property(oc => oc.UpdatedAt)
                .ValueGeneratedOnUpdate(); // Update timestamp on record update

            modelBuilder.Entity<OrderCoupon>()
             .HasKey(oc => new { oc.OrderId, oc.CouponId });

            modelBuilder.Entity<OrderCoupon>()
                .HasOne(oc => oc.Order)
                .WithMany(o => o.OrderCoupons)
                .HasForeignKey(oc => oc.OrderId);

            modelBuilder.Entity<OrderCoupon>()
                .HasOne(oc => oc.Coupon)
                .WithMany(c => c.OrderCoupons)
                .HasForeignKey(oc => oc.CouponId);*/

              entity.ToTable("order_coupons");

              entity.Property(e => e.Id)
                 .HasColumnType("char(36)")
                 .IsRequired();

              entity.Property(oc => oc.Code)
                  .HasMaxLength(64)
                  .IsRequired();

           /*   entity.Property(oc => oc.CouponId)
                  .IsRequired();

              entity.Property(oc => oc.OrderId)
                  .IsRequired();*/

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
                  .HasDefaultValueSql("GETUTCDATE()")
                  .IsRequired();

           /* modelBuilder.Entity<OrderCoupon>()
                .Property(oc => oc.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");*/

            entity.Property(oc => oc.UpdatedAt);

           // modelBuilder.Entity<OrderCoupon>()
             entity.HasKey(oc => new { oc.OrderId, oc.CouponId });

           // modelBuilder.Entity<OrderCoupon>()
                entity.HasOne(oc => oc.Order)
                .WithMany(o => o.OrderCoupons)
                .HasForeignKey(oc => oc.OrderId);

          //  modelBuilder.Entity<OrderCoupon>()
                entity.HasOne(oc => oc.Coupon)
                .WithMany(c => c.OrderCoupons)
                .HasForeignKey(oc => oc.CouponId);

            /*CREATE TABLE [dbo].[order_coupons] (
    [Id]                 CHAR (36)     NOT NULL,
    [Code]               NVARCHAR (64) NULL,
    [CouponId]           CHAR (36)    NOT NULL,
    [OrderId]            CHAR (36)    NOT NULL,
    [DiscountValue]      FLOAT (53)    DEFAULT ((0.00)) NULL,
    [DiscountPercentage] FLOAT (53)    DEFAULT ((0.00)) NULL,
    [DiscountMaxAmount]  FLOAT (53)    DEFAULT ((0.00)) NULL,
    [Applied]            BIT           DEFAULT ('True') NULL,
    [AppliedAt]          DATETIME      NULL,
    [CreatedAt]          DATETIME      NULL,
    [UpdatedAt]          NCHAR (10)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CouponId]) REFERENCES [dbo].[coupons] ([Id]),
    FOREIGN KEY ([OrderId]) REFERENCES [dbo].[orders] ([Id])
);
            */




        });


        modelBuilder.Entity<OrderHistory>(entity =>
        {
            entity.ToTable("order_histories");

            entity.Property(e => e.Id)
               .HasColumnType("char(36)")
               //.IsRequired()
               .HasMaxLength(36);

            entity.Property(e => e.OrderId)
                .IsRequired()
                .HasColumnType("char(36)")
                .HasMaxLength(36);

            entity.Property(e => e.PreviousStatus)
                .HasColumnType("nvarchar")
                .HasMaxLength(64)
                .HasDefaultValue(OrderStatusTypes.DRAFT)
                //.IsRequired()
                ;

            entity.Property(e => e.Status)
                .HasColumnType("nvarchar")
                .HasMaxLength(64)
                .HasDefaultValue(OrderStatusTypes.DRAFT)
                //.IsRequired()
                ;

            entity.Property(e => e.UpdatedByUserId)
             .HasColumnType("char(36)")
                .HasMaxLength(36);

            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                // .IsRequired()
                ;

            // Configure one-to-one relationship with OrderHistory
            entity.HasOne(e => e.Orders)
                .WithOne(oh => oh.OrderHistorys)
                .HasForeignKey<OrderHistory>(oh => oh.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


        });

        modelBuilder.Entity<OrderLineItem>(entity =>
        {
            entity.ToTable("order_line_items");

            entity.Property(e => e.Id)
                .HasColumnType("char(36)")
                .IsRequired();

            entity.Property(e => e.Name)
                .HasMaxLength(512)
                .HasColumnType("nvarchar")
                .IsRequired();

            entity.Property(e => e.CatalogId)
              .HasColumnType("char(36)")
              ;

            entity.Property(e => e.Quantity)
                 .HasColumnType("int")
                .HasDefaultValue(0);

            entity.Property(e => e.UnitPrice)
              .HasColumnType("float")
                .HasDefaultValue(0.0);

            entity.Property(e => e.Discount)
                .HasColumnType("float")
                .HasDefaultValue(0.0);

            entity.Property(e => e.DiscountSchemeId)
                .HasColumnType("char(36)")
                .IsRequired();

            entity.Property(e => e.Tax)
               .HasColumnType("float")
                .HasDefaultValue(0.0);

            entity.Property(e => e.ItemSubTotal)
               .HasColumnType("float")
                .HasDefaultValue(0.0);

            entity.Property(e => e.OrderId)
                .HasColumnType("char(36)")
                .IsRequired();

            entity.Property(e => e.CartId)
                .HasColumnType("char(36)")
                .IsRequired();

            entity.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Configure one-to-many relationship with Cart
            entity.HasOne(e => e.Carts)
                .WithMany(c => c.OrderLineItems)
                .HasForeignKey(e => e.CartId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure one-to-many relationship with Order
            entity.HasOne(e => e.Orders)
                .WithMany(o => o.OrderLineItems)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        // OrderType entity configuration
        modelBuilder.Entity<OrderType>(entity =>
        {
            entity.ToTable("order_types");

            entity.Property(e => e.Id)
               .HasColumnType("char(36)")
               .IsRequired();

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

            /* // Configure one-to-many relationship with Order
             entity.HasMany(e => e.Orders)
                 .WithOne(o => o.OrderTypes)
                 .HasForeignKey(o => o.OrderTypeId)
                 .OnDelete(DeleteBehavior.Restrict);*/
        });


        modelBuilder.Entity<PaymentTransaction>(entity =>
        {
            entity.ToTable("payment_transactions");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("CHAR(36)");

            entity.Property(e => e.DisplayCode)
                .HasColumnName("DisplayCode")
                .HasMaxLength(64)
                .HasColumnType("NVARCHAR");

            entity.Property(e => e.InvoiceNumber)
                .HasColumnName("InvoiceNumber")
                .HasMaxLength(64)
                .HasColumnType("NVARCHAR");

            entity.Property(e => e.BankTransactionId)
                .HasColumnName("BankTransactionId")
                .HasColumnType("CHAR(36)");

            entity.Property(e => e.PaymentGatewayTransactionId)
                .HasColumnName("PaymentGatewayTransactionId")
                .HasColumnType("CHAR(36)");

            entity.Property(e => e.PaymentStatus)
                .HasColumnName("PaymentStatus")
                .HasMaxLength(64)
                .HasColumnType("NVARCHAR")
                .HasDefaultValue(PaymentStatusTypes.UNKNOWN);

            entity.Property(e => e.PaymentMode)
                .HasColumnName("PaymentMode")
                .HasMaxLength(36)
                .HasColumnType("NVARCHAR");

            entity.Property(e => e.PaymentAmount)
                .HasColumnName("PaymentAmount")
                .HasColumnType("FLOAT")
                .HasDefaultValue(0.0);

            entity.Property(e => e.PaymentCurrency)
                .HasColumnName("PaymentCurrency")
                .HasColumnType("DECIMAL(18, 2)");

            entity.Property(e => e.InitiatedDate)
                .HasColumnName("InitiatedDate")
                .HasColumnType("DATETIME");

            entity.Property(e => e.CompletedDate)
                .HasColumnName("CompletedDate")
                .HasColumnType("DATETIME");

            entity.Property(e => e.PaymentResponse)
                .HasColumnName("PaymentResponse")
                .HasMaxLength(1024)
                .HasColumnType("NVARCHAR");

            entity.Property(e => e.PaymentResponseCode)
                .HasColumnName("PaymentResponseCode")
                .HasMaxLength(36)
                .HasColumnType("NVARCHAR");

            entity.Property(e => e.InitiatedBy)
                .HasColumnName("InitiatedBy")
                .HasMaxLength(36)
                .HasColumnType("NVARCHAR");

            entity.Property(e => e.IsRefund)
                .HasColumnName("IsRefund")
                .HasColumnType("BIT");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("DATETIME");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("UpdatedAt")
                .HasColumnType("DATETIME");

            // Relationships
            entity.HasOne(e => e.Customers)
                .WithMany(c => c.PaymentTransactions)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Orders)
                .WithMany(o => o.PaymentTransactions)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        //OnModelCreatingPartial(modelBuilder);
    }

    // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}









/*using Microsoft.EntityFrameworkCore;
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

            //One-to-many relationship with Merchant
            entity.HasMany(e => e.Merchants)
           .WithOne(e => e.Addressess)
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
                .HasColumnType("int")
                .HasDefaultValue(0);

            entity.Property(e => e.TotalTax)
                .HasColumnType("float")
                .HasDefaultValue(0.0f);

            entity.Property(e => e.TotalDiscount)
                .HasColumnType("float")
                .HasDefaultValue(0.0f);

            entity.Property(e => e.TotalAmount)
                .HasColumnType("float")
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
                .WithOne(e => e.Carts)
                .HasForeignKey(e => e.AssociatedCartId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-many relationship with OrderLineItem
            entity.HasMany(e => e.OrderLineItems)
                .WithOne(e => e.Carts)
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

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("char(36)");

            entity.Property(e => e.CustomerId)
                .HasColumnType("char(36)");

            entity.Property(e => e.AddressId)
                .HasColumnType("char(36)");

            entity.Property(e => e.AddressType)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue(AddressTypes.SHIPPING);

            entity.Property(e => e.IsFavorite)
                .HasColumnType("bit");

            entity.HasOne(e => e.Customers)
                .WithMany(c => c.CustomerAddresses)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Addresses)
                .WithMany(a => a.CustomerAddresses)
                .HasForeignKey(e => e.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
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

            
            entity.HasOne(e => e.Addressess)
                .WithMany(c => c.Merchants)
                .HasForeignKey(e => e.AddressId)
                .OnDelete(DeleteBehavior.SetNull);


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

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("char(36)");

            entity.Property(e => e.DisplayCode)
                .HasMaxLength(64)
                .HasColumnType("nvarchar(64)");

            entity.Property(e => e.OrderStatus)
                .IsRequired()
                .HasDefaultValue(OrderStatusTypes.DRAFT)
                .HasColumnType("nvarchar(64)");

            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(64)
                .HasColumnType("nvarchar(64)");

            entity.Property(e => e.AssociatedCartId)
                .HasColumnType("char(36)");

            entity.Property(e => e.TotalItemsCount)
                .HasDefaultValue(0)
                .HasColumnType("int");

            entity.Property(e => e.OrderDiscount)
                .HasDefaultValue(0.0)
                .HasColumnType("float(53)");

            entity.Property(e => e.TipApplicable)
                .HasDefaultValue(false)
                .HasColumnType("bit");

            entity.Property(e => e.TipAmount)
                .HasDefaultValue(0.0)
                .HasColumnType("float(53)");

            entity.Property(e => e.TotalTax)
                .HasDefaultValue(0.0)
                .HasColumnType("float(53)");

            entity.Property(e => e.TotalDiscount)
                .HasDefaultValue(0.0)
                .HasColumnType("float(53)");

            entity.Property(e => e.TotalAmount)
                .HasDefaultValue(0.0)
                .HasColumnType("float(53)");

            entity.Property(e => e.Notes)
                .HasMaxLength(1024)
                .HasColumnType("nvarchar(1024)");

            entity.Property(e => e.CustomerId)
                .HasColumnType("char(36)");

            entity.Property(e => e.ShippingAddressId)
                .HasColumnType("char(36)");

            entity.Property(e => e.BillingAddressId)
                .HasColumnType("char(36)");

            entity.Property(e => e.OrderTypeId)
                .HasColumnType("char(36)");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime");

            // Foreign key relationships
            entity.HasOne(e => e.Carts)
                .WithMany(c => c.Orders)
                .HasForeignKey(e => e.AssociatedCartId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.Customers)
                .WithMany(c => c.Orders)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ShippingAddress)
                .WithMany(c => c.ShippingOrders)
                .HasForeignKey(e => e.ShippingAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.BillingAddress)
                .WithMany(c => c.BillingOrders)
                .HasForeignKey(e => e.BillingAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.OrderTypes)
                .WithMany(c => c.Orders)
                .HasForeignKey(e => e.OrderTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure one-to-one relationship with OrderHistory
            entity.HasOne(e => e.OrderHistorys)
                .WithOne(oh => oh.Orders)
                .HasForeignKey<OrderHistory>(oh => oh.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


        });

        modelBuilder.Entity<OrderCoupon>(entity =>
        {
            entity.ToTable("order_coupons");

            entity.Property(e => e.Id)
               .HasColumnType("char(36)")
               .IsRequired();

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

            entity.Property(e => e.Id)
               .HasColumnType("char(36)")
               //.IsRequired()
               .HasMaxLength(36);

            entity.Property(e => e.OrderId)
                .IsRequired()
                .HasColumnType("char(36)")
                .HasMaxLength(36);

            entity.Property(e => e.PreviousStatus)
                .HasColumnType("nvarchar")
                .HasMaxLength(64)
                .HasDefaultValue(OrderStatusTypes.DRAFT)
                //.IsRequired()
                ;

            entity.Property(e => e.Status)
                .HasColumnType("nvarchar")
                .HasMaxLength(64)
                .HasDefaultValue(OrderStatusTypes.DRAFT)
                //.IsRequired()
                ;

            entity.Property(e => e.UpdatedByUserId)
             .HasColumnType("char(36)")
                .HasMaxLength(36);

            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
               // .IsRequired()
                ;

          

        });

        modelBuilder.Entity<OrderLineItem>(entity =>
        {
            entity.ToTable("order_line_items");

            entity.Property(e => e.Id)
                .HasColumnType("char(36)")
                .IsRequired();

            entity.Property(e => e.Name)
                .HasMaxLength(512)
                .HasColumnType("nvarchar")
                .IsRequired();

            entity.Property(e => e.CatalogId)
              .HasColumnType("char(36)")
              ;

            entity.Property(e => e.Quantity)
                 .HasColumnType("int")
                .HasDefaultValue(0);

            entity.Property(e => e.UnitPrice)
              .HasColumnType("float")
                .HasDefaultValue(0.0);

            entity.Property(e => e.Discount)
                .HasColumnType("float")
                .HasDefaultValue(0.0);

            entity.Property(e => e.DiscountSchemeId)
                .HasColumnType("char(36)")
                .IsRequired();

            entity.Property(e => e.Tax)
               .HasColumnType("float")
                .HasDefaultValue(0.0);

            entity.Property(e => e.ItemSubTotal)
               .HasColumnType("float")
                .HasDefaultValue(0.0);

            entity.Property(e => e.OrderId)
                .HasColumnType("char(36)")
                .IsRequired();

            entity.Property(e => e.CartId)
                .HasColumnType("char(36)")
                .IsRequired();

            entity.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Configure one-to-many relationship with Cart
            entity.HasOne(e => e.Carts)
                .WithMany(c => c.OrderLineItems)
                .HasForeignKey(e => e.CartId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure one-to-many relationship with Order
            entity.HasOne(e => e.Orders)
                .WithMany(o => o.OrderLineItems)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        // OrderType entity configuration
        modelBuilder.Entity<OrderType>(entity =>
        {
            entity.ToTable("order_types");

            entity.Property(e => e.Id)
               .HasColumnType("char(36)")
               .IsRequired();

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
                .WithOne(o => o.OrderTypes)
                .HasForeignKey(o => o.OrderTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });


        modelBuilder.Entity<PaymentTransaction>(entity =>
        {
            entity.ToTable("payment_transactions");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("CHAR(36)");

            entity.Property(e => e.DisplayCode)
                .HasColumnName("DisplayCode")
                .HasMaxLength(64)
                .HasColumnType("NVARCHAR");

            entity.Property(e => e.InvoiceNumber)
                .HasColumnName("InvoiceNumber")
                .HasMaxLength(64)
                .HasColumnType("NVARCHAR");

            entity.Property(e => e.BankTransactionId)
                .HasColumnName("BankTransactionId")
                .HasColumnType("CHAR(36)");

            entity.Property(e => e.PaymentGatewayTransactionId)
                .HasColumnName("PaymentGatewayTransactionId")
                .HasColumnType("CHAR(36)");

            entity.Property(e => e.PaymentStatus)
                .HasColumnName("PaymentStatus")
                .HasMaxLength(64)
                .HasColumnType("NVARCHAR")
                .HasDefaultValue(PaymentStatusTypes.UNKNOWN);

            entity.Property(e => e.PaymentMode)
                .HasColumnName("PaymentMode")
                .HasMaxLength(36)
                .HasColumnType("NVARCHAR");

            entity.Property(e => e.PaymentAmount)
                .HasColumnName("PaymentAmount")
                .HasColumnType("FLOAT")
                .HasDefaultValue(0.0);

            entity.Property(e => e.PaymentCurrency)
                .HasColumnName("PaymentCurrency")
                .HasColumnType("DECIMAL(18, 2)");

            entity.Property(e => e.InitiatedDate)
                .HasColumnName("InitiatedDate")
                .HasColumnType("DATETIME");

            entity.Property(e => e.CompletedDate)
                .HasColumnName("CompletedDate")
                .HasColumnType("DATETIME");

            entity.Property(e => e.PaymentResponse)
                .HasColumnName("PaymentResponse")
                .HasMaxLength(1024)
                .HasColumnType("NVARCHAR");

            entity.Property(e => e.PaymentResponseCode)
                .HasColumnName("PaymentResponseCode")
                .HasMaxLength(36)
                .HasColumnType("NVARCHAR");

            entity.Property(e => e.InitiatedBy)
                .HasColumnName("InitiatedBy")
                .HasMaxLength(36)
                .HasColumnType("NVARCHAR");

            entity.Property(e => e.IsRefund)
                .HasColumnName("IsRefund")
                .HasColumnType("BIT");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("DATETIME");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("UpdatedAt")
                .HasColumnType("DATETIME");

            // Relationships
            entity.HasOne(e => e.Customers)
                .WithMany(c => c.PaymentTransactions)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Orders)
                .WithMany(o => o.PaymentTransactions)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        //OnModelCreatingPartial(modelBuilder);
    }

   // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
*/