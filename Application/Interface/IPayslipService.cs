using Application.Validator;

namespace Application.Interface;

public interface IPayslipService
{
    Task<ResultService> GetAllEmployee();
    Task<ResultService> GetPayslipByEmployeeId(int id);
}