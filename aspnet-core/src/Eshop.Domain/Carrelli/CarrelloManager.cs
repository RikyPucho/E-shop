using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Prodotti;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Eshop.Carrelli;

public class CarrelloManager : DomainService
{
    private readonly ICarrelloRepository _carrelloRepository;
    private readonly IProdottoRepository _prodottoRepository;

    public CarrelloManager (ICarrelloRepository carrelloRepository, IProdottoRepository prodottoRepository)
    {
        _prodottoRepository = prodottoRepository;
        _carrelloRepository = carrelloRepository;
    }

    public async Task CreateAsync(
        string userId,
        int numdif,
        [CanBeNull] string[] prodottiNames,
        [CanBeNull] int[] ProdottiNum)
    {


        var car =new Carrello(
            GuidGenerator.Create(),
            userId,
            numdif);
        await SetProdottiAsync(car, prodottiNames, ProdottiNum);

        await _carrelloRepository.InsertAsync(car);
    }
    public async Task UpdateAsync(
        Carrello car,
        string userId,
        int numdif,
        [CanBeNull] string[] prodottiNames,
        [CanBeNull] int[] ProdottiNum
        )
    {
        car.UserId = userId;
        car.NumDif = numdif;
        
        await SetProdottiAsync(car, prodottiNames, ProdottiNum);

        await _carrelloRepository.UpdateAsync(car);
    }
    private async Task SetProdottiAsync(Carrello car, [CanBeNull] string[] prodottiNames, [CanBeNull] int[] ProdottiNum)
    {
        if(prodottiNames == null || !prodottiNames.Any())
        {
            car.RemoveAllProds();
            return;
        }if(ProdottiNum == null || !ProdottiNum.Any())
        {
            car.RemoveAllProds();
            return;
        }

        var query = (await _prodottoRepository.GetQueryableAsync())
            .Where(x => prodottiNames.Contains(x.Id.ToString()))
            .Select(x => x.Id)
            .Distinct();

        var prodottiIds = await AsyncExecuter.ToListAsync(query);
        if(!prodottiIds.Any())
        {
            return;
        }

        car.RemoveAllProdsExceptGivenIds(prodottiIds);

        for (var i =0; i<prodottiIds.LongCount(); i++)
        {
            var inde = prodottiNames.FindIndex(x => x.Contains(prodottiIds[i].ToString()));
            if(inde != -1)
            {
                car.AddProd(prodottiIds[i], ProdottiNum[inde]);
            }
        }
    }
}