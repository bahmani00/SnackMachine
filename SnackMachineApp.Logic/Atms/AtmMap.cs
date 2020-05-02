using FluentNHibernate.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackMachineApp.Logic.Atms;
using SnackMachineApp.Logic.SharedKernel;

namespace SnackMachineApp.Logic.SnackMachines
{
    internal class AtmMap : ClassMap<Atm>, IEntityTypeConfiguration<Atm>
    {
        public AtmMap()
        {
            Id(x => x.Id);
            Map(x => x.MoneyCharged);

            Component(x => x.MoneyInside, y =>
            {
                y.Map(x => x.OneCentCount);
                y.Map(x => x.TenCentCount);
                y.Map(x => x.QuarterCount);
                y.Map(x => x.OneDollarCount);
                y.Map(x => x.FiveDollarCount);
                y.Map(x => x.TwentyDollarCount);
            });
        }

        public void Configure(EntityTypeBuilder<Atm> builder)
        {
            builder.ToTable(nameof(Atm));
            builder.HasKey(x => x.Id).HasName("PK_AtmId");
            builder.Property(x => x.Id).HasColumnName("AtmId").ValueGeneratedNever();

            builder.Property(x => x.MoneyCharged).HasColumnType("decimal(19, 5)").IsRequired();

            builder.OwnsOne(x => x.MoneyInside);
            builder.OwnsOne(typeof(Money), "MoneyInside");
            builder.OwnsOne(x => x.MoneyInside, nav =>
            {
                nav.Property(x => x.OneCentCount).HasColumnName("OneCentCount").IsRequired();
                nav.Property(x => x.TenCentCount).HasColumnName("TenCentCount").IsRequired();
                nav.Property(x => x.QuarterCount).HasColumnName("QuarterCount").IsRequired();
                nav.Property(x => x.OneDollarCount).HasColumnName("OneDollarCount").IsRequired();
                nav.Property(x => x.FiveDollarCount).HasColumnName("FiveDollarCount").IsRequired();
                nav.Property(x => x.TwentyDollarCount).HasColumnName("TwentyDollarCount").IsRequired();
            });
        }
    }
}
