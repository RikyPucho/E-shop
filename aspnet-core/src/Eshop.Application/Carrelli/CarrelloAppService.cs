using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Permissions;
using Eshop.Prodotti;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Eshop.Carrelli;

[Authorize(EshopPermissions.Carrelli.Default)]
public class CarrelloAppService : EshopAppService, ICarrelloAppService
{
    private readonly ICarrelloRepository _carrelloRepository;
    private readonly CarrelloManager _carrelloManager;
    private readonly IProdottoRepository _prodottoRepository;

    public CarrelloAppService(
        ICarrelloRepository carrelloRepository,
        CarrelloManager carrelloManager,
        IProdottoRepository prodottoRepository)
    {
        _carrelloRepository = carrelloRepository;
        _carrelloManager = carrelloManager;
        _prodottoRepository = prodottoRepository;
    }


    public async Task<PagedResultDto<CarrelloDto>> GetListAsync(GetCarrelloListDto input)
    {
        var carrelli = await _carrelloRepository.GetListAsync (input.SkipCount, input.MaxResultCount, input.Sorting);
        var totalCount = await _carrelloRepository.CountAsync();

        return new PagedResultDto<CarrelloDto>(totalCount, ObjectMapper.Map<List<CarrelloWithDetails>, List<CarrelloDto>>(carrelli));
    }
    public async Task<CarrelloDto> GetAsync(Guid id)
    {
        var carrello = await _carrelloRepository.GetAsync(id);
        return ObjectMapper.Map<CarrelloWithDetails, CarrelloDto>(carrello);
    }

    public async Task<CarrelloDto> GetByUserIdAsync(string id)
    {
        var carrello = await _carrelloRepository.GetByUserIdAsync(id);
        return ObjectMapper.Map<CarrelloWithDetails, CarrelloDto>(carrello);
    }
    [Authorize(EshopPermissions.Carrelli.Create)]
    public async Task CreateAsync(CreateCarrelloDto input)
    {
        await _carrelloManager.CreateAsync(
        input.UserId,
        input.NumDif,
        input.ProdottiNames,
        input.ProdottiNum);
    }

    [Authorize(EshopPermissions.Carrelli.Edit)]
    public async Task UpdateAsync(Guid id, UpdateCarrelloDto input)
    {
        var carrello = await _carrelloRepository.GetAsync(id, includeDetails: true);

        await _carrelloManager.UpdateAsync(
            carrello,
            input.UserId,
            input.NumDif,
            input.ProdottiNames,
            input.ProdottiNum);
    }

    [Authorize(EshopPermissions.Carrelli.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _carrelloRepository.DeleteAsync(id);
    }
    public async Task<ListResultDto<ProdottoLookupDto>> GetProdottoLookupAsync()
    {
        var prodotti = await _prodottoRepository.GetListAsync();
        return new ListResultDto<ProdottoLookupDto>(
            ObjectMapper.Map<List<Prodotto>, List<ProdottoLookupDto>>(prodotti)
            );
    }
}