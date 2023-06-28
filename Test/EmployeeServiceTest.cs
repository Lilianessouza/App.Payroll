using Application.Dto;
using Application.Service;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace UnitTest;

public class EmployeeServiceTest
{

    private EmployeeService _service;
    private Mock<IEmployeeRepository> _repository = new Mock<IEmployeeRepository>();
    private Mock<IMapper> _mapper = new Mock<IMapper>();

    public EmployeeServiceTest()
    {
        _service = new EmployeeService(_repository.Object, _mapper.Object);
    }

    [Test]
    public async Task ShouldNotInsertEmployeeNull()
    {
        var employeeDto = new EmployeeDto();
        var employee = new Employee();

        _repository.Setup(x => x.InsertEmployee(employee));

        var result = await _service.Insert(employeeDto);

        Assert.False(result.IsSucess);
    }

    [Test]
    public async Task ShouldInsertEmployeeWhitoutDataRequired()
    {
        var employeeDto = new EmployeeDto { Nome = "teste123" };
        var employee = new Employee { Name = "teste123", GrossSalary = 1m, LastName = "123", Sector = "abc" };

        _repository.Setup(x => x.InsertEmployee(employee)).ReturnsAsync(It.IsAny<int>);
        _mapper.Setup(x => x.Map<Employee>(employeeDto)).Returns(employee);

        var result = await _service.Insert(employeeDto);

        Assert.False(result.IsSucess);
    }

    [Test]
    public async Task ShouldInsertEmployeeWhitDataRequired()
    {
        var employeeDto = new EmployeeDto { Nome = "teste123", Sobrenome = "123", SalarioBruto = 1m, Setor = "abc" };
        var employee = new Employee { Name = "teste123", GrossSalary = 1m, LastName = "123", Sector = "abc" };

        _repository.Setup(x => x.InsertEmployee(employee)).ReturnsAsync(It.IsAny<int>);
        _mapper.Setup(x => x.Map<Employee>(employeeDto)).Returns(employee);
        
        var result = await _service.Insert(employeeDto);

        Assert.True(result.IsSucess);
    }
}