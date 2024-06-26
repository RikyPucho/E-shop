using Eshop.Prodotti;
using Microsoft.EntityFrameworkCore;
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
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Eshop.Carrelli;
using Eshop.Relazioni;
using Eshop.Ordini;
using Eshop.OrdiniProdotti;

namespace Eshop.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class EshopDbContext :
    AbpDbContext<EshopDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Prodotto> Prodotti { get; set; }
    public DbSet<Carrello> Carrelli { get; set; }
    public DbSet<Ordine> Ordini { get; set; }
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

    public EshopDbContext(DbContextOptions<EshopDbContext> options)
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
        builder.ConfigureBlobStoring();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(EshopConsts.DbTablePrefix + "YourEntities", EshopConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        builder.Entity<Prodotto>(b =>
        {
            b.ToTable(EshopConsts.DbTablePrefix + "Prodotti",
                EshopConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(ProdottoConsts.MaxNameLength);
            b.HasIndex(x => x.Nome);
        });
        builder.Entity<Carrello>(b =>
        {
            b.ToTable(EshopConsts.DbTablePrefix + "Carrelli",
                EshopConsts.DbSchema);
            b.ConfigureByConvention();

            b.HasMany(x => x.Prodotti).WithOne().HasForeignKey(x => x.IdCarrello).IsRequired();
        });
        builder.Entity<Relazione>(b =>
        {
            b.ToTable(EshopConsts.DbTablePrefix + "Relazioni",
                EshopConsts.DbSchema);
            b.ConfigureByConvention();

            b.HasKey(x => new { x.IdCarrello, x.IdProdotto });

            b.HasOne<Carrello>().WithMany(x => x.Prodotti).HasForeignKey(x => x.IdCarrello).IsRequired();
            b.HasOne<Prodotto>().WithMany().HasForeignKey(x => x.IdProdotto).IsRequired();

            b.HasIndex(x => new { x.IdCarrello, x.IdProdotto });
        });

        builder.Entity<Ordine>(b =>
        {
            b.ToTable(EshopConsts.DbTablePrefix + "Ordini",
                EshopConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Nome)
            .HasMaxLength(30).IsRequired();
            b.HasMany(x => x.Prodotti).WithOne().HasForeignKey(x => x.OrdineId).IsRequired();
        });
        builder.Entity<OrdineProdotti>(b =>
        {
            b.ToTable(EshopConsts.DbTablePrefix + "OrdineProdotti",
               EshopConsts.DbSchema);
            b.ConfigureByConvention();

            b.HasKey(x => new { x.OrdineId, x.ProdottoId });

            b.HasOne<Ordine>().WithMany(x => x.Prodotti).HasForeignKey(x => x.OrdineId).IsRequired();
            b.HasOne<Prodotto>().WithMany().HasForeignKey(x => x.ProdottoId).IsRequired();

            b.HasIndex(x => new { x.OrdineId, x.ProdottoId });
        });
    }
}
