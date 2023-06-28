namespace Application.Dto;

public class PayslipDto
{
    public DateTime ReferenceDate { get; set; }
    public List<PlaymentEntryDto>? PlaymentEntrys { get; set; }
    public EmployeePayslipDto? Employee { get; set; }
    public decimal TotalDiscounts { get; set; }
    public decimal NetSalary { get; set; }
}
