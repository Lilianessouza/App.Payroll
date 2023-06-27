using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DbSqlContext _context;
    public EmployeeRepository(DbSqlContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployee()
    {
       return await _context.Employee.ToListAsync();
    }

    public async Task<Employee> GetEmployeeById(int id)
    {
        var result = await _context.Employee.FirstOrDefaultAsync(x => x.Id == id);
        return result == null ? null : result;   
    }

    public async Task<int> InsertEmployee(Employee entity)
    {
        _context.Employee.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<Employee> UpdateEmployee(Employee entity)
    {
        _context.Employee.Entry(entity).State= EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Employee> DeleteEmployee(Employee entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
     
}
