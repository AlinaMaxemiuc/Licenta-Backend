using Microsoft.EntityFrameworkCore;

using Renting.Customers;
using Renting.Drones;
using Renting.Rentals;
using Renting.Reviews;

using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Renting.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class RentingDbContext :
    AbpDbContext<RentingDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Drone> Drones { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<RentalItem> RentalItems { get; set; }
    public RentingDbContext(DbContextOptions<RentingDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(RentingConsts.DbTablePrefix + "YourEntities", RentingConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        builder.Entity<Customer>(b =>
        {
            b.ToTable(RentingConsts.DbTablePrefix + "Customers", RentingConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Drone>(b => {
            b.ToTable(RentingConsts.DbTablePrefix + "Drones", RentingConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.PricePerDay).HasPrecision(precision: 18, 4);
        });

        builder.Entity<Review>(b =>
        {
            b.ToTable(RentingConsts.DbTablePrefix + "Reviews", RentingConsts.DbSchema);
            b.ConfigureByConvention();

            //b.HasIndex(r => r.DroneId);
            // b.HasIndex(r => r.CustomerId);

            b.HasOne<Drone>()
            .WithMany()
            .HasForeignKey(r => r.DroneId).OnDelete(DeleteBehavior.Cascade);

            b.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(r => r.CustomerId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Rental>(b =>
        {
            b.ToTable(RentingConsts.DbTablePrefix + "Rentals", RentingConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Total).HasPrecision(precision: 18, 4);


            b.HasMany(r => r.RentalItems)
                .WithOne()
                .HasForeignKey(ri => ri.RentalId)
                .IsRequired();
        });

        builder.Entity<RentalItem>(b =>
        {
            b.ToTable(RentingConsts.DbTablePrefix + "RentalItems", RentingConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.PricePerDay).HasPrecision(precision: 18, 4);
            b.Property(x => x.Price).HasPrecision(precision: 18, 4);

            b.HasOne<Rental>()
                .WithMany(r => r.RentalItems)
                .HasForeignKey(ri => ri.RentalId)
                .IsRequired();
        });
    }
}
