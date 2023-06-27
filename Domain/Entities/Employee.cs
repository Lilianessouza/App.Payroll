
namespace Domain.Entities;

public class Employee : Audit
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }
    public long CPF { get; set; }
    public string? Setor { get; set; }
    public decimal SalarioBruto { get; set; }
    public DateTime DataAdmicao {get; set;}
    public bool DescontoSaude { get;set; }
    public bool DescontoDental { get;set; }
    public bool DescontoVale { get;set; }

}
