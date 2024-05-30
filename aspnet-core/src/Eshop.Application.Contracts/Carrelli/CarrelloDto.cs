using System;
using Volo.Abp.Application.Dtos;

namespace Eshop.Carrelli;

public class CarrelloDto : EntityDto<Guid>
{
    public string UserId { get; set; }
    public int NumDif {get; set;}
    public string[] ProdottiNames { get; set;}
    public int[] ProdottiNum { get; set;}
    public string[] ProdottiNomi { get; set; }
    public float[] ProdottiPrezzi { get; set; }
    public string[] Immagini1 { get; set; }
    public string[] Immagini2 { get; set; }
    public string[] Immagini3 { get; set; }

}