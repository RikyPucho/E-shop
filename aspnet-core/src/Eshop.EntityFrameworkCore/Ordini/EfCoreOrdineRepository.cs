using Eshop.Carrelli;
using Eshop.EntityFrameworkCore;
using Eshop.Prodotti;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Eshop.Ordini
{
    public class EfCoreOrdineRepository : EfCoreRepository<EshopDbContext, Ordine, Guid>, IOrdineRepository
    {
        public EfCoreOrdineRepository(IDbContextProvider<EshopDbContext> dbContextProvider) : base(dbContextProvider) { }

        public async Task<List<OrdineWithDetails>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount)
        {
            var query = await ApplyFilterAsync();

            return await query
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : "Nome ASC")
                .PageBy(skipCount, maxResultCount)
                .ToListAsync();
        }

        public async Task<OrdineWithDetails> GetAsync(Guid id)
        {
            var query = await ApplyFilterAsync();

            return await query
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
        private async Task<IQueryable<OrdineWithDetails>> ApplyFilterAsync()
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync())
                .Include(x => x.Prodotti)
                .Select(x => new OrdineWithDetails
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Cognome = x.Cognome,
                    Telefono = x.Telefono,
                    Provincia = x.Provincia,
                    Indrizzo = x.Indrizzo,
                    Cap = x.Cap,
                    Citta = x.Citta,
                    Prezzo = x.Prezzo,
                    Stato = x.Stato,
                    ProdottiNames = (from relazioni in x.Prodotti
                                     join prodotto in dbContext.Set<Prodotto>() on relazioni.ProdottoId equals prodotto.Id
                                     select prodotto.Id.ToString()).ToArray(),
                    ProdottiNum = (from relazioni in x.Prodotti
                                   select relazioni.ProdottoNum).ToArray(),
                    ProdottiNomi = (from relazioni in x.Prodotti
                                    join prodotto in dbContext.Set<Prodotto>() on relazioni.ProdottoId equals prodotto.Id
                                    select prodotto.Nome).ToArray(),
                    ProdottiPrezzi = (from relazioni in x.Prodotti
                                      join prodotto in dbContext.Set<Prodotto>() on relazioni.ProdottoId equals prodotto.Id
                                      select prodotto.Prezzo).ToArray(),
                });
        }
        public override Task<IQueryable<Ordine>> WithDetailsAsync()
        {
            return base.WithDetailsAsync(x => x.Prodotti);
        }
    }
}
