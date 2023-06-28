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
            await lancamentoDeDescontos(employee);
            var discounts = await TotalDescontos();
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

        await lancamentoDeDescontos(employee);
        decimal discounts = await TotalDescontos();
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


    private async Task<decimal> TotalDescontos()
    {
        var discounts = 0m;

        foreach (var lancamento in _playmentEntryList)
        {
            if (lancamento.Type == PlaymentEntryTypes.Desconto.ToString())
                discounts = discounts + lancamento.Value;
        }

        return discounts;
    }

    private async Task lancamentoDeDescontos(Employee employee)
    {
        var lancamentoSalarioBruto = new PlaymentEntryDto
        {
            Type = PlaymentEntryTypes.Remuneracao.ToString(),
            Description = "Salario Bruto",
            Value = employee.GrossSalary

        };
        
        _playmentEntryList.Add(lancamentoSalarioBruto);

        CalculaDescontoSaude(employee);

        CalculaDescontDental(employee);

        CalculaDescontoTransporte(employee);

        IRPF(employee.GrossSalary);

        INSS(employee.GrossSalary);

        FGTS(employee);

    }

    private void FGTS(Employee employee)
    {
        decimal fgtsValue = 0.08m;

        decimal fgts = employee.GrossSalary * fgtsValue;

        var lancamento = new PlaymentEntryDto
        {
            Type = PlaymentEntryTypes.Desconto.ToString(),
            Description = DiscountsType.FGTS.ToString(),
            Value = fgts * (-1)

        };

        _playmentEntryList.Add(lancamento);
    }

    private void CalculaDescontoSaude(Employee employee)
    {
        if (employee.DiscountSaude)
        {

            var lancamento = new PlaymentEntryDto
            {
                Type = PlaymentEntryTypes.Desconto.ToString(),
                Description = DiscountsType.PlanoSaude.ToString(),
                Value = 10 * (-1)

            };

            _playmentEntryList.Add(lancamento);
        }
    }

    private void CalculaDescontDental(Employee employee)
    {
        if (employee.DiscountDental)
        {
            var lancamento = new PlaymentEntryDto
            {
                Type = PlaymentEntryTypes.Desconto.ToString(),
                Description = DiscountsType.PlanoDental.ToString(),
                Value = 5 * (-1)

            };
            _playmentEntryList.Add(lancamento);
        }
    }

    private void CalculaDescontoTransporte(Employee employee)
    {
        decimal transporteValue = 0.06m;

        if (employee.DiscountVale)
        {
            if (employee.GrossSalary > 1500)
            {
                var descontoTransporte = employee.GrossSalary * transporteValue;

                var lancamento = new PlaymentEntryDto
                {
                    Type = PlaymentEntryTypes.Desconto.ToString(),
                    Description = DiscountsType.ValeTransporte.ToString(),
                    Value = descontoTransporte * (-1)

                };
                _playmentEntryList.Add(lancamento);
            }
            else
            {
                var lancamento = new PlaymentEntryDto
                {
                    Type = PlaymentEntryTypes.Desconto.ToString(),
                    Description = DiscountsType.ValeTransporte.ToString(),
                    Value = 0

                };
                _playmentEntryList.Add(lancamento);
            }
        }
    }

    private void IRPF(decimal salario)
    {
        decimal faixa1 = 1903.98m;
        decimal faixa2 = 2826.65m;
        decimal faixa3 = 3751.05m;
        decimal faixa4 = 4664.68m;
        decimal desconto = 0;

        if (faixa1 < salario && salario <= faixa2)
            desconto = 142.8m;

        if (faixa2 < salario && faixa3 <= faixa2)
            desconto = salario - 354.8m;

        if (faixa3 < salario && salario <= faixa4)
            desconto = 636.13m;

        if (faixa4 < salario)
            desconto = 898.36m;


        var lancamento = new PlaymentEntryDto
        {
            Type = PlaymentEntryTypes.Desconto.ToString(),
            Description = DiscountsType.IRPF.ToString(),
            Value = desconto * (-1)

        };

        _playmentEntryList.Add(lancamento);
    }

    private void INSS(decimal salario)
    {
        var faixa1 = 1045m;
        var faixa2 = 2089.60m;
        var faixa3 = 3134.40m;
        var faixa4 = 6101.06m;
        decimal desconto = 0;

        if (salario <= faixa1)
            desconto = (salario * 0.075m);

        if (salario > faixa1 && salario < faixa2)
            desconto = (salario * 0.09m);

        if (salario > faixa2 && salario < faixa3)
            desconto = (salario * 0.12m);

        if (salario > faixa3 && salario < faixa4)
            desconto = (salario * 0.14m);

        var lancamento = new PlaymentEntryDto
        {
            Type = PlaymentEntryTypes.Desconto.ToString(),
            Description = DiscountsType.INSS.ToString(),
            Value = desconto * (-1)

        };

        _playmentEntryList.Add(lancamento);
    }
}
