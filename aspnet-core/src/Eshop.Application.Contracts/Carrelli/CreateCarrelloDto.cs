using System;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Carrelli;

public class CreateCarrelloDto
{
    public string UserId { get; set; }
    [Required]
    public int NumDif {get; set;}
    public string[] ProdottiNames { get; set;}
    public int[] ProdottiNum { get; set;}

}