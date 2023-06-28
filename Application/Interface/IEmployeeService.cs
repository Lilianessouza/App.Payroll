using Application.Dto;
using Application.Validator;

namespace Application.Interface;

public interface IEmployeeService
{
    Task<ResultService> GetAll();
    Task<ResultService> Insert(EmployeeDto employeeDto);
    Task<ResultService> GetById(int id);
    Task<ResultService> Update(EmployeeDto entity);
    Task<ResultService> Delete(int id);
}
