using Logic.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SnackMachineApp.Logic.Atms;
using SnackMachineApp.Logic.Core.Interfaces;
using SnackMachineApp.Logic.Management;
using SnackMachineApp.Logic.SnackMachines;
using SnackMachineApp.Logic.Utils;
using System;
using System.Linq;

namespace SnackMachineApp.Logic.Core
{
    internal partial class AppDbContext : DbContext
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

        private static readonly Type[] _TrackingTypes = { typeof(SnackMachine), typeof(HeadOffice), typeof(Atm) };

        public override int SaveChanges()
        {
            var enumerationEntries = ChangeTracker.Entries()
                .Where(x => _TrackingTypes.Contains(x.Entity.GetType()));

            foreach (var enumerationEntry in enumerationEntries)
            {
                enumerationEntry.State = EntityState.Unchanged;
            }

            var entities = ChangeTracker
                .Entries()
                .Where(x => x.Entity is Entity)
                .Select(x => (Entity)x.Entity)
                .ToList();

            int result = base.SaveChanges();
            var eventDispatcher = ObjectFactory.Instance.Resolve<IDomainEventDispatcher>();

            foreach (var entity in entities)
            {
                foreach (var domainEvent in entity.DomainEvents)
                {
                    eventDispatcher.Dispatch(domainEvent);
                }
                entity.ClearEvents();
            }

            return result;
        }
    }
}