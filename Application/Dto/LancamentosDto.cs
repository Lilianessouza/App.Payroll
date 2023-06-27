using Domain.Enum;

namespace Application.Dto
{
    public class LancamentosDto
    {
        public string? Tipo { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

    }
}
