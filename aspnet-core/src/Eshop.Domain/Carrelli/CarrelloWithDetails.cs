using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;

namespace Eshop.Carrelli
{
    public class CarrelloWithDetails : IHasCreationTime
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int NumDif { get; set; }
        public string[] ProdottiNames { get; set; }
        public int[] ProdottiNum { get; set; }
        public string[] ProdottiNomi {  get; set; }
        public float[] ProdottiPrezzi { get; set; }
        public string[] Immagini1 { get; set; }
        public string[] Immagini2 { get; set; }
        public string[] Immagini3 { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
