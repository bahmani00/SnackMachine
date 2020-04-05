using System;
using static SnackMachine.Logic.Money;

namespace SnackMachine.Logic
{
    public class SnackMachineEntity
    {
        public Money MoneyInside { get; private set; } = None;
        public Money MoneyInTransaction { get; private set; } = None;

        public void InsertMoney(Money money)
        {
            if (Money.Validate(money))
                MoneyInTransaction += money;
            else
                throw new InvalidOperationException();
        }

        public void ReturnMoney() => MoneyInTransaction = None;

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }
    }
}
