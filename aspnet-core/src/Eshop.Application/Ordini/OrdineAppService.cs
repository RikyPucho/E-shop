using Eshop.Prodotti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Eshop.Ordini
{
    public class OrdineAppService : EshopAppService, IOrdineAppService
    {
        private readonly IOrdineRepository _ordineRepository;
        private readonly OrdineManager _ordineManager;
        private readonly IProdottoRepository _prodottoRepository;

        public OrdineAppService(IOrdineRepository ordineRepository, OrdineManager ordineManager, IProdottoRepository prodottoRepository)
        {
            _ordineRepository = ordineRepository;
            _ordineManager = ordineManager;
            _prodottoRepository = prodottoRepository;
        }
        public async Task<PagedResultDto<OrdineDto>> GetListAsync(OrdineGeyListInput input)
        {
            var ordini = await _ordineRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount);
            var totalCount = await _ordineRepository.CountAsync();

            return new PagedResultDto<OrdineDto>(totalCount, ObjectMapper.Map<List<OrdineWithDetails>, List<OrdineDto>>(ordini));
        }
        public async Task<OrdineDto> GetAsync(Guid id)
        {
            var ordine = await _ordineRepository.GetAsync(id);

            return ObjectMapper.Map<OrdineWithDetails, OrdineDto>(ordine); 
        }
        public async Task CreateAsync(CreateOrdineDto input)
        {
            await _ordineManager.CreateAsync(
                input.Nome,
                input.Cognome,
                input.Telefono,
                input.Provincia,
                input.Indrizzo,
                input.Cap,
                input.Citta,
                input.Prezzo,
                input.Stato,
                input.ProdottiNames,
                input.ProdottiNum
                );
        }
        public async Task UpdateAsync(Guid id, UpdateOrdineDto input)
        {
            var ordine = await _ordineRepository.GetAsync(id, includeDetails: true);

            await _ordineManager.UpdateAsync(
                ordine,
                input.Cognome,
                input.Telefono,
                input.Provincia,
                input.Indrizzo,
                input.Cap,
                input.Citta,
                input.Stato
                );
        }
        public async Task DeleteAsync(Guid id)
        {
            await _ordineRepository.DeleteAsync(id);
        }
        public async Task<ListResultDto<ProdottoLookupDto>> GetProdottiLookupAsync()
        {
            var prodotti = await _prodottoRepository.GetListAsync();

            return new ListResultDto<ProdottoLookupDto>(
                ObjectMapper.Map<List<Prodotto>, List<ProdottoLookupDto>>(prodotti) );
        }
    }
}
