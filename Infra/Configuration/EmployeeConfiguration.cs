using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
        .Property(c => c.Name)
        .HasMaxLength(50)
        .IsRequired();

        builder
           .Property(c => c.LastName)
           .HasMaxLength(50)
           .IsRequired();

        builder
           .Property(c => c.Sector)
           .HasMaxLength(50)
           .IsRequired();

        builder
           .Property(c => c.GrossSalary)
           .HasPrecision(10, 2);
    }
}
