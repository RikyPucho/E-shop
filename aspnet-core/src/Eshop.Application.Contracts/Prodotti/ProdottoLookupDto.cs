using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Eshop.Prodotti
{
    public class ProdottoLookupDto : EntityDto<Guid>
    {
        public string Nome { get; set; }
        public string Des { get; set; }
        public float Prezzo { get; set; }
        public string Immagine1 { get; set; }
        public string Immagine2 { get; set; }
        public string Immagine3 { get; set; }
    }
}
