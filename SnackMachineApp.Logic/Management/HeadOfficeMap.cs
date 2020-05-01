using FluentNHibernate.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SnackMachineApp.Logic.Management
{
    public class HeadOfficeMap : ClassMap<HeadOffice>, IEntityTypeConfiguration<HeadOffice>
    {
        public HeadOfficeMap()
        {
            Id(x => x.Id);
            Map(x => x.Balance);

            Component(x => x.Cash, y =>
            {
                y.Map(x => x.OneCentCount);
                y.Map(x => x.TenCentCount);
                y.Map(x => x.QuarterCount);
                y.Map(x => x.OneDollarCount);
                y.Map(x => x.FiveDollarCount);
                y.Map(x => x.TwentyDollarCount);
            });
        }

        public void Configure(EntityTypeBuilder<HeadOffice> builder)
        {
            builder.ToTable(nameof(HeadOffice));
            builder.HasKey(x => x.Id).HasName("PK_HeadOfficeId");
            builder.Property(x => x.Id).HasColumnName("HeadOfficeId").ValueGeneratedNever();
            builder.Ignore(x => x.ValidationMessages);

            builder.Property(x => x.Balance).HasColumnType("decimal(19, 5)").IsRequired();

            builder.OwnsOne(x => x.Cash, nav =>
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
