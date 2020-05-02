using FluentNHibernate;
using FluentNHibernate.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackMachineApp.Logic.SharedKernel;

namespace SnackMachineApp.Logic.SnackMachines
{
    internal class SnackMachineMap : ClassMap<SnackMachine>, IEntityTypeConfiguration<SnackMachine>
    {
        public SnackMachineMap()
        {
            //Table("SnackMachine");
            //Id(x => x.Id, "SnackMachineId");
            Id(x => x.Id);

            Component(x => x.MoneyInside, y =>
            {
                y.Map(x => x.OneCentCount);
                y.Map(x => x.TenCentCount);
                y.Map(x => x.QuarterCount);
                y.Map(x => x.OneDollarCount);
                y.Map(x => x.FiveDollarCount);
                y.Map(x => x.TwentyDollarCount);
            });

            HasMany<Slot>(Reveal.Member<SnackMachine>(SnackMachine.Slots_Name))//reveal Slots since it's protected
               .Cascade.SaveUpdate() //Updating all inner objects. ex. Slots as well
               .Not.LazyLoad() //All repositories are working in detached mode(once session is closed, impossible to do Lazy loading)
               .Inverse();
        }

        public void Configure(EntityTypeBuilder<SnackMachine> builder)
        {
            builder.ToTable(nameof(SnackMachine));
            builder.HasKey(x => x.Id).HasName("PK_SnackMachineId");
            builder.Property(x => x.Id).HasColumnName("SnackMachineId").ValueGeneratedNever();
            builder.Ignore(x => x.ValidationMessages);
            builder.Ignore(x => x.MoneyInTransaction);

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

            builder.HasMany<Slot>(SnackMachine.Slots_Name).WithOne(p => p.SnackMachine)
                .HasConstraintName("FK_SlotId_SnackMachineId")
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }

    internal class SlotMap : ClassMap<Slot>, IEntityTypeConfiguration<Slot>
    {
        public SlotMap()
        {
            Id(x => x.Id);
            Map(x => x.Position);

            Component(x => x.SnackPile, y =>
            {
                y.Map(x => x.Quantity);
                y.Map(x => x.Price);
                y.References(x => x.Snack).Not.LazyLoad();
            });

            References(x => x.SnackMachine);
        }

        public void Configure(EntityTypeBuilder<Slot> builder)
        {
            builder.ToTable(nameof(Slot));
            builder.HasKey(x => x.Id).HasName("PK_SlotId");
            builder.Property(x => x.Id).HasColumnName("SlotId").ValueGeneratedNever();
            builder.Ignore(x => x.Name);

            builder.Property(x => x.Position).IsRequired();

            builder.OwnsOne(x => x.SnackPile);
            builder.OwnsOne(typeof(SnackPile), "SnackPile");
            builder.OwnsOne(x => x.SnackPile, nav =>
            {
                nav.Property(p => p.Price).HasColumnName("Price").IsRequired();
                nav.Property(p => p.Quantity).HasColumnName("Quantity").IsRequired();

                nav.Property<long>("SnackId").HasColumnName("SnackId");
                nav.HasOne(pp => pp.Snack).WithMany().HasForeignKey("SnackId").HasConstraintName("FK_SlotId_SnackId").IsRequired();
            });

            //builder.HasOne(d => d.SnackMachine)
            //   .WithMany(SnackMachine.Slots_Name)
            //   .HasForeignKey("SnackMachineId")
            //   .HasConstraintName("FK_SlotId_SnackMachineId");
        }
    }

    internal class SnackMap : ClassMap<Snack>, IEntityTypeConfiguration<Snack>
    {
        public SnackMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.ImageWidth);
        }

        public void Configure(EntityTypeBuilder<Snack> builder)
        {
            builder.ToTable(nameof(Snack));
            builder.HasKey(x => x.Id).HasName("PK_SnackId");
            builder.Property(x => x.Id).HasColumnName("SnackId").ValueGeneratedNever();
            builder.Ignore(x => x.ValidationMessages);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.ImageWidth).HasDefaultValueSql("70");
        }
    }
}
