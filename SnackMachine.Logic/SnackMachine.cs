namespace SnackMachine.Logic
{
    public class SnackMachine
    {
        public Money MoneyInside { get; private set; }
        public Money MoneyInTransaction { get; private set; }

        public void InsertMoney(Money money) => MoneyInTransaction += money;

        public void ReturnMoney() => MoneyInTransaction = Money.Null;

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = Money.Null;
        }
    }
}
