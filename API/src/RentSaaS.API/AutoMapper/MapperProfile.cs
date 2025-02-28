using Common;
using AutoMapper;
using RentSaaS.Domain.Entities;
using RentSaaS.Application.DTOs.Lease;
using RentSaaS.Application.Dtos.Company;
using RentSaaS.Application.DTOs.Address;
using RentSaaS.Application.DTOs.Expense;
using RentSaaS.Application.DTOs.Property;
using RentSaaS.Application.DTOs.UserDtos;
using RentSaaS.Application.DTOs.Maintenace;
using RentSaaS.Application.DTOs.Advertising;
using RentSaaS.Application.DTOs.RecordPayment;
using RentSaaS.Application.DTOs.ApplicationAndLeads;

using RentSaaS.Application.DTOs.Tenant;

namespace RentSaaS.API.AutoMapper;

public class MapperProfile :Profile
{

    public MapperProfile()
    {

        #region Property
        CreateMap<PropertyCreateDto, Property>();
        CreateMap<PropertyUpdateDto, Property>().ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Property, PropertyGetDto>();
        #endregion

        #region Auth



        CreateMap<UserRegistrationRequestDto, User>()
            .ForMember(x=>x.PasswordHash,Dis=>Dis.MapFrom(Source => Password.HashPassword(Source.Password)));
        #endregion



        #region Expense 
        CreateMap<ExpenseCreateDTO, Expense>()/*.ReverseMap()*/;
        CreateMap<ExpenseUpdateDTO, Expense>().ForMember(dest => dest.Id, opt => opt.Ignore()); 
        CreateMap<Expense, GetExpenseDto>();
        CreateMap<Expense, GetExpenseByIdDto>();
        #endregion



        #region Company
        CreateMap<CompanyCreateDto, Company>();
        CreateMap<Company, CompanyGetDto>();
        #endregion


        #region Lease
        CreateMap<LeaseCreateDto, Lease>()/*.ReverseMap()*/;
        CreateMap<LeaseUpdateDto, Lease>().ForMember(dest => dest.Id, opt => opt.Ignore()); ;
        CreateMap<Lease, LeaseGetDto>();
        #endregion



        #region Address
        CreateMap<AddressCreateDto, Address>()/*.ReverseMap()*/;
        CreateMap<AddressUpdateDto, Address>().ForMember(dest => dest.Id, opt => opt.Ignore()); ;
        CreateMap<Address, AddressGetDto>();
        #endregion

        #region Advertizing
        CreateMap<AdvertisingCreateDto,Advertising>()/*.ReverseMap()*/;
        CreateMap<AdvertisingUpdateDto, Advertising>().ForMember(dest => dest.Id, opt => opt.Ignore()); ;
        CreateMap<Advertising, AdvertisingGetDto>();
        #endregion

        #region Application&Leads
        CreateMap<ApplicationCreateDto, ApplicationAndLeads>()/*.ReverseMap()*/;
        CreateMap<ApplicationUpdateDto, ApplicationAndLeads>().ForMember(dest => dest.Id, opt => opt.Ignore()); ;
        CreateMap<ApplicationAndLeads, ApplicationGetDto>();
        #endregion


        #region RecordPayment
        CreateMap<RecordPaymentCreateDto, RecordPayment>()/*.ReverseMap()*/;
        CreateMap<RecordPaymentUpdateDto, RecordPayment>().ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<RecordPayment, GetRecordPaymentDto>();
        CreateMap<RecordPayment, RecordPaymentByIdDto>();

        #endregion


        #region Tenant
        CreateMap<TenantCreateDto, Tenant>()/*.ReverseMap()*/;
        CreateMap<TenantUpdateDto, Tenant>().ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Tenant, TenantGetDto>();
        CreateMap<Tenant, TenantGetByIdDto>();

        #endregion

        #region Maintenance 
        CreateMap<MaintenanceCreateDTO, Maintenance>()/*.ReverseMap()*/;
        CreateMap<MaintenanceUpdateDTO, Maintenance>().ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Maintenance, GetMaintenanceDto>();
        CreateMap<Maintenance, GetMaintenaceByIdDto>();
        #endregion
    }


}
