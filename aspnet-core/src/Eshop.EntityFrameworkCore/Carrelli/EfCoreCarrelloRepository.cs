using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Eshop.EntityFrameworkCore;
using Eshop.Prodotti;
using Eshop.Relazioni;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Eshop.Carrelli;

public class EfCoreCarrelloRepository
    : EfCoreRepository<EshopDbContext, Carrello, Guid>,
        ICarrelloRepository
{
    public EfCoreCarrelloRepository(
        IDbContextProvider<EshopDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<List<CarrelloWithDetails>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        var query = await ApplyDataFilterAsync();

        return await query
            .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : "NumDif ASC")
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }
    public async Task<CarrelloWithDetails> GetAsync(Guid id)
    {
        var query = await ApplyDataFilterAsync();

        return await query
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }
    public async Task<CarrelloWithDetails> GetByUserIdAsync(string id)
    {
        var query = await ApplyDataFilterAsync();

        return await query
            .Where(x => x.UserId == id)
            .FirstOrDefaultAsync();
    }
    private async Task<IQueryable<CarrelloWithDetails>> ApplyDataFilterAsync()
    {
        var dbContext = await GetDbContextAsync();

        return (await GetDbSetAsync())
            .Include(x => x.Prodotti)
            .Select(x => new CarrelloWithDetails
            {
                Id = x.Id,
                UserId = x.UserId,
                NumDif = x.NumDif,
                ProdottiNames = (from relazioni in x.Prodotti
                                 join prodotto in dbContext.Set<Prodotto>() on relazioni.IdProdotto equals prodotto.Id
                                 select prodotto.Id.ToString()).ToArray(),
                ProdottiNum = (from relazioni in x.Prodotti 
                                  select relazioni.NumProdotto).ToArray(),
                ProdottiNomi = (from relazioni in x.Prodotti
                                join prodotto in dbContext.Set<Prodotto>() on relazioni.IdProdotto equals prodotto.Id
                                select prodotto.Nome).ToArray(),
                ProdottiPrezzi = (from relazioni in x.Prodotti
                                  join prodotto in dbContext.Set<Prodotto>() on relazioni.IdProdotto equals prodotto.Id
                                  select prodotto.Prezzo).ToArray(),
                Immagini1 = (from relazioni in x.Prodotti
                             join prodotto in dbContext.Set<Prodotto>() on relazioni.IdProdotto equals prodotto.Id
                             select prodotto.Immagine1).ToArray(),
                Immagini2 = (from relazioni in x.Prodotti
                             join prodotto in dbContext.Set<Prodotto>() on relazioni.IdProdotto equals prodotto.Id
                             select prodotto.Immagine2).ToArray(),
                Immagini3 = (from relazioni in x.Prodotti
                             join prodotto in dbContext.Set<Prodotto>() on relazioni.IdProdotto equals prodotto.Id
                             select prodotto.Immagine3).ToArray(),
            }) ;
    }
    public override Task<IQueryable<Carrello>> WithDetailsAsync()
    {
        return base.WithDetailsAsync(x => x.Prodotti);
    }
}