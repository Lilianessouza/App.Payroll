using Application.Dto;
using Application.Interface;
using Application.Validator;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Service;

public class PayslipService : IPayslipService
{
    private IEmployeeRepository _repositoryEmployee;
    private List<PlaymentEntryDto> _playmentEntryList;

    public PayslipService(IEmployeeRepository repository)
    {
        _repositoryEmployee = repository;
        _playmentEntryList = new List<PlaymentEntryDto>();
    }

    public async Task<ResultService> GetAllPayslipEmployee()
    {
        var employees = await _repositoryEmployee.GetAllEmployee();
        var payslip = new List<PayslipDto>();

        foreach (var employee in employees)
        {
            await playmentEntryWithDiscounts(employee);
            var discounts = await DiscountsTotal();
            var salary = employee.GrossSalary - discounts;

            var folha = new PayslipDto
            {
                Employee = new EmployeePayslipDto { Id = employee.Id, NameFull = employee.Name + ' ' + employee.LastName, Cpf = employee.CPF },
                ReferenceDate = DateTime.Now,
                TotalDiscounts = discounts,
                NetSalary = salary,
                PlaymentEntrys = _playmentEntryList
            };

            payslip.Add(folha);
        }

        return ResultService.Ok<IEnumerable<PayslipDto>>(payslip);

    }


    public async Task<ResultService> GetPayslipByEmployeeId(int id)
    {
        if (id == 0)
            return ResultService.Fail<EmployeeDto>("Informar Id do funcionario");

        var employee = await _repositoryEmployee.GetEmployeeById(id);

        if (employee == null)
            return ResultService.NoContent<EmployeeDto>("Funcionario não existe na base");

        await playmentEntryWithDiscounts(employee);
        decimal discounts = await DiscountsTotal();
        var salary = employee.GrossSalary - (-discounts);

        var payslip = new PayslipDto
        {
            Employee = new EmployeePayslipDto { Id = employee.Id, NameFull = employee.Name + ' ' + employee.LastName, Cpf = employee.CPF },
            ReferenceDate = DateTime.Now,
            TotalDiscounts = discounts,
            NetSalary = salary,
            PlaymentEntrys = _playmentEntryList

        };

        return ResultService.Ok(payslip);
    }


    private async Task<decimal> DiscountsTotal()
    {
        var discounts = 0m;

        foreach (var playmentEntry in _playmentEntryList)
        {
            if (playmentEntry.Type == PlaymentEntryTypes.Desconto.ToString())
                discounts = discounts + playmentEntry.Value;
        }

        return discounts;
    }

    private async Task playmentEntryWithDiscounts(Employee employee)
    {
        var playmentEntrySalarioBruto = new PlaymentEntryDto
        {
            Type = PlaymentEntryTypes.Remuneracao.ToString(),
            Description = "Salario Bruto",
            Value = employee.GrossSalary

        };
        
        _playmentEntryList.Add(playmentEntrySalarioBruto);

        CalculatedHealthDiscount(employee);

        CalculatedDentalDiscount(employee);

        CalculatedTransportDiscount(employee);

        CalculatedIRPF(employee.GrossSalary);

        CalculatedINSS(employee.GrossSalary);

        CalculatedFGTS(employee);

    }

    private void CalculatedFGTS(Employee employee)
    {
        decimal fgtsValue = 0.08m;

        decimal fgts = employee.GrossSalary * fgtsValue;

        var playmentEntry = new PlaymentEntryDto
        {
            Type = PlaymentEntryTypes.Desconto.ToString(),
            Description = DiscountsType.FGTS.ToString(),
            Value = fgts * (-1)

        };

        _playmentEntryList.Add(playmentEntry);
    }

    private void CalculatedHealthDiscount(Employee employee)
    {
        if (employee.DiscountSaude)
        {

            var playmentEntry = new PlaymentEntryDto
            {
                Type = PlaymentEntryTypes.Desconto.ToString(),
                Description = DiscountsType.PlanoSaude.ToString(),
                Value = 10 * (-1)

            };

            _playmentEntryList.Add(playmentEntry);
        }
    }

    private void CalculatedDentalDiscount(Employee employee)
    {
        if (employee.DiscountDental)
        {
            var playmentEntry = new PlaymentEntryDto
            {
                Type = PlaymentEntryTypes.Desconto.ToString(),
                Description = DiscountsType.PlanoDental.ToString(),
                Value = 5 * (-1)

            };
            _playmentEntryList.Add(playmentEntry);
        }
    }

    private void CalculatedTransportDiscount(Employee employee)
    {
        decimal transportPercent = 0.06m;

        if (employee.DiscountVale)
        {
            if (employee.GrossSalary > 1500)
            {
                var transportDiscount = employee.GrossSalary * transportPercent;

                var playmentEntry = new PlaymentEntryDto
                {
                    Type = PlaymentEntryTypes.Desconto.ToString(),
                    Description = DiscountsType.ValeTransporte.ToString(),
                    Value = transportDiscount * (-1)

                };
                _playmentEntryList.Add(playmentEntry);
            }
            else
            {
                var playmentEntry = new PlaymentEntryDto
                {
                    Type = PlaymentEntryTypes.Desconto.ToString(),
                    Description = DiscountsType.ValeTransporte.ToString(),
                    Value = 0

                };
                _playmentEntryList.Add(playmentEntry);
            }
        }
    }

    private void CalculatedIRPF(decimal salary)
    {
        decimal range1 = 1903.98m;
        decimal range2 = 2826.65m;
        decimal range3 = 3751.05m;
        decimal range4 = 4664.68m;
        decimal discount = 0;

        if (range1 < salary && salary <= range2)
            discount = 142.8m;

        if (range2 < salary && range3 <= range2)
            discount = salary - 354.8m;

        if (range3 < salary && salary <= range4)
            discount = 636.13m;

        if (range4 < salary)
            discount = 898.36m;


        var playmentEntry = new PlaymentEntryDto
        {
            Type = PlaymentEntryTypes.Desconto.ToString(),
            Description = DiscountsType.IRPF.ToString(),
            Value = discount * (-1)

        };

        _playmentEntryList.Add(playmentEntry);
    }

    private void CalculatedINSS(decimal salary)
    {
        var range1 = 1045m;
        var range2 = 2089.60m;
        var range3 = 3134.40m;
        var range4 = 6101.06m;
        decimal discount = 0;

        if (salary <= range1)
            discount = (salary * 0.075m);

        if (salary > range1 && salary < range2)
            discount = (salary * 0.09m);

        if (salary > range2 && salary < range3)
            discount = (salary * 0.12m);

        if (salary > range3 && salary < range4)
            discount = (salary * 0.14m);

        var playmentEntry = new PlaymentEntryDto
        {
            Type = PlaymentEntryTypes.Desconto.ToString(),
            Description = DiscountsType.INSS.ToString(),
            Value = discount * (-1)

        };

        _playmentEntryList.Add(playmentEntry);
    }
}
