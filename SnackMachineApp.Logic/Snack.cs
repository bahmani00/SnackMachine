namespace SnackMachineApp.Logic
{
    public class Snack : Entity
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
