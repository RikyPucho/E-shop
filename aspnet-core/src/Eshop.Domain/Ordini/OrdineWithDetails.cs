using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;

namespace Eshop.Ordini
{
    public class OrdineWithDetails : IHasCreationTime
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Telefono { get; set; }
        public string Provincia { get; set; }
        public string Indrizzo { get; set; }
        public string Cap { get; set; }
        public string Citta { get; set; }
        public float Prezzo { get; set; }
        public Stati Stato { get; set; }
        public string[] ProdottiNames { get; set; }
        public int[] ProdottiNum { get; set; }
        public string[] ProdottiNomi { get; set; }
        public float[] ProdottiPrezzi { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
