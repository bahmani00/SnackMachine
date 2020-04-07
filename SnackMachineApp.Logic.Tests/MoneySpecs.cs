using FluentAssertions;
using System;
using Xunit;

namespace SnackMachineApp.Logic.Tests
{
    public class MoneySpecs
    {
        [Fact]
        public void Sum_of_two_moneys_produces_correct_result()
        {
            //Arranage
            var m1 = new Money(1, 2, 3, 4, 5, 6);
            var m2 = new Money(1, 2, 3, 4, 5, 6);

            //Act
            var m = m1 + m2;

            //Assert
            Assert.Equal(2, m.OneCentCount);
            m.OneCentCount.Should().Be(2);
            m.TenCentCount.Should().Be(4);
            m.QuarterCount.Should().Be(6);
            m.OneDollarCount.Should().Be(8);
            m.FiveDollarCount.Should().Be(10);
            m.TwentyDollarCount.Should().Be(12);
        }

        [Fact]
        public void Two_Equal_Money_Are_Equal()
        {
            //Arranage
            var m1 = new Money(1, 2, 3, 4, 5, 6);
            var m2 = new Money(1, 2, 3, 4, 5, 6);

            //Act

            //Assert
            m1.Should().Be(m2);
            m1.GetHashCode().Should().Be(m2.GetHashCode());
        }

        [Fact]
        public void Two_money_instances_do_not_equal_if_contain_different_money_amounts()
        {
            //Arrange
            var dollar = new Money(0, 0, 0, 1, 0, 0);
            var hundredCents = new Money(100, 0, 0, 0, 0, 0);

            //Act

            //Assert
            dollar.Should().NotBe(hundredCents);
            dollar.GetHashCode().Should().NotBe(hundredCents.GetHashCode());
        }

        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -2, 0, 0, 0, 0)]
        [InlineData(0, 0, -3, 0, 0, 0)]
        [InlineData(0, 0, 0, -4, 0, 0)]
        [InlineData(0, 0, 0, 0, -5, 0)]
        [InlineData(0, 0, 0, 0, 0, -6)]
        public void Cannot_create_money_with_negative_value(
             int oneCentCount,
             int tenCentCount,
             int quarterCount,
             int oneDollarCount,
             int fiveDollarCount,
             int twentyDollarCount)
        {
            //Arrange
            Action action = () => new Money(
                oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount);

            //Action & Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0, 0, 0, 0.01)]
        [InlineData(1, 2, 0, 0, 0, 0, 0.21)]
        [InlineData(1, 2, 3, 0, 0, 0, 0.96)]
        [InlineData(1, 2, 3, 4, 0, 0, 4.96)]
        [InlineData(1, 2, 3, 4, 5, 0, 29.96)]
        [InlineData(1, 2, 3, 4, 5, 6, 149.96)]
        [InlineData(11, 0, 0, 0, 0, 0, 0.11)]
        [InlineData(110, 0, 0, 0, 100, 0, 501.1)]
        public void Amount_is_calculated_correctly(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount,
            decimal expectedAmount)
        {
            //Arrange
            Money money = new Money(
                oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount);

            //Act & Assert
            money.Amount.Should().Be(expectedAmount);
        }

        [Fact]
        public void Subtraction_of_two_moneys_produces_correct_result()
        {
            //Arrange
            var money1 = new Money(11, 10, 10, 10, 10, 10);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            //Act
            var result = money1 - money2;

            //Assert
            result.OneCentCount.Should().Be(10);
            result.TenCentCount.Should().Be(8);
            result.QuarterCount.Should().Be(7);
            result.OneDollarCount.Should().Be(6);
            result.FiveDollarCount.Should().Be(5);
            result.TwentyDollarCount.Should().Be(4);

            result.Amount.Should().Be(113.65m);
        }

        [Fact]
        public void Cannot_subtract_more_than_exists()
        {
            //Arrange
            var money1 = new Money(0, 1, 0, 0, 0, 0);
            var money2 = new Money(1, 0, 0, 0, 0, 0);

            //Act
            Action action = () =>
            {
                var money = money1 - money2;
            };

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(0.01, 1, 0, 0, 0, 0, 0)]
        [InlineData(0.21, 1, 2, 0, 0, 0, 0)]
        [InlineData(0.96, 1, 2, 3, 0, 0, 0)]
        [InlineData(4.96, 1, 2, 3, 4, 0, 0)]
        [InlineData(29.96, 1, 2, 3, 4, 1, 1)]
        [InlineData(149.96, 1, 2, 3, 4, 1, 7)]
        [InlineData(0.11, 1, 1, 0, 0, 0, 0)]
        [InlineData(501.1, 0, 1, 0, 1, 0, 25)]
        public void Allocate_with_highest_denomination_first(
            decimal amount,
            int expectedOneCentCount,
            int expectedTenCentCount,
            int expectedQuarterCount,
            int expectedOneDollarCount,
            int expectedFiveDollarCount,
            int expectedTwentyDollarCount
            )
        {
            //Arrange
            //Insert tons of money to test allocation
            var bank = new Money(1_000, 1_000, 1_000, 1_000, 1_000, 1_000);

            //Ac
            var allocated = bank.Allocate(amount);

            //Assert
            allocated.OneCentCount.Should().Be(expectedOneCentCount);
            allocated.TenCentCount.Should().Be(expectedTenCentCount);
            allocated.QuarterCount.Should().Be(expectedQuarterCount);
            allocated.OneDollarCount.Should().Be(expectedOneDollarCount);
            allocated.FiveDollarCount.Should().Be(expectedFiveDollarCount);
            allocated.TwentyDollarCount.Should().Be(expectedTwentyDollarCount);
        }
    }
}
