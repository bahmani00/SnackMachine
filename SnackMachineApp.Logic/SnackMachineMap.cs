using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace SnackMachineApp.Logic
{
    public class SnackMachineMap : ClassMap<SnackMachine>
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

            HasMany<Slot>(Reveal.Member<SnackMachine>("Slots"))
               .Cascade.SaveUpdate()
               .Not.LazyLoad()
               .Inverse();
        }
    }

    public class SlotMap : ClassMap<Slot>
    {
        public SlotMap()
        {
            Id(x => x.Id);
            Map(x => x.Position);
            Map(x => x.Quantity);
            Map(x => x.Price);


            References(x => x.Snack);
            References(x => x.SnackMachine);
        }
    }

    public class SnackMap : ClassMap<Snack>
    {
        public SnackMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}
