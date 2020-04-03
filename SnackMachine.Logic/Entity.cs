using System;

namespace SnackMachine.Logic
{
    public class Entity
    {
        public int Id { get; private set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            if (obj == null) return false;

            if (obj?.GetType() != this.GetType()) return false;

            return Id == ((Entity)obj).Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}