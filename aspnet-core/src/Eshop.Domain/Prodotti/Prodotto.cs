using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Eshop.Prodotti;

public class Prodotto : FullAuditedAggregateRoot<Guid>
{
    public string Nome {get; set;}
    public string Des {get; set;}
    public float Prezzo {get; set;}
    public string Immagine1 {get; set;}
    public string Immagine2 {get; set;}
    public string Immagine3 {get; set;}


    private Prodotto() { }

    internal Prodotto(
        Guid id, string nome, string? des, float prezzo, string? immagine1, string? immagine2, string? immagine3) : base(id)
    {
        SetName(nome);
        Nome = nome;
        Des = des;
        Prezzo = prezzo;
        Immagine1 = immagine1;
        Immagine2 = immagine2;
        Immagine3 = immagine3;

    }

    internal Prodotto ChangeName(string name)
    {
        SetName(name);
        return this;
    }

    private void SetName(string name)
    {
        Nome = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: ProdottoConsts.MaxNameLength
            );
    }

}