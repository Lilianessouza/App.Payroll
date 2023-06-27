namespace Domain.Entities;

public class Audit
{
    public string? UserName { get; set; }
    public DateTime UpdateData { get; set; } = DateTime.Now;
    public bool Active { get; set; } = true;
}
