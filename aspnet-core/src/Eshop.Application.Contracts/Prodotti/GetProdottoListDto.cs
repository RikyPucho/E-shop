using Volo.Abp.Application.Dtos;

namespace Eshop.Prodotti;

public class GetProdottoListDto : PagedAndSortedResultRequestDto
{
    public string? Nome {  get; set; }
    public float? Prezzo { get; set; }
    public bool? Maggiore { get; set; }
}