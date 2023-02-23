using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Contexts;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectModels;Database=LocationInformation;Trusted_Connection=True;MultipleActiveResultSets=true;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly());
    }
}
