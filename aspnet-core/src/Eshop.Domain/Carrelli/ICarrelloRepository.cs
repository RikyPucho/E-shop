using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Eshop.Carrelli;

public interface ICarrelloRepository : IRepository<Carrello, Guid>
{

    Task<List<CarrelloWithDetails>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
        );
    Task<CarrelloWithDetails> GetAsync(Guid id);
    Task<CarrelloWithDetails> GetByUserIdAsync(string id);
}
