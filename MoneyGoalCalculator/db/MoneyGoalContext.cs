using Microsoft.EntityFrameworkCore;
using MoneyGoalCalculator.Entities;

namespace MoneyGoalCalculator.db;

public class MoneyGoalContext : DbContext
{
    public MoneyGoalContext(DbContextOptions<MoneyGoalContext> options) : base(options)
    {
        
    }

    public DbSet<CalculationEntity> Calculations { get; set; }
}
