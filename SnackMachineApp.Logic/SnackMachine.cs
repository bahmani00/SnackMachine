using System;
using static SnackMachineApp.Logic.Money;

namespace SnackMachineApp.Logic
{
    public class SnackMachine: Entity
    {
        public virtual Money MoneyInside { get; protected set; } = None;
        public virtual Money MoneyInTransaction { get; protected set; } = None;

        public SnackMachine() {

        }

        public virtual void InsertMoney(Money money)
        {
            if (Money.Validate(money))
                MoneyInTransaction += money;
            else
                throw new InvalidOperationException();
        }

        public virtual void ReturnMoney() => MoneyInTransaction = None;

        public virtual void BuySnack()
        {
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }
    }
}
