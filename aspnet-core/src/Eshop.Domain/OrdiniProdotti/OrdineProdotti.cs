using Eshop.Prodotti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Eshop.OrdiniProdotti
{
    public class OrdineProdotti : Entity
    {
        public Guid OrdineId { get; set; }
        public Guid ProdottoId { get; set; }
        public int ProdottoNum { get; set; }

        private OrdineProdotti() { }

        public OrdineProdotti(Guid ordineId, Guid prodottoId, int prodottoNum)
        {
            OrdineId = ordineId;
            ProdottoId = prodottoId;
            ProdottoNum = prodottoNum;
        }
        public override object[] GetKeys()
        {
            return new object[] { OrdineId, ProdottoId };
        }
    }
}
