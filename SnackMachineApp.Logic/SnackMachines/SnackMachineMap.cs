using FluentNHibernate;
using FluentNHibernate.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SnackMachineApp.Logic.SnackMachines
{
    public class SnackMachineMap : ClassMap<SnackMachine>, IEntityTypeConfiguration<SnackMachine>
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
            //TODO: Map all the fields
            builder.HasKey(t => t.Id);
            //builder.HasMany<SnackMachine, slot>(t => t);

        }
    }

    public class SlotMap : ClassMap<Slot>
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
    }

    public class SnackMap : ClassMap<Snack>
    {
        public SnackMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.ImageWidth);
        }
    }
}
