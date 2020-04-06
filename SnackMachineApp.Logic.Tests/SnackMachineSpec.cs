using FluentAssertions;
using System;
using Xunit;

using static SnackMachineApp.Logic.Money;

namespace SnackMachineApp.Logic.Tests
{
    public class SnackMachineSpec
    {
        [Fact]
        public void InsertMoney_ReturnMoney_ResultsIn_Empty_MoneyInTransaction()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Dollar);

            snackMachine.ReturnMoney();

            snackMachine.MoneyInTransaction.Should().Be(None);
        }

        [Fact]
        public void InsertMoney_goes_to_money_in_transaction()
        {
            var snackMachine = new SnackMachine();

            snackMachine.InsertMoney(Cent);
            snackMachine.InsertMoney(Dollar);

            snackMachine.MoneyInTransaction.Amount.Should().Be(1.01m);
        }

        [Fact]
        public void InsertMoney_Cannot_insert_more_than_one_coin_or_note_at_a_time()
        {
            var snackMachine = new SnackMachine();
            Money twoCent = Cent + Cent;

            Action action = () => snackMachine.InsertMoney(twoCent);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void BuySnack_trades_inserted_money_for_a_snack()
        {
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(new Snack("Some Snack"), 10, 1m));
            snackMachine.InsertMoney(Dollar);

            snackMachine.BuySnack(1);

            snackMachine.MoneyInTransaction.Amount.Should().Be(0m);
            snackMachine.MoneyInside.Should().Be(Dollar);
            snackMachine.GetSnackPile(1).Quantity.Should().Be(9);
        }
    }
}
