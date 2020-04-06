namespace SnackMachineApp.Logic
{
    public class Slot : Entity
    {
        protected Slot() { }
        public Slot(SnackMachine snackMachine, int position)
        {
            this.SnackMachine = snackMachine;
            this.Position = position;
            this.SnackPile = new SnackPile(null, 0, 0m);
        }

        public virtual SnackMachine SnackMachine { get; set; }

        public virtual SnackPile SnackPile { get; set; }

        public virtual string Name { get; set; }
        public virtual int Position { get; set; }
    }
}
