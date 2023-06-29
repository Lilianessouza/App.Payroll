using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context;

public class DbSqlContext : DbContext
{
    public DbSqlContext(DbContextOptions<DbSqlContext> options) : base(options) { }

    public DbSet<Employee> Employee { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelbilder)
    {
        base.OnModelCreating(modelbilder);
        modelbilder.ApplyConfigurationsFromAssembly(typeof(DbSqlContext).Assembly);
       
    }

}
