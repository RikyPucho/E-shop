using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Eshop.Prodotti;

public interface IProdottoAppService : IApplicationService
{
    Task<ProdottoDto> GetAsync(Guid id);

    Task<PagedResultDto<ProdottoDto>> GetListAsync(GetProdottoListDto input);

    Task<ProdottoDto> CreateAsync(CreateProdottoDto input);

    Task UpdateAsync(Guid id, UpdateProdottoDto input);

    Task DeleteAsync(Guid id);
}