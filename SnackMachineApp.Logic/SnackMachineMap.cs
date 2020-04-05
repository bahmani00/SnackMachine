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
        }
    }
}
