using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Eshop.Relazioni;
using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Linq;

namespace Eshop.Carrelli;

public class Carrello : FullAuditedAggregateRoot<Guid>
{
    public string UserId { get; set; }
    public int NumDif {get; set;}
    public Collection<Relazione> Prodotti { get; set;}


    private Carrello() { }

    internal Carrello(
        Guid id,string userId, int numdif) : base(id)
    {
        UserId = userId;
        NumDif = numdif;

        Prodotti = new Collection<Relazione>();

    }

    public async void AddProd(Guid id, int num)
    {
        Check.NotNull(id, nameof(id));
        if(IsInProd(id))
        {
            var pro = Prodotti.Where(x => x.IdProdotto == id).Where(x => x.IdCarrello == Id);
            var list = pro.ToList();
            if (list.LongCount() == 1 && list[0].NumProdotto == num)
            {
                return;
            }
            else
            {
                RemoveProd(id);
            }
        }
        Prodotti.Add(new Relazione(idcarrello: Id, idprodotto: id, numprodotto: num));
    }
    public void RemoveProd(Guid id)
    {
        Check.NotNull(id, nameof(id));

        if(!IsInProd(id))
        {
            return;
        }
        Prodotti.RemoveAll(x => x.IdProdotto == id);
    }
    public void RemoveAllProdsExceptGivenIds(List<Guid> ids)
    {
        Check.NotNullOrEmpty(ids, nameof(ids));

        Prodotti.RemoveAll(x => !ids.Contains(x.IdProdotto));
    }
    public void RemoveAllProds()
    {
        Prodotti.RemoveAll(x => x.IdCarrello == Id);
    }


    private bool IsInProd(Guid id)
    {
        return Prodotti.Any(x => x.IdProdotto == id);
    }

}