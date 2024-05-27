using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Eshop.Prodotti;

public interface IProdottoRepository : IRepository<Prodotto, Guid>
{
    Task<Prodotto> FindByNameAsync(string name);

    Task<List<Prodotto>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
        );
}
