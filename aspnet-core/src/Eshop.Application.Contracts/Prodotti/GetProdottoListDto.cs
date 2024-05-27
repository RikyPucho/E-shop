using Volo.Abp.Application.Dtos;

namespace Eshop.Prodotti;

public class GetProdottoListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}