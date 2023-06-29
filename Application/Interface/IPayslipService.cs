using Application.Validator;

namespace Application.Interface;

public interface IPayslipService
{
    Task<ResultService> GetAllPayslipEmployee();
    Task<ResultService> GetPayslipByEmployeeId(int id);
}