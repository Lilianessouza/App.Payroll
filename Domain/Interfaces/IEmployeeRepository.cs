using Domain.Entities;

namespace Domain.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllEmployee();   
    Task<Employee> GetEmployeeById(int id);   
    Task<int> InsertEmployee(Employee entity);   
    Task<Employee> UpdateEmployee(Employee entity);   
    Task<Employee> DeleteEmployee(Employee entity);   

}
