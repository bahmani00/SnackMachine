namespace SnackMachineApp.Logic
{
    public class Snack : AggregateRoot
    {
        public Snack():this(null)
        {
        }
        public Snack(string name)
        {
            Name = name;
        }
        public virtual string Name { get; set; }
    }
}
