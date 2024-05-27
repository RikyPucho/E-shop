using Eshop.Prodotti;

public DbSet<Prodotto> Prodotti { get; set; }


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