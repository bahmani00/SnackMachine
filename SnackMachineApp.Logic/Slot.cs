namespace SnackMachineApp.Logic
{
    public class Slot : Entity
    {
        protected Slot() { }
        public Slot(SnackMachine snackMachine, int position, Snack snack, int quantity, decimal price)
        {
            this.SnackMachine = snackMachine;
            this.Position = position;
            this.Snack = snack;
            this.Quantity = quantity;
            this.Price = price;
        }

        public virtual SnackMachine SnackMachine { get; set; }
        public virtual Snack Snack { get; set; }

        public virtual string Name { get; set; }
        public virtual int Position { get; set; }
        public virtual int Quantity { get; set; }
        public virtual decimal Price { get; set; }
    }
}
