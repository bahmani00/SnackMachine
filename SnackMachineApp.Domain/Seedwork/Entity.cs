using SnackMachineApp.Domain.Seedwork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SnackMachineApp.Domain.SeedWork
{
    public class Entity
    {
        public virtual long Id { get; protected set; }

        [NotMapped]
        public virtual IList<string> ValidationMessages { get; protected set; } = new List<string>();

        public virtual bool AnyErrors()
        {
            return ValidationMessages.Any();
        }

        #region Events
        private readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();
        public virtual IReadOnlyList<IDomainEvent> DomainEvents => domainEvents;

        protected virtual void AddDomainEvent(IDomainEvent newEvent)
        {
            domainEvents.Add(newEvent);
        }

        public virtual void ClearEvents()
        {
            domainEvents.Clear();
        }
        #endregion Events

        public override bool Equals(object obj)
        {
            var other = obj as Entity;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}