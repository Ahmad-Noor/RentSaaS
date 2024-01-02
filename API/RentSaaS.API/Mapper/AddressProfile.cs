using AutoMapper;
using RentSaaS.API.DOTs;
using RentSaaS.Domain.Entities;
namespace RentSaaS.API.Mapper;
public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressResource>()
            .ForMember(t => t.Id, o => o.MapFrom(t => t.Id))
            .ForMember(t => t.TenantId, o => o.MapFrom(t => t.TenantId))
            .ForMember(t => t.IsDeleted, o => o.MapFrom(t => t.IsDeleted))
            .ForMember(t => t.Note, o => o.MapFrom(t => t.Note))

            .ForMember(t => t.Street, o => o.MapFrom(t => t.Street))
            .ForMember(t => t.Apartment, o => o.MapFrom(t => t.Apartment))
            .ForMember(t => t.Line2, o => o.MapFrom(t => t.Line2))
            .ForMember(t => t.City, o => o.MapFrom(t => t.City))
            .ForMember(t => t.POBox, o => o.MapFrom(t => t.POBox))
            .ForMember(t => t.State, o => o.MapFrom(t => t.State))
            .ForMember(t => t.Country, o => o.MapFrom(t => t.Country))
            .ForMember(t => t.PostalCode, o => o.MapFrom(t => t.PostalCode))
             
            .ForMember(t => t.CreatedAt, o => o.MapFrom(t => t.CreatedAt))
            .ForMember(t => t.UpdatedAt, o => o.MapFrom(t => t.UpdatedAt)) 
            .ReverseMap();
    }
}