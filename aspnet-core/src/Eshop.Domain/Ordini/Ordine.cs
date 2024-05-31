using Eshop.OrdiniProdotti;
using Scriban.Functions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Eshop.Ordini
{
    public class Ordine : FullAuditedAggregateRoot<Guid>
    {
        public string Nome {  get; set; }
        public string Cognome { get; set; }
        public string Telefono { get; set; }
        public string Provincia { get; set; }
        public string Indrizzo { get; set; }
        public string Cap {  get; set; }
        public string Citta { get; set; }
        public float Prezzo { get; set; }
        public Stati Stato { get; set; }
        public ICollection<OrdineProdotti> Prodotti { get; set; }

        private Ordine ()
        {
        }

        internal Ordine(Guid id,string nome, string cognome, string telefono, string provincia, string indrizzo, string cap, string citta, float prezzo, Stati stato)
        {
            Id = id;
            Nome = nome;
            Cognome = cognome;
            Telefono = telefono;
            Provincia = provincia;
            Indrizzo = indrizzo;
            Cap = cap;
            Citta = citta;
            Prezzo = prezzo;
            Stato = stato;
            
            Prodotti= new Collection<OrdineProdotti> ();
        }

        public void SetName(string name)
        {
            Nome = Check.NotNullOrWhiteSpace(name, nameof(name), Ordini.OridneConsts.MaxNameLenght);
        }

        public void AddProdotti(Guid id, int num)
        {
            Check.NotNull(id, nameof(id));

            if(IsInProdotti(id))
            {
                return;
            }

            Prodotti.Add(new OrdineProdotti(ordineId: Id, prodottoId: id, prodottoNum: num));
        }
        
        public void RemoveProdotti(Guid id)
        {
            Check.NotNull(id, nameof(id));

            if(!IsInProdotti(id))
            {
                return;
            }

            Prodotti.RemoveAll(x=> x.ProdottoId == id);
        }
        public void RemoveAllProdsExceptGivenIds(List<Guid> ids)
        {
            Check.NotNullOrEmpty(ids, nameof(ids));

            Prodotti.RemoveAll(x => !ids.Contains(x.ProdottoId));
        }
        public void RemoveAllProds()
        {
            Prodotti.RemoveAll(x => x.OrdineId == Id);
        }

        private bool IsInProdotti(Guid id) 
        {
            return Prodotti.Any(x => x.ProdottoId == id); 
        }
    }
}
