using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Eshop.Relazioni;

public class Relazione : Entity
{
    public Guid IdCarrello {get; set;}
    public Guid IdProdotto {get; set;}
    public int NumProdotto {get; set;}


    private Relazione() { }

    public Relazione(
       Guid idcarrello, Guid idprodotto, int numprodotto)
    {
        IdCarrello = idcarrello;
        IdProdotto = idprodotto;
        NumProdotto = numprodotto;
    }

    public override object[] GetKeys()
    {
        return new object[] { IdCarrello, IdProdotto, NumProdotto };
    }
}