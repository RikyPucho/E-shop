using Eshop.Prodotti;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Eshop.Ordini
{
    public class OrdineManager : DomainService
    {
        private readonly IOrdineRepository _ordineRepository;
        private readonly IProdottoRepository _prodottoRepository;

        public OrdineManager(IOrdineRepository ordineRepository, IProdottoRepository prodottoRepository)
        {
            _ordineRepository = ordineRepository;
            _prodottoRepository = prodottoRepository;
        }

        public async Task CreateAsync(string nome, string cognome, string telefono, string provincia, string indirizzo, string cap, string citta, float prezzo,Stati stato, [CanBeNull] string[] prodottiNames, [CanBeNull]int[] prodottiNum)
        {
            var ordine = new Ordine(GuidGenerator.Create(), nome, cognome, telefono, provincia, indirizzo, cap, citta, prezzo, stato);
            
            await SetProdottiAsync(ordine, prodottiNames, prodottiNum);

            await _ordineRepository.InsertAsync(ordine);
        }
        public async Task UpdateAsync(Ordine ordine, string cognome, string telefono, string provincia, string indirizzo, string cap, string citta, Stati stato)
        {
            ordine.Cognome = cognome;
            ordine.Telefono = telefono;
            ordine.Provincia = provincia;
            ordine.Indrizzo = indirizzo;
            ordine.Cap = cap;
            ordine.Citta=citta;
            ordine.Stato = stato;

            await _ordineRepository.UpdateAsync(ordine);
        }
        private async Task SetProdottiAsync(Ordine ordine, [CanBeNull] string[] prodottiNames, [CanBeNull] int[] prodottiNum)
        {
            if (prodottiNames == null || !prodottiNames.Any())
            {
                ordine.RemoveAllProds();
                return;
            }
            if (prodottiNum == null || !prodottiNum.Any())
            {
                ordine.RemoveAllProds();
                return;
            }

            var query = (await _prodottoRepository.GetQueryableAsync())
                .Where(x => prodottiNames.Contains(x.Id.ToString()))
                .Select(x => x.Id)
                .Distinct();

            var prodottiIds = await AsyncExecuter.ToListAsync(query);
            if (!prodottiIds.Any())
            {
                return;
            }

            ordine.RemoveAllProdsExceptGivenIds(prodottiIds);

            for (var i = 0; i < prodottiIds.LongCount(); i++)
            {
                var inde = prodottiNames.FindIndex(x => x.Contains(prodottiIds[i].ToString()));
                if (inde != -1)
                {
                    ordine.AddProdotti(prodottiIds[i], prodottiNum[inde]);
                }
            }
        }
    }
}
