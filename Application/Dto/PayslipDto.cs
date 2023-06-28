namespace Application.Dto
{
    public class PayslipDto
    {
        public DateTime ReferenceDate { get; set; }
        public List<PlaymentEntryDto>? PlaymentEntry { get; set; }
        public EmployeePayslipDto? Employee { get; set; }
        public decimal TotalDescontos { get; set; }
        public decimal SalarioLiquido { get; set; }
    }
}
