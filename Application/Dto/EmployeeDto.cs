namespace Application.Dto;

public class EmployeeDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public long CPF { get; set; }
    public string? Sector { get; set; }
    public decimal GrossSalary { get; set; }
    public DateTime AdmissionDate { get; set; }
    public bool DiscountSaude { get; set; }
    public bool DiscountDental { get; set; }
    public bool DiscountVale { get; set; }
    public string? UserName { get; set; }
    public DateTime UpdateData { get; set; } 
    public bool Active { get; set; } 
}

