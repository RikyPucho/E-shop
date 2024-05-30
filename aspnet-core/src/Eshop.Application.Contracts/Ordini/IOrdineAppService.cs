using Eshop.Prodotti;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Eshop.Ordini
{
    public interface IOrdineAppService : IApplicationService
    {
        Task<PagedResultDto<OrdineDto>> GetListAsync(OrdineGeyListInput input);
        Task<OrdineDto> GetAsync(Guid id);
        Task CreateAsync(CreateOrdineDto input);
        Task UpdateAsync(Guid id,UpdateOrdineDto input);
        Task DeleteAsync(Guid id);
        Task<ListResultDto<ProdottoLookupDto>> GetProdottiLookupAsync();
    }
}
