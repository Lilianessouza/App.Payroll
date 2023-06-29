using Application.Dto;
using FluentValidation;

namespace Application.Validator;

public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
{
    public EmployeeDtoValidator() 
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Nome deve ser informado");

        RuleFor(x => x.CPF)
           .NotEmpty()
           .NotNull()
           .WithMessage("Cpf deve ser informado");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Sobrenome deve ser informado");

        RuleFor(x => x.Sector)
            .NotEmpty()
            .NotNull()
            .WithMessage("Setor deve ser informado");

        RuleFor(x => x.GrossSalary)
            .GreaterThan(0)
            .WithMessage("Salário deve ser maior que 0!");
    }
}
