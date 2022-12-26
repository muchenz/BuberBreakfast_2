using BuberBreakfast.Models;
using BuberBreakfast.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Persistence;

public class BubberBreakfastDbContext:DbContext
{
	public BubberBreakfastDbContext(DbContextOptions<BubberBreakfastDbContext> options):base(options)
	{

	}

	public DbSet<Breakfast> Breakfasts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(BreakfastConfigurations).Assembly);

    }
}
