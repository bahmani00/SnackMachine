using FluentAssertions;
using SnackMachineApp.Logic.Atms;
using SnackMachineApp.Logic.Utils;
using System;
using System.Linq;
using Xunit;
using static SnackMachineApp.Logic.SharedKernel.Money;

namespace SnackMachineApp.Logic.Tests
{
    public class AtmSpecs
    {
        [Fact]
        public void Withrawl_Charge_Happens()
        {
            //Arranage
            Atm atm = new Atm();
            atm.LoadMoney(Dollar);

            //Act
            atm.Withdrawal(1m);

            //Assert
            atm.MoneyInside.Amount.Should().Be(0m);
            atm.MoneyCharged.Should().Be(1.01m);
        }
        
        [Fact]
        public void Withrawl_Cannot_if_not_enough_change_in_Atm()
        {
            //Arranage
            Atm atm = new Atm();
            atm.LoadMoney(Dollar);

            //Act
            atm.Withdrawal(2m);

            //Assert
            true.Should().Equals(atm.ValidationMessages.Any());
            Constants.NotEnoughChange.Should().Equals(atm.ValidationMessages.Project());
        }

        [Fact]
        public void Withrawl_Charge_IsAtLeast_OneCent()
        {
            //Arranage
            var atm = new Atm();
            atm.LoadMoney(Cent);

            //Act
            atm.Withdrawal(Cent.Amount);

            //Assert
            atm.MoneyInside.Amount.Should().Be(0m);
            atm.MoneyCharged.Should().Be(.02m);
        }

        [Fact]
        public void Withrawl_Charge_IsAtLeast_OneCent2()
        {
            //Arranage
            var atm = new Atm();
            atm.LoadMoney(Dollar + TenCent);

            //Act
            atm.Withdrawal(1.1m);

            //Assert
            atm.MoneyInside.Amount.Should().Be(0m);
            atm.MoneyCharged.Should().Be(1.12m);
        }
    }
}
