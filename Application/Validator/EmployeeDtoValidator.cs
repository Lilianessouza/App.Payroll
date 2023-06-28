using Application.Dto;
using FluentValidation;

namespace Application.Validator;

public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
{
    public EmployeeDtoValidator() 
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .NotNull()
            .WithMessage("Nome deve ser informado");

        RuleFor(x => x.Sobrenome)
            .NotEmpty()
            .NotNull()
            .WithMessage("Sobrenome deve ser informado");

        RuleFor(x => x.Setor)
            .NotEmpty()
            .NotNull()
            .WithMessage("Setor deve ser informado");

        RuleFor(x => x.SalarioBruto)
            .GreaterThan(0)
            .WithMessage("Salário deve ser maior que 0!");
    }
}
