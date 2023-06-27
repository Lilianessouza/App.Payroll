using Domain.Entities;

namespace Application.Dto
{
    public class ContraChequeDto
    {
        public DateTime ReferenceDate { get; set; }
        public List<LancamentosDto>? Lancamentos { get; set; }
        public EmployeeContraChequeDto? Funcionario { get; set; }
        public decimal TotalDescontos { get; set; }
        public decimal SalarioLiquido { get; set; }
    }
}
