using AutoMapper;
using RentSaaS.Application.Dtos.Company;
using RentSaaS.Application.DTOs.Address;
using RentSaaS.Application.DTOs.Expense;
using RentSaaS.Application.DTOs.Advertising;
using RentSaaS.Application.DTOs.Property;
using RentSaaS.Domain.Entities;
using RentSaaS.Application.DTOs.Lease;
using RentSaaS.Application.DTOs.RentApplication;

namespace RentSaaS.API.AutoMapper
{
    public class MapperProfile :Profile
    {

        public MapperProfile()
        {












            #region Expense 
            CreateMap<ExpenseCreateDTO, Expense>()/*.ReverseMap()*/;
            CreateMap<ExpenseUpdateDTO, Expense>();
            CreateMap<Expense, GetExpenseDto>();
            CreateMap<Expense, GetExpenseByIdDto>();
            #endregion


            #region Property
            CreateMap<PropertyCreateDto, Property>()/*.ReverseMap()*/;
            CreateMap<PropertyUpdateDto, Property>();
            CreateMap<Property, PropertyGetDto>();
            #endregion





            #region Company
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<Company, CompanyGetDto>();
            #endregion


            #region Lease
            CreateMap<LeaseCreateDto, Lease>()/*.ReverseMap()*/;
            CreateMap<LeaseUpdateDto, Lease>();
            CreateMap<Lease, LeaseGetDto>();
            #endregion



            #region Address
            CreateMap<AddressCreateDto, Address>()/*.ReverseMap()*/;
            CreateMap<AddressUpdateDto, Address>();
            CreateMap<Address, AddressGetDto>();
            #endregion

            #region Advertizing
            CreateMap<AdvertisingCreateDto,Advertising>()/*.ReverseMap()*/;
            CreateMap<AddressUpdateDto, Advertising>();
            CreateMap<Advertising, AdvertisingGetDto>();
            #endregion

            #region Application&Leads
            CreateMap<ApplicationCreateDto, ApplicationAndLeads>()/*.ReverseMap()*/;
            CreateMap<ApplicationUpdateDto, ApplicationAndLeads>();
            CreateMap<ApplicationAndLeads, ApplicationGetDto>();
            #endregion

        }


    }
}
