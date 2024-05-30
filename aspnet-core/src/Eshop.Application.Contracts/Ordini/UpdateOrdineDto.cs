using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Ordini
{
    public class UpdateOrdineDto
    {
        public string Cognome { get; set; }
        public string Telefono { get; set; }
        public string Provincia { get; set; }
        public string Indrizzo { get; set; }
        public string Cap { get; set; }
        public string Citta { get; set; }
        public Stati Stato { get; set; }
    }
}
