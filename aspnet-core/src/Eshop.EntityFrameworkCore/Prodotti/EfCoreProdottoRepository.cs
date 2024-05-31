using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Eshop.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Eshop.Prodotti;

public class EfCoreProdottoRepository
    : EfCoreRepository<EshopDbContext, Prodotto, Guid>,
        IProdottoRepository
{
    public EfCoreProdottoRepository(
        IDbContextProvider<EshopDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Prodotto> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(prodotto => prodotto.Nome == name);
    }

    public async Task<List<Prodotto>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet 
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}