using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(c => c.Active, option => option.Ignore())
            .ForMember(c => c.UserName, option => option.Ignore())
            .ForMember(c => c.UpdateData, option => option.Ignore()).ReverseMap();
    }
}
