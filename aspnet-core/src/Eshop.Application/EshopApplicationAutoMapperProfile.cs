using AutoMapper;
using Eshop.Prodotti;

namespace Eshop;

public class EshopApplicationAutoMapperProfile : Profile
{
    public EshopApplicationAutoMapperProfile()
    {
        CreateMap<Prodotto, ProdottoDto>();
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
