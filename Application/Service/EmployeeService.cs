using Application.Dto;
using Application.Interface;
using Application.Validator;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Service;

public class EmployeeService : IEmployeeService
{
    private IEmployeeRepository _repository;
    private IMapper _mapper;

    public EmployeeService(IEmployeeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResultService> Delete(int id)
    {
        if (id == 0)
            return ResultService.Fail<EmployeeDto>("Informar Id do funcionario");

        var entity = await _repository.GetEmployeeById(id);

        if (entity == null)
            return ResultService.NoContent<EmployeeDto>("Funcionario não existe na base");

        var result = await _repository.DeleteEmployee(entity);

        return ResultService.Ok<EmployeeDto>(_mapper.Map<EmployeeDto>(result));
    }

    public async Task<ResultService> GetAll()
    {
        var employees = await _repository.GetAllEmployee();
        var result = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        return ResultService.Ok<IEnumerable<EmployeeDto>>(result);
    }

    public async Task<ResultService> GetById(int id)
    {
        try
        {
            if (id == 0)
                return ResultService.Fail<EmployeeDto>("Informar Id do funcionario");

            var result = await _repository.GetEmployeeById(id);

            if (result == null)
                return ResultService.NoContent<EmployeeDto>("Funcionario não existe na base");
            else
                return ResultService.Ok<EmployeeDto>(_mapper.Map<EmployeeDto>(result));
        }
        catch (Exception ex)
        {
            return ResultService.Fail<EmployeeDto>(ex.ToString());
        }
        

    }

    public async Task<ResultService> Insert(EmployeeDto employeeDto)
    {
        try
        {
            if (employeeDto == null)
                return ResultService.Fail<EmployeeDto>("Informar Funcionario");

            var result = new EmployeeDtoValidator().Validate(employeeDto);

            if (!result.IsValid)
                return ResultService.CreateError<EmployeeDto>("Validação dos dados", result);

            var employee = _mapper.Map<Employee>(employeeDto);

            var data = await _repository.InsertEmployee(employee);

            return ResultService.Ok<int>(data);
        }
        catch (Exception ex)
        {
            return ResultService.Fail<EmployeeDto>(ex.ToString());
        }

    }

    public async Task<ResultService> Update(EmployeeDto employeeDto)
    {
        if (employeeDto == null)
            return ResultService.Fail<EmployeeDto>("Informar Funcionario");

        var result = new EmployeeDtoValidator().Validate(employeeDto);

        if (!result.IsValid)
            return ResultService.CreateError<EmployeeDto>("Validação dos dados", result);

        var employee = _mapper.Map<Employee>(employeeDto);
        var data = await _repository.UpdateEmployee(employee);

        return ResultService.Ok<EmployeeDto>(_mapper.Map<EmployeeDto>(data));
    }

    //public async Task<ResultService> Delete(EmployeeDto employeedto)
    //{
    //    var entity = _mapper.Map<Employee>(employeedto);
    //    await _repository.DeleteEmployee(entity);

    //}
}
