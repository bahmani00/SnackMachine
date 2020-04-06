using System;
using System.Collections.Generic;
using System.Linq;
using static SnackMachineApp.Logic.Money;

namespace SnackMachineApp.Logic
{
    public class SnackMachine: AggregateRoot
    {
        public virtual Money MoneyInside { get; protected set; }
        public virtual Money MoneyInTransaction { get; protected set; }

        public virtual IList<Slot> Slots { get; protected set; }

        public SnackMachine() {
            MoneyInside = None;
            MoneyInTransaction = None;

            Slots = new List<Slot>
            {
                new Slot(this, 1),
                new Slot(this, 2),
                new Slot(this, 3),
            };
        }

        public virtual void InsertMoney(Money money)
        {
            if (Money.Validate(money))
                MoneyInTransaction += money;
            else
                throw new InvalidOperationException();
        }

        public virtual void ReturnMoney() => MoneyInTransaction = None;

        public virtual void BuySnack(int position)
        {
            var slot = GetSlot(position);
            slot.SnackPile = slot.SnackPile.SubtaractOne();
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }

        public virtual void LoadSnacks(int position, SnackPile snackPile)
        {
            var slot = GetSlot(position);
            slot.SnackPile = snackPile;
        }

        private Slot GetSlot(int position)
        {
            return Slots.Single(x => x.Position == position);
        }

        public virtual SnackPile GetSnackPile(int position)
        {
            return GetSlot(position).SnackPile;
        }
    }
}
