using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Eshop.Prodotti;

public class ProdottoAppService : EshopAppService, IProdottoAppService
{
    private readonly IProdottoRepository _prodottoRepository;
    private readonly ProdottoManager _prodottoManager;

    public ProdottoAppService(
        IProdottoRepository prodottoRepository,
        ProdottoManager prodottoManager)
    {
        _prodottoRepository = prodottoRepository;
        _prodottoManager = prodottoManager;
    }

    public async Task<ProdottoDto> GetAsync(Guid id)
    {
        var prodotto = await _prodottoRepository.GetAsync(id);
        return ObjectMapper.Map<Prodotto, ProdottoDto>(prodotto);
    }

    public async Task<PagedResultDto<ProdottoDto>> GetListAsync(GetProdottoListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Prodotto.Nome);
        }

        var prodotti = await _prodottoRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting
        );
        
        prodotti = prodotti
                        .WhereIf(!input.Nome.IsNullOrWhiteSpace(), X=> X.Nome.Contains(input.Nome))
                        .WhereIf(input.Prezzo.HasValue && input.Maggiore == true, x=> x.Prezzo >= input.Prezzo)
                        .WhereIf(input.Prezzo.HasValue && input.Maggiore == false, x=> x.Prezzo <= input.Prezzo)
                        .ToList();

        var totalCount = prodotti.Count();

        return new PagedResultDto<ProdottoDto>(
            totalCount,
            ObjectMapper.Map<List<Prodotto>, List<ProdottoDto>>(prodotti)
        );
    }
    [Authorize(EshopPermissions.Prodotti.Create)]
    public async Task<ProdottoDto> CreateAsync(CreateProdottoDto input)
    {
        var prodotto = await _prodottoManager.CreateAsync(
        input.Nome,
        input.Des,
        input.Prezzo,
        input.Immagine1,
        input.Immagine2,
        input.Immagine3        );

        await _prodottoRepository.InsertAsync(prodotto);

        return ObjectMapper.Map<Prodotto, ProdottoDto>(prodotto);
    }

    [Authorize(EshopPermissions.Prodotti.Edit)]
    public async Task UpdateAsync(Guid id, UpdateProdottoDto input)
    {
        var prodotto = await _prodottoRepository.GetAsync(id);

        if (prodotto.Nome != input.Nome)
        {
            await _prodottoManager.ChangeNameAsync(prodotto, input.Nome);
        }

        prodotto.Des = input.Des;
        prodotto.Prezzo = input.Prezzo;
        prodotto.Immagine1 = input.Immagine1;
        prodotto.Immagine2 = input.Immagine2;
        prodotto.Immagine3 = input.Immagine3;


        await _prodottoRepository.UpdateAsync(prodotto);
    }

    [Authorize(EshopPermissions.Prodotti.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _prodottoRepository.DeleteAsync(id);
    }

}