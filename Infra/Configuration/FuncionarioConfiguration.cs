using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
            .Property(c => c.Nome)
            .HasMaxLength(50)
            .IsRequired();

            builder
               .Property(c => c.Sobrenome)
               .HasMaxLength(50)
               .IsRequired();

            builder
               .Property(c => c.Setor)
               .HasMaxLength(50)
               .IsRequired();

            builder
               .Property(c => c.SalarioBruto)
               .HasPrecision(10, 2);
        }
    }
}
