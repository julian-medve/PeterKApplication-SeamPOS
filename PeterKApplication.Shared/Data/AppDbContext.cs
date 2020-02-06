using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PeterKApplication.Shared.Enums;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Add soft delete for AppUsers

            builder
                .Entity<AppUser>()
                .Property<bool>("IsDeleted");

            builder
                .Entity<AppUser>()
                .HasQueryFilter(u => EF.Property<bool>(u, "IsDeleted") == false);


            // Unique constraints

            /*builder
                .Entity<AppUser>()
                .HasIndex(u => new { u.BusinessId, u.Pin })
                .IsUnique();*/

            builder
                .Entity<BusinessCategory>()
                .HasIndex(c => c.Name)
                .IsUnique();

            builder
                .Entity<BusinessLocation>()
                .HasIndex(l => l.Name)
                .IsUnique();

            builder
                .Entity<BusinessSetup>()
                .HasIndex(s => s.Name)
                .IsUnique();

            builder
                .Entity<KnowledgeBase>()
                .HasIndex(kb => kb.Title)
                .IsUnique();

            builder
                .Entity<PaymentType>()
                .HasIndex(t => t.Name)
                .IsUnique();

            builder
                .Entity<RetailSubcategory>()
                .HasIndex(rs => rs.Name)
                .IsUnique();

            // Many to many relationships

            builder
                .Entity<BusinessBusinessCategory>(entity =>
                {
                    entity.HasKey(bc => new {bc.BusinessId, bc.BusinessCategoryId});

                    entity
                        .HasOne(bc => bc.Business)
                        .WithMany(b => b.BusinessBusinessCategories)
                        .HasForeignKey(bc => bc.BusinessId);

                    entity
                        .HasOne(bc => bc.BusinessCategory)
                        .WithMany(c => c.BusinessBusinessCategories)
                        .HasForeignKey(bc => bc.BusinessCategoryId);
                });

            builder
                .Entity<BusinessPaymentType>(entity =>
                {
                    entity.HasKey(bp => new {bp.BusinessId, bp.PaymentTypeId});

                    entity
                        .HasOne(bp => bp.Business)
                        .WithMany(b => b.BusinessPaymentTypes)
                        .HasForeignKey(bp => bp.BusinessId);

                    entity
                        .HasOne(bp => bp.PaymentType)
                        .WithMany(p => p.BusinessesPaymentTypes)
                        .HasForeignKey(bp => bp.PaymentTypeId);
                });

            // Enum conversions

            builder
                .Entity<Coupon>()
                .Property(c => c.CouponType)
                .HasConversion(new EnumToStringConverter<CouponType>());

            builder
                .Entity<Order>()
                .Property(o => o.OrderStatus)
                .HasConversion(new EnumToStringConverter<OrderStatus>());


            // Data seeding

            builder
                .Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Id = Guid.Parse("beca8248-e776-4702-8548-38b4c1a0c260").ToString(),
                        Name = UserRole.Owner,
                        NormalizedName = UserRole.Owner.ToUpper()
                    },
                    new IdentityRole
                    {
                        Id = Guid.Parse("065c2456-ac0d-4bac-b9d9-858ff287186d").ToString(),
                        Name = UserRole.Administrator,
                        NormalizedName = UserRole.Administrator.ToUpper()
                    },
                    new IdentityRole
                    {
                        Id = Guid.Parse("27196e84-d952-4c22-b8f9-0e447ab0cdb2").ToString(),
                        Name = UserRole.Agent.ToUpper(),
                        NormalizedName = UserRole.Agent.ToUpper()
                    });

            builder
                .Entity<BusinessCategory>()
                .HasData(
                    new BusinessCategory {Id = Guid.Parse("74dc9a1b-9a60-4232-8244-2797a884dfb5"), Name = "Automotive"},
                    new BusinessCategory
                        {Id = Guid.Parse("16385f89-a49b-401d-a261-da315d19f31b"), Name = "Bars & Restaurants"},
                    new BusinessCategory
                        {Id = Guid.Parse("68987eb6-ff39-4992-a11d-f907420b0767"), Name = "Business Services"},
                    new BusinessCategory
                        {Id = Guid.Parse("a93c015f-8be6-43a0-a0b5-5a39900263ea"), Name = "Construction (Hardware)"},
                    new BusinessCategory {Id = Guid.Parse("4391e7f7-1b1e-47ff-8ae3-bb43d47744ea"), Name = "Retail"},
                    new BusinessCategory
                        {Id = Guid.Parse("fbd5109a-cea1-4834-8d13-4536e0496c6c"), Name = "Supermarket"},
                    new BusinessCategory
                        {Id = Guid.Parse("cb8ecfab-767d-4ad4-87b1-a0ca1a8d15dc"), Name = "General Store"});

            builder
                .Entity<BusinessLocation>()
                .HasData(
                    new BusinessLocation {Id = Guid.Parse("00d7748d-fedb-4b62-88ee-79eb568031cf"), Name = "Physical"},
                    new BusinessLocation {Id = Guid.Parse("e14d0527-7baf-4c70-9dc1-1f3ddc264287"), Name = "Online"});

            builder
                .Entity<BusinessSetup>()
                .HasData(
                    new BusinessSetup
                        {Id = Guid.Parse("d427dd85-38bb-474c-bf19-07d76a851f60"), Name = "Sole Proprietor"},
                    new BusinessSetup {Id = Guid.Parse("5f4a338f-c73b-45c8-857d-a717dfcd096a"), Name = "Partnership"},
                    new BusinessSetup
                        {Id = Guid.Parse("a8f8e301-a325-4029-b853-6c8791e562f6"), Name = "Limited Company"});

            builder
                .Entity<KnowledgeBase>()
                .HasData(
                    new KnowledgeBase
                    {
                        Id = Guid.Parse("545bbf8e-a850-4b1e-8041-4ffc72a4bba9"), Title = "Test",
                        Description = "Description text"
                    });

            builder
                .Entity<PaymentType>()
                .HasData(
                    new PaymentType { Id = Guid.Parse("25fa086b-5288-443b-b157-a0f6e748cbf0"), Name = "Cash" },
                    new PaymentType { Id = Guid.Parse("3d8702a0-a604-4139-8bd6-66fe76080064"), Name = "mPesa" },
                    new PaymentType { Id = Guid.Parse("70376e3b-012b-45f7-894c-bfb77c8f0414"), Name = "Cheque" },
                    new PaymentType { Id = Guid.Parse("cab752fd-1ec7-4aa4-9493-3a7527032084"), Name = "Bank Transfer" });

            /*        new PaymentType {Id = Guid.Parse("d2ec5a91-a99e-46fc-942a-4a36b3173456"), Name = "Bank Transfer"});*/


            builder
                .Entity<RetailSubcategory>()
                .HasData(
                    new RetailSubcategory
                        {Id = Guid.Parse("5246cf01-468e-4861-ab37-13dc1232cc1f"), Name = "Car Accessories"},
                    new RetailSubcategory {Id = Guid.Parse("a154d280-3627-46d4-a8d0-c3a698b8e578"), Name = "Garage"},
                    new RetailSubcategory {Id = Guid.Parse("d50d730d-be27-4055-82e7-83a469b4e3fa"), Name = "Printing"},
                    new RetailSubcategory
                        {Id = Guid.Parse("39a09a44-2be2-4a28-863e-5b6d237ffa28"), Name = "Computer Services"},
                    new RetailSubcategory
                        {Id = Guid.Parse("f38992c0-cff5-4267-a4c5-7aefaa284e31"), Name = "Apparels Clothes"},
                    new RetailSubcategory
                        {Id = Guid.Parse("1805bd8b-48b7-4e03-8677-c05732194d1d"), Name = "Computer Accessories"},
                    new RetailSubcategory
                        {Id = Guid.Parse("1ac64278-6cdb-4303-b8e8-c23f865f7d70"), Name = "Electronics"},
                    new RetailSubcategory
                        {Id = Guid.Parse("0655627c-cff5-42a1-b3a3-0d99c7aa288a"), Name = "Education And Books"},
                    new RetailSubcategory
                    {
                        Id = Guid.Parse("a4fd1d1e-d92f-4e2b-b1d4-5dc44ebe0815"), Name = "Mobile Phones And Accessories"
                    },
                    new RetailSubcategory
                        {Id = Guid.Parse("8298309b-3d3b-4c03-8bf3-1f4d4b14d870"), Name = "Home And Furniture"}
                );
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            SoftDeleteUsers();
            ApplyAudit();
        }

        private void ApplyAudit()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => (e.Entity is IBaseEntity || e.Entity is BaseEntity) &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            var v = new HttpContextAccessor();
            var userName = v.HttpContext?.User?.Identity?.Name;

            foreach (var entityEntry in entries)
            {
                var eentry = (IBaseEntity) entityEntry.Entity;
                if (!string.IsNullOrEmpty(userName)) eentry.ModifiedBy = userName;

                switch (entityEntry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        eentry.ModifiedOn = DateTime.Now;
                        break;
                    case EntityState.Added:
                        eentry.ModifiedOn = DateTime.Now;
                        eentry.CreatedOn = DateTime.Now;
                        break;
                }
            }
        }

        private void SoftDeleteUsers()
        {
            foreach (var entry in ChangeTracker.Entries<AppUser>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }
        }

        public DbSet<Business> Businesses { get; set; }
        public DbSet<BusinessBusinessCategory> BusinessBusinessCategories { get; set; }
        public DbSet<BusinessCategory> BusinessCategories { get; set; }
        public DbSet<BusinessLocation> BusinessLocations { get; set; }
        public DbSet<BusinessPaymentType> BusinessPaymentTypes { get; set; }
        public DbSet<BusinessSetup> BusinessSetups { get; set; }
        public DbSet<BusinessRetailSubcategory> BusinessRetailSubcategories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<ImageModel> ImageModels { get; set; }
        public DbSet<KnowledgeBase> KnowledgeBases { get; set; }
        public DbSet<MPesaTransaction> MPesaTransactions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentPlan> PaymentPlans { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<RetailSubcategory> RetailSubcategories { get; set; }
        public DbSet<Sync> Syncs { get; set; }
    }
}