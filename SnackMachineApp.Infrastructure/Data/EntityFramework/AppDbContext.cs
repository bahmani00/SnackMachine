using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.Management;
using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure.Data;
using System;

namespace SnackMachineApp.Interface.Data
{
    internal partial class AppDbContext : DbContext
    {
        private readonly IServiceProvider serviceProvider;

        public AppDbContext(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public virtual DbSet<Atm> Atm { get; set; }
        public virtual DbSet<HeadOffice> HeadOffice { get; set; }
        public virtual DbSet<SnackMachine> SnackMachine { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var cnnString = serviceProvider.GetService<CommandsConnectionProvider>();

                optionsBuilder.UseApplicationServiceProvider(serviceProvider)
                    .UseSqlServer(cnnString.Value,
                        serverOptions => serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null));

                optionsBuilder
                    .UseLoggerFactory(new LoggerFactory(new[] {
                        new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
                    }))
                    .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuration Files with AutoDiscovery
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}