using FluentAssertions;
using System;
using System.Linq;
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
            //Arrange
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(Snack.None, 10, 1m));
            snackMachine.InsertMoney(Dollar);

            //Act
            snackMachine.BuySnack(1);

            //Assert
            0m.Should().Be(snackMachine.MoneyInTransaction);
            Dollar.Should().Be(snackMachine.MoneyInside);
            9.Should().Be(snackMachine.GetSnackPile(1).Quantity);
        }

        [Fact]
        public void BuySnack_NoSnackAvailable_CannotBuy()
        {
            //Arrange
            var snackMachine = new SnackMachine();

            //Act
            var valid = snackMachine.CanBuySnack(1);

            //Assert
            false.Should().Equals(valid);
            true.Should().Equals(snackMachine.ValidationMessages.Any());
            Helper.NoSnackAvailableToBuy.Should().Equals(snackMachine.ValidationMessages.Project());
        }

        [Fact]
        public void BuySnack_Cannot_NotEnoughMoneyInserted_CannotBuy()
        {
            //Arrange
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(Snack.None, 1, 2m));

            //Action
            snackMachine.InsertMoney(Dollar);
            var valid = snackMachine.CanBuySnack(1);

            //Assert
            false.Should().Equals(valid);
            true.Should().Equals(snackMachine.ValidationMessages.Any());
            Helper.NotEnoughMoneyInserted.Should().Equals(snackMachine.ValidationMessages.Project());
        }

        [Fact]
        public void ReturnMoney_returns_money_with_highest_denomination_first()
        {
            //Arrange
            SnackMachine snackMachine = new SnackMachine();
            snackMachine.LoadMoney(Dollar);

            //Action
            snackMachine.InsertMoney(Quarter);
            snackMachine.InsertMoney(Quarter);
            snackMachine.InsertMoney(Quarter);
            snackMachine.InsertMoney(Quarter);
            snackMachine.ReturnMoney();

            //Assert
            4.Should().Be(snackMachine.MoneyInside.QuarterCount);
            0.Should().Be(snackMachine.MoneyInside.OneDollarCount);
        }

        [Fact]
        public void BuySnack_After_purchase_Change_is_returned()
        {
            //Arrange
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(Snack.None, 1, 0.5m));
            snackMachine.LoadMoney(TenCent * 10);

            //Action
            snackMachine.InsertMoney(Dollar);
            snackMachine.BuySnack(1);

            //Assert
            snackMachine.MoneyInside.Amount.Should().Be(1.5m);
            snackMachine.MoneyInTransaction.Should().Be(0m);
        }

        [Fact]
        public void BuySnack_Cannot_buy_snack_if_not_enough_change_in_machine()
        {
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(Snack.None, 1, 0.5m));
            snackMachine.InsertMoney(Dollar);

            var valid = snackMachine.CanBuySnack(1);

            //Assert
            false.Should().Equals(valid);
            true.Should().Equals(snackMachine.ValidationMessages.Any());
            Helper.NotEnoughChange.Should().Equals(snackMachine.ValidationMessages.Project());
        }
    }
}
