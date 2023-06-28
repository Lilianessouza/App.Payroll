using Application.Service;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace UnitTest;

public class PayslipServiceTest
{
    private PayslipService _service;
    private Mock<IEmployeeRepository> _repository = new Mock<IEmployeeRepository>();

    public PayslipServiceTest()
    {
        _service = new PayslipService(_repository.Object);
    }

    [Test]
    [TestCase(true, true, true, 1000.02)]
    [TestCase(true, false, true, 3000.02)]
    [TestCase(true, true, false, 4000.02)]
    [TestCase(false, true, false, 5000.02)]
    [TestCase(false, false, true, 6000.02)]
    [TestCase(false, false, false, 8000.02)]
    public async Task ShouldShowPayslipEmployeeByIdWithAnyPossibilityDiscounts(bool transporte, bool saude, bool dental, decimal salario)
    {
        var employee = new Employee
        {
            Id = 1,
            DiscountDental = dental,
            DiscountSaude = saude,
            DiscountVale = transporte,
            GrossSalary = salario,
            Name = "Teste"
        };

        _repository.Setup(x => x.GetEmployeeById(1)).ReturnsAsync(employee);

        var result = await _service.GetPayslipByEmployeeId(1);

        Assert.True(result.IsSucess);
    }

    [Test]
    public async Task ShouldNotFoundEmployeeInDatabase()
    {
        var employee = new Employee
        {
            Id = 1,
        };

        _repository.Setup(x => x.GetEmployeeById(employee.Id)).ReturnsAsync(It.IsAny<Employee>());

        var result = await _service.GetPayslipByEmployeeId(It.IsAny<int>());

        Assert.False(result.IsSucess);
    }
}
