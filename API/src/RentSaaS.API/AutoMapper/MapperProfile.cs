using AutoMapper;
using RentSaaS.API.Dto.Expenses;
using RentSaaS.Domain.Entities;

namespace RentSaaS.API.AutoMapper
{
    public class MapperProfile :Profile
    {

        public MapperProfile()
        {
            CreateMap<ExpenseCreateDto, Expense>()/*.ReverseMap()*/; 
            CreateMap<ExpenseUpdateDto, Expense>(); 
            CreateMap<Expense, ExpenseGetAllDto>();
            CreateMap<Expense, ExpenseGetByIdDto>();

        }


    }
}
