

using AutoMapper;
using Invoices.Api.Models;
using Invoices.API.Models;
using Invoices.Data.Models;

namespace Invoices.Api;

public class AutomapperConfigurationProfile : Profile
{
    public AutomapperConfigurationProfile()
    {
        CreateMap<Person, PersonDto>();
        CreateMap<PersonDto, Person>();
        CreateMap<Invoice,InvoiceDto>();
        CreateMap<InvoiceDto,Invoice>()
            .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.Buyer.PersonId))
			.ForMember(dest => dest.Buyer, opt => opt.Ignore())
            .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.Seller.PersonId))
			.ForMember(dest => dest.Seller, opt => opt.Ignore());
	}
}