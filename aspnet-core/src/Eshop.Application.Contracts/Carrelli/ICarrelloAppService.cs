using Eshop.Prodotti;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Eshop.Carrelli;

public interface ICarrelloAppService : IApplicationService
{
    Task<PagedResultDto<CarrelloDto>> GetListAsync(GetCarrelloListDto input);
    Task<CarrelloDto> GetAsync(Guid id);
    Task<CarrelloDto> GetByUserIdAsync(string id);

    Task CreateAsync(CreateCarrelloDto input);

    Task UpdateAsync(Guid id, UpdateCarrelloDto input);
    Task DeleteAsync(Guid id);
    Task<ListResultDto<ProdottoLookupDto>> GetProdottoLookupAsync();

}