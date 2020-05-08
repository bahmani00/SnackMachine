using SnackMachineApp.Domain.SeedWork;
using SnackMachineApp.Domain.SharedKernel;
using SnackMachineApp.Domain.Utils;
using System;

namespace SnackMachineApp.Domain.Atms
{
    public class Atm : AggregateRoot
    {
        private const decimal ChargeRate = .01m;

        public virtual Money MoneyInside { get; protected set; } = Money.None;
        public virtual decimal MoneyCharged { get; protected set; }

        public virtual void LoadMoney(Money money)
        {
            MoneyInside += money;
        }

        public virtual void Withdrawal(decimal amount)
        {
            if (!CanWithdrawal(amount))
                return;

            var output = MoneyInside.Allocate(amount);
            MoneyInside -= output;

            var amountWithCommision = amount + CalculateCommision(amount);
            MoneyCharged += amountWithCommision;

            AddDomainEvent(new BalanceChangedEvent(amountWithCommision, Constants.HeadOfficeId));
        }

        public virtual decimal CalculateCommision(decimal amount)
        {
            var commission = amount * ChargeRate;
            return Math.Ceiling(commission * 100) / 100m;
        }

        public virtual bool CanWithdrawal(decimal amount)
        {
            ValidationMessages.Clear();

            if (amount <= 0m)
            {
                ValidationMessages.Add(Constants.InvalidAmount);
                return false;
            }

            if (amount > MoneyInside.Amount)
            {
                ValidationMessages.Add(Constants.NotEnoughChange);
                return false;
            }

            if (!MoneyInside.CanAllocate(amount))
            {
                ValidationMessages.Add(Constants.NotEnoughChange);
                return false;
            }

            return true;
        }
    }
}
