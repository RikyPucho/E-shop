using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Eshop.Ordini
{
    public class OrdineGeyListInput : PagedAndSortedResultRequestDto
    {
        public Stati? Stato { get; set; }
        public string? Nome { get; set; }
        public float? Prezzo { get; set; }
        public bool? Maggiore { get; set; }
        public string? Citta { get; set; }
    }
}
