using AutoMapper;
using RentSaaS.API.Dto.Company;
using RentSaaS.API.Dto.Expenses;
using RentSaaS.Domain.Entities;

namespace RentSaaS.API.AutoMapper
{
    public class MapperProfile :Profile
    {

        public MapperProfile()
        {
            #region Expense 
            CreateMap<ExpenseCreateDto, Expense>()/*.ReverseMap()*/;
            CreateMap<ExpenseUpdateDto, Expense>();
            CreateMap<Expense, ExpenseGetAllDto>();
            CreateMap<Expense, ExpenseGetByIdDto>();
            #endregion








            #region Company
            CreateMap<CompanyCreateDto, Company>();
            #endregion

        }


    }
}
