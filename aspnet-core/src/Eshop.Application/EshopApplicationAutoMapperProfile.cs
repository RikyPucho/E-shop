using AutoMapper;
using Eshop.Carrelli;
using Eshop.Ordini;
using Eshop.Prodotti;

namespace Eshop;

public class EshopApplicationAutoMapperProfile : Profile
{
    public EshopApplicationAutoMapperProfile()
    {
        CreateMap<Prodotto, ProdottoDto>();
        CreateMap<Prodotto, ProdottoLookupDto>();

        CreateMap<CarrelloWithDetails, CarrelloDto>();

        CreateMap<OrdineWithDetails, OrdineDto>();
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
