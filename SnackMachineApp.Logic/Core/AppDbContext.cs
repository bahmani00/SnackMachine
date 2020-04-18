using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SnackMachineApp.Logic.Core
{
    public class AppDbContext : DbContext
    {
        //TODO: The model is missing here:
        //public DbSet<SnackMachine> SnackMachine { get; set; }
        //public DbSet<Atm> Atm { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configuration Files with AutoDiscovery
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}