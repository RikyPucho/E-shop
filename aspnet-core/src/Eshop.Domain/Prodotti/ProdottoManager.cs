using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Eshop.Prodotti;

public class ProdottoManager : DomainService
{
    private readonly IProdottoRepository _prodottoRepository;

    public ProdottoManager (IProdottoRepository prodottoRepository)
    {
        _prodottoRepository = prodottoRepository;
    }

    public async Task<Prodotto> CreateAsync(
        string nome,
        string des,
        float prezzo,
        string immagine1,
        string immagine2,
        string immagine3)
    {
        Check.NotNullOrWhiteSpace(nome, nameof(nome));

        var existingProdotto = await _prodottoRepository.FindByNameAsync(nome);
        if (existingProdotto != null) 
        {
            throw new ProdottoAlreadyExistsException(nome);
        }

        return new Prodotto(
            GuidGenerator.Create(),
            nome,
            des,
            prezzo,
            immagine1,
            immagine2,
            immagine3);
    }
    public async Task ChangeNameAsync(
        Prodotto prodotto,
        string newName)
    {
        Check.NotNull(prodotto, nameof(prodotto));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingProdotto = await _prodottoRepository.FindByNameAsync(newName);
        if (existingProdotto != null && existingProdotto.Id != prodotto.Id) 
        {
            throw new ProdottoAlreadyExistsException(newName);
        }

        prodotto.ChangeName(newName);
    }
}