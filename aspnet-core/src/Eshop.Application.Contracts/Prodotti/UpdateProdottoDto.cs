using System;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Prodotti;

public class UpdateProdottoDto
{
    [Required]
    [StringLength(ProdottoConsts.MaxNameLength)]
    public string Nome {get; set;} = string.Empty;
    public string? Des {get; set;}
    [Required]
    public float Prezzo {get; set;}
    public string? Immagine1 {get; set;}
    public string? Immagine2 {get; set;}
    public string? Immagine3 {get; set;}

}