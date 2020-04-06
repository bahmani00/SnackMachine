using System;
using System.Collections.Generic;
using static SnackMachineApp.Logic.Money;

namespace SnackMachineApp.Logic
{
    public class SnackMachine: Entity
    {
        public virtual Money MoneyInside { get; protected set; }
        public virtual Money MoneyInTransaction { get; protected set; }

        public virtual IList<Slot> Slots { get; protected set; }

        public SnackMachine() {
            MoneyInside = None;
            MoneyInTransaction = None;

            Slots = new List<Slot>
            {
                new Slot(this, 1, null, 5, 0m),
                new Slot(this, 2, null, 5, 0m),
                new Slot(this, 3, null, 5, 0m),
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
            //Slots[position - 1].Quantity--;
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }
    }
}
