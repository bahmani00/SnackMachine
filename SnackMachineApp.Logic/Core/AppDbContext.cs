using Logic.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SnackMachineApp.Logic.Atms;
using SnackMachineApp.Logic.Management;
using SnackMachineApp.Logic.SnackMachines;
using SnackMachineApp.Logic.Utils;
using System;

namespace SnackMachineApp.Logic.Core
{
    public partial class AppDbContext : DbContext
    {
        public virtual DbSet<Atm> Atm { get; set; }
        public virtual DbSet<HeadOffice> HeadOffice { get; set; }
        //public virtual DbSet<Slot> Slot { get; set; }
        //public virtual DbSet<Snack> Snack { get; set; }
        public virtual DbSet<SnackMachine> SnackMachine { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var serviceProvider = ObjectFactory.Instance.Resolve<IServiceProvider>();
                var cnnString = ObjectFactory.Instance.Resolve<CommandsConnectionProvider>();

                optionsBuilder.UseApplicationServiceProvider(serviceProvider)
                    .UseSqlServer(cnnString.Value,
                        serverOptions => serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null))
                    .UseLazyLoadingProxies();

                ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder
                        .AddFilter((category, level) =>
                            category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                        ;//.AddConsole();
                });

                optionsBuilder
                    .UseLoggerFactory(loggerFactory)
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