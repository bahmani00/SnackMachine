using SnackMachineApp.Domain.Core;

namespace SnackMachineApp.Domain.SnackMachines
{
    public class Slot : Entity
    {
        protected Slot() { }

        public Slot(SnackMachine snackMachine, int position)
        {
            SnackMachine = snackMachine;
            Position = position;
            SnackPile = SnackPile.Empty;
        }

        public virtual SnackMachine SnackMachine { get; set; }

        public virtual SnackPile SnackPile { get; set; }

        public virtual string Name { get; set; }
        public virtual int Position { get; set; }
    }
}
