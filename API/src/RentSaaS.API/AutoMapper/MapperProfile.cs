using AutoMapper;
using RentSaaS.Application.Dtos.Company;
using RentSaaS.Application.DTOs.Expense;
using RentSaaS.Domain.Entities;

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








            #region Company
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<Company, CompanyGetDto>();
            #endregion

        }


    }
}
