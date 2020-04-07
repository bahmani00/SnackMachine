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

            snackMachine.MoneyInTransaction.Should().Be(0);
        }

        [Fact]
        public void InsertMoney_goes_to_money_in_transaction()
        {
            var snackMachine = new SnackMachine();

            snackMachine.InsertMoney(Cent);
            snackMachine.InsertMoney(Dollar);

            snackMachine.MoneyInTransaction.Should().Be(1.01m);
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

            snackMachine.MoneyInTransaction.Should().Be(0m);
            snackMachine.MoneyInside.Should().Be(Dollar);
            snackMachine.GetSnackPile(1).Quantity.Should().Be(9);
        }

        [Fact]
        public void BuySnack_NoSnackAvailable_CannotBuy()
        {
            //Arrange
            var snackMachine = new SnackMachine();

            //Act
            Action action = () => snackMachine.BuySnack(1);

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void BuySnack_Cannot_NotEnoughMoneyInserted_CannotBuy()
        {
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(Snack.None, 1, 2m));
            snackMachine.InsertMoney(Dollar);

            Action action = () => snackMachine.BuySnack(1);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void ReturnMoney_returns_money_with_highest_denomination_first()
        {
            SnackMachine snackMachine = new SnackMachine();
            snackMachine.LoadMoney(Dollar);

            snackMachine.InsertMoney(Quarter);
            snackMachine.InsertMoney(Quarter);
            snackMachine.InsertMoney(Quarter);
            snackMachine.InsertMoney(Quarter);
            snackMachine.ReturnMoney();

            snackMachine.MoneyInside.QuarterCount.Should().Be(4);
            snackMachine.MoneyInside.OneDollarCount.Should().Be(0);
        }
    }
}
