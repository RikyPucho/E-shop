using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Eshop.Ordini
{
    public interface IOrdineRepository : IRepository<Ordine, Guid>
    {
        Task<List<OrdineWithDetails>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount
            );
        Task<OrdineWithDetails> GetAsync(Guid id);

    }
}
