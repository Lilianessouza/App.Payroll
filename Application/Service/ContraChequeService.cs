using Application.Dto;
using Application.Interface;
using Application.Validator;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Service;

public class ContraChequeService : IContraChequeService
{
    private IEmployeeRepository _repositoryEmployee;
    private List<LancamentosDto> _lancamentoList;

    public ContraChequeService(IEmployeeRepository repository)
    {
        _repositoryEmployee = repository;
        _lancamentoList = new List<LancamentosDto>();
    }

    public async Task<ResultService> GetAllFuncionario()
    {
        var employees = await _repositoryEmployee.GetAllEmployee();
        var contraCheque = new List<ContraChequeDto>();

        foreach (var employee in employees)
        {
            await lancamentoDeDescontos(employee);
            var descontos = await TotalDescontos();
            var salarioLiquido = employee.SalarioBruto - descontos;

            var folha = new ContraChequeDto
            {
                Funcionario = new EmployeeContraChequeDto { Id = employee.Id, NomeCompleto = employee.Nome + ' ' + employee.Sobrenome, Cpf = employee.CPF },
                ReferenceDate = DateTime.Now,
                TotalDescontos = descontos,
                SalarioLiquido = salarioLiquido,
                Lancamentos = _lancamentoList
            };

            contraCheque.Add(folha);
        }

        return ResultService.Ok<IEnumerable<ContraChequeDto>>(contraCheque);

    }


    public async Task<ResultService> GetContraChequeByFuncionarioId(int id)
    {
        if (id == 0)
            return ResultService.Fail<EmployeeDto>("Informar Id do funcionario");

        var employee = await _repositoryEmployee.GetEmployeeById(id);

        if (employee == null)
            return ResultService.NoContent<EmployeeDto>("Funcionario não existe na base");

        await lancamentoDeDescontos(employee);
        decimal descontos = await TotalDescontos();
        var salarioLiquido = employee.SalarioBruto - (-descontos);

        var contraCheque = new ContraChequeDto
        {
            Funcionario = new EmployeeContraChequeDto { Id = employee.Id, NomeCompleto = employee.Nome + ' ' + employee.Sobrenome, Cpf = employee.CPF },
            ReferenceDate = DateTime.Now,
            TotalDescontos = descontos,
            SalarioLiquido = salarioLiquido,
            Lancamentos = _lancamentoList

        };

        return ResultService.Ok<ContraChequeDto>(contraCheque);
    }


    private async Task<decimal> TotalDescontos()
    {
        var descontos = 0m;

        foreach (var lancamento in _lancamentoList)
        {
            if (lancamento.Tipo == TipoLancamento.Desconto.ToString())
                descontos = descontos + lancamento.Valor;
        }

        return descontos;
    }

    private async Task lancamentoDeDescontos(Employee employee)
    {
        var lancamentoSalarioBruto = new LancamentosDto
        {
            Tipo = TipoLancamento.Remuneracao.ToString(),
            Descricao = "Salario Bruto",
            Valor = employee.SalarioBruto

        };
        
        _lancamentoList.Add(lancamentoSalarioBruto);

        CalculaDescontoSaude(employee);

        CalculaDescontDental(employee);

        CalculaDescontoTransporte(employee);

        IRPF(employee.SalarioBruto);

        INSS(employee.SalarioBruto);

        FGTS(employee);

    }

    private void FGTS(Employee employee)
    {
        decimal fgtsValue = 0.08m;

        decimal fgts = employee.SalarioBruto * fgtsValue;

        var lancamento = new LancamentosDto
        {
            Tipo = TipoLancamento.Desconto.ToString(),
            Descricao = TiposDescontos.FGTS.ToString(),
            Valor = fgts * (-1)

        };

        _lancamentoList.Add(lancamento);
    }

    private void CalculaDescontoSaude(Employee employee)
    {
        if (employee.DescontoSaude)
        {

            var lancamento = new LancamentosDto
            {
                Tipo = TipoLancamento.Desconto.ToString(),
                Descricao = TiposDescontos.PlanoSaude.ToString(),
                Valor = 10 * (-1)

            };

            _lancamentoList.Add(lancamento);
        }
    }

    private void CalculaDescontDental(Employee employee)
    {
        if (employee.DescontoDental)
        {
            var lancamento = new LancamentosDto
            {
                Tipo = TipoLancamento.Desconto.ToString(),
                Descricao = TiposDescontos.PlanoDental.ToString(),
                Valor = 5 * (-1)

            };
            _lancamentoList.Add(lancamento);
        }
    }

    private void CalculaDescontoTransporte(Employee employee)
    {
        decimal transporteValue = 0.06m;

        if (employee.DescontoVale)
        {
            if (employee.SalarioBruto > 1500)
            {
                var descontoTransporte = employee.SalarioBruto * transporteValue;

                var lancamento = new LancamentosDto
                {
                    Tipo = TipoLancamento.Desconto.ToString(),
                    Descricao = TiposDescontos.ValeTransporte.ToString(),
                    Valor = descontoTransporte * (-1)

                };
                _lancamentoList.Add(lancamento);
            }
            else
            {
                var lancamento = new LancamentosDto
                {
                    Tipo = TipoLancamento.Desconto.ToString(),
                    Descricao = TiposDescontos.ValeTransporte.ToString(),
                    Valor = 0

                };
                _lancamentoList.Add(lancamento);
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


        var lancamento = new LancamentosDto
        {
            Tipo = TipoLancamento.Desconto.ToString(),
            Descricao = TiposDescontos.IRPF.ToString(),
            Valor = desconto * (-1)

        };

        _lancamentoList.Add(lancamento);
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

        var lancamento = new LancamentosDto
        {
            Tipo = TipoLancamento.Desconto.ToString(),
            Descricao = TiposDescontos.INSS.ToString(),
            Valor = desconto * (-1)

        };

        _lancamentoList.Add(lancamento);
    }
}
